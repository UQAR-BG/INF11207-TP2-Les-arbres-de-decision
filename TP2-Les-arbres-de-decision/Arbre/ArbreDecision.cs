using System;
using System.Collections.Generic;
using System.Data;
using TP2_Les_arbres_de_decision.Services;

namespace TP2_Les_arbres_de_decision.Arbre
{
    public class ArbreDecision
    {
        private Gains service;

        public Noeud Racine { get; private set; }

        public void ConstruireArbreDecisionID3(DataTable data, Attribut classe, List<Attribut> attributs)
        {
            Racine = NouveauNoeud(data, classe, attributs);
        }

        public Attribut CalculerAttributLePlusSignificatif(Gains service, List<Attribut> attributs)
        {
            int indexAttributSignificatif = 0;
            double gainPrecedent = 0;
            double gain = 0;

            for (int i = 0; i < attributs.Count; i++)
            {
                gain = service.GainsInformation(attributs[i]);

                if (gain > gainPrecedent)
                {
                    gainPrecedent = gain;
                    indexAttributSignificatif = i;
                }
            }

            return attributs[indexAttributSignificatif];
        }

        public DataTable ContientUneValeurSpecifiquePourAttribut(string valeur, Attribut cible, DataTable data)
        {
            DataTable sousEnsemble = new DataTable();
            DataRow[] lignes = data.Select($"{cible.Titre} = '{valeur}'");

            if (lignes.Length > 0)
            {
                sousEnsemble = lignes.CopyToDataTable();
            }

            return sousEnsemble;
        }

        public Noeud NouveauNoeud(DataTable data, Attribut classe, List<Attribut> attributs)
        {
            Noeud noeud = new Noeud();
            service = new Gains(data, classe);

            if (TousLesEnregistrementOntMemeClasse(classe))
            {
                string ensemble = EnsembleDeClasseLePlusPresent(classe);
                return CreerRacineSeule(ensemble);
            }

            if (attributs.Count == 0)
            {
                string ensemble = EnsembleDeClasseLePlusPresent(classe);
                return CreerRacineSeule(ensemble);
            }

            Attribut attributLePlusSignificatif = CalculerAttributLePlusSignificatif(service, attributs);
            noeud.Valeur = attributLePlusSignificatif.Titre;
            noeud.CreerBranches(attributLePlusSignificatif.Ensembles);

            // Pour chaque branche du noeud
            for (int i = 0; i < attributLePlusSignificatif.Ensembles.Count; i++)
            {
                DataTable sousEnsemble = ContientUneValeurSpecifiquePourAttribut(attributLePlusSignificatif.Ensembles[i], attributLePlusSignificatif, data);

                if (sousEnsemble.Rows.Count > 0)
                {
                    attributs.Remove(attributLePlusSignificatif);
                    noeud.AjouterNoeudAuBoutDeBranche(NouveauNoeud(sousEnsemble, classe, attributs), i);
                }
                else
                {
                    noeud.RetirerBranche(i);
                }
            }

            return noeud;
        }

        private bool TousLesEnregistrementOntMemeClasse(Attribut classe)
        {
            bool tousOntMemeClasse = false;

            Recherche conditions = new Recherche(classe);
            conditions.Valeur = EnsembleDeClasseLePlusPresent(classe);

            if (service.Probabilite(conditions) == 1)
            {
                tousOntMemeClasse = true;
            }

            return tousOntMemeClasse;
        }

        private Noeud CreerRacineSeule(string nomRacine)
        {
            Noeud noeud = new Noeud();
            noeud.Valeur = nomRacine;
            return noeud;
        }

        private string EnsembleDeClasseLePlusPresent(Attribut classe)
        {
            int nombreLignesPrecedent = 0;
            int nombreLignes;
            string ensembleLePlusPresent = classe.Ensembles[0];
            Recherche conditions = new Recherche(classe);

            foreach (string ensemble in classe.Ensembles)
            {
                conditions.ValeurClasse = ensemble;
                nombreLignes = service.NombreLignes(conditions);
                if (nombreLignes > nombreLignesPrecedent)
                {
                    nombreLignesPrecedent = nombreLignes;
                    ensembleLePlusPresent = ensemble;
                }
            }

            return ensembleLePlusPresent;
        }
    }
}
