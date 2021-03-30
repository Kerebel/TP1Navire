using GestionNavire.Exceptions;
using System.Collections.Generic;

namespace NavireHeritage.ClassesMetier
{
    class Utilitaire
    {
        /// <summary>
        /// Va rechercher dans le port les stockages à parcourir pour décharger le navire passé en paramètre de la quantité passée en paramètre
        /// </summary>
        /// <param name="navire">Navire à décharger</param>
        /// <param name="quantite">Quantité à décharger</param>
        /// <returns></returns>
        public Dictionary <int,int> ItineraireDeChargeNavire(Navire navire, int quantite )
        {
            Dictionary<int, Stockage> stockages = new Dictionary<int, Stockage>();
            Port port = new Port();
            foreach (Stockage stockage in stockages.Values)
            {
                if (port.EstPresent(navire.Imo))
                {
                    if (navire is Tanker tanker)
                    {
                        if (tanker.TonnageGT < 130000)
                        {
                            if (stockage.CapaciteDispo < stockage.CapaciteMaxi && !tanker.EstDecharge())
                            {
                                stockage.Stocker(quantite);
                                tanker.Decharger(quantite);
                            }
                            else if (stockage.CapaciteDispo == stockage.CapaciteMaxi)
                            {
                                throw new GestionPortException("La capacité de stockage a atteint sa limite.");
                            }
                            else
                            {
                                throw new GestionPortException("Le Tanker est déjà déchargé.");
                            }
                        }
                        else
                        {
                            if (stockage.CapaciteDispo < stockage.CapaciteMaxi && !tanker.EstDecharge())
                            {
                                stockage.Stocker(quantite);
                                tanker.Decharger(quantite);
                            }
                            else if (stockage.CapaciteDispo == stockage.CapaciteMaxi)
                            {
                                throw new GestionPortException("La capacité de stockage a atteint sa limite.");
                            }
                            else
                            {
                                throw new GestionPortException("Le Super Tanker est déjà déchargé.");
                            }
                        }
                    }
                    if (navire is Cargo cargo)
                    {
                        if (stockage.CapaciteDispo < stockage.CapaciteMaxi && !cargo.EstDecharge())
                        {
                            stockage.Stocker(quantite);
                            cargo.Decharger(quantite);
                        }
                        else if (stockage.CapaciteDispo == stockage.CapaciteMaxi)
                        {
                            throw new GestionPortException("La capacité de stockage a atteint sa limite.");
                        }
                        else
                        {
                            throw new GestionPortException("Le Cargo est déjà déchargé.");
                        }
                    }
                }
            }
        }
    }
}
