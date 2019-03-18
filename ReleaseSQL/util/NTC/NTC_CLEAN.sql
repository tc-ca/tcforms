--SELECT  'DROP SEQUENCE ' || sequence_name || ' ;' FROM user_sequences;
--SELECT  'DROP TABLE ' || table_name || ' cascade constraints;' FROM user_tables;
--SELECT  'DROP VIEW ' || view_name || ' cascade constraints;' FROM user_views;
--SELECT 'DROP '||object_type||' "'|| object_name || '";' FROM user_objects WHERE object_type IN ('TRIGGER');

DROP PACKAGE NTC_UTIL;

DROP TABLE YY001_FORM_USER cascade constraints;
DROP TABLE TY001_ROLE cascade constraints;
DROP TABLE YY002_REMOTE_USER cascade constraints;
DROP TABLE YY003_XREF_USER_ROLE cascade constraints;
DROP TABLE YY004_XREF_USER_MAPPING cascade constraints;

DROP SEQUENCE TY001_ROLE_SEQ ;
DROP SEQUENCE YY001_FORM_USER_SEQ ;
DROP SEQUENCE YY002_REMOTE_USER_SEQ ;

