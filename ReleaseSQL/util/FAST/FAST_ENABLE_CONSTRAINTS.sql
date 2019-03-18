SET SERVEROUTPUT ON;

BEGIN
  dbms_output.enable(1000000);
  FOR c IN
  (SELECT c.owner, c.table_name, c.constraint_name
   FROM user_constraints c, user_tables t
   WHERE c.table_name = t.table_name
   AND c.status = 'DISABLED'
   AND c.constraint_type NOT IN ('P', 'U')
   AND t.iot_type IS NULL
   ORDER BY c.constraint_type)
  LOOP
    dbms_output.put_line('Enabling ' || c.constraint_name);
    dbms_utility.exec_ddl_statement('alter table "' || c.owner || '"."' || c.table_name || '" enable constraint ' || c.constraint_name);
  END LOOP;
END;
/

alter trigger FAST.YY004_BI_SECTION_SEQ enable;
alter trigger FAST.YY003_BI_FORM_SEQ enable;
alter trigger FAST.YY002_BI_TIME_WINDOW_SEQ enable;
alter trigger FAST.YY001_BI_PROGRAM_SEQ enable;
alter trigger FAST.YY000_BI_USER_SEQ enable;
alter trigger FAST.TY007_BI_FIELD_SET_SEQ enable;
alter trigger FAST.TY001_BI_LANGUAGE_SEQ enable;
alter trigger FAST.TY003_BI_RESOURCE_SEQ enable;
alter trigger FAST.YY006_BI_VALIDATION_SEQ enable;
alter trigger FAST.YY007_BI_VALIDATION_RULE_SEQ enable;
alter trigger FAST.YY010_BI_DOCUMENT_SEQ enable;
alter trigger FAST.YY011_BI_DOCUMENT_DATA_SEQ enable;
alter trigger FAST.YY014_BI_SUBMISSION_SEQ enable;
alter trigger FAST.YY005_BI_FIELD_SEQ enable;