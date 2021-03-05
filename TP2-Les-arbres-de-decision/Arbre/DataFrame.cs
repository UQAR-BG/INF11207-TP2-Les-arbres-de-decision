using System.Collections.Generic;
using System.Data;
using TP2_Les_arbres_de_decision.Services;
using TP2_Les_arbres_de_decision.Services.GainsInformation;

namespace TP2_Les_arbres_de_decision.Arbre
{
    public class DataFrame
    {
        public DataTable Data { get; }
        public List<Attribut> Attributs { get; }
        public Attribut Classe { get; }
        public bool TableVide { get; private set; }

        public DataFrame(DataTable data, List<Attribut> attributs, Attribut classe)
        {
            Data = data;
            Attributs = attributs;
            Classe = classe;
        }

        public bool TousLesEnregistrementOntMemeClasse()
        {
            bool tousOntMemeClasse = false;
            int nbreLignesPourEnsemblePlusPresent;

            Recherche conditions = new Recherche(Classe);
            conditions.ValeurClasse = EnsembleDeClasseLePlusPresent();

            nbreLignesPourEnsemblePlusPresent = NombreLignes.CompterNombreLignes(conditions);
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
                nombreLignes = NombreLignes.CompterNombreLignes(conditions);
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
