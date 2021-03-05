﻿using System.Collections.Generic;
using System.Data;
using TP2_Les_arbres_de_decision.Arbre;

namespace TP2_Tests
{
    public class CreationDataTable
    {
        public static DataTable CreerDataTablePourTests()
        {
            DataTable data = new DataTable();

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

            return data;
        }

        public static List<Attribut> GenererListAttributsPourTableDeTest()
        {
            return new List<Attribut>
            {
                new Attribut("outlook", new List<string> { "sunny", "overcast", "rainy" }, false),
                new Attribut("temperature", new List<string> { "hot", "mild", "cool" }, false),
                new Attribut("humidity", new List<string> { "high", "normal" }, false),
                new Attribut("windy", new List<string> { "weak", "strong" }, false)
            };
        }

        public static Attribut GenererClassePourTableDeTest()
        {
            return new Attribut("play", new List<string> { "yes", "no" }, true);
        }

        public static DataTable CreerDataTablePourTestsAvecMemesClasses()
        {
            DataTable data = new DataTable();

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

            return data;
        }
    }
}