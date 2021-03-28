using System;
using System.Collections.Generic;
using System.Text;

namespace NavireHeritage.ClassesMetier
{
    class Tanker : Navire
    {
        /// <summary>
        /// Gaz liquide, pétrole, huile…
        /// </summary>
        private string typeFluide;
        public Tanker(string imo, string latitude, string longitude, string nom, int tonnageActuel, int tonnageGT, int tonnageDWT, string typeFluide) : base(imo, latitude, longitude, nom, tonnageActuel, tonnageGT, tonnageDWT)
        {
        }
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
        }
        /// <summary>
        /// Vérifie si le Tanker a bien déchargé toute sa cargaison
        /// </summary>
        /// <returns></returns>
        public bool EstDecharge()
        {
            return tonnageActuel == 0;
        }
        public string TypeFluide { get => typeFluide;  }
    }
}
