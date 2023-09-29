create database societe;

\c societe;

create table employe(
    id serial primary key,
    nom varchar(40),
    prenom varchar(40),
    ip real,
    datenaissance date
);

insert into employe values(default, 'RAKOTO', 'Niaina', 2000, '2001-12-05');
insert into employe values(default, 'RABE', 'Manana', 3000, '2001-02-05');
insert into employe values(default, 'MINO', 'Rajao', 5000, '1999-10-05');
insert into employe values(default, 'ZOTO', 'Bao', 2500, '2005-08-24');