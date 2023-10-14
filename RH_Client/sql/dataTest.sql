insert into services values(default,'service informatique');

insert into poste values(default,1,'informaticien');

insert into besoin values(default,1,1,'recherche travailleur',30);

insert into besoin_critere values(default,1,1,5);
insert into besoin_critere values(default,1,2,5);
insert into besoin_critere values(default,1,3,5);
insert into besoin_critere values(default,1,4,5);
insert into besoin_critere values(default,1,6,5);

-- ------------------------------------
insert into candidat values(1, 'RAKOTO', 'Nomena', '1978-05-23', '0342456733', 'exemple@gmail.com', 'Tanjombato');
insert into candidat values(2, 'RAHARY', 'Malala', '1960-05-23', '0342456733', 'exemple@gmail.com', 'Tanjombato');

--                                          Cdt Bsn
insert into besoin_candidat values(default, 1,  1);
insert into besoin_candidat values(default, 2,  1);

insert into  candidat_critere values(default, 1, 4);
insert into  candidat_critere values(default, 1, 8);
insert into  candidat_critere values(default, 1, 20);
insert into  candidat_critere values(default, 1, 21);

insert into  candidat_critere values(default, 2, 5);
insert into  candidat_critere values(default, 2, 10);
insert into  candidat_critere values(default, 2, 19);
insert into  candidat_critere values(default, 2, 20);
insert into  candidat_critere values(default, 2, 21);


create view vw_candidatoptionnote as
select candidat_critere.*, critere.nature, critere_option_note.note, critere_option_note.idbesoincritere,besoin_critere.idbesoin,besoin_critere.coeff  from candidat_critere
    join critere_option_note on candidat_critere.idcritereoption = critere_option_note.idcritereoption
    join critere_option on critere_option_note.idcritereoption = critere_option.id
    join besoin_critere on critere_option.idcritere = besoin_critere.idcritere
    join critere on critere_option.idcritere = critere.id;
