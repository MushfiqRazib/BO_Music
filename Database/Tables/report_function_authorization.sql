CREATE TABLE report_function_authorization
(
  "roleId" integer NOT NULL,
  reportcode character varying NOT NULL,
  function_name character varying NOT NULL
)
WITH (
  OIDS=FALSE
);