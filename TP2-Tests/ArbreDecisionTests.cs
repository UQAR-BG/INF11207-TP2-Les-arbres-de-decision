using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Data;
using TP2_Les_arbres_de_decision.Arbre;
using TP2_Les_arbres_de_decision.Services.GainsInformation;

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
            data = CreationDataTable.CreerDataTablePourTests();
            classe = CreationDataTable.GenererClassePourTableDeTest();
            attributs = CreationDataTable.GenererListAttributsPourTableDeTest();

            DataStorage.StockerDonnees(data, attributs, classe);
            arbre = new ArbreDecision();
        }

        [TestMethod]
        public void ComportementAvecMemeEnsembleClasse()
        {
            data = CreationDataTable.CreerDataTablePourTestsAvecMemesClasses();
            DataStorage.StockerDonnees(data);

            arbre.ConstruireArbreDecisionID3(data, classe, attributs);

            Assert.AreEqual(0, arbre.Racine.Branches.Count);
        }

        [TestMethod]
        public void CalculAttributLePlusSignificatif()
        {
            Attribut attribut;

            attribut = arbre.CalculerAttributLePlusSignificatif(attributs);

            Assert.AreEqual("outlook", attribut.Titre);
        }

        [TestMethod]
        public void GenererSousEnsembleAvecValeurAttribut()
        {
            DataTable sousEnsemble;

            sousEnsemble = arbre.ContientUneValeurSpecifiquePourAttribut("sunny", attributs[0], data);

            Assert.AreEqual(5, sousEnsemble.Rows.Count);
        }
    }
}
