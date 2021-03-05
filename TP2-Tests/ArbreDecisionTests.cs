using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using TP2_Les_arbres_de_decision.Arbre;
using TP2_Les_arbres_de_decision.Services;

namespace TP2_Tests
{
    [TestClass]
    public class ArbreDecisionTests
    {
        ArbreDecision arbre;
        DataTable data;
        List<Attribut> attributs;
        Attribut classe;

        [TestInitialize]
        public void TestInitialize()
        {
            classe = new Attribut("play", new List<string> { "yes", "no" }, true);
            attributs = new List<Attribut>
            {
                new Attribut("outlook", new List<string> { "sunny", "overcast", "rainy" }, false),
                new Attribut("temperature", new List<string> { "hot", "mild", "cool" }, false),
                new Attribut("humidity", new List<string> { "high", "normal" }, false),
                new Attribut("windy", new List<string> { "weak", "strong" }, false)
            };

            arbre = new ArbreDecision();
        }

        [TestMethod]
        public void ComportementAvecMemeEnsembleClasse()
        {
            CreerDataTablePourTestsAvecMemesClasses();
            arbre.ConstruireArbreDecisionID3(data, classe, attributs);

            Assert.AreEqual(0, arbre.Racine.Branches.Count);
        }

        [TestMethod]
        public void CalculAttributLePlusSignificatif()
        {
            CreerDataTablePourTests();

            Attribut attribut;
            Gains service = new Gains(data, classe);

            attribut = arbre.CalculerAttributLePlusSignificatif(service, attributs);

            Assert.AreEqual("outlook", attribut.Titre);
        }

        [TestMethod]
        public void GenererSousEnsembleAvecValeurAttribut()
        {
            CreerDataTablePourTests();
            DataTable sousEnsemble;

            sousEnsemble = arbre.ContientUneValeurSpecifiquePourAttribut("sunny", attributs[0], data);

            Assert.AreEqual(5, sousEnsemble.Rows.Count);
        }

        private void CreerDataTablePourTests()
        {
            data = new DataTable();

            DataColumn play = new DataColumn("play", typeof(string));
            DataColumn outlook = new DataColumn("outlook", typeof(string));
            DataColumn temperature = new DataColumn("temperature", typeof(string));
            DataColumn humidity = new DataColumn("humidity", typeof(string));
            DataColumn windy = new DataColumn("windy", typeof(string));

            data.Columns.Add(play);
            data.Columns.Add(outlook);
            data.Columns.Add(temperature);
            data.Columns.Add(humidity);
            data.Columns.Add(windy);

            data.Rows.Add("no", "sunny", "hot", "high", "weak");
            data.Rows.Add("no", "sunny", "hot", "high", "strong");
            data.Rows.Add("yes", "overcast", "hot", "high", "weak");
            data.Rows.Add("yes", "rainy", "mild", "high", "weak");
            data.Rows.Add("yes", "rainy", "cool", "normal", "weak");
            data.Rows.Add("no", "rainy", "cool", "normal", "strong");
            data.Rows.Add("yes", "overcast", "cool", "normal", "strong");
            data.Rows.Add("no", "sunny", "mild", "high", "weak");
            data.Rows.Add("yes", "sunny", "cool", "normal", "weak");
            data.Rows.Add("yes", "rainy", "mild", "normal", "weak");
            data.Rows.Add("yes", "sunny", "mild", "normal", "strong");
            data.Rows.Add("yes", "overcast", "mild", "high", "strong");
            data.Rows.Add("yes", "overcast", "hot", "normal", "weak");
            data.Rows.Add("no", "rainy", "mild", "high", "strong");
        }

        private void CreerDataTablePourTestsAvecMemesClasses()
        {
            data = new DataTable();

            DataColumn play = new DataColumn("play", typeof(string));
            DataColumn outlook = new DataColumn("outlook", typeof(string));
            DataColumn temperature = new DataColumn("temperature", typeof(string));

            data.Columns.Add(play);
            data.Columns.Add(outlook);
            data.Columns.Add(temperature);

            data.Rows.Add("yes", "sunny", "hot");
            data.Rows.Add("yes", "sunny", "hot");
            data.Rows.Add("yes", "overcast", "hot");
            data.Rows.Add("yes", "rainy", "mild");
        }
    }
}
