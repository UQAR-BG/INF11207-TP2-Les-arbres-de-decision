using System;
using System.Data;
using TP2_Les_arbres_de_decision.Arbre;

namespace TP2_Les_arbres_de_decision.Services
{
    public class Gains
    {
        private DataTable data;
        private Attribut classe;

        private int nbreLignes;
        private double entropieTotale;

        public Gains(DataTable data, Attribut classe)
        {
            this.data = data;
            this.classe = classe;
            nbreLignes = data.Rows.Count;
            entropieTotale = Entropie();
        }

        public double GainsInformation(Attribut attributCible)
        {
            double gainsInfo = entropieTotale;
            Recherche conditions = new Recherche(attributCible);

            foreach (string ensemble in attributCible.Ensembles)
            {
                conditions.Valeur = ensemble;
                gainsInfo -= Probabilite(attributCible, conditions.Valeur) * Entropie(conditions);
            }

            return gainsInfo;
        }

        public double Probabilite(Recherche conditions)
        {
            if (conditions.SurClasseUniquement)
            {
                return Probabilite(conditions.Cible, conditions.Valeur);
            }
            else
            {
                return Probabilite(conditions.Cible, conditions.Valeur, conditions.ValeurClasse);
            }
        }

        public int NombreLignes(Recherche conditions)
        {
            if (conditions.SurClasseUniquement)
            {
                return CompterNombreLignes(classe, conditions.ValeurClasse);
            }
            else
            {
                return CompterNombreLignes(conditions.Cible, conditions.Valeur, conditions.ValeurClasse);
            }
        }

        public double Entropie()
        {
            Recherche conditions = new Recherche(classe);
            return Entropie(conditions);
        }

        public double Entropie(Attribut attributCible, string ensemble)
        {
            Recherche conditions = new Recherche(attributCible, ensemble);
            return Entropie(conditions);
        }

        public double Entropie(Recherche conditions)
        {
            double entropie = 0;
            double probabilite;

            foreach (string ensemblePivot in classe.Ensembles)
            {
                conditions.Valeur = conditions.SurClasseUniquement ? ensemblePivot : conditions.Valeur;
                conditions.ValeurClasse = ensemblePivot;

                probabilite = Probabilite(conditions);
                entropie -= probabilite * Math.Log2(probabilite);

                if (double.IsNaN(entropie))
                    return 0;
            }

            return entropie;
        }

        private double Probabilite(Attribut attributCible, string valeur)
        {
            int nbreEnregistrementsParValeur = CompterNombreLignes(attributCible, valeur);

            return (double)nbreEnregistrementsParValeur / nbreLignes;
        }

        private double Probabilite(Attribut attributCible, string valeur, string valeurClasse)
        {
            int nbreEnregistrementsParValeur = CompterNombreLignes(attributCible, valeur, valeurClasse);
            int nbreEnregistrementsTotal = CompterNombreLignes(attributCible, valeur);

            return (double)nbreEnregistrementsParValeur / nbreEnregistrementsTotal;
        }

        private int CompterNombreLignes(Attribut attributCible, string valeur)
        {
            string requete = $"{attributCible.Titre} = '{valeur}'";
            return EssayerCompterLignes(requete);
        }

        private int CompterNombreLignes(Attribut attributCible, string valeur, string valeurClasse)
        {
            string requete = $"{attributCible.Titre} = '{valeur}' AND {classe.Titre} = '{valeurClasse}'";
            return EssayerCompterLignes(requete);
        }

        private int EssayerCompterLignes(string requete)
        {
            int nombre;

            try
            {
                nombre = data.Select(requete).GetLength(0);
            }
            catch (EvaluateException)
            {
                nombre = 0;
            }

            return nombre;
        }
    }
}
