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
            DataTable zooTable = Csv.LireCsv("zoo.csv");
            Console.WriteLine("Lecture du fichier zoo.csv complétée\n");

            Console.WriteLine("Lecture du fichier class.csv en cours...");
            DataTable classTable = Csv.LireCsv("class.csv");
            Console.WriteLine("Lecture du fichier class.csv complétée\n");

            Console.WriteLine("Fusion des fichiers zoo.csv et class.csv en cours...");
            DataTable animauxTable = Csv.FusionnerTables(zooTable, classTable);
            Console.WriteLine("Fusion des fichiers complétée\n");

            if (Csv.EcrireCsv(animauxTable, "animaux.csv"))
            {
                animauxTable = Csv.LireCsv("animaux.csv");

                Attribut classe = new Attribut("class_type", new List<string> { "Mammal", "Bird", "Reptile", "Fish", "Amphibian", "Bug", "Invertebrate" }, true);
                List<Attribut> attributs = new List<Attribut>
                {
                    new Attribut("hair", new List<string> { "0", "1" }, false),
                    new Attribut("feathers", new List<string> { "0", "1" }, false),
                    new Attribut("eggs", new List<string> { "0", "1" }, false),
                    new Attribut("milk", new List<string> { "0", "1" }, false),
                    new Attribut("airborne", new List<string> { "0", "1" }, false),
                    new Attribut("aquatic", new List<string> { "0", "1" }, false),
                    new Attribut("predator", new List<string> { "0", "1" }, false),
                    new Attribut("toothed", new List<string> { "0", "1" }, false),
                    new Attribut("backbone", new List<string> { "0", "1" }, false),
                    new Attribut("breathes", new List<string> { "0", "1" }, false),
                    new Attribut("venomous", new List<string> { "0", "1" }, false),
                    new Attribut("fins", new List<string> { "0", "1" }, false),
                    new Attribut("legs", new List<string> { "0", "2", "4", "5", "6", "8" }, false),
                    new Attribut("tail", new List<string> { "0", "1" }, false),
                    new Attribut("domestic", new List<string> { "0", "1" }, false),
                    new Attribut("catsize", new List<string> { "0", "1" }, false),

                };

                ArbreDecision arbre = new ArbreDecision();
                arbre.ConstruireArbreDecisionID3(animauxTable, classe, attributs);
                Console.WriteLine("L'arbre de décision entrâiné par les données du fichier animaux.csv est prêt\n");

                DataRow echantillon = animauxTable.NewRow();
                echantillon["animal_name"] = "spider";
                echantillon["hair"] = "0";
                echantillon["feathers"] = "0";
                echantillon["eggs"] = "1";
                echantillon["milk"] = "0";
                echantillon["airborne"] = "0";
                echantillon["aquatic"] = "0";
                echantillon["predator"] = "1";
                echantillon["toothed"] = "0";
                echantillon["backbone"] = "0";
                echantillon["breathes"] = "1";
                echantillon["venomous"] = "1";
                echantillon["fins"] = "0";
                echantillon["legs"] = "8";
                echantillon["tail"] = "0";
                echantillon["domestic"] = "0";
                echantillon["catsize"] = "0";

                Console.WriteLine("Test de l'échantillon [ 'animal_name': 'spider', 'eggs': '1', 'predator': '1', 'breathes': '1', 'venomous': '1' ]");
                Console.WriteLine("Tous les autres attributs étant à 0 : ");
                TesterUnEchantillon(arbre, echantillon);

                echantillon = animauxTable.NewRow();
                echantillon["animal_name"] = "labrador";
                echantillon["hair"] = "1";
                echantillon["feathers"] = "0";
                echantillon["eggs"] = "0";
                echantillon["milk"] = "1";
                echantillon["airborne"] = "0";
                echantillon["aquatic"] = "0";
                echantillon["predator"] = "1";
                echantillon["toothed"] = "1";
                echantillon["backbone"] = "1";
                echantillon["breathes"] = "1";
                echantillon["venomous"] = "0";
                echantillon["fins"] = "0";
                echantillon["legs"] = "4";
                echantillon["tail"] = "1";
                echantillon["domestic"] = "1";
                echantillon["catsize"] = "0";

                Console.WriteLine("Test de l'échantillon [ 'animal_name': 'labrador', 'hair': '1', 'predator': '1', 'breathes': '1', 'milk': '1'");
                Console.WriteLine("                        'toothed': '1', 'backbone': '1', 'legs': '4', 'tail': '1', 'domestic': '1' ]");
                Console.WriteLine("Tous les autres attributs étant à 0 : ");
                TesterUnEchantillon(arbre, echantillon);
            }
        }
    }
}
