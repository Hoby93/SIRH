create table services(
    id serial primary key,
    libelle varchar(80)
);

create table poste(
    id serial primary key,
    idservice int,
    libelle varchar(80),
    description varchar(160),
    foreign key(idservice) references services(id)
);

-- BESOIN ---------------------------------------------------------------------------------------------------

create table critere(
    id serial primary key,
    libelle varchar(80),
    nature int
);

create table typecontrat(
    id serial primary key,
    libelle varchar(80)
);

create table critere_option(
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

create table besoin_critere(
    id serial primary key,
    idbesoin int,
    idcritere int,
    coeff int,
    foreign key(idbesoin) references besoin(id),
    foreign key(idcritere) references critere(id)
);

create table critere_option_note(
    id serial primary key,
    idbesoincritere int default null,
    idcritereoption int,
    note real,
    foreign key(idbesoincritere) references besoin_critere(id),
    foreign key(idcritereoption) references critere_option(id)
);

-- select * from besoin_critere
-- join critere_option_note on besoin_critere.id = critere_option_note.idbesoincritere;
-- join critere_option on critere_option.id = critere_option_note.idcritereoption
-- join critere on critere.id = besoin_critere.idcritere;

create table candidat(
    id serial primary key,
    nom varchar(80),
    prenom varchar(120),
    dtn date,
    telephone varchar(10),
    email varchar(120),
    adresse varchar(80)
);

create table besoin_candidat(
    id serial primary key,
    idcandidat int,
    idbesoin int,
    dateinscription date,
    foreign key(idcandidat) references candidat(id),
    foreign key(idbesoin) references besoin(id)
);

create table candidat_critere(
    id serial primary key,
    idcandidat int,
    idcritereoption int,
    foreign key(idcandidat) references besoin_candidat(id),
    foreign key(idcritereoption) references critere_option_note(id)
);


create table question(
    id serial primary key,
    idposte int,
    question varchar(120),
    points int,
    foreign key(idposte) references poste(id)
);

create table proposition(
    id serial primary key,
    idquestion int,
    libelle varchar(120),
    etat int, -- Vrai(1) ou Faux(0)
    foreign key(idquestion) references question(id)
);

create table candidat_reponse(
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

-- TYPE CONTRAT
INSERT INTO typecontrat
VALUES(default, 'Contrat de travail a duree indeterminee');
INSERT INTO typecontrat
VALUES(default, 'Contrat de travail a duree determinee');
INSERT INTO typecontrat
VALUES(default, 'Contrat de travail a duree determinee a objet defini');
INSERT INTO typecontrat
VALUES(default, 'Contrat de travail a duree determinee Senior');
INSERT INTO typecontrat
VALUES(default, 'CDI interimaire');
INSERT INTO typecontrat
VALUES(default, 'Contrat de travail a duree determinee d'' usage');
INSERT INTO typecontrat
VALUES(default, 'Travail a temps partiel');

-- CRITERE
insert into critere values(default, 'Diplome', 0);
insert into critere values(default, 'Experience', 0);
insert into critere values(default, 'Sexe', 2);
insert into critere values(default, 'Nationalite', 2);
insert into critere values(default, 'Situation matrimoniale', 2);
insert into critere values(default, 'Langue', 1);

-- CRITERE OPTION
insert into critere_option values(default, 1, 'CEPE');
insert into critere_option values(default, 1, 'BEPC');
insert into critere_option values(default, 1, 'BACC');
insert into critere_option values(default, 1, 'Licence');
insert into critere_option values(default, 1, 'Maitrise');
insert into critere_option values(default, 1, 'Doctorat');

insert into critere_option values(default, 2, 'Aucun'); -- 7
insert into critere_option values(default, 2, 'Moins de 2 ans');
insert into critere_option values(default, 2, 'Moins de 5 ans');
insert into critere_option values(default, 2, 'Moins de 10 ans');
insert into critere_option values(default, 2, 'Plus de 10 ans');

insert into critere_option values(default, 3, 'Homme');
insert into critere_option values(default, 3, 'Femme');

insert into critere_option values(default, 4, 'Malagasy');
insert into critere_option values(default, 4, 'Etranger');

insert into critere_option values(default, 5, 'Celibataire');
insert into critere_option values(default, 5, 'Mariee');
insert into critere_option values(default, 5, 'Veuf');

insert into critere_option values(default, 6, 'Anglais'); --19
insert into critere_option values(default, 6, 'Malagasy');
insert into critere_option values(default, 6, 'Francais');
insert into critere_option values(default, 6, 'Autre');

-- CRITERE OPTION NOTE PAR DEFAUT
insert into critere_option_note values(default, default, 1, 5);
insert into critere_option_note values(default, default, 2, 8);
insert into critere_option_note values(default, default, 3, 10);
insert into critere_option_note values(default, default, 4, 12);
insert into critere_option_note values(default, default, 5, 15);
insert into critere_option_note values(default, default, 6, 19);

insert into critere_option_note values(default, default, 7, 5);
insert into critere_option_note values(default, default, 8, 8);
insert into critere_option_note values(default, default, 9, 12);
insert into critere_option_note values(default, default, 10, 15);
insert into critere_option_note values(default, default, 11, 19);

insert into critere_option_note values(default, default, 12, 20);
insert into critere_option_note values(default, default, 13, 20);

insert into critere_option_note values(default, default, 14, 20);
insert into critere_option_note values(default, default, 15, 20);

insert into critere_option_note values(default, default, 16, 20);
insert into critere_option_note values(default, default, 17, 20);
insert into critere_option_note values(default, default, 18, 20);

insert into critere_option_note values(default, default, 19, 5);
insert into critere_option_note values(default, default, 20, 10);
insert into critere_option_note values(default, default, 21, 3);
insert into critere_option_note values(default, default, 22, 2);


