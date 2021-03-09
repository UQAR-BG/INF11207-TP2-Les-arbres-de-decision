using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using TP2_Les_arbres_de_decision.Arbre;

namespace TP2_Les_arbres_de_decision.Services
{
    static class Config
    {
        public static string Get(string cle)
        {
            return ConfigurationManager.AppSettings.Get(cle);
        }

        public static int GetInt(string cle)
        {
            return int.Parse(ConfigurationManager.AppSettings.Get(cle));
        }

        public static string[] GetArray(string cle, char separateur)
        {
            return ConfigurationManager.AppSettings.Get(cle).Split(separateur);
        }

        public static Attribut GetClasse(string nomSetDonnees)
        {
            string[] paramClasse = GetArray($"Classe{nomSetDonnees}", '=');
            string nomClasse = paramClasse[0];
            List<string> ensembles = paramClasse[1].Split(',').ToList();

            return new Attribut(nomClasse, ensembles, true);
        }

        public static List<Attribut> GetAttributs(string nomSetDonnees)
        {
            string[] paramAttributs = GetArray($"Attributs{nomSetDonnees}", ';');
            List<Attribut> attributs = new List<Attribut>();

            if (paramAttributs.Length > 0)
            {
                for (int i = 0; i < paramAttributs.Length; i++)
                {
                    string[] paramAttribut = paramAttributs[i].Split('=');
                    string nomAttribut = paramAttribut[0];
                    List<string> ensembles = paramAttribut[1].Split(',').ToList();

                    attributs.Add(new Attribut(nomAttribut, ensembles, false));
                }
            }

            return attributs;
        }

        public static DataRow GetEchantillon(DataTable tableau, string nomDataSet, int numero)
        {
            string[] parametres = GetArray($"Echantillon{nomDataSet}{numero}", ',');
            DataRow echantillon = tableau.NewRow();

            foreach (string parametre in parametres)
            {
                string[] cleEtValeur = parametre.Split('=');
                echantillon[cleEtValeur[0]] = cleEtValeur[1];
            }

            return echantillon;
        }

        public static void AfficherEchantillon(DataRow echantillon)
        {
            string structure = "";

            if (echantillon.ItemArray.Length > 0)
            {
                structure = "[ ";
                for (int i = 0; i < echantillon.Table.Columns.Count; i++)
                {
                    string nomAttribut = echantillon.Table.Columns[i].ColumnName;
                    string terminaison = ", ";

                    if (i == echantillon.Table.Columns.Count - 1)
                    {
                        terminaison = " ]";
                    }
                    structure += $"'{nomAttribut}': '{echantillon[nomAttribut]}'{terminaison}";
                }
            }

            Console.WriteLine($"Test de l'échantillon {structure}");
        }
    }
}