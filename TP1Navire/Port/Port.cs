using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace GestionNavire.Classesmetier
{
    class Port
    {

        private string nom;
        private int nbNaviresMax = 5;
        private Dictionary<string, Navire> navires = new Dictionary<string, Navire>();

        public Port(string nom)
        {
            this.nom = nom;
        }
        public void EnregistrerArrivee(Navire navire)
        {
            if (this.navires.Count < nbNaviresMax)
            {
                this.navires.Add(navire.Imo, navire);
            }
            else
            {
                throw new Exception("Ajout Impossible, le port est complet");
            }
        }
        public void EnregistrerDepart(string imo)
        {
            if (EstPresent(imo))
            {
                this.navires.Remove(imo);
            }
            else
            {
                throw new Exception("Impossible d'enregistrer le navire" + imo + " , il n'est pas dans le port");
            }
        }
        public bool EstPresent(string imo)
        {
            return navires.ContainsKey(imo);
        }
        public string Nom { get => nom; }
        public int NbNaviresMax { get => nbNaviresMax; set => nbNaviresMax = value; }
        internal Dictionary<string, Navire> Navires { get => navires; }
    }
}
