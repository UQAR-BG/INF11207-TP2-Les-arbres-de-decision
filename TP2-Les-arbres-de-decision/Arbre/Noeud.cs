using System;
using System.Collections.Generic;
using System.Text;

namespace TP2_Les_arbres_de_decision.Arbre
{
    public class Noeud
    {
        public List<Noeud> Successeurs { get; }
        public string Valeur { get; set; }

        public Noeud()
        {
            Successeurs = new List<Noeud>();
        }

        /*public bool AjouterParent(Noeud parent)
        {
            if (parent.Valeur == Valeur)
            {
                return false;
            }

            if (parent.Valeur < Valeur)
            {
                ParentGauche = parent;
            }
            else
            {
                ParentDroite = parent;
            }

            return true;
        }*/
    }
}
