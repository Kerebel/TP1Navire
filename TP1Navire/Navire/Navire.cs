using System;
using System.Collections.Generic;
using System.Text;

namespace GestionNavire.Classesmetier
{
    class Navire
    {
        private string imo;
        private string nom;
        private string libelleFret;
        private int qteFretMaxi;

        public Navire(string imo, string nom, string libelleFret, int qteFretMaxi)
        {
            this.imo = imo;
            this.nom = nom;
            this.libelleFret = libelleFret;
            this.QteFretMaxi = qteFretMaxi;
        }

        public Navire(string imo, string nom) : this(imo, nom, libelleFret: "Indéfini", 0) { }
        public override string ToString()
        {
            return "Identification : " + this.imo + " \n Nom : " + this.nom + "\n Type de frêt : " + this.libelleFret + " \n Quantité de Frêt : " + this.qteFretMaxi;
        }
        public int QteFretMaxi
        {
            get => qteFretMaxi;
            set
            {
                if (value >=0)
                {
                    this.qteFretMaxi = value;
                }
                else
                {
                    throw new Exception("Erreur, quantité de fret non valide");
                }
            }
        }

        public string Imo { get => imo; set => imo = value; }
        public string Nom { get => nom; set => nom = value; }
        public string LibelleFret { get => libelleFret; set => libelleFret = value; }

    }
}
