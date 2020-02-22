CREATE TABLE report_authorization
(
  report_code character varying NOT NULL,
  id serial NOT NULL,
  "role" character varying,
  CONSTRAINT report_authorization_pkey PRIMARY KEY (id)
)
WITH (
  OIDS=FALSE
);
INSERT INTO report_authorization (report_code, id, role) VALUES ('1', 1, 'Admin');
INSERT INTO report_authorization (report_code, id, role) VALUES ('2', 2, 'Admin');
INSERT INTO report_authorization (report_code, id, role) VALUES ('3', 3, 'Admin');
INSERT INTO report_authorization (report_code, id, role) VALUES ('4', 4, 'Admin');

INSERT INTO report_authorization (report_code, id, role) VALUES ('5', 5, 'Admin');
INSERT INTO report_authorization (report_code, id, role) VALUES ('6', 6, 'Admin');
INSERT INTO report_authorization (report_code, id, role) VALUES ('7', 7, 'Admin');
INSERT INTO report_authorization (report_code, id, role) VALUES ('8', 8, 'Admin');
INSERT INTO report_authorization (report_code, id, role) VALUES ('9', 9, 'Admin');
INSERT INTO report_authorization (report_code, id, role) VALUES ('10', 10, 'Admin');
INSERT INTO report_authorization (report_code, id, role) VALUES ('11', 11, 'Admin');
INSERT INTO report_authorization (report_code, id, role) VALUES ('12', 12, 'Admin');
INSERT INTO report_authorization (report_code, id, role) VALUES ('13', 13, 'Admin');
INSERT INTO report_authorization (report_code, id, role) VALUES ('14', 14, 'Admin');
INSERT INTO report_authorization (report_code, id, role) VALUES ('15', 15, 'Admin');
