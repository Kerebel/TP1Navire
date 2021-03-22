using GestionNavire.Exceptions;
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
        private int qteFret;

        public Navire(string imo, string nom, string libelleFret, int qteFretMaxi, int qteFret)
        {
            if (IsMatch(imo))
            {
                this.imo = imo;
            }
            else
            {
                throw new GestionPortException("Erreur : IMO non valide");
            }
            this.nom = nom;
            this.libelleFret = libelleFret;
            this.QteFretMaxi = qteFretMaxi;
            if (qteFret >= 0 && qteFret <= qteFretMaxi)
            {
                this.qteFret = qteFret;
            }
            else
            {
                throw new GestionPortException("Valeur incohérente pour la quantité de fret stockée dans le navire");
            }
            
        }

        public Navire(string imo, string nom) : this(imo, nom, libelleFret: "Indéfini", 0, 0) { }
        public override string ToString()
        {
            return "Identification : " + this.imo + " \n Nom : " + this.nom + "\n Type de frêt : " + this.libelleFret + " \n Quantité de Frêt : " + this.qteFretMaxi;
        }
        
        public bool IsMatch(string imo)
        {
            string modeleImo = @"IMO\d{7}$";
            Match match = Regex.Match(imo, modeleImo);
            return match.Success;
        }
        public void Decharger(int quantite)
        {
            if (quantite > 0 && quantite < this.QteFret)
            {
                this.qteFret -= quantite;
            }
            else
            {
                if (quantite > 0)
                {
                    throw new GestionPortException("La quantité à décharger ne peut être négative ou nulle");
                }
                else
                {
                    throw new GestionPortException("Impossible de décharger plus que la quantité de fret dans le navire");
                }
            }
        }
        public bool EstDecharge()
        {
            return qteFret == 0;
        }
        public string Imo { get => imo; }
        public string Nom { get => nom; set => nom = value; }
        public string LibelleFret { get => libelleFret; set => libelleFret = value; }
        public int QteFret { get => qteFret; }
        public int QteFretMaxi
        {
            get => qteFretMaxi;
            set
            {
                if (value >= 0)
                {
                    this.qteFretMaxi = value;
                }
                else
                {
                    throw new Exception("Erreur, quantité de fret non valide");
                }
            }
        }
    }
}
