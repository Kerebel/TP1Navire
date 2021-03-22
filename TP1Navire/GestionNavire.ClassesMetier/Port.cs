using GestionNavire.ClassesMetier;
using GestionNavire.Exceptions;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace GestionNavire.Classesmetier
{
    class Port
    {

        private string nom;
        private int nbNaviresMax = 5;
        private Dictionary<string, Navire> navires = new Dictionary<string, Navire>();
        private List<Stockage> stockages = new List<Stockage>() ;

        public Port(string nom)
        {
            this.nom = nom;
        }
        public void EnregistrerArrivee(Navire navire)
        {
            try
            {
                if (this.navires.Count < nbNaviresMax)
                {
                    this.navires.Add(navire.Imo, navire);
                }
                else
                {
                    throw new GestionPortException("Ajout Impossible, le port est complet");
                }
            }
            catch (ArgumentException)
            {
                throw new GestionPortException("Le navire " + navire.Imo + " est déjà enregistré");
            }
        }
        public void EnregistrerDepart(string imo)
        {
            if (EstPresent(imo))
            {
                this.navires.Remove(imo);
            }
            else
            {
                throw new GestionPortException("Impossible d'enregistrer le navire " + imo + " , il n'est pas dans le port");
            }
        }
        public bool EstPresent(string imo)
        {
            return navires.ContainsKey(imo);
        }
        public void Dechargement(string imo)
        {
            Navire navire = GetNavire(imo);
            if (navire != null && navire.LibelleFret == "Porte-conteneurs")
            {
                int i = 0;
                while (i < this.stockages.Count && !navire.EstDecharge())
                {
                    if (this.stockages[i].CapaciteDispo != 0 && this.stockages[i].CapaciteDispo > navire.QteFret)
                    {
                        navire.Decharger(navire.QteFret);
                        this.stockages[i].Stocker(navire.QteFret);
                    }
                    else
                    {
                        navire.Decharger(this.stockages[i].CapaciteDispo);
                        this.stockages[i].Stocker(this.stockages[i].CapaciteDispo);
                    }
                    i++;
                }
                if (navire.EstDecharge())
                {
                    Console.WriteLine("Le navire à bien été déchargé");
                }
                else
                {
                    throw new GestionPortException("Le navire " + navire.Imo + " n'a pas pu être entièrement déchargé, il reste " + navire.QteFret + " tonnes.");
                }
            }
            else
            {
                throw new GestionPortException("Impossible de décharger le navire " + imo + " il n'est pas dans le port ou n'est pas un porte-conteneurs.");
            }
        }
        public void AjoutStockage(Stockage stockage)
        {
            this.stockages.Add(stockage);
        }
        public Navire GetNavire(String imo)
        {
            if (this.navires.TryGetValue(imo, out Navire navire))
            {
                return navire;
            }
            else
            {
                return null;
            }
        }
        public string Nom { get => nom; }
        public int NbNaviresMax { get => nbNaviresMax; set => nbNaviresMax = value; }
        internal Dictionary<string, Navire> Navires { get => navires; }
    }
}
