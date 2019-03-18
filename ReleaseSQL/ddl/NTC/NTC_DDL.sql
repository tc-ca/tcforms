
CREATE SEQUENCE TY001_ROLE_SEQ;

CREATE SEQUENCE YY001_FORM_USER_SEQ;

CREATE SEQUENCE YY002_REMOTE_USER_SEQ;

CREATE TABLE YY001_FORM_USER
(
	USER_ID              NUMBER(10) NOT NULL ,
	DATE_CREATED_DTE     DATE NOT NULL ,
	DATE_LAST_UPDATE_DTE DATE NULL ,
	DATE_DELETED_DTE     DATE NULL ,
	USER_CREATED_ID      NUMBER(10) NOT NULL ,
	USER_LAST_UPDATE_ID  NUMBER(10) NULL 
);

COMMENT ON TABLE YY001_FORM_USER IS 'contains the users that are using the form';

COMMENT ON COLUMN YY001_FORM_USER.USER_ID IS 'a unique system generated identifier for the user.

';

COMMENT ON COLUMN YY001_FORM_USER.USER_CREATED_ID IS 'a unique system generated identifier for the user.

';

COMMENT ON COLUMN YY001_FORM_USER.DATE_CREATED_DTE IS 'The date on which the data item was created and became available';

COMMENT ON COLUMN YY001_FORM_USER.DATE_DELETED_DTE IS 'The date from which the data item is no longer allowed to be referenced for any new inserts or updates';

COMMENT ON COLUMN YY001_FORM_USER.DATE_LAST_UPDATE_DTE IS 'The date the data item was last changed';

COMMENT ON COLUMN YY001_FORM_USER.USER_LAST_UPDATE_ID IS 'The responsible user that affected the last change of the data item.';

CREATE UNIQUE INDEX YY001_PK ON YY001_FORM_USER
(USER_ID   ASC);

ALTER TABLE YY001_FORM_USER
	ADD CONSTRAINT  YY001_PK PRIMARY KEY (USER_ID);

ALTER TABLE YY001_FORM_USER
	MODIFY DATE_CREATED_DTE DEFAULT SYSDATE;

ALTER TABLE YY001_FORM_USER
	MODIFY DATE_LAST_UPDATE_DTE DEFAULT SYSDATE;

CREATE INDEX YY001_IF1 ON YY001_FORM_USER
(USER_CREATED_ID   ASC);

CREATE INDEX YY001_IF2 ON YY001_FORM_USER
(USER_LAST_UPDATE_ID   ASC);

CREATE TABLE TY001_ROLE
(
	ROLE_ID              NUMBER(10) NOT NULL ,
	ROLE_CD              VARCHAR2(20) NOT NULL ,
	DATE_CREATED_DTE     DATE NOT NULL ,
	DATE_LAST_UPDATE_DTE DATE NULL ,
	DATE_DELETED_DTE     DATE NULL ,
	USER_CREATED_ID      NUMBER(10) NOT NULL ,
	USER_LAST_UPDATE_ID  NUMBER(10) NULL 
);

COMMENT ON TABLE TY001_ROLE IS 'Identifies a role that a user can have.

example: Admin.';

COMMENT ON COLUMN TY001_ROLE.ROLE_CD IS 'The code value used to represent each role.

example: admin.
';

COMMENT ON COLUMN TY001_ROLE.DATE_CREATED_DTE IS 'The date on which the data item was created and became available';

COMMENT ON COLUMN TY001_ROLE.DATE_LAST_UPDATE_DTE IS 'The date the data item was last changed';

COMMENT ON COLUMN TY001_ROLE.DATE_DELETED_DTE IS 'The date from which the data item is no longer allowed to be referenced for any new inserts or updates';

COMMENT ON COLUMN TY001_ROLE.ROLE_ID IS 'a unique system generated identifier for the role.';

COMMENT ON COLUMN TY001_ROLE.USER_CREATED_ID IS 'a unique system generated identifier for the user.

';

COMMENT ON COLUMN TY001_ROLE.USER_LAST_UPDATE_ID IS 'a unique system generated identifier for the user.

';

CREATE UNIQUE INDEX TY001_PK ON TY001_ROLE
(ROLE_ID   ASC);

