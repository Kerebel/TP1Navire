﻿using GestionNavire.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace NavireHeritage.ClassesMetier
{
    class Croisiere : Navire
    {
        /// <summary>
        /// V: voilier ; M: moteur
        /// </summary>
        private string typeNavireCroisiere;
        /// <summary>
        /// Nombre de passager maximum que peut embarquer un navire
        /// </summary>
        private int nbPassagersMaxi;
        /// <summary>
        ///  Représente les personnes croisiéristes à bord du bateau.
        /// </summary>
        private Dictionary<string, Passager> passagers;

        public Croisiere(string imo, string latitude, string longitude, string nom, int tonnageActuel, int tonnageGT, int tonnageDWT, string typeNavireCroisiere, int nbPassagersMaxi ) : base(imo, latitude, longitude, nom, tonnageActuel, tonnageGT, tonnageDWT)
        {
            this.typeNavireCroisiere = typeNavireCroisiere;
            if (nbPassagersMaxi > passagers.Count)
            {
                this.nbPassagersMaxi = nbPassagersMaxi;
            }
        }

        public Croisiere(string imo, string latitude, string longitude, string nom, int tonnageActuel, int tonnageGT, int tonnageDWT, string typeNavireCroisiere, int nbPassagersMaxi, Dictionary<int, Passager> passagers) : base(imo, latitude, longitude, nom, tonnageActuel, tonnageGT, tonnageDWT) { this.passagers = passagers; }
        /// <summary>
        /// Met à jour la liste des passagers du bateau en ajoutant les passagers passés en paramètre
        /// </summary>
        public void Embarquer(List<Passager>nouvPassagers)
        {
            if (nouvPassagers.Count < nbPassagersMaxi - passagers.Count)
            {
                foreach (Passager passager in nouvPassagers)
                {
                    if (passagers.ContainsKey(passager.NumPasseport))
                    {
                        passagers.Add(passager.NumPasseport, passager);
                    }
                    else
                    {
                        throw new GestionPortException("Le passager n'est pas dans la liste d'embarquement");
                    }
                }
            }
            else
            {
                throw new GestionPortException("Le nombre de passagers dépasse le nombre de passegers maximum du navire. Aucun passager n'a été ajouté");
            }
        }
        /// <summary>
        ///  Met à jour la liste des passagers du bateau en retirant les passagers passés en paramètre
        /// </summary>
        /// <returns>retourne la liste des passagers passés en paramètre et qui n'ont pas été trouvés dans la liste des passagers du navire.</returns>
        public List<Passager> Debarquer(List<Passager> passagersEmbarques)
        {
            List<Passager> passagersNonTrouves = new List<Passager>();
            foreach (Passager passager in passagersEmbarques)
            {
                if (passagers.ContainsKey(passager.NumPasseport))
                {
                    passagers.Remove(passager.NumPasseport);
                }
                else
                {
                    passagersNonTrouves.Add(passager);
                }
            }
            return passagersNonTrouves;
        }
        public string TypeNavireCroisiere { get => typeNavireCroisiere; }
        public int NbPassagersMaxi { get => nbPassagersMaxi; }
        internal Dictionary<string, Passager> Passagers { get => passagers; }
    }
}
