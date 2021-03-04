using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using TP2_Les_arbres_de_decision.Arbre;
using TP2_Les_arbres_de_decision.Services;

namespace TP2_Tests
{
    [TestClass]
    public class GainsTests
    {
        DataTable data;
        Gains gains;
        Attribut classe;
        Attribut outlook;
        Attribut temperature;
        Attribut humidity;
        Attribut windy;

        [TestInitialize]
        public void TestInitialize()
        {
            CreerDataTablePourTests("tennis.csv");
            classe = new Attribut("play", new List<string>
            {
                "yes",
                "no"
            }, true);
            outlook = new Attribut("outlook", new List<string>
            {
                "sunny",
                "overcast",
                "rainy"
            }, false);
            temperature = new Attribut("temperature", new List<string>
            {
                "hot",
                "mild",
                "cool"
            }, false);
            humidity = new Attribut("humidity", new List<string>
            {
                "high",
                "normal"
            }, false);
            windy = new Attribut("windy", new List<string>
            {
                "weak",
                "strong"
            }, false);

            gains = new Gains(data, classe);
        }

        [TestMethod]
        public void CalculDuGainInformationsTemps()
        {
            double gainsInfo;

            gainsInfo = gains.GainsInformation(outlook);
            gainsInfo = Math.Round(gainsInfo, 3);

            Assert.AreEqual(0.247, gainsInfo);
        }

        [TestMethod]
        public void CalculDuGainInformationsTemperature()
        {
            double gainsInfo;

            gainsInfo = gains.GainsInformation(temperature);
            gainsInfo = Math.Round(gainsInfo, 3);

            Assert.AreEqual(0.029, gainsInfo);
        }

        [TestMethod]
        public void CalculDuGainInformationsHumidite()
        {
            double gainsInfo;

            gainsInfo = gains.GainsInformation(humidity);
            gainsInfo = Math.Round(gainsInfo, 3);

            Assert.AreEqual(0.152, gainsInfo);
        }

        [TestMethod]
        public void CalculDuGainInformationsVent()
        {
            double gainsInfo;

            gainsInfo = gains.GainsInformation(windy);
            gainsInfo = Math.Round(gainsInfo, 3);

            Assert.AreEqual(0.048, gainsInfo);
        }

        [TestMethod]
        public void ComportementBasiqueCalculEntropie()
        {
            double entropie;

            entropie = gains.Entropie();
            entropie = Math.Round(entropie, 2);

            Assert.AreEqual(0.94, entropie);
        }

        [TestMethod]
        public void CalculDeEntropiePourUnEnsoleille()
        {
            double entropie;

            entropie = gains.Entropie(outlook, outlook.Ensembles[0]);
            entropie = Math.Round(entropie, 3);

            Assert.AreEqual(0.971, entropie);
        }

        [TestMethod]
        public void CalculDeEntropiePourUnNuageux()
        {
            double entropie;

            entropie = gains.Entropie(outlook, outlook.Ensembles[1]);
            entropie = Math.Round(entropie, 3);

            Assert.AreEqual(0.0, entropie);
        }

        [TestMethod]
        public void CalculDeEntropiePourUnPluvieux()
        {
            double entropie;

            entropie = gains.Entropie(outlook, outlook.Ensembles[2]);
            entropie = Math.Round(entropie, 3);

            Assert.AreEqual(0.971, entropie);
        }

        [TestMethod]
        public void CalculDeProbabiliteDansLaClassePourOui()
        {
            double probabilite;
            Recherche conditions = new Recherche(classe, "yes");

            probabilite = gains.Probabilite(conditions);

            Assert.AreEqual(9.0 / 14, probabilite);
        }

        [TestMethod]
        public void CalculDeProbabiliteDansLaClassePourNon()
        {
            double probabilite;
            Recherche conditions = new Recherche(classe, "no");

            probabilite = gains.Probabilite(conditions);

            Assert.AreEqual(5.0 / 14, probabilite);
        }

        private void CreerDataTablePourTests(string nomDeFichier)
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
    }
}