ALTER TABLE TY001_ROLE
	ADD CONSTRAINT  TY001_PK PRIMARY KEY (ROLE_ID);

ALTER TABLE TY001_ROLE
	MODIFY DATE_CREATED_DTE DEFAULT SYSDATE;

ALTER TABLE TY001_ROLE
	MODIFY DATE_LAST_UPDATE_DTE DEFAULT SYSDATE;

CREATE INDEX TY001_IF1 ON TY001_ROLE
(USER_CREATED_ID   ASC);

CREATE INDEX TY001_IF2 ON TY001_ROLE
(USER_LAST_UPDATE_ID   ASC);

CREATE INDEX TC046_DATE_DELETED_DTE_IE1 ON TY001_ROLE
(DATE_DELETED_DTE   ASC,1   ASC);

CREATE TABLE YY002_REMOTE_USER
(
	REMOTE_USER_ID       NUMBER(10) NOT NULL ,
	IDENTIFIER_TXT       VARCHAR2(255) NULL ,
	DATE_CREATED_DTE     DATE NOT NULL ,
	DATE_LAST_UPDATE_DTE DATE NULL ,
	DATE_DELETED_DTE     DATE NULL ,
	USER_CREATED_ID      NUMBER(10) NOT NULL ,
	USER_LAST_UPDATE_ID  NUMBER(10) NULL 
);

COMMENT ON TABLE YY002_REMOTE_USER IS 'keeps tracks of id''s for a single user.';

COMMENT ON COLUMN YY002_REMOTE_USER.REMOTE_USER_ID IS 'a unique system generated identifier for the external role.';

COMMENT ON COLUMN YY002_REMOTE_USER.IDENTIFIER_TXT IS 'a unique per-program identifier that indentifies which user is accessing the system.';

COMMENT ON COLUMN YY002_REMOTE_USER.DATE_CREATED_DTE IS 'The date on which the data item was created and became available for use.';

COMMENT ON COLUMN YY002_REMOTE_USER.DATE_LAST_UPDATE_DTE IS 'The date the data item was last updated.';

COMMENT ON COLUMN YY002_REMOTE_USER.DATE_DELETED_DTE IS 'The date from which the data item is no longer allowed to be referenced for any new inserts or updates.
This can only happen for the current day or previous dates.';

COMMENT ON COLUMN YY002_REMOTE_USER.USER_CREATED_ID IS 'a unique system generated identifier for the user.

';

COMMENT ON COLUMN YY002_REMOTE_USER.USER_LAST_UPDATE_ID IS 'a unique system generated identifier for the user.

';

CREATE UNIQUE INDEX YY002_PK ON YY002_REMOTE_USER
(REMOTE_USER_ID   ASC);

ALTER TABLE YY002_REMOTE_USER
	ADD CONSTRAINT  YY002_PK PRIMARY KEY (REMOTE_USER_ID);

CREATE INDEX YY002_IF1 ON YY002_REMOTE_USER
(USER_CREATED_ID   ASC);

CREATE INDEX YY002_IF2 ON YY002_REMOTE_USER
(USER_LAST_UPDATE_ID   ASC);

CREATE TABLE YY003_XREF_USER_ROLE
(
	USER_ID              NUMBER(10) NOT NULL ,
	ROLE_ID              NUMBER(10) NOT NULL ,
	DATE_CREATED_DTE     DATE NOT NULL ,
	DATE_LAST_UPDATE_DTE DATE NULL ,
	DATE_DELETED_DTE     DATE NULL ,
	USER_CREATED_ID      NUMBER(10) NOT NULL ,
	USER_LAST_UPDATE_ID  NUMBER(10) NULL 
);

COMMENT ON TABLE YY003_XREF_USER_ROLE IS 'cross reference table for users and their roles.
';

COMMENT ON COLUMN YY003_XREF_USER_ROLE.DATE_CREATED_DTE IS 'The date on which the data item was created and became available';

COMMENT ON COLUMN YY003_XREF_USER_ROLE.DATE_DELETED_DTE IS 'The date from which the data item is no longer allowed to be referenced for any new inserts or updates';

COMMENT ON COLUMN YY003_XREF_USER_ROLE.DATE_LAST_UPDATE_DTE IS 'The date the data item was last changed';

