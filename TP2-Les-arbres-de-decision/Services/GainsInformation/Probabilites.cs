using TP2_Les_arbres_de_decision.Arbre;

namespace TP2_Les_arbres_de_decision.Services.GainsInformation
{
    public class Probabilites
    {
        public static double CalculerProbabilite(Recherche conditions)
        {
            if (conditions.SurClasseUniquement)
            {
                return CalculerProbabilite(conditions.Cible, conditions.Valeur);
            }
            else
            {
                return CalculerProbabilite(conditions.Cible, conditions.Valeur, conditions.ValeurClasse);
            }
        }

        public static double CalculerProbabilite(Attribut attributCible, string valeur)
        {
            int nbreEnregistrementsParValeur = NombreLignes.CompterNombreLignes(attributCible, valeur);

            return (double)nbreEnregistrementsParValeur / DataStorage.NbreLignes;
        }

        public static double CalculerProbabilite(Attribut attributCible, string valeur, string valeurClasse)
        {
            int nbreEnregistrementsParValeur = NombreLignes.CompterNombreLignes(attributCible, valeur, valeurClasse);
            int nbreEnregistrementsTotal = NombreLignes.CompterNombreLignes(attributCible, valeur);

            return (double)nbreEnregistrementsParValeur / nbreEnregistrementsTotal;
        }
    }
}
