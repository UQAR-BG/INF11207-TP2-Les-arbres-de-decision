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

        private Noeud NouveauNoeud(DataTable data, Attribut classe, List<Attribut> attributs)
        {
            Noeud noeud = new Noeud();
            service = new Gains(data, classe);

            if (TousLesEnregistrementOntMemeClasse(classe))
            {
                noeud.Valeur = EnsembleDeClasseLePlusPresent(classe);
                return noeud;
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
