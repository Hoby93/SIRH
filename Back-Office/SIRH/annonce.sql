CREATE TABLE annonce(
    id SERIAL PRIMARY KEY,
    idBesion INT,
    dateEnvoi DATETIME,
    entreprise VARCHAR(255)
);

CREATE TABLE typeContrat(
    id SERIAL PRIMARY KEY,
    nom VARCHAR(255) 
);

INSERT INTO typeContrat VALUES(default, "contrat de travail a duree indeterminee");
INSERT INTO typeContrat VALUES(default, "contrat de travail a duree determinee");
INSERT INTO typeContrat VALUES(default, "contrat de travail a duree determinee a objet defini");
INSERT INTO typeContrat VALUES(default, "contrat de travail a duree determinee Senior");
INSERT INTO typeContrat VALUES(default, "CDI interimaire");
INSERT INTO typeContrat VALUES(default, "contrat de travail a duree determinee d' usage");
INSERT INTO typeContrat VALUES(default, "travail a temps partiel");