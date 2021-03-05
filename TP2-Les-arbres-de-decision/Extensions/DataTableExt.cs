﻿using System.Data;

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
    }
}