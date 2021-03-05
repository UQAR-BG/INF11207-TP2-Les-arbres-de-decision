using TP2_Les_arbres_de_decision.Arbre;
using TP2_Les_arbres_de_decision.Services.GainsInformation;

namespace TP2_Les_arbres_de_decision.Services
{
    public class Gains
    {
        public static double CalculerGainsInformation(Attribut attributCible)
        {
            double gainsInfo = Entropie.CalculerEntropie();
            Recherche conditions = new Recherche(attributCible);

            foreach (string ensemble in attributCible.Ensembles)
            {
                conditions.Valeur = ensemble;
                gainsInfo -= Probabilites.CalculerProbabilite(attributCible, conditions.Valeur) * Entropie.CalculerEntropie(conditions);
            }

            return gainsInfo;
        }
    }
}