COMMENT ON COLUMN YY003_XREF_USER_ROLE.USER_ID IS 'a unique system generated identifier for the user.

';

COMMENT ON COLUMN YY003_XREF_USER_ROLE.USER_LAST_UPDATE_ID IS 'a unique system generated identifier for the user.

';

COMMENT ON COLUMN YY003_XREF_USER_ROLE.USER_CREATED_ID IS 'a unique system generated identifier for the user.

';

COMMENT ON COLUMN YY003_XREF_USER_ROLE.ROLE_ID IS 'a unique system generated identifier for the role.';

CREATE UNIQUE INDEX YY003_PK ON YY003_XREF_USER_ROLE
(USER_ID   ASC,ROLE_ID   ASC);

ALTER TABLE YY003_XREF_USER_ROLE
	ADD CONSTRAINT  YY003_PK PRIMARY KEY (USER_ID,ROLE_ID);

ALTER TABLE YY003_XREF_USER_ROLE
	MODIFY DATE_CREATED_DTE DEFAULT SYSDATE;

ALTER TABLE YY003_XREF_USER_ROLE
	MODIFY DATE_LAST_UPDATE_DTE DEFAULT SYSDATE;

CREATE INDEX YY003_IF1 ON YY003_XREF_USER_ROLE
(USER_ID   ASC);

CREATE INDEX YY003_IF2 ON YY003_XREF_USER_ROLE
(ROLE_ID   ASC);

CREATE INDEX YY003_IF3 ON YY003_XREF_USER_ROLE
(USER_CREATED_ID   ASC);

CREATE INDEX YY003_IF4 ON YY003_XREF_USER_ROLE
(USER_LAST_UPDATE_ID   ASC);

CREATE TABLE YY004_XREF_USER_MAPPING
(
	USER_ID              NUMBER(10) NOT NULL ,
	REMOTE_USER_ID       NUMBER(10) NOT NULL ,
	DATE_CREATED_DTE     DATE NOT NULL ,
	DATE_LAST_UPDATE_DTE DATE NULL ,
	DATE_DELETED_DTE     DATE NULL ,
	USER_CREATED_ID      NUMBER(10) NOT NULL ,
	USER_LAST_UPDATE_ID  NUMBER(10) NULL 
);

COMMENT ON TABLE YY004_XREF_USER_MAPPING IS 'cross reference table for user and external users.';

COMMENT ON COLUMN YY004_XREF_USER_MAPPING.USER_ID IS 'a unique system generated identifier for the user.

';

COMMENT ON COLUMN YY004_XREF_USER_MAPPING.USER_LAST_UPDATE_ID IS 'a unique system generated identifier for the user.

';

COMMENT ON COLUMN YY004_XREF_USER_MAPPING.USER_CREATED_ID IS 'a unique system generated identifier for the user.

';

COMMENT ON COLUMN YY004_XREF_USER_MAPPING.REMOTE_USER_ID IS 'a unique system generated identifier for the external user id.';

COMMENT ON COLUMN YY004_XREF_USER_MAPPING.DATE_CREATED_DTE IS 'The date on which the data item was created and became available for use.';

COMMENT ON COLUMN YY004_XREF_USER_MAPPING.DATE_LAST_UPDATE_DTE IS 'The date the data item was last updated.';

COMMENT ON COLUMN YY004_XREF_USER_MAPPING.DATE_DELETED_DTE IS 'The date from which the data item is no longer allowed to be referenced for any new inserts or updates.
This can only happen for the current day or previous dates.';

CREATE UNIQUE INDEX YY004_PK ON YY004_XREF_USER_MAPPING
(USER_ID   ASC,REMOTE_USER_ID   ASC);

ALTER TABLE YY004_XREF_USER_MAPPING
	ADD CONSTRAINT  YY004_PK PRIMARY KEY (USER_ID,REMOTE_USER_ID);

CREATE INDEX YY004_IF1 ON YY004_XREF_USER_MAPPING
(USER_ID   ASC);

CREATE INDEX YY004_IF2 ON YY004_XREF_USER_MAPPING
(REMOTE_USER_ID   ASC);

