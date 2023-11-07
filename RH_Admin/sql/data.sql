-- Donnees de teste:

/* 29-09-2023*/

INSERT INTO entreprise
VALUES(default, 'DIMPEX', 'Import - Export');

insert into services
values(default, 'Informatique');
insert into services
values(default, 'Ressource Humaine');
insert into services
values(default, 'Commerciale');
insert into services
values(default, 'Stock');

insert into niveau values(default, null);

--
insert into poste
values(default, 1, 1, 'Administrateur System', 'Responsable de la gestion et de la maintenance des systèmes informatiques');
insert into poste
values(default, 1, 1, 'Développeur de Logiciels', 'Charge de la conception, du developpement et de la maintenance des logiciels et des applications');
insert into poste
values(default, 1, 1, 'Spécialiste de la Sécurité Informatique', 'Responsable de la securite des donnees et des systemes informatiques');
insert into poste
values(default, 1, 1, 'Analyste de Donnees', 'Charge analyser et exploiter les donnees au profit du societe');

insert into poste
values(default, 2, 1, 'Gestionnaire des RH', 'Responsable de la gestion quotidienne des ressources humaines');
insert into poste
values(default, 2, 1, 'Recruteur', 'Charge de trouver et de recruter de nouveaux talents pour norte entreprise');
insert into poste
values(default, 2, 1, 'Specialiste des Avantages Sociaux', 'Responsable de la gestion des avantages sociaux, des salaires ou des primes');
insert into poste
values(default, 2, 1, 'Responsable de la Formation et du Développement', 'En charge de la planification des programmes de formation et du developpement');

insert into poste
values(default, 3, 1, 'Directeur Commercial', 'Responsable de la gestion globale des activités de vente de notre entreprise');
insert into poste
values(default, 3, 1, 'Representant des Ventes', 'Charge de la vente directe des produits ou services de notre entreprise aux clients');
insert into poste
values(default, 3, 1, 'Chef de Produit', 'Responsable de la gestion et de la promotion des produits');
insert into poste
values(default, 3, 1, 'Responsable des Relations Clients', 'En charge de la gestion des relations avec les clients');

insert into poste
values(default, 4, 1, 'Gestionnaire des Stocks', 'Responsable de la gestion des niveaux de stocks');
insert into poste
values(default, 4, 1, 'Responsable de la Logistique', 'Charge de coordonner les opérations logistiques');
insert into poste
values(default, 4, 1, 'Acheteur', 'Responsable de l achat de produits et de materiaux necessaires');
insert into poste
values(default, 4, 1, 'Gestionnaire des Stocks', 'En charge de l analyse des données de stocks');

-- 

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
insert into critere
values(default, 'Diplome', 0);
insert into critere
values(default, 'Experience', 0);
insert into critere
values(default, 'Sexe', 2);
insert into critere
values(default, 'Nationalite', 2);
insert into critere
values(default, 'Situation matrimoniale', 2);
insert into critere
values(default, 'Langue', 1);

-- CRITERE OPTION
insert into critere_option
values(default, 1, 'CEPE');
insert into critere_option
values(default, 1, 'BEPC');
insert into critere_option
values(default, 1, 'BACC');
insert into critere_option
values(default, 1, 'Licence');
insert into critere_option
values(default, 1, 'Maitrise');
insert into critere_option
values(default, 1, 'Doctorat');

insert into critere_option
values(default, 2, 'Aucun');
insert into critere_option
values(default, 2, 'Moins de 2 ans');
insert into critere_option
values(default, 2, 'Moins de 5 ans');
insert into critere_option
values(default, 2, 'Moins de 10 ans');
insert into critere_option
values(default, 2, 'Plus de 10 ans');

insert into critere_option
values(default, 3, 'Homme');
insert into critere_option
values(default, 3, 'Femme');

insert into critere_option
values(default, 4, 'Malagasy');
insert into critere_option
values(default, 4, 'Etranger');

insert into critere_option
values(default, 5, 'Celibataire');
insert into critere_option
values(default, 5, 'Mariee');
insert into critere_option
values(default, 5, 'Veuf');

insert into critere_option
values(default, 6, 'Anglais');
insert into critere_option
values(default, 6, 'Malagasy');
insert into critere_option
values(default, 6, 'Francais');
insert into critere_option
values(default, 6, 'Autre');

-- CRITERE OPTION NOTE
insert into critere_option_note
values(default, default, 1, 5);
insert into critere_option_note
values(default, default, 2, 8);
insert into critere_option_note
values(default, default, 3, 10);
insert into critere_option_note
values(default, default, 4, 12);
insert into critere_option_note
values(default, default, 5, 15);
insert into critere_option_note
values(default, default, 6, 19);

insert into critere_option_note
values(default, default, 7, 5);
insert into critere_option_note
values(default, default, 8, 8);
insert into critere_option_note
values(default, default, 9, 12);
insert into critere_option_note
values(default, default, 10, 15);
insert into critere_option_note
values(default, default, 11, 19);

insert into critere_option_note
values(default, default, 12, 20);
insert into critere_option_note
values(default, default, 13, 20);

insert into critere_option_note
values(default, default, 14, 20);
insert into critere_option_note
values(default, default, 15, 20);

insert into critere_option_note
values(default, default, 16, 20);
insert into critere_option_note
values(default, default, 17, 20);
insert into critere_option_note
values(default, default, 18, 20);

insert into critere_option_note
values(default, default, 19, 5);
insert into critere_option_note
values(default, default, 10, 10);
insert into critere_option_note
values(default, default, 21, 3);
insert into critere_option_note
values(default, default, 22, 2);


-- Autre 

insert into embauche values(default, 1, 1, 7, 2, current_date, 'M0001', 1);