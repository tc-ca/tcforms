SET SERVEROUTPUT ON;

BEGIN
	dbms_output.enable(1000000);
	dbms_output.put_line('=======================================================');
	dbms_output.put_line('== Resetting All Sequences');

	dbms_output.put_line('=======================================================');
	dbms_output.put_line('== Lookup Tables');

	NTC_UTIL.RESET_SEQ_TO_DATA('NTC', 'TY001_ROLE', 'ROLE_ID', 'TY001_ROLE_SEQ');
	
	dbms_output.put_line('=======================================================');
	dbms_output.put_line('== Data Tables');

	NTC_UTIL.RESET_SEQ_TO_DATA('NTC', 'YY001_FORM_USER', 'USER_ID', 'YY001_FORM_USER_SEQ');
	NTC_UTIL.RESET_SEQ_TO_DATA('NTC', 'YY002_REMOTE_USER', 'REMOTE_USER_ID', 'YY002_REMOTE_USER_SEQ');

	dbms_output.put_line('=======================================================');
END;