CREATE INDEX YY004_IF3 ON YY004_XREF_USER_MAPPING
(USER_CREATED_ID   ASC);

CREATE INDEX YY004_IF4 ON YY004_XREF_USER_MAPPING
(USER_LAST_UPDATE_ID   ASC);

ALTER TABLE YY001_FORM_USER
	ADD (CONSTRAINT YY001_YY001_USER_CREATE_FK FOREIGN KEY (USER_CREATED_ID) REFERENCES YY001_FORM_USER (USER_ID));

ALTER TABLE YY001_FORM_USER
	ADD (CONSTRAINT YY001_YY001_USER_UPDATE_FK FOREIGN KEY (USER_LAST_UPDATE_ID) REFERENCES YY001_FORM_USER (USER_ID) ON DELETE SET NULL);

ALTER TABLE TY001_ROLE
	ADD (CONSTRAINT TY001_YY001_USER_CREATE_FK FOREIGN KEY (USER_CREATED_ID) REFERENCES YY001_FORM_USER (USER_ID));

ALTER TABLE TY001_ROLE
	ADD (CONSTRAINT TY001_YY001_USER_UPDATE_FK FOREIGN KEY (USER_LAST_UPDATE_ID) REFERENCES YY001_FORM_USER (USER_ID) ON DELETE SET NULL);

ALTER TABLE YY002_REMOTE_USER
	ADD (CONSTRAINT YY002_YY001_USER_CREATE_FK FOREIGN KEY (USER_CREATED_ID) REFERENCES YY001_FORM_USER (USER_ID));

ALTER TABLE YY002_REMOTE_USER
	ADD (CONSTRAINT YY002_YY001_USER_UPDATE_FK FOREIGN KEY (USER_LAST_UPDATE_ID) REFERENCES YY001_FORM_USER (USER_ID) ON DELETE SET NULL);

ALTER TABLE YY003_XREF_USER_ROLE
	ADD (CONSTRAINT YY003_YY001_FK FOREIGN KEY (USER_ID) REFERENCES YY001_FORM_USER (USER_ID));

ALTER TABLE YY003_XREF_USER_ROLE
	ADD (CONSTRAINT YY003_TY001_FK FOREIGN KEY (ROLE_ID) REFERENCES TY001_ROLE (ROLE_ID));

ALTER TABLE YY003_XREF_USER_ROLE
	ADD (CONSTRAINT YY003_YY001_USER_CREATE_FK FOREIGN KEY (USER_CREATED_ID) REFERENCES YY001_FORM_USER (USER_ID));

ALTER TABLE YY003_XREF_USER_ROLE
	ADD (CONSTRAINT YY003_YY001_USER_UPDATE_FK FOREIGN KEY (USER_LAST_UPDATE_ID) REFERENCES YY001_FORM_USER (USER_ID) ON DELETE SET NULL);

ALTER TABLE YY004_XREF_USER_MAPPING
	ADD (CONSTRAINT YY004_YY001_FK FOREIGN KEY (USER_ID) REFERENCES YY001_FORM_USER (USER_ID));

ALTER TABLE YY004_XREF_USER_MAPPING
	ADD (CONSTRAINT YY004_YY002_FK FOREIGN KEY (REMOTE_USER_ID) REFERENCES YY002_REMOTE_USER (REMOTE_USER_ID));

ALTER TABLE YY004_XREF_USER_MAPPING
	ADD (CONSTRAINT YY004_YY001_USER_CREATE_FK FOREIGN KEY (USER_CREATED_ID) REFERENCES YY001_FORM_USER (USER_ID));

ALTER TABLE YY004_XREF_USER_MAPPING
	ADD (CONSTRAINT YY004_YY001_USER_UPDATE_FK FOREIGN KEY (USER_LAST_UPDATE_ID) REFERENCES YY001_FORM_USER (USER_ID) ON DELETE SET NULL);

CREATE  OR REPLACE  PACKAGE NTC_UTIL 
AS

PROCEDURE RESET_SEQ (p_schema_owner varchar2,p_seq_name varchar2, p_val number default 0);
PROCEDURE RESET_SEQ_TO_DATA(p_schema_owner varchar2, p_TableName varchar2, p_FieldName varchar2, p_seq_name  varchar2);

