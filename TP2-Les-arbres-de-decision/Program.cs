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
            ArbreAnimaux();
        }

        private static void ArbreTennis()
        {
            Console.WriteLine("Lecture du fichier tennis.csv en cours...");
            DataTable tennis = Csv.LireCsv("tennis.csv");
            Console.WriteLine("Lecture du fichier tennis.csv complétée\n");

            Attribut classe = new Attribut("play", new List<string> { "yes", "no" }, true);
            List<Attribut> attributs = new List<Attribut>
            {
                new Attribut("outlook", new List<string> { "sunny", "overcast", "rainy" }, false),
                new Attribut("temperature", new List<string> { "hot", "mild", "cool" }, false),
                new Attribut("humidity", new List<string> { "high", "normal" }, false),
                new Attribut("windy", new List<string> { "weak", "strong" }, false)
            };

            ArbreDecision arbre = new ArbreDecision();
            arbre.ConstruireArbreDecisionID3(tennis, classe, attributs);
            Console.WriteLine("L'arbre de décision entrâiné par les données du fichier tennis.csv est prêt\n");

            DataRow echantillon = tennis.NewRow();
            echantillon["outlook"] = "overcast";
            echantillon["temperature"] = "hot";
            echantillon["humidity"] = "normal";
            echantillon["windy"] = "strong";

            Console.WriteLine("Test de l'échantillon [ 'outlook': 'overcast', 'temperature': 'hot', 'humidity': 'normal', 'windy': 'strong' ]");
            TesterUnEchantillon(arbre, echantillon);

            echantillon = tennis.NewRow();
            echantillon["outlook"] = "rainy";
            echantillon["temperature"] = "mild";
            echantillon["humidity"] = "high";
            echantillon["windy"] = "strong";

            Console.WriteLine("Test de l'échantillon [ 'outlook': 'rainy', 'temperature': 'mild', 'humidity': 'high', 'windy': 'strong' ]");
            TesterUnEchantillon(arbre, echantillon);
        }

        public static void TesterUnEchantillon(ArbreDecision arbre, DataRow echantillon)
        {
            string decision = arbre.TesterUnEchantillon(echantillon);
            Console.WriteLine(decision + "\n");
        }

        private static void ArbreAnimaux()
        {
            Console.WriteLine("Lecture du fichier zoo.csv en cours...");
            DataTable zoo = Csv.LireCsv("zoo.csv");
            Console.WriteLine("Lecture du fichier zoo.csv complétée\n");

            Console.WriteLine("Lecture du fichier class.csv en cours...");
            DataTable classAnimaux = Csv.LireCsv("class.csv");
            Console.WriteLine("Lecture du fichier class.csv complétée\n");
        }
    }
}
