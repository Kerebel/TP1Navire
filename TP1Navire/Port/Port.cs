using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace TP1Navire
{
    class Port
    {

        private string nom;
        private int nbNaviresMax = 5;
        private List<Navire> navires = new List<Navire>();

        public Port(string nom)
        {
            this.nom = nom;
        }
        public void EnregistrerArrivee(Navire navire)
        {
            if (this.navires.Count < nbNaviresMax)
            {
                this.navires.Add(navire);
            }
            else
            {
                throw new Exception("Ajout Impossible, le port est complet");
            }
        }
        public void EnregistrerDepart(string imo)
        {
            int index = RecupPosition(imo);
            if (index >=0)
            {
                this.navires.RemoveAt(index);
            }
            else
            {
                throw new Exception("Impossible d'enregistrer le navire" + imo + " , il n'est pas dans le port");
            }
        }
        public void TesterRecupPosition()
        {
            this.EnregistrerArrivee(new Navire("IMO9427639", "Copper Spirit", "Hydrocarbures", 156827));
            this.EnregistrerArrivee(new Navire("IMO9839272", "MSC Isabella", "Porte-conteneurs", 197500));
            this.EnregistrerArrivee(new Navire("IMO8715871", "MSC PILAR"));
            String imo = "IMO9427639";
            Console.WriteLine("Indice du navire" + imo + " dans la collection ; " + this.RecupPosition(imo));
            imo = "IMO8715871";
            Console.WriteLine("Indice du navire " + imo + " dans la collection : " + this.RecupPosition(imo));
            imo = "IMO1111111";
            Console.WriteLine("Indice du navire " + imo + " dans la collection : " + this.RecupPosition(imo)); ;
        }
        public void TesterRecupPositionV2()
        {
            Navire navire = new Navire("IMO9427639", "Copper Spirit", "Hydrocarbures", 156827);
            this.EnregistrerArrivee(navire);
            this.EnregistrerArrivee(new Navire("IMO9839272", "MSC Isabella", "Porte-conteneurs", 197500));
            this.EnregistrerArrivee(new Navire("IMO8715871", "MSC PILAR"));
            Console.WriteLine("Indice du navire " + navire.Imo + " dans la collection : " + this.RecupPosition(navire));
            Navire unAutreNavire = new Navire("IMO8715871", "MSC PILAR");
            Console.WriteLine("Indice du navire " + unAutreNavire.Imo + " dans la collection : " + this.RecupPosition(unAutreNavire));
            unAutreNavire = new Navire("IMO8715871", "MSC PILAR", "Porte-conteneurs", 52181);
            Console.WriteLine("Indice du navire " + unAutreNavire.Imo + " dans la collection : " + this.RecupPosition(unAutreNavire));
        }
        public bool EstPresent(string imo)
        {
            return RecupPosition(imo) >= 0;
        }
        private int RecupPosition(string imo)
        {
            int i = 0;
            while (i < this.navires.Count && this.navires[i].Imo != imo)
            {
                i++;
            }
            if ( i < this.navires.Count) 
            {
                return i;
            }
            else
            {
                return -1;
            }
        }
        private int RecupPosition(Navire navire)
        {
            return RecupPosition(navire.Imo);
        }
        public string Nom { get => nom; }
        public int NbNaviresMax { get => nbNaviresMax; set => nbNaviresMax = value; }
    }
}
