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

SELECT besoin_critere.*, critere.libelle, critere.nature
FROM besoin_critere JOIN critere ON critere.id = besoin_critere.idcritere;

SELECT *
FROM besoin_critere
    JOIN critere_option ON critere_option.idcritere = besoin_critere.idcritere;

CREATE VIEW vw_besoin_critere_option_note AS
SELECT critere_option_note.*, critere_option.idcritere, critere_option.libelle
FROM critere_option_note
    JOIN critere_option ON critere_option_note.idcritereoption = critere_option.id;


SELECT * FROM vw_besoin_critere_option_note
WHERE idcritere = 2 AND idbesoincritere IS NULL AND note = (SELECT MIN(note)
    FROM vw_besoin_critere_option_note
    WHERE idcritere = 2 AND idbesoincritere IS NULL);

SELECT vw_besoin_critere_option_note.*, besoin_critere.idbesoin, besoin_critere.coeff
FROM vw_besoin_critere_option_note
    JOIN besoin_critere ON besoin_critere.id = idbesoincritere
WHERE vw_besoin_critere_option_note.idcritere = 1 AND besoin_critere.idbesoin = 1 AND note = (SELECT MIN(note)
    FROM vw_besoin_critere_option_note
        JOIN besoin_critere ON besoin_critere.id = idbesoincritere
    WHERE vw_besoin_critere_option_note.idcritere = 1 AND besoin_critere.idbesoin = 1 AND note >= 0);