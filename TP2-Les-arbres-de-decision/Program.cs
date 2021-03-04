using CsvHelper;
using System;
using System.Data;
using System.Globalization;
using System.IO;

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
                    }
                }
            }
        }
    }
}
