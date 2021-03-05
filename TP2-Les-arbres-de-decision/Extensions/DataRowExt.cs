using System.Data;

namespace TP2_Les_arbres_de_decision.Extensions
{
    public class DataRowExt
    {
        public static bool AttributExiste(DataRow enregistrement, string nomAttribut)
        {
            return enregistrement.Table.Columns.Contains(nomAttribut);
        }
    }
}
