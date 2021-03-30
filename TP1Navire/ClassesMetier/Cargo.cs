using GestionNavire.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using Station.Interfaces;

namespace NavireHeritage.ClassesMetier
{
    class Cargo : Navire, INavCommercable
    {
        /// <summary>
        /// Chaine représentatnt le type de cargaison du bateau : denrées périssables, matériel, ...
        /// </summary>
        private string typeFret;
        public Cargo(string imo, string latitude, string longitude, string nom, int tonnageActuel, int tonnageGT, int tonnageDWT, string typeFret) : base(imo, latitude, longitude, nom, tonnageActuel, tonnageGT, tonnageDWT)
        {
            this.typeFret = typeFret;
        }
        /// <summary>
        /// Met à jour le tonnage actuel du bateau. 
        /// La quantité passée en paramètre est ajoutée à la quantité actuelle
        /// </summary>
        /// <param name="quantite">Quantité à charger dans le Cargo</param>
        public void Charger(int quantite)
        {
            if (quantite >= 0 && quantite <= tonnageDWT)
            {
                this.TonnageActuel += quantite;
            }
        }
        /// <summary>
        /// Met à jour le tonnage actuel du bateau. 
        /// La quantité passée en paramètre est enlevée à la quantité actuelle
        /// </summary>
        /// <param name="quantite">Quantité à décharger du Cargo</param>
        public void Decharger(int quantite)
        {
            if (quantite >= 0 && quantite <= this.tonnageDWT)
            {
                this.TonnageActuel -= quantite;
            }
            else
            {
                if (quantite < 0)
                {
                    throw new GestionPortException("La quantité à décharger ne peut être négative");
                }
                else
                {
                    throw new GestionPortException("Impossible de décharger plus que la quantité de fret dans le navire");
                }
            }
        }
        /// <summary>
        /// Vérifie si le Cargo a bien déchargé toute sa cargaison
        /// </summary>
        /// <returns></returns>
        public bool EstDecharge()
        {
            return this.tonnageActuel == 0;
        }
        public string TypeFret { get => typeFret; }
    }
}
