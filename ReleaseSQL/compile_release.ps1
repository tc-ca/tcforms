#requires -version 5

<#
.SYNOPSIS
  Script to prepare SQL scripts into an output folder for deployment.
.DESCRIPTION
  This script will:
    - Collect all insert statements 
    - Collect DDL creation statements
    - Publish to a folder with ordered files
.PARAMETER <Parameter_Name>
  <Brief description of parameter input required. Repeat this attribute if required>
.INPUTS
  <Inputs if any, otherwise state None>
.OUTPUTS
  None -- Output Folder contents
.NOTES
  Version:        1.1
  Author:         Bryen Begg & Austin Dunning
  Creation Date:  2018-12-04
  Purpose/Change: Automate collection of SQL scripts from folders
.EXAMPLE
  <Example explanation goes here>
  
  <Example goes here. Repeat this attribute for more than one example>
#>

#---------------------------------------------------------[Script Parameters]------------------------------------------------------

param (
    [switch]$ForceClean
 )

#---------------------------------------------------------[Initialisations]--------------------------------------------------------

#Set Error Action to Silently Continue
#$ErrorActionPreference = 'SilentlyContinue'
$ErrorActionPreference = 'Stop'

#Import Modules & Snap-ins

#----------------------------------------------------------[Declarations]----------------------------------------------------------

#Any Global Declarations go here

$Paths = @{
    "Output" = ".\out";
    "DDL" = ".\ddl";
    "Data" = ".\data";
    "Utility" = ".\util";
}

$Schemas = @{
	"NTC" = "NTC";
	"FAST" = "FAST";
}

#-----------------------------------------------------------[Functions]------------------------------------------------------------
Function Set-EnvironmentFolder{
    Param(
        [Parameter(Mandatory=$true, 
                   Position=0,
                   ParameterSetName='Foldername')]
        [ValidateNotNull()]
        [ValidateNotNullOrEmpty()]        
        [Alias("p1")] 
        [string]$FolderName,
        [bool]$DoClean = $false
    )
    Begin {
        "Checking for $($FolderName) existence and making if missing."
    }
    Process {
        Try {
            if (!(Test-Path -Path $FolderName)) {
                
                Write-Host "The $($FolderName) folder was missing." -ForegroundColor Yellow


                New-Item -ItemType Directory -Force -Path $FolderName | Out-Null
            }else{
                if ($DoClean){
                    Write-Host "Cleaning $($FolderName)."

                    #Remove all other files in the folder.
                    Remove-Item -Path "$FolderName\*" -Recurse
                }
            }
        }
        Catch {
            Write-Host -BackgroundColor Red "Error: $($_.Exception)"
            Break
        }
    }
    End {
        If ($?) {
            Write-Host 'Completed Successfully.' -ForegroundColor Green
            Write-Host ' '
        }
    }
}

Function Combine-SQLFiles{
    Param(
        [string]$DataFolder,
        [string]$OutputFile
    )
    Begin {
        Write-Host "Combining *.sql from $($DataFolder) to $($OutputFile)."
    }
    Process {
        Try {
            if (Test-Path -Path $OutputFile){          
                Clear-Content $OutputFile
            }

            Get-ChildItem -Path "$($DataFolder)\*.sql" | % { 
                    "Loading SQL file $($_.Name)"
                    Get-Content -Path "$($DataFolder)\$($_.Name)" | Add-Content -Path $OutputFile
                }
        }
        Catch {
            Write-Host -BackgroundColor Red "Error: $($_.Exception)"
            Break
        }
    }
    End {
        If ($?) {
            Write-Host 'Completed Successfully.' -ForegroundColor Green
            Write-Host ' '
        }
    }
}

Function Collect-SourceFile{
    Param(
        [string]$SQLFile,
        [string]$OutputFile
    )
    Begin {
        Write-Host "Collect from $($SQLFile) to $($OutputFile)."
    }
    Process {
        Try {
            If (!(Test-Path -Path $SQLFile)){
                Write-Error "The source file is missing! ($($SQLFile))" -ErrorAction Stop
            }else{
                Copy-Item -Path $SQLFile -Destination $OutputFile
            }

        }
        Catch {
            Write-Host -BackgroundColor Red "Error: $($_.Exception)"
            Break
        }
    }
    End {
        If ($?) {
            Write-Host 'Completed Successfully.' -ForegroundColor Green
            Write-Host ' '
        }
    }
}



#-----------------------------------------------------------[Execution]------------------------------------------------------------

#Script Execution goes here

