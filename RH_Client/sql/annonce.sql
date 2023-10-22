create table entreprise
(
    id SERIAL PRIMARY KEY,
    nom VARCHAR(100),
    activite VARCHAR(100)
);

INSERT INTO entreprise
VALUES(default, 'DIMPEX', 'Import - Export');

CREATE TABLE annonce
(
    id SERIAL PRIMARY KEY,
    idBesoin INT,
    dateEnvoi TIMESTAMP,
    idEntreprise INT,
    FOREIGN KEY(idBesoin) REFERENCES besoin(id),
    FOREIGN KEY(idEntreprise) REFERENCES entreprise(id)
);

INSERT INTO annonce VALUES(default,1,'12-11-2021',1);

CREATE VIEW vw_besoin_critere_option_note AS
SELECT critere_option_note.*, critere_option.idcritere, critere_option.libelle
FROM critere_option_note
    JOIN critere_option ON critere_option_note.idcritereoption = critere_option.id;

