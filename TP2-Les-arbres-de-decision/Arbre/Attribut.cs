using System.Collections.Generic;
using TP2_Les_arbres_de_decision.Services;
using TP2_Les_arbres_de_decision.Services.GainsInformation;

namespace TP2_Les_arbres_de_decision.Arbre
{
    public class Attribut
    {
        public string Titre { get; }
        public List<string> Ensembles { get; }
        public bool EstClasse { get; }

        public Attribut(string titre, List<string> ensembles, bool estClasse)
        {
            Titre = titre;
            Ensembles = ensembles;
            EstClasse = estClasse;
        }

        public static Attribut CalculerAttributLePlusSignificatif(List<Attribut> attributs)
        {
            int indexAttributSignificatif = 0;
            double gainPrecedent = 0;
            double gain;

            for (int i = 0; i < attributs.Count; i++)
            {
                gain = Gains.CalculerGainsInformation(attributs[i]);

                if (gain > gainPrecedent)
                {
                    gainPrecedent = gain;
                    indexAttributSignificatif = i;
                }
            }

            return attributs[indexAttributSignificatif];
        }

        public static bool TousLesEnregistrementOntMemeClasse(Attribut classe)
        {
            bool tousOntMemeClasse = false;

            Recherche conditions = new Recherche(classe);
            conditions.Valeur = EnsembleDeClasseLePlusPresent(classe);

            if (Probabilites.CalculerProbabilite(conditions) == 1)
            {
                tousOntMemeClasse = true;
            }

            return tousOntMemeClasse;
        }

        public static string EnsembleDeClasseLePlusPresent(Attribut classe)
        {
            int nombreLignesPrecedent = 0;
            int nombreLignes;
            string ensembleLePlusPresent = classe.Ensembles[0];
            Recherche conditions = new Recherche(classe);

            foreach (string ensemble in classe.Ensembles)
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
