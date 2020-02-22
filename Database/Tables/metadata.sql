CREATE TABLE metadata
(
  id serial NOT NULL,
  fieldname character varying NOT NULL,
  fieldtype character varying,
  caption character varying,
  mandatory character varying(3) NOT NULL,
  "Default" character varying,
  lovc character varying,
  lovp character varying,
  lovcp character varying,
  "minvalue" character varying,
  "maxvalue" character varying,
  decimals character varying,
  strlen integer,
  displen integer,
  allowedit boolean NOT NULL,
  tip character varying,
  groupname character varying,
  errorlevel integer NOT NULL DEFAULT 0,
  CONSTRAINT "PK_Metadata_id" PRIMARY KEY (id),
  CONSTRAINT metadata_fieldtype_check CHECK (fieldtype::text = ANY (ARRAY['STR'::character varying::text, 'INT'::character varying::text, 'FLOAT'::character varying::text, 'Date'::character varying::text, 'OBJLEN'::character varying::text, 'OBJAREA'::character varying::text, 'LASTUPDATE'::character varying::text])),
  CONSTRAINT metadata_mandatory_check CHECK (mandatory::text = ANY (ARRAY['Yes'::character varying::text, 'No'::character varying::text, 'LOV'::character varying::text]))
)
WITH (
  OIDS=FALSE
);