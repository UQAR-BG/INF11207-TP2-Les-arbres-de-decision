using CsvHelper;
using System;
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
                using (StreamReader reader = new StreamReader(nomFichier))
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
            else
            {
                Console.WriteLine($"Le fichier {nomFichier} n'existe pas.");
            }

            return data;
        }

        public static bool EcrireCsv(DataTable data, string nomFichier)
        {
            int nbreColonnes = data.Columns.Count;

            try
            {
                using (StreamWriter writer = new StreamWriter(nomFichier, false))
                {
                
                    for (int i = 0; i < nbreColonnes; i++)
                    {
                        writer.Write(data.Columns[i]);
                        if (NecessiteUneVirgule(i, nbreColonnes - 1))
                            writer.Write(",");
                    }
                    writer.Write(writer.NewLine);

                    foreach (DataRow ligne in data.Rows)
                    {
                        for (int i = 0; i < nbreColonnes; i++)
                        {
                            writer.Write(ligne[i].ToString());
                            if (NecessiteUneVirgule(i, nbreColonnes - 1))
                                writer.Write(",");
                        }
                        writer.Write(writer.NewLine);
                    }
                
                }
            }
            catch (IOException)
            {
                Console.WriteLine($"Le fichier {nomFichier} est déjà ouvert.");
                return false;
            }

            return true;
        }

        public static DataTable FusionnerTables(DataTable zooTable, DataTable classTable)
        {
            string numeroClasse;
            string nomClasse;

            zooTable.Columns["Class_Type"].ReadOnly = false;

            foreach (DataRow ligne in zooTable.Rows)
            {
                numeroClasse = ligne["class_type"].ToString();

                DataRow classe = classTable.Select($"Class_Number = '{numeroClasse}'")[0];
                nomClasse = classe["Class_Type"].ToString();

                ligne.BeginEdit();
                ligne["class_type"] = nomClasse;
                ligne.EndEdit();
            }

            return zooTable;
        }

        private static bool NecessiteUneVirgule(int position, int dernierePosition)
        {
            return position < dernierePosition;
        }
    }
}
