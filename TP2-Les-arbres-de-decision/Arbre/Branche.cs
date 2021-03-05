namespace TP2_Les_arbres_de_decision.Arbre
{
    public class Branche
    {
        public string Titre { get; }
        public Noeud Successeur { get; set; }

        public Branche(string titre)
        {
            Titre = titre;
        }
    }
}
