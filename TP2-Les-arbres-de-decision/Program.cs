using CsvHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using TP2_Les_arbres_de_decision.Arbre;
using TP2_Les_arbres_de_decision.Services;

namespace TP2_Les_arbres_de_decision
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var reader = new StreamReader("tennis.csv"))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    using (var dr = new CsvDataReader(csv))
                    {
                        var dt = new DataTable();
                        dt.Load(dr);

                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            for (int i = 0; i < dt.Columns.Count; i++)
                            {
                                Console.Write(dt.Columns[i].ColumnName + " ");
                                Console.WriteLine(dt.Rows[j].ItemArray[i]);
                            }
                        }

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

                        Attribut classe = new Attribut("play", new List<string>
                        {
                            "yes",
                            "no"
                        }, true);
                        Attribut attr = new Attribut("outlook", new List<string>
                        {
                            "sunny",
                            "overcast",
                            "rainy"
                        }, false);
                        Gains gains = new Gains(data, classe);

                        double gainsInfo;

                        gainsInfo = gains.GainsInformation(attr);
                        gainsInfo = Math.Round(gainsInfo, 3);
                    }
                }
            }
        }
    }
}
