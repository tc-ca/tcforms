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

--select 'alter trigger ' ||owner||'.'||trigger_name||' disable;' from all_triggers where owner='NTC';
--select 'alter trigger ' ||owner||'.'||trigger_name||' enable;' from all_triggers where owner='NTC';

alter trigger NTC.YY002_BI_REMOTE_USER_SEQ enable;
alter trigger NTC.YY001_BI_FORM_USER_SEQ enable;
alter trigger NTC.TY001_BI_ROLE_SEQ enable;