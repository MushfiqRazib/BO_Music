-- View: view_article

-- DROP VIEW view_article;

CREATE OR REPLACE VIEW view_article AS 
 SELECT a.articlecode, a.descriptionen, a.title, a.subtitle, (((c.firstname::text || ' '::text) || c.middlename::text) || ' '::text) || c.lastname::text AS composer, (((e.firstname::text || ' '::text) || e.middlename::text) || ' '::text) || e.lastname::text AS editor, (((p.firstname::text || ' '::text) || p.middlename::text) || ' '::text) || p.lastname::text AS publisher, a.serie, g.gradenamenl, a.subcategory, cnt.countryname, a.price, a.editionno, a.publicationno, a.pages, a.publishdate, a.duration, a.ismn, a.isbn10, a.isbn13, a.articletype, a.quantity, a.imagefile, a.pdffile, a.purchaseprice, a.descriptionnl, l.languagename, cat.categorynamenl, a.period, a.isactive, a.containsmusic, a.keywords, a.instrumentation, a.events
   FROM article a
   LEFT JOIN editor e ON a.editor = e.editorid::numeric
   LEFT JOIN publisher p ON a.publisher = p.publisherid::numeric
   LEFT JOIN composer c ON a.composer = c.composerid::numeric
   LEFT JOIN grade g ON g.gradeid::text = a.grade::text
   LEFT JOIN country cnt ON cnt.countrycode::text = a.country::text
   LEFT JOIN language l ON l.languagecode::text = a.language::text
   LEFT JOIN category cat ON cat.categoryid::text = a.category::text;

ALTER TABLE view_article OWNER TO postgres;

