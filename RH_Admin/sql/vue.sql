-- Permet de lier les criteres options au notes specifies pour chaque besoin
CREATE VIEW vw_besoin_critere_option_note
AS
    SELECT critere_option_note.*, critere_option.idcritere, critere_option.libelle
    FROM critere_option_note
        JOIN critere_option ON critere_option_note.idcritereoption = critere_option.id;

-- Permet de recuperer les notes associee a un besoin_critere_option d'un candidat
create view vw_candidatoptionnote as
select candidat_critere.*, critere.id as idcritere, critere.nature, critere_option_note.note, critere_option_note.idbesoincritere, 
       besoin_critere.idbesoin, besoin_critere.coeff  from candidat_critere
    join critere_option_note on candidat_critere.idcritereoption = critere_option_note.idcritereoption
    join critere_option on critere_option_note.idcritereoption = critere_option.id
    join besoin_critere on critere_option.idcritere = besoin_critere.idcritere
    join critere on critere_option.idcritere = critere.id;

-- Pour avoir les details du candidat, poste, service d'un embauche
create view vw_embauche as
select candidat.*, services.id as idservice, services.libelle as service, poste.id as idposte, poste.libelle as poste, poste.idniveau, embauche.idbesoincandidat, embauche.etat, embauche.dateembauche 
    from embauche 
    join candidat on embauche.idcandidat = candidat.id
    join poste on embauche.idposte = poste.id
    join services on poste.idservice = services.id;

-- Pour avoir les criteres options d'un candidat d'un embauche
create view vw_candidatcritere as
select candidat_critere.*, critere.id as idcritere, critere.libelle as critere, critere.nature, critere_option.libelle from candidat_critere 
    join critere_option_note on critere_option_note.id = candidat_critere.idcritereoption
    join critere_option on critere_option.id = critere_option_note.idcritereoption
    join critere on critere_option.idcritere = critere.id;

-- Pour avoir les proposistions choisie par un candidat pour une question
create view vw_candidat_reponse as
select candidat_reponse.*,proposition.idquestion from candidat_reponse
join proposition on candidat_reponse.idproposition = proposition.id;