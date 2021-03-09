using System;
using System.Collections.Generic;
using System.Data;
using TP2_Les_arbres_de_decision.Arbre;
using TP2_Les_arbres_de_decision.Services;

namespace TP2_Les_arbres_de_decision
{
    class Program
    {
        static void Main(string[] args)
        {
            ArbreTennis();
            Console.WriteLine("--------------------------------------");
            ArbreAnimaux();
        }

        private static void ArbreTennis()
        {
            string nomDataSet = "Tennis";
            DataTable tennis = Csv.LireCsv("tennis.csv");

            Attribut classe = Config.GetClasse(nomDataSet);
            List<Attribut> attributs = Config.GetAttributs(nomDataSet);

            ArbreDecision arbre = new ArbreDecision();
            arbre.ConstruireArbreDecisionID3(tennis, classe, attributs);
            Console.WriteLine("L'arbre de décision entrâiné par les données du fichier tennis.csv est prêt\n");

            int nbreEchantillons = Config.GetInt("NombreEchantillonsTennis");
            for (int i = 1; i <= nbreEchantillons; i++)
            {
                DataRow echantillon = Config.GetEchantillon(tennis, nomDataSet, i);
                TesterUnEchantillon(arbre, echantillon);
            }
        }

        public static void TesterUnEchantillon(ArbreDecision arbre, DataRow echantillon)
        {
            Config.AfficherEchantillon(echantillon);
            string decision = arbre.TesterUnEchantillon(echantillon);
            Console.WriteLine(decision + "\n");
        }

        private static void ArbreAnimaux()
        {
            string nomDataSet = "Animaux";
            DataTable zooTable = Csv.LireCsv("zoo.csv");
            DataTable classTable = Csv.LireCsv("class.csv");

            DataTable animauxTable = Csv.FusionnerTables(zooTable, classTable);

            if (Csv.EcrireCsv(animauxTable, "animaux.csv"))
            {
                animauxTable = Csv.LireCsv("animaux.csv");

                Attribut classe = Config.GetClasse(nomDataSet);
                List<Attribut> attributs = Config.GetAttributs(nomDataSet);

                ArbreDecision arbre = new ArbreDecision();
                arbre.ConstruireArbreDecisionID3(animauxTable, classe, attributs);
                Console.WriteLine("L'arbre de décision entrâiné par les données du fichier animaux.csv est prêt\n");

                int nbreEchantillons = Config.GetInt("NombreEchantillonsAnimaux");
                for (int i = 1; i <= nbreEchantillons; i++)
                {
                    DataRow echantillon = Config.GetEchantillon(animauxTable, nomDataSet, i);
                    TesterUnEchantillon(arbre, echantillon);
                }
            }
        }
    }
}
