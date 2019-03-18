================================================================================
Date: {Current_Date}
Author: {Author}
Description: {Description}
Change #: {SMGS_Change}
================================================================================

Files
--------------------------------------------------------------------------------
1. 00_README.txt
	- Contains a list of files and instructions included in the change.

2. Inside the NTC Folder:
2.2. 01_NTC_DDL.sql
	- Full create DDL for DBA to run on target schema
2.3. 02_NTC_DISABLE_CONSTRAINTS.sql
	- disables all constraints and triggers on target schema
2.4. 03_NTC_INSERTS.sql
	- inserts configuration data for NTC
2.5. 04_NTC_USERS.sql
	- inserts configuration for users of the NTC
2.5. 05_NTC_UPDATE_SEQUENCES.sql
	- updates the sequences to the proper values
2.6. 06_NTC_ENABLE_CONSTRAINTS.sql
	- re-enables all constraints and triggers on target schema

3. Inside the FAST Folder:
3.2. 01_FAST_DDL.sql
	- Full create DDL for DBA to run on target schema
3.3. 02_FAST_DISABLE_CONSTRAINTS.sql
	- disables all constraints and triggers on target schema
3.4. 03_FAST_INSERTS.sql
	- inserts configuration data for NTC
3.5. 04_FAST_UPDATE_SEQUENCES.sql
	- updates the sequences to the proper values
3.6. 05_FAST_ENABLE_CONSTRAINTS.sql
	- re-enables all constraints and triggers on target schema

Synonyms
--------------------------------------------------------------------------------
1. FAST_USER
2. FAST_REPORT
3. NTC_USER
4. NTC_REPORT


Schemas
--------------------------------------------------------------------------------
1. FAST
2. NTC

Instructions
--------------------------------------------------------------------------------
2. Execute NTC\01_NTC_DDL.sql against NTC schema.
3. Execute NTC\02_NTC_DISABLE_CONSTRAINTS.sql against NTC schema.
4. Execute NTC\03_NTC_INSERTS.sql against NTC schema.
5. Execute NTC\04_NTC_USERS.sql against NTC schema.
6. Execute NTC\05_NTC_UPDATE_SEQUENCES.sql against NTC schema.
7. Execute NTC\06_NTC_ENABLE_CONSTRAINTS.sql against NTC schema.
8. Recreate synonyms / grants for FAST_USER on NTC schema
9. Recreate synonyms / grants for FAST_REPORT on NTC schema

10. Execute FAST\02_FAST_DDL.sql against FAST schema.
11. Execute FAST\03_FAST_DISABLE_CONSTRAINTS.sql against FAST schema.
12. Execute FAST\04_FAST_INSERTS.sql against FAST schema.
13. Execute FAST\05_FAST_UPDATE_SEQUENCES.sql against FAST schema.
14. Execute FAST\06_FAST_ENABLE_CONSTRAINTS.sql against FAST schema.
15. Recreate synonyms / grants for FAST_USER on FAST schema
16. Recreate synonyms / grants for FAST_REPORT on FAST schema