using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using TP2_Les_arbres_de_decision.Arbre;

namespace TP2_Tests
{
    [TestClass]
    public class ArbreDecisionTests
    {
        ArbreDecision arbre;
        Noeud racine;
        DataTable data;
        List<Attribut> attributes;

        [TestInitialize]
        public void TestInitialize()
        {
            CreerDataTablePourTests();
            attributes = new List<Attribut> 
            {
                new Attribut("play", new List<string> { "yes", "no" }, true),
                new Attribut("outlook", new List<string> { "sunny", "overcast", "rainy" }, false),
                new Attribut("temperature", new List<string> { "hot", "mild", "cool" }, false)
            };

            arbre = new ArbreDecision();
        }

        [TestMethod]
        public void ComportementAvecMemeEnsembleClasse()
        {
            CreerDataTablePourTestsAvecMemesClasses();
            arbre.ConstruireArbreDecisionID3(data, attributes[0], attributes);

            Assert.AreEqual(0, arbre.Racine.Successeurs.Count);
        }

        private void CreerDataTablePourTests()
        {
            data = new DataTable();

            DataColumn play = new DataColumn("play", typeof(string));
            DataColumn outlook = new DataColumn("outlook", typeof(string));
            DataColumn temperature = new DataColumn("temperature", typeof(string));

            data.Columns.Add(play);
            data.Columns.Add(outlook);
            data.Columns.Add(temperature);

            data.Rows.Add("no", "sunny", "hot");
            data.Rows.Add("no", "sunny", "hot");
            data.Rows.Add("yes", "overcast", "hot");
            data.Rows.Add("yes", "rainy", "mild");
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