END NTC_UTIL;
/



CREATE  OR REPLACE  PACKAGE BODY NTC_UTIL 
AS

PROCEDURE RESET_SEQ (p_schema_owner varchar2, p_seq_name  varchar2, p_val number default 0)
IS
	l_current number := 0;
  	l_difference number := 0;
  	l_minvalue all_sequences.min_value%type := 0;
  
  	v_dml clob;
BEGIN

	dbms_output.put_line('-------------------------------------------------------');
	dbms_output.put_line('-- Reset Sequence');
	dbms_output.put_line('-------------------------------------------------------');
    dbms_output.put_line('-- p_schema_owner = ' || p_schema_owner);
	dbms_output.put_line('-- p_seq_name = ' || p_seq_name);
	dbms_output.put_line('-- p_val = ' || p_val);
	dbms_output.put_line('-------------------------------------------------------');

	-- Get the min value for the sequence
	select min_value
	into l_minvalue
	from all_sequences
	where sequence_name = p_seq_name AND SEQUENCE_OWNER = p_schema_owner;
	
	dbms_output.put_line('-- l_minvalue = ' || l_minvalue);

	-- Determine the current count of the sequence
	execute immediate 'select ' || p_seq_name || '.nextval from dual' INTO l_current;

	dbms_output.put_line('-- l_current = ' || l_current);

    -- use the set val as long as it above the min value
	if p_Val < l_minvalue THEN
		l_difference := l_minvalue - l_current;
		dbms_output.put_line('-- Specified value is greater than minvalue (' || l_difference || ')' );
	else
	  l_difference := p_Val - l_current;
	 dbms_output.put_line('-- Specified value is not greater than minvalue (' || l_difference || ')' );
	end if;
	
    -- If there is no difference, skip and do nothing
	if l_difference = 0 THEN
		dbms_output.put_line('-- There is delta 0; Skip to next' );
	  	return;
	end if;

	dbms_output.put_line('-- Setting sequence number to max' );
	-- set the sequence based on the delta determined 
	execute immediate 'alter sequence ' || p_schema_owner || '.' || p_seq_name || ' increment by ' || l_difference || ' minvalue ' || l_minvalue;

	dbms_output.put_line('-- Incrementing the sequence' );
	-- set the sequence to count
	execute immediate 'select ' || p_schema_owner || '.' || p_seq_name || '.nextval from dual' INTO l_difference;

	dbms_output.put_line('-- Finalizing changes to the sequence' );
	-- finalize the changes to the sequence
	execute immediate 'alter sequence ' || p_schema_owner || '.' || p_seq_name || ' increment by 1 minvalue ' || l_minvalue;

	dbms_output.put_line('-------------------------------------------------------');

END RESET_SEQ;


PROCEDURE RESET_SEQ_TO_DATA(p_schema_owner varchar2, p_TableName varchar2, p_FieldName varchar2, p_seq_name  varchar2)
IS
  l_MaxUsed NUMBER;
BEGIN

	dbms_output.put_line('-------------------------------------------------------');
	dbms_output.put_line('-- Reset Sequence from Data');
	dbms_output.put_line('-------------------------------------------------------');
    dbms_output.put_line('-- p_schema_owner = ' || p_schema_owner);
    dbms_output.put_line('-- p_TableName = ' || p_TableName);
	dbms_output.put_line('-- p_FieldName = ' || p_FieldName);
	dbms_output.put_line('-- p_seq_name = ' || p_seq_name);
	dbms_output.put_line('-------------------------------------------------------');
	-- Determine max number for the field specified
	execute IMMEDIATE 'select coalesce(max(' || p_FieldName || '),0) from '|| p_schema_owner || '.' || p_TableName into l_MaxUsed;

	dbms_output.put_line('-- MaxUsed = ' || l_MaxUsed);

	dbms_output.put_line('-- Calling Reset sequence');
	RESET_SEQ(p_schema_owner, p_seq_name, l_MaxUsed );

    dbms_output.put_line('-------------------------------------------------------');
   
END RESET_SEQ_TO_DATA;

END NTC_UTIL;

/




CREATE  OR REPLACE  TRIGGER YY001_BI_FORM_USER_SEQ
  BEFORE INSERT
  ON YY001_FORM_USER
  
  for each row
  
