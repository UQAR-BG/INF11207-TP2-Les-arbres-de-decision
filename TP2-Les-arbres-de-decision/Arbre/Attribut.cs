using System.Collections.Generic;

namespace TP2_Les_arbres_de_decision.Arbre
{
    public class Attribut
    {
        public string Titre { get; }
        public List<string> Ensembles { get; }
        public bool EstClasse { get; }

        public Attribut(string titre, List<string> ensembles, bool estClasse)
        {
            Titre = titre;
            Ensembles = ensembles;
            EstClasse = estClasse;
        }
    }
}