# Check for directories .\data, .\out, .\util, .\ddl
Set-EnvironmentFolder -FolderName $Paths.Data
Set-EnvironmentFolder -FolderName $Paths.Output -DoClean $true
Set-EnvironmentFolder -FolderName "$($Paths.Output)\$($Schemas.FAST)" -DoClean $true
Set-EnvironmentFolder -FolderName "$($Paths.Output)\$($Schemas.NTC)" -DoClean $true
Set-EnvironmentFolder -FolderName $Paths.Utility
Set-EnvironmentFolder -FolderName $Paths.DDL

# Check if we are doing a clean
if ($ForceClean){

    # Collect Util Readme for SMGS change
    Collect-SourceFile -SQLFile "$($Paths.Utility)\ReadMe_Clean.txt" -OutputFile "$($Paths.Output)\00_README.txt"

	$CurrentSchema = $Schemas.FAST;
	
    # Collect the clean script
    Collect-SourceFile -SQLFile "$($Paths.Utility)\$($CurrentSchema)\$($CurrentSchema)_CLEAN.sql" -OutputFile "$($Paths.Output)\$($CurrentSchema)\01_$($CurrentSchema)_CLEAN.sql"

    # Collect DDL file to output
    #Collect-SourceFile -SQLFile "$($Paths.DDL)\NTC_DDL.sql" -OutputFile "$($Paths.Output)\01_NTC_DDL.sql"
    Combine-SQLFiles -DataFolder "$($Paths.DDL)\$($CurrentSchema)" -OutputFile "$($Paths.Output)\$($CurrentSchema)\02_$($CurrentSchema)_DDL.sql"

    # Collect Util Turn off trigger/constraints
    Collect-SourceFile -SQLFile "$($Paths.Utility)\$($CurrentSchema)\$($CurrentSchema)_DISABLE_CONSTRAINTS.sql" -OutputFile "$($Paths.Output)\$($CurrentSchema)\03_NTC_DISABLE_CONSTRAINTS.sql"

    # Collect Insert SQLs to output
    Combine-SQLFiles -DataFolder "$($Paths.Data)\$($CurrentSchema)" -OutputFile "$($Paths.Output)\$($CurrentSchema)\04_$($CurrentSchema)_INSERTS.SQL"

    # Collect Util update seq #'s
    Collect-SourceFile -SQLFile "$($Paths.Utility)\$($CurrentSchema)\$($CurrentSchema)_UPDATE_SEQUENCES.sql" -OutputFile "$($Paths.Output)\$($CurrentSchema)\05_$($CurrentSchema)_UPDATE_SEQUENCES.sql"


    # Collect Util Turn off trigger/constraints
    Collect-SourceFile -SQLFile "$($Paths.Utility)\$($CurrentSchema)\$($CurrentSchema)_ENABLE_CONSTRAINTS.sql" -OutputFile "$($Paths.Output)\$($CurrentSchema)\06_$($CurrentSchema)_ENABLE_CONSTRAINTS.sql"
	
	$CurrentSchema = $Schemas.NTC;
	
	# Collect the clean script
    Collect-SourceFile -SQLFile "$($Paths.Utility)\$($CurrentSchema)\$($CurrentSchema)_CLEAN.sql" -OutputFile "$($Paths.Output)\$($CurrentSchema)\01_$($CurrentSchema)_CLEAN.sql"

    # Collect DDL file to output
    #Collect-SourceFile -SQLFile "$($Paths.DDL)\NTC_DDL.sql" -OutputFile "$($Paths.Output)\01_NTC_DDL.sql"
    Combine-SQLFiles -DataFolder "$($Paths.DDL)\$($CurrentSchema)" -OutputFile "$($Paths.Output)\$($CurrentSchema)\02_$($CurrentSchema)_DDL.sql"

    # Collect Util Turn off trigger/constraints
    Collect-SourceFile -SQLFile "$($Paths.Utility)\$($CurrentSchema)\$($CurrentSchema)_DISABLE_CONSTRAINTS.sql" -OutputFile "$($Paths.Output)\$($CurrentSchema)\03_NTC_DISABLE_CONSTRAINTS.sql"

    # Collect Insert SQLs to output
    Combine-SQLFiles -DataFolder "$($Paths.Data)\$($CurrentSchema)" -OutputFile "$($Paths.Output)\$($CurrentSchema)\04_$($CurrentSchema)_INSERTS.SQL"

    # Collect Util update seq #'s
    Collect-SourceFile -SQLFile "$($Paths.Utility)\$($CurrentSchema)\$($CurrentSchema)_UPDATE_SEQUENCES.sql" -OutputFile "$($Paths.Output)\$($CurrentSchema)\06_$($CurrentSchema)_UPDATE_SEQUENCES.sql"


    # Collect Util Turn off trigger/constraints
    Collect-SourceFile -SQLFile "$($Paths.Utility)\$($CurrentSchema)\$($CurrentSchema)_ENABLE_CONSTRAINTS.sql" -OutputFile "$($Paths.Output)\$($CurrentSchema)\07_$($CurrentSchema)_ENABLE_CONSTRAINTS.sql"

}else{

    # Collect Util Readme for SMGS change
    Collect-SourceFile -SQLFile "$($Paths.Utility)\ReadMe.txt" -OutputFile "$($Paths.Output)\00_README.txt"

    $CurrentSchema = $Schemas.FAST;
	
    # Collect DDL file to output
    #Collect-SourceFile -SQLFile "$($Paths.DDL)\NTC_DDL.sql" -OutputFile "$($Paths.Output)\01_NTC_DDL.sql"
    Combine-SQLFiles -DataFolder "$($Paths.DDL)\$($CurrentSchema)" -OutputFile "$($Paths.Output)\$($CurrentSchema)\01_$($CurrentSchema)_DDL.sql"

    # Collect Util Turn off trigger/constraints
    Collect-SourceFile -SQLFile "$($Paths.Utility)\$($CurrentSchema)\$($CurrentSchema)_DISABLE_CONSTRAINTS.sql" -OutputFile "$($Paths.Output)\$($CurrentSchema)\02_$($CurrentSchema)_DISABLE_CONSTRAINTS.sql"

    # Collect Insert SQLs to output
    Combine-SQLFiles -DataFolder "$($Paths.Data)\$($CurrentSchema)" -OutputFile "$($Paths.Output)\$($CurrentSchema)\03_$($CurrentSchema)_INSERTS.SQL"

    # Collect Util update seq #'s
    Collect-SourceFile -SQLFile "$($Paths.Utility)\$($CurrentSchema)\$($CurrentSchema)_UPDATE_SEQUENCES.sql" -OutputFile "$($Paths.Output)\$($CurrentSchema)\04_$($CurrentSchema)_UPDATE_SEQUENCES.sql"


    # Collect Util Turn off trigger/constraints
    Collect-SourceFile -SQLFile "$($Paths.Utility)\$($CurrentSchema)\$($CurrentSchema)_ENABLE_CONSTRAINTS.sql" -OutputFile "$($Paths.Output)\$($CurrentSchema)\05_$($CurrentSchema)_ENABLE_CONSTRAINTS.sql"
	
	$CurrentSchema = $Schemas.NTC;
	
    # Collect DDL file to output
    #Collect-SourceFile -SQLFile "$($Paths.DDL)\NTC_DDL.sql" -OutputFile "$($Paths.Output)\01_NTC_DDL.sql"
    Combine-SQLFiles -DataFolder "$($Paths.DDL)\$($CurrentSchema)" -OutputFile "$($Paths.Output)\$($CurrentSchema)\01_$($CurrentSchema)_DDL.sql"

    # Collect Util Turn off trigger/constraints
    Collect-SourceFile -SQLFile "$($Paths.Utility)\$($CurrentSchema)\$($CurrentSchema)_DISABLE_CONSTRAINTS.sql" -OutputFile "$($Paths.Output)\$($CurrentSchema)\02_$($CurrentSchema)_DISABLE_CONSTRAINTS.sql"

    # Collect Insert SQLs to output
    Combine-SQLFiles -DataFolder "$($Paths.Data)\$($CurrentSchema)" -OutputFile "$($Paths.Output)\$($CurrentSchema)\03_$($CurrentSchema)_INSERTS.SQL"

    # Collect Util update seq #'s
    Collect-SourceFile -SQLFile "$($Paths.Utility)\$($CurrentSchema)\$($CurrentSchema)_UPDATE_SEQUENCES.sql" -OutputFile "$($Paths.Output)\$($CurrentSchema)\05_$($CurrentSchema)_UPDATE_SEQUENCES.sql"


    # Collect Util Turn off trigger/constraints
    Collect-SourceFile -SQLFile "$($Paths.Utility)\$($CurrentSchema)\$($CurrentSchema)_ENABLE_CONSTRAINTS.sql" -OutputFile "$($Paths.Output)\$($CurrentSchema)\06_$($CurrentSchema)_ENABLE_CONSTRAINTS.sql"

}