/* ERwin Builtin Trigger */
/* default body for YY001_BI_FORM_USER_SEQ */
DECLARE NUMROWS INTEGER;
BEGIN

IF INSERTING THEN
        IF :NEW.USER_ID = 0  THEN

         SELECT YY001_FORM_USER_SEQ.NEXTVAL INTO :NEW.USER_ID FROM DUAL;

       ELSIF  :NEW.USER_ID IS NULL THEN

                 SELECT YY001_FORM_USER_SEQ.NEXTVAL INTO :NEW.USER_ID FROM DUAL;
            END IF;
    END IF;

/* ERwin Builtin Trigger */
    /* YY001_FORM_USER  YY001_FORM_USER on child insert set null */
    /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="YY001_FORM_USER"
    CHILD_OWNER="", CHILD_TABLE="YY001_FORM_USER"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="YY001_YY001_USER_UPDATE_FK", FK_COLUMNS="USER_LAST_UPDATE_ID" */
    UPDATE YY001_FORM_USER
      SET
        /* YY001_FORM_USER.USER_LAST_UPDATE_ID = NULL */
        YY001_FORM_USER.USER_LAST_UPDATE_ID = NULL
      WHERE
        NOT EXISTS (
          SELECT * FROM YY001_FORM_USER
            WHERE
              /* :new.USER_LAST_UPDATE_ID = YY001_FORM_USER.USER_ID */
              :new.USER_LAST_UPDATE_ID = YY001_FORM_USER.USER_ID
        ) 
        /* YY001_FORM_USER.USER_ID = :new.USER_ID */
         and YY001_FORM_USER.USER_ID = :new.USER_ID;

/* ERwin Builtin Trigger */
    /* YY001_FORM_USER  YY001_FORM_USER on child insert restrict */
    /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="YY001_FORM_USER"
    CHILD_OWNER="", CHILD_TABLE="YY001_FORM_USER"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="YY001_YY001_USER_CREATE_FK", FK_COLUMNS="USER_CREATED_ID" */
    SELECT count(*) INTO NUMROWS
      FROM YY001_FORM_USER
      WHERE
        /* :new.USER_CREATED_ID = YY001_FORM_USER.USER_ID */
        :new.USER_CREATED_ID = YY001_FORM_USER.USER_ID;
    IF (
      /*  */
      
      NUMROWS = 0
    )
    THEN
      raise_application_error(
        -20002,
        'Cannot insert YY001_FORM_USER because YY001_FORM_USER does not exist.'
      );
    END IF;



END;


/



ALTER TRIGGER YY001_BI_FORM_USER_SEQ
	ENABLE;


CREATE  OR REPLACE  TRIGGER TY001_BI_ROLE_SEQ
  BEFORE INSERT
  ON TY001_ROLE
  
  for each row
  
/* ERwin Builtin Trigger */
/* default body for TY001_BI_ROLE_SEQ */
DECLARE NUMROWS INTEGER;
BEGIN

IF INSERTING THEN
        IF :NEW.ROLE_ID = 0  THEN

         SELECT TY001_ROLE_SEQ.NEXTVAL INTO :NEW.ROLE_ID FROM DUAL;

       ELSIF  :NEW.ROLE_ID IS NULL THEN

                 SELECT TY001_ROLE_SEQ.NEXTVAL INTO :NEW.ROLE_ID FROM DUAL;
            END IF;
    END IF;

/* ERwin Builtin Trigger */
    /* YY001_FORM_USER  TY001_ROLE on child insert set null */
    /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="YY001_FORM_USER"
    CHILD_OWNER="", CHILD_TABLE="TY001_ROLE"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="TY001_YY001_USER_UPDATE_FK", FK_COLUMNS="USER_LAST_UPDATE_ID" */
    UPDATE TY001_ROLE
      SET
        /* TY001_ROLE.USER_LAST_UPDATE_ID = NULL */
        TY001_ROLE.USER_LAST_UPDATE_ID = NULL
      WHERE
        NOT EXISTS (
          SELECT * FROM YY001_FORM_USER
            WHERE
              /* :new.USER_LAST_UPDATE_ID = YY001_FORM_USER.USER_ID */
              :new.USER_LAST_UPDATE_ID = YY001_FORM_USER.USER_ID
        ) 
        /* TY001_ROLE.ROLE_ID = :new.ROLE_ID */
         and TY001_ROLE.ROLE_ID = :new.ROLE_ID;

