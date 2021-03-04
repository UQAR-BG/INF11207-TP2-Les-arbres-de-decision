using System.Collections.Generic;
using System.Data;
using TP2_Les_arbres_de_decision.Services;

namespace TP2_Les_arbres_de_decision.Arbre
{
    public class DataFrame
    {
        private Gains service;

        public DataTable Data { get; }
        public List<Attribut> Attributs { get; }
        public Attribut Classe { get; }
        public bool TableVide { get; private set; }

        public DataFrame(DataTable data, List<Attribut> attributs, Attribut classe)
        {
            Data = data;
            Attributs = attributs;
            Classe = classe;
            service = CreerServiceGains(data, classe);
        }

        public bool TousLesEnregistrementOntMemeClasse()
        {
            bool tousOntMemeClasse = false;
            int nbreLignesPourEnsemblePlusPresent;

            Recherche conditions = new Recherche(Classe);
            conditions.ValeurClasse = EnsembleDeClasseLePlusPresent();

            nbreLignesPourEnsemblePlusPresent = service.NombreLignes(conditions);
            if (nbreLignesPourEnsemblePlusPresent >= Data.Rows.Count)
            {
                tousOntMemeClasse = true;
            }

            return tousOntMemeClasse;
        }

        public string EnsembleDeClasseLePlusPresent()
        {
            int nombreLignesPrecedent = 0;
            int nombreLignes;
            string ensembleLePlusPresent = TableVide ? "" : Classe.Ensembles[0];
            Recherche conditions = new Recherche(Classe);

            foreach (string ensemble in Classe.Ensembles)
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

        private Gains CreerServiceGains(DataTable data, Attribut classe)
        {
            if (classe.Ensembles.Count == 0)
            {
                TableVide = true;
            }

            return new Gains(data, classe);
        }
    }
}
