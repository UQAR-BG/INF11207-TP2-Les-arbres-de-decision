using TP2_Les_arbres_de_decision.Arbre;

namespace TP2_Les_arbres_de_decision.Services
{
    public class Recherche
    {
        public Attribut Cible { get; }
        public string Valeur { get; set; }
        public string ValeurClasse { get; set; }
        public bool SurClasseUniquement { get; }

        public Recherche(Attribut cible, string valeur, string valeurClasse)
        {
            Cible = cible;
            Valeur = valeur;
            ValeurClasse = valeurClasse;
            SurClasseUniquement = cible.EstClasse;
        }

        public Recherche(Attribut classe, string valeurClasse) : this(classe, valeurClasse, valeurClasse) { }

        public Recherche(Attribut classe) : this(classe, "") { }
    }
}
