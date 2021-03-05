using System.Collections.Generic;
using System.Data;
using TP2_Les_arbres_de_decision.Services;
using TP2_Les_arbres_de_decision.Extensions;
using TP2_Les_arbres_de_decision.Services.GainsInformation;

namespace TP2_Les_arbres_de_decision.Arbre
{
    public class ArbreDecision
    {
        public Noeud Racine { get; private set; }

        public void ConstruireArbreDecisionID3(DataTable data, Attribut classe, List<Attribut> attributs)
        {
            DataStorage.StockerDonnees(data, attributs, classe);

            Racine = NouveauNoeud(data, classe, attributs);
        }

        public Attribut CalculerAttributLePlusSignificatif(List<Attribut> attributs)
        {
            int indexAttributSignificatif = 0;
            double gainPrecedent = 0;
            double gain;

            for (int i = 0; i < attributs.Count; i++)
            {
                gain = Gains.CalculerGainsInformation(attributs[i]);

                if (gain > gainPrecedent)
                {
                    gainPrecedent = gain;
                    indexAttributSignificatif = i;
                }
            }

            return attributs[indexAttributSignificatif];
        }

        public DataTable ContientUneValeurSpecifiquePourAttribut(string valeur, Attribut cible, DataTable data)
        {
            DataTable sousEnsemble = new DataTable();
            DataRow[] lignes = data.Select($"{cible.Titre} = '{valeur}'");

            if (lignes.Length > 0)
            {
                sousEnsemble = lignes.CopyToDataTable();
            }

            return sousEnsemble;
        }

        public string TesterUnEchantillon(DataRow echantillon)
        {
            if (echantillon.ItemArray.Length == 0)
            {
                return "Malheureusement, l'échantillon est vide";
            }

            if (Racine != null)
            {
                return CheminATraversArbre(Racine, echantillon);
            }

            return "";
        }

        private Noeud NouveauNoeud(DataTable data, Attribut classe, List<Attribut> attributs)
        {
            Noeud noeud = new Noeud();
            DataStorage.StockerDonnees(data);

            if (TousLesEnregistrementOntMemeClasse(classe))
            {
                string ensemble = EnsembleDeClasseLePlusPresent(classe);
                return CreerRacineSeule(ensemble);
            }

            if (attributs.Count == 0)
            {
                string ensemble = EnsembleDeClasseLePlusPresent(classe);
                return CreerRacineSeule(ensemble);
            }

            Attribut attributLePlusSignificatif = CalculerAttributLePlusSignificatif(attributs);
            noeud.Valeur = attributLePlusSignificatif.Titre;
            noeud.CreerBranches(attributLePlusSignificatif.Ensembles);

            // Pour chaque branche du noeud
            for (int i = 0; i < attributLePlusSignificatif.Ensembles.Count; i++)
            {
                DataTable sousEnsemble = ContientUneValeurSpecifiquePourAttribut(attributLePlusSignificatif.Ensembles[i], attributLePlusSignificatif, data);

                if (sousEnsemble.Rows.Count > 0)
                {
                    attributs.Remove(attributLePlusSignificatif);
                    noeud.AjouterNoeudAuBoutDeBranche(NouveauNoeud(sousEnsemble, classe, attributs));
                }
                else
                {
                    noeud.RetirerBranche(attributLePlusSignificatif.Ensembles[i]);
                }
            }

            return noeud;
        }

        private bool TousLesEnregistrementOntMemeClasse(Attribut classe)
        {
            bool tousOntMemeClasse = false;

            Recherche conditions = new Recherche(classe);
            conditions.Valeur = EnsembleDeClasseLePlusPresent(classe);

            if (Probabilites.CalculerProbabilite(conditions) == 1)
            {
                tousOntMemeClasse = true;
            }

            return tousOntMemeClasse;
        }

        private Noeud CreerRacineSeule(string nomRacine)
        {
            Noeud noeud = new Noeud();
            noeud.Valeur = nomRacine;
            return noeud;
        }

        private string EnsembleDeClasseLePlusPresent(Attribut classe)
        {
            int nombreLignesPrecedent = 0;
            int nombreLignes;
            string ensembleLePlusPresent = classe.Ensembles[0];
            Recherche conditions = new Recherche(classe);

            foreach (string ensemble in classe.Ensembles)
            {
                conditions.ValeurClasse = ensemble;
                nombreLignes = NombreLignes.CompterNombreLignes(conditions);
                if (nombreLignes > nombreLignesPrecedent)
                {
                    nombreLignesPrecedent = nombreLignes;
                    ensembleLePlusPresent = ensemble;
                }
            }

            return ensembleLePlusPresent;
        }

        private string CheminATraversArbre(Noeud position, DataRow echantillon)
        {
            if (position.Branches.Count == 0)
            {
                return $"Décision prise : {position.Valeur}";
            }
            else
            {
                if (!DataTableExt.AttributExiste(echantillon, position.Valeur))
                {
                    return $"Erreur : Il manque l'attribut {position.Valeur} dans l'échantillon ";
                }

                string valeur = echantillon[position.Valeur].ToString();
                int indexBranche = position.IndexOf(valeur);
                if (indexBranche >= 0)
                {
                    return CheminATraversArbre(position.Branches[indexBranche].Successeur, echantillon);
                }
                else
                {
                    return $"Erreur : La valeur {valeur} dans l'attribut {position.Valeur} est inconnue ";
                }
            }
        }
    }
}
