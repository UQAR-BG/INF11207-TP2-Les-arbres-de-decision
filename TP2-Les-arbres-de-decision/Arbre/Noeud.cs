using System.Collections.Generic;

namespace TP2_Les_arbres_de_decision.Arbre
{
    public class Noeud
    {
        private int pointeurProchaineBrancheVide;

        public List<Branche> Branches { get; private set; }
        public string Valeur { get; set; }

        public Noeud()
        {
            Branches = new List<Branche>();
            pointeurProchaineBrancheVide = 0;
        }

        public void CreerBranches(List<string> ensembles)
        {
            Branches = new List<Branche>();
            foreach (string ensemble in ensembles)
            {
                Branches.Add(new Branche(ensemble));
            }
        }

        public bool AjouterNoeudAuBoutDeBranche(Noeud nouveauNoeud)
        {
            if (pointeurProchaineBrancheVide < Branches.Count)
            {
                Branches[pointeurProchaineBrancheVide].Successeur = nouveauNoeud;
                pointeurProchaineBrancheVide++;
                return true;
            }

            return false;
        }

        public bool RetirerBranche(string titre)
        {
            int index = IndexOf(titre);
            if (index >= 0)
            {
                Branches.RemoveAt(index);
                return true;
            }

            return false;
        }

        public int IndexOf(string valeur)
        {
            bool brancheTrouvee = false;
            int indexBranche = -1;
            int compteur = 0;

            while (!brancheTrouvee && compteur < Branches.Count)
            {
                if (Branches[compteur].Titre.Equals(valeur))
                {
                    indexBranche = compteur;
                    brancheTrouvee = true;
                }
                compteur++;
            }

            return indexBranche;
        }
    }
}
