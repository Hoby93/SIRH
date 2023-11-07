/* 29-09-2023 */

create table entreprise
(
    id SERIAL PRIMARY KEY,
    nom VARCHAR(100),
    activite VARCHAR(100)
);

create table services
(
    id serial primary key,
    libelle varchar(80)
);

create table niveau(
    id serial primary key,
    libelle varchar(80)
);

INSERT INTO niveau VALUES(default, null);

create table poste(
    id serial primary key,
    idservice int,
    idniveau int,
    libelle varchar(80),
    description varchar(160),
    foreign key(idservice) references services(id),
    foreign key(idniveau) references niveau(id)
);

-- BESOIN ---------------------------------------------------------------------------------------------------

create table critere
(
    id serial primary key,
    libelle varchar(80),
    nature int
);

create table typecontrat
(
    id serial primary key,
    libelle varchar(80)
);

create table critere_option
(
    id serial primary key,
    idcritere int,
    libelle varchar(80),
    foreign key(idcritere) references critere(id)
);

create table besoin(
    id serial primary key,
    idposte int,
    idtypecontrat int,
    libelle varchar(120),
    volumehoraire real,
    noteadmis int default 10,
    datedebut date default now(),
    datefin date default null,
    agemin int default 18,
    agemax int default 65,
    foreign key(idposte) references poste(id),
    foreign key(idtypecontrat) references typecontrat(id)
);

CREATE TABLE annonce
(
    id SERIAL PRIMARY KEY,
    idBesoin INT,
    dateEnvoi TIMESTAMP,
    idEntreprise INT,
    FOREIGN KEY(idBesoin) REFERENCES besoin(id),
    FOREIGN KEY(idEntreprise) REFERENCES entreprise(id)
);

create table besoin_critere
(
    id serial primary key,
    idbesoin int,
    idcritere int,
    coeff int,
    foreign key(idbesoin) references besoin(id),
    foreign key(idcritere) references critere(id)
);

create table critere_option_note
(
    id serial primary key,
    idbesoincritere int default null,
    idcritereoption int,
    note real,
    foreign key(idbesoincritere) references besoin_critere(id),
    foreign key(idcritereoption) references critere_option(id)
);

create table candidat
(
    id serial primary key,
    nom varchar(80),
    prenom varchar(120),
    telephone varchar(10),
    email varchar(120),
    adresse varchar(80), 
    genre int,
    dateNaissance timestamp
);

create table besoin_candidat(
    id serial primary key,
    idcandidat int,
    idbesoin int,
    foreign key(idcandidat) references candidat(id),
    foreign key(idbesoin) references besoin(id)
);

create table candidat_critere
(
    id serial primary key,
    idcandidat int,
    idcritereoption int,
    foreign key(idcritereoption) references critere_option_note(id)
);


create table question
(
    id serial primary key,
    idposte int,
    question varchar(120),
    points int,
    foreign key(idposte) references poste(id)
);

create table proposition
(
    id serial primary key,
    idquestion int,
    libelle varchar(120),
    etat int,
    -- Vrai(1) ou Faux(0)
    foreign key(idquestion) references question(id)
);

create table candidat_reponse
(
    id serial primary key,
    idbesoincandidat int,
    idproposition int,
    foreign key(idbesoincandidat) references besoin_candidat(id),
    foreign key(idproposition) references proposition(id)
);

/* NB:
        - La nature critere: drop_down_list(0)/check_box(1)/radio(2)
        - Les options_critere au desous du minimal requis (Note) => -100
*/

-- TEST ---------------------------------------------------------------------------------------------------

create table embauche(
    id serial primary key,
    idcandidat int,
    idposte int,
    idbesoincandidat int,
    idtypecontrat int,
    dateembauche date,
    numeroMatricule VARCHAR(50),
    etat int,
    foreign key(idcandidat) references candidat(id),
    foreign key(idposte) references poste(id),
    foreign key(idbesoincandidat) references besoin_candidat(id),
    foreign key(idtypecontrat) references typecontrat(id)
);

create table salaire(
    id serial primary key,
    idembauche int,
    valeurbrute real,
    valeurnet real,
    datedebut date,
    datefin date,
    foreign key(idembauche) references embauche(id)
);

CREATE TABLE typeConge(
    id SERIAL PRIMARY KEY,
    nom VARCHAR(255),
    estDeductible INTEGER -- 0 non ,1 oui
);

create table historique_fait(
    id serial primary key,
    idembauche int,
    fait int, -- Avertissement(-1) & Renvoye (-3) & ...
    datefait date,
    foreign key(idembauche) references embauche(id)
);

CREATE TABLE demandeConge(
    id SERIAL PRIMARY KEY,
    idembauche INTEGER,
    idtypeconge INTEGER,
    debutconge TIMESTAMP,
    finconge TIMESTAMP,
    etat INTEGER DEFAULT 0, -- -2 refus;0 en attente;2 accepter
    FOREIGN KEY(idtypeconge) REFERENCES typeConge(id),
    FOREIGN KEY(idembauche) REFERENCES embauche(id)
);

CREATE TABLE reelConge(
    id SERIAL PRIMARY KEY,
    idembauche INTEGER,
    idtypeconge INTEGER,
    debutconge TIMESTAMP,
    finconge TIMESTAMP, -- null si pas encore terminer
    FOREIGN KEY(idtypeconge) REFERENCES typeConge(id),
    FOREIGN KEY(idembauche) REFERENCES embauche(id)
);

CREATE TABLE avantage(
    id SERIAL PRIMARY KEY,
    libelle VARCHAR(50)
);

CREATE TABLE contratEssai(
    id SERIAL PRIMARY KEY,
    idEmbauche INT,
    tempsTravail REAL,
    dateDebutContrat TIMESTAMP,
    dureeEssai REAL,
    joursTravailles REAL,
    FOREIGN KEY(idEmbauche) REFERENCES embauche(id)
);

CREATE TABLE contratEssaiAvantage(
    id SERIAL PRIMARY KEY,
    idContratEssai INT,
    idAvantage INT,
    FOREIGN KEY(idContratEssai) REFERENCES contratEssai(id),
    FOREIGN KEY(idAvantage) REFERENCES avantage(id)
);

create table noteCandidat(
    id SERIAL PRIMARY KEY,
    idcandidat INTEGER,
    idbesoin INTEGER,
    note REAL NOT NULL,
    FOREIGN KEY(idcandidat) REFERENCES candidat(id),
    FOREIGN KEY(idbesoin) REFERENCES besoin(id)
);


