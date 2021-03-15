using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

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
            if (IsMatch(imo))
            {
                this.imo = imo;
            }
            else
            {
                throw new Exception("Erreur : IMO non valide");
            }
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
        public bool IsMatch(string regex)
        {
            string protoImo = @"\b^IMO[0-9]{7}\b";
            if( Regex.IsMatch(protoImo, regex))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public string Imo { get => imo; set => imo = value; }
        public string Nom { get => nom; set => nom = value; }
        public string LibelleFret { get => libelleFret; set => libelleFret = value; }

    }
}
