using System.Collections.Generic;
using System.Data;
using TP2_Les_arbres_de_decision.Arbre;
using TP2_Les_arbres_de_decision.Extensions;

namespace TP2_Les_arbres_de_decision.Services.GainsInformation
{
    public class DataStorage
    {
        public static DataTable Table { get; private set; }
        public static List<Attribut> Attributs { get; private set; }
        public static Attribut Classe { get; private set; }
        public static int NbreLignes { get; private set; }

        public static bool StockerDonnees(DataTable table, List<Attribut> attributs, Attribut classe)
        {
            if (AttributFaitParteDeLaTable(classe, table) && AttributsFontPartieDeLaTable(attributs, table))
            {
                Table = table;
                Attributs = attributs;
                Classe = classe;
                NbreLignes = table.Rows.Count;

                return true;
            }

            return false;
        }

        public static bool StockerDonnees(DataTable table)
        {
            if (Classe != null && AttributFaitParteDeLaTable(Classe, table))
            {
                Table = table;
                NbreLignes = table.Rows.Count;

                return true;
            }

            return false;
        }

        private static bool AttributFaitParteDeLaTable(Attribut classe, DataTable table)
        {
            return DataTableExt.AttributExiste(table, classe.Titre);
        }

        private static bool AttributsFontPartieDeLaTable(List<Attribut> attributs, DataTable table)
        {
            bool attributsFontPartie = true;

            foreach (Attribut attr in attributs)
            {
                if (!AttributFaitParteDeLaTable(attr, table))
                    attributsFontPartie = false;
            }

            return attributsFontPartie;
        }
    }
}
