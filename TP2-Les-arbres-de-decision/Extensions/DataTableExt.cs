using System.Data;
using TP2_Les_arbres_de_decision.Arbre;

namespace TP2_Les_arbres_de_decision.Extensions
{
    public class DataTableExt
    {
        public static bool AttributExiste(DataTable table, string nomAttribut)
        {
            return table.Columns.Contains(nomAttribut);
        }

        public static bool AttributExiste(DataRow enregistrement, string nomAttribut)
        {
            return enregistrement.Table.Columns.Contains(nomAttribut);
        }

        public static DataTable ContientUneValeurSpecifiquePourAttribut(string valeur, Attribut cible, DataTable data)
        {
            DataTable sousEnsemble = new DataTable();
            DataRow[] lignes = data.Select($"{cible.Titre} = '{valeur}'");

            if (lignes.Length > 0)
            {
                sousEnsemble = lignes.CopyToDataTable();
            }

            return sousEnsemble;
        }
    }
}
