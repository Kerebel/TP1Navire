using System;
using System.Collections.Generic;
using System.Text;

namespace Station.Interfaces
{
    interface INavCommercable
    {
        /// <summary>
        /// Méthode qui met à jour le tonnage actuel du navire avec la valeur passée en paramètre. La quantité passée en paramètre est enlevée à la quantité actuelle
        /// </summary>
        /// <param name="quantite"></param>
        void Decharger(int quantite);
        /// <summary>
        /// Méthode qui met à jour le tonnage actuel du bateau avec la valeur passée en paramètre. La quantité passée en paramètre est ajoutée à la quantité actuelle.
        /// </summary>
        /// <param name="quantite"></param>
        void Charger(int quantite);
    }
}
