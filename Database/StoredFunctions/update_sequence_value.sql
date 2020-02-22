CREATE OR REPLACE FUNCTION update_sequence_value()
  RETURNS void AS
$BODY$
DECLARE
	_token text[] ;
	_tablename text;
	_columnname text;
	_maxValue bigint;
	_seq text;
BEGIN
	FOR _seq IN SELECT relname
			FROM pg_class
			 WHERE relkind = 'S'
			   AND relnamespace IN (
				SELECT oid
				  FROM pg_namespace
				 WHERE nspname NOT LIKE 'pg_%'
				   AND nspname != 'information_schema'
			)  LOOP
		_token = regexp_split_to_array(_seq, E'_');	--split the string using underscore
		_tablename = '';

		--this part is very helpfull when tablename contains any underscore
		FOR i IN array_lower(_token,1)..array_upper(_token,1)-2 LOOP
			_tablename = _tablename || _token[i] || '_';
		END LOOP;
		_tablename = trim(both '_' from _tablename);	
--		raise notice 'table name : %', trim(both '_' from _tablename);
--		_tablename = _token[1];
--		_columnname = _token[2];
		_columnname = _token[array_length(_token,1)-1];
		EXECUTE('select coalesce (max('|| _columnname ||'),1)  from ' || _tablename ) into _maxValue;
		EXECUTE('SELECT setval(''public."' || _seq || '"'',' || _maxValue || ', true)');
		raise notice 'tablename : %, Seq_field: %, max: %', _tablename, _columnname, _maxValue;
	
	END LOOP;

	return ;
	END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE
  COST 100;

  select update_sequence_value();