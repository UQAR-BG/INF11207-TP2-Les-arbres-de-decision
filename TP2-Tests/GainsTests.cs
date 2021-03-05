using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using TP2_Les_arbres_de_decision.Arbre;
using TP2_Les_arbres_de_decision.Services;
using TP2_Les_arbres_de_decision.Services.GainsInformation;

namespace TP2_Tests
{
    [TestClass]
    public class GainsTests
    {
        DataTable data;
        Attribut classe;
        List<Attribut> attributs;

        [TestInitialize]
        public void TestInitialize()
        {
            data = CreationDataTable.CreerDataTablePourTests();
            classe = CreationDataTable.GenererClassePourTableDeTest();
            attributs = CreationDataTable.GenererListAttributsPourTableDeTest();

            DataStorage.StockerDonnees(data, attributs, classe);
        }

        [TestMethod]
        public void CalculDuGainInformationsTemps()
        {
            double gainsInfo;

            gainsInfo = Gains.CalculerGainsInformation(attributs[0]);
            gainsInfo = Math.Round(gainsInfo, 3);

            Assert.AreEqual(0.247, gainsInfo);
        }

        [TestMethod]
        public void CalculDuGainInformationsTemperature()
        {
            double gainsInfo;

            gainsInfo = Gains.CalculerGainsInformation(attributs[1]);
            gainsInfo = Math.Round(gainsInfo, 3);

            Assert.AreEqual(0.029, gainsInfo);
        }

        [TestMethod]
        public void CalculDuGainInformationsHumidite()
        {
            double gainsInfo;

            gainsInfo = Gains.CalculerGainsInformation(attributs[2]);
            gainsInfo = Math.Round(gainsInfo, 3);

            Assert.AreEqual(0.152, gainsInfo);
        }

        [TestMethod]
        public void CalculDuGainInformationsVent()
        {
            double gainsInfo;

            gainsInfo = Gains.CalculerGainsInformation(attributs[3]);
            gainsInfo = Math.Round(gainsInfo, 3);

            Assert.AreEqual(0.048, gainsInfo);
        }

        [TestMethod]
        public void ComportementBasiqueCalculEntropie()
        {
            double entropie;

            entropie = Entropie.CalculerEntropie();
            entropie = Math.Round(entropie, 2);

            Assert.AreEqual(0.94, entropie);
        }

        [TestMethod]
        public void CalculDeEntropiePourUnEnsoleille()
        {
            double entropie;

            entropie = Entropie.CalculerEntropie(attributs[0], attributs[0].Ensembles[0]);
            entropie = Math.Round(entropie, 3);

            Assert.AreEqual(0.971, entropie);
        }

        [TestMethod]
        public void CalculDeEntropiePourUnNuageux()
        {
            double entropie;

            entropie = Entropie.CalculerEntropie(attributs[0], attributs[0].Ensembles[1]);
            entropie = Math.Round(entropie, 3);

            Assert.AreEqual(0.0, entropie);
        }

        [TestMethod]
        public void CalculDeEntropiePourUnPluvieux()
        {
            double entropie;

            entropie = Entropie.CalculerEntropie(attributs[0], attributs[0].Ensembles[2]);
            entropie = Math.Round(entropie, 3);

            Assert.AreEqual(0.971, entropie);
        }

        [TestMethod]
        public void CalculDeProbabiliteDansLaClassePourOui()
        {
            double probabilite;
            Recherche conditions = new Recherche(classe, "yes");

            probabilite = Probabilites.CalculerProbabilite(conditions);

            Assert.AreEqual(9.0 / 14, probabilite);
        }

        [TestMethod]
        public void CalculDeProbabiliteDansLaClassePourNon()
        {
            double probabilite;
            Recherche conditions = new Recherche(classe, "no");

            probabilite = Probabilites.CalculerProbabilite(conditions);

            Assert.AreEqual(5.0 / 14, probabilite);
        }
    }
}
