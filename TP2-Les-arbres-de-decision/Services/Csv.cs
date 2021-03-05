using CsvHelper;
using System.Data;
using System.Globalization;
using System.IO;

namespace TP2_Les_arbres_de_decision.Services
{
    public class Csv
    {
        public static DataTable LireCsv(string nomFichier)
        {
            DataTable data = new DataTable();

            if (File.Exists(nomFichier))
            {
                using (StreamReader reader = new StreamReader("tennis.csv"))
                {
                    using (CsvReader csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        using (CsvDataReader dataReader = new CsvDataReader(csv))
                        {
                            data = new DataTable();
                            data.Load(dataReader);
                        }
                    }
                }
            }

            return data;
        }
    }
}
