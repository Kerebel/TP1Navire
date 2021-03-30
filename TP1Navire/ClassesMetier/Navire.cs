using GestionNavire.Exceptions;
using System;
using System.Text.RegularExpressions;

namespace NavireHeritage.ClassesMetier
{
    abstract class Navire
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
            if (tonnageDWT > 0)
            {
                this.tonnageDWT = tonnageDWT;
            }
            else
            {
                throw new GestionPortException("Impossible de créer une capacité maximale de chargement avec une capacité négative");
            }
            
            
        }
        public override string ToString()
        {
            return "Identification : {0} \n Nom : {1} \n Latitude : {2} " + this.libelleFret + " \n Longitude : {3}",  this.Imo, this.nom, this.qteFretMaxi;
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
        public int TonnageActuel { 
            get => tonnageActuel; 
            protected set
            {
                if (value < 0)
                {
                    throw new GestionPortException("le tonnage actuel ne peut être négatif");
                }
                else if (this.tonnageDWT >= value)
                {
                    // On peut stocker
                    this.tonnageActuel = value;
                }
                else
                {
                    throw new GestionPortException("Impossible d'avoir un tonnage actuel plus grand que le tonnage DWT ");
                }
            }
        }
        public int TonnageGT { get => tonnageGT; }
        public int TonnageDWT { get => tonnageDWT; }
    }
}
