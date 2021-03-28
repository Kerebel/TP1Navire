using GestionNavire.Exceptions;
using System;
using System.Text.RegularExpressions;

namespace NavireHeritage.ClassesMetier
{
    class Navire
    {
        /// <summary>
        /// Numéro IMO du bateau sous la forme IMO9999999.
        /// </summary>
        protected string imo;
        /// <summary>
        /// Nom du navire
        /// </summary>
        protected string nom;
        /// <summary>
        /// Position mise à jour à intervalles réguliers par un programme externe
        /// </summary>
        protected string latitude;
        /// <summary>
        /// Position mise à jour à intervalles réguliers par un programme externe
        /// </summary>
        protected string longitude;
        /// <summary>
        /// Partie du tonnage du bateau actuellement utilisée. Il est exprimé en tonnaux.
        /// </summary>
        protected int tonnageActuel;
        /// <summary>
        /// Mesure du tonnage du bateau
        /// </summary>
        protected int tonnageGT;
        /// <summary>
        /// Chargement maximal que peut embarquer un navire. Il comprend le personnel, les consommables(nourriture, fluides, ...) et la cargaison.
        /// </summary>
        protected int tonnageDWT;

        public Navire(string imo, string latitude, string longitude, string nom, int tonnageActuel, int tonnageGT, int tonnageDWT)
        {
            if (IsMatch(imo))
            {
                this.imo = imo;
            }
            else
            {
                throw new GestionPortException("Erreur : IMO non valide");
            }
            this.latitude = latitude;
            this.longitude = longitude;
            this.nom = nom;
            this.tonnageActuel = tonnageActuel;
            this.tonnageGT = tonnageGT;
            this.tonnageDWT = tonnageDWT;
            
        }
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
        
        public string Imo { get => imo; }
        public string Latitude { get => latitude; set => latitude = value; }
        public string Longitude { get => longitude; set => longitude = value; }
        public string Nom { get => nom; }
        public int TonnageActuel { get => tonnageActuel; set => tonnageActuel = value; }
        public int TonnageGT { get => tonnageGT; }
        public int TonnageDWT { get => tonnageDWT;  }
    }
}
