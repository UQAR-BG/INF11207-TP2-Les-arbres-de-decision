using System.Data;
using TP2_Les_arbres_de_decision.Arbre;

namespace TP2_Les_arbres_de_decision.Services.GainsInformation
{
    public class NombreLignes
    {
        public static int CompterNombreLignes(Recherche conditions)
        {
            if (conditions.SurClasseUniquement)
            {
                return CompterNombreLignes(DataStorage.Classe, conditions.ValeurClasse);
            }
            else
            {
                return CompterNombreLignes(conditions.Cible, conditions.Valeur, conditions.ValeurClasse);
            }
        }

        public static int CompterNombreLignes(Attribut attributCible, string valeur)
        {
            string requete = $"{attributCible.Titre} = '{valeur}'";
            return EssayerCompterLignes(requete);
        }

        public static int CompterNombreLignes(Attribut attributCible, string valeur, string valeurClasse)
        {
            string requete = $"{attributCible.Titre} = '{valeur}' AND {DataStorage.Classe.Titre} = '{valeurClasse}'";
            return EssayerCompterLignes(requete);
        }

        private static int EssayerCompterLignes(string requete)
        {
            int nombre;

            try
            {
                nombre = DataStorage.Table.Select(requete).GetLength(0);
            }
            catch (EvaluateException)
            {
                nombre = 0;
            }

            return nombre;
        }
    }
}
