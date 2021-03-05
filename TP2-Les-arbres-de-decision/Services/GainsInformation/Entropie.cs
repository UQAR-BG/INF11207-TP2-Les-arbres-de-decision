using System;
using TP2_Les_arbres_de_decision.Arbre;

namespace TP2_Les_arbres_de_decision.Services.GainsInformation
{
    public class Entropie
    {
        public static double CalculerEntropie()
        {
            Recherche conditions = new Recherche(DataStorage.Classe);
            return CalculerEntropie(conditions);
        }

        public static double CalculerEntropie(Attribut attributCible, string ensemble)
        {
            Recherche conditions = new Recherche(attributCible, ensemble);
            return CalculerEntropie(conditions);
        }

        public static double CalculerEntropie(Recherche conditions)
        {
            double entropie = 0;
            double probabilite;

            foreach (string ensemblePivot in DataStorage.Classe.Ensembles)
            {
                conditions.Valeur = conditions.SurClasseUniquement ? ensemblePivot : conditions.Valeur;
                conditions.ValeurClasse = ensemblePivot;

                probabilite = Probabilites.CalculerProbabilite(conditions);
                entropie -= probabilite * Math.Log2(probabilite);

                if (double.IsNaN(entropie))
                    return 0;
            }

            return entropie;
        }
    }
}
