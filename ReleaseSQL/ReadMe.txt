Execute powershell compile_release.ps1 in the root of the folder that contains:
 .\data
	This contains the inserts under the name of of each of the tables with extension .sql
 .\ddl
	This contains the full DDL create and should contain NTC_DDL.sql
 .\util
	This contains the utility scripts and should contain 
		- ReadMe.txt
		- NTC_DISABLE_CONSTRAINTS.sql
		- NTC_ENABLE_CONSTRAINTS.sql
		- NTC_UPDATE_SEQUENCES.sql
		- NTC_CLEAN.sql
 .\out
	This folder will be cleaned and contain the collected scripts after run.
	This folder will be generated if not present.