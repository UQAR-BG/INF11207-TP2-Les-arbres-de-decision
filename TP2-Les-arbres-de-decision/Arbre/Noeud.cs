using System.Collections.Generic;

namespace TP2_Les_arbres_de_decision.Arbre
{
    public class Noeud
    {
        public List<Branche> Branches { get; private set; }
        public string Valeur { get; set; }

        public Noeud()
        {
            Branches = new List<Branche>();
        }

        public void CreerBranches(List<string> ensembles)
        {
            Branches = new List<Branche>();
            foreach (string ensemble in ensembles)
            {
                Branches.Add(new Branche(ensemble));
            }
        }

        public bool AjouterNoeudAuBoutDeBranche(Noeud nouveauNoeud, int index)
        {
            if (index >= 0)
            {
                Branches[index].Successeur = nouveauNoeud;
                return true;
            }

            return false;
        }

        public bool RetirerBranche(int index)
        {
            if (IndexExiste(index))
            {
                Branches.RemoveAt(index);
                return true;
            }

            return false;
        }

        private bool IndexExiste(int index)
        {
            return index > 0 && index < Branches.Count;
        }
    }
}