/* ERwin Builtin Trigger */
    /* YY001_FORM_USER  TY001_ROLE on child insert restrict */
    /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="YY001_FORM_USER"
    CHILD_OWNER="", CHILD_TABLE="TY001_ROLE"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="TY001_YY001_USER_CREATE_FK", FK_COLUMNS="USER_CREATED_ID" */
    SELECT count(*) INTO NUMROWS
      FROM YY001_FORM_USER
      WHERE
        /* :new.USER_CREATED_ID = YY001_FORM_USER.USER_ID */
        :new.USER_CREATED_ID = YY001_FORM_USER.USER_ID;
    IF (
      /*  */
      
      NUMROWS = 0
    )
    THEN
      raise_application_error(
        -20002,
        'Cannot insert TY001_ROLE because YY001_FORM_USER does not exist.'
      );
    END IF;



END;


/



ALTER TRIGGER TY001_BI_ROLE_SEQ
	ENABLE;


CREATE  OR REPLACE  TRIGGER YY002_BI_REMOTE_USER_SEQ
  BEFORE INSERT
  ON YY002_REMOTE_USER
  
  for each row
  
/* ERwin Builtin Trigger */
/* default body for YY002_BI_REMOTE_USER_SEQ */
DECLARE NUMROWS INTEGER;
BEGIN

IF INSERTING THEN
        IF :NEW.REMOTE_USER_ID = 0  THEN

         SELECT YY002_REMOTE_USER_SEQ.NEXTVAL INTO :NEW.REMOTE_USER_ID FROM DUAL;

       ELSIF  :NEW.REMOTE_USER_ID IS NULL THEN

                 SELECT YY002_REMOTE_USER_SEQ.NEXTVAL INTO :NEW.REMOTE_USER_ID FROM DUAL;
            END IF;
    END IF;

/* ERwin Builtin Trigger */
    /* YY001_FORM_USER  YY002_REMOTE_USER on child insert set null */
    /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="YY001_FORM_USER"
    CHILD_OWNER="", CHILD_TABLE="YY002_REMOTE_USER"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="YY002_YY001_USER_UPDATE_FK", FK_COLUMNS="USER_LAST_UPDATE_ID" */
    UPDATE YY002_REMOTE_USER
      SET
        /* YY002_REMOTE_USER.USER_LAST_UPDATE_ID = NULL */
        YY002_REMOTE_USER.USER_LAST_UPDATE_ID = NULL
      WHERE
        NOT EXISTS (
          SELECT * FROM YY001_FORM_USER
            WHERE
              /* :new.USER_LAST_UPDATE_ID = YY001_FORM_USER.USER_ID */
              :new.USER_LAST_UPDATE_ID = YY001_FORM_USER.USER_ID
        ) 
        /* YY002_REMOTE_USER.REMOTE_USER_ID = :new.REMOTE_USER_ID */
         and YY002_REMOTE_USER.REMOTE_USER_ID = :new.REMOTE_USER_ID;

/* ERwin Builtin Trigger */
    /* YY001_FORM_USER  YY002_REMOTE_USER on child insert restrict */
    /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="YY001_FORM_USER"
    CHILD_OWNER="", CHILD_TABLE="YY002_REMOTE_USER"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="YY002_YY001_USER_CREATE_FK", FK_COLUMNS="USER_CREATED_ID" */
    SELECT count(*) INTO NUMROWS
      FROM YY001_FORM_USER
      WHERE
        /* :new.USER_CREATED_ID = YY001_FORM_USER.USER_ID */
        :new.USER_CREATED_ID = YY001_FORM_USER.USER_ID;
    IF (
      /*  */
      
      NUMROWS = 0
    )
    THEN
      raise_application_error(
        -20002,
        'Cannot insert YY002_REMOTE_USER because YY001_FORM_USER does not exist.'
      );
    END IF;



END;


/



ALTER TRIGGER YY002_BI_REMOTE_USER_SEQ
	ENABLE;
