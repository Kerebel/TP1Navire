using NavireHeritage.ClassesMetier;
using GestionNavire.Exceptions;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace NavireHeritage.ClassesMetier
{
    class Port
    {
        /// <summary>
        /// Nom du port
        /// </summary>
        private string nom;
        private string latitude;
        private string longitude;
        /// <summary>
        /// Nombre de points d'accueil d'un cargo
        /// </summary>
        private int nbPortique;
        /// <summary>
        /// Nombre de quais d'accueil pour navires passagers
        /// </summary>
        private int nbQuaisPassager;
        /// <summary>
        /// Nombre de quais d'accueil pour les tankers de jusqu'à 130000 tonnes (GT)
        /// </summary>
        private int nbQuaisTanker;
        /// <summary>
        /// Nombre de quais d'accueil pour les tankers de plus de 130000 tonnes(GT)
        /// </summary>
        private int nbQuaisSuperTanker;
        /// <summary>
        /// Dictionnaire des navires attendus. String = id du navire
        /// </summary>
        private Dictionary<string, Navire> navireAttendus;
        /// <summary>
        /// Dictionnaire des navires arrivés, c’est-à-dire présents dans le port.String = id du navire
        /// </summary>
        private Dictionary<string, Navire> navireArrives;
        /// <summary>
        /// Dictionnaire des navires partis récemment. String = id du navire
        /// </summary>
        private Dictionary<string, Navire> navirePartis;
        /// <summary>
        /// Dictionnaire des navires en attente d'avoir un quai libre pour stationner.String = id du navire
        /// </summary>
        private Dictionary<string, Navire> navireEnAttente;
        private List<Stockage> stockages;
        public Port(string nom, string latitude, string longitude, int nbPortique, int nbQuaisPassager, int nbQuaisTanker, int nbQuaisSuperTanker)
        {
            this.nom = nom;
            this.latitude = latitude;
            this.longitude = longitude;
            this.nbPortique = nbPortique;
            this.nbQuaisPassager = nbQuaisPassager;
            this.nbQuaisTanker = nbQuaisTanker;
            this.nbQuaisSuperTanker = nbQuaisSuperTanker;
            this.navireAttendus = new Dictionary<string, Navire>();
            this.navireArrives = new Dictionary<string, Navire>();
            this.navirePartis = new Dictionary<string, Navire>();
            this.navireEnAttente = new Dictionary<string, Navire>();
            this.stockages = new List<Stockage>();
        }
        /// <summary>
        /// Enregistrement de l'arrivée proche d'un navire
        /// </summary>
        /// <param name="navire"></param>
        public void EnregistrerArriveePrevue(Navire navire)
        {
            //try
            //{
            //    if (this.navires.Count < nbNaviresMax)
            //    {
            //        this.navires.Add(navire.Imo, navire);
            //    }
            //    
            //}
            //catch (ArgumentException)
            //{
            //    throw new GestionPortException("Le navire " + navire.Imo + " est déjà enregistré");
            //}
            if (navire is Cargo)
            {
                if (this.NbPortique > navireArrives.Count)
                {
                    navireArrives.Add(navire.Imo, navire);
                }
                else
                {
                    throw new GestionPortException("Enregistrement impossible, il ne reste plus de quais libres pour les cargos");
                }
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
                    if (this.stockages[i].CapaciteDispo >= 0 && this.stockages[i].CapaciteDispo > navire.QteFret)
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
        public string Nom { get => nom; set => nom = value; }
        public string Latitude { get => latitude; }
        public string Longitude { get => longitude; }
        public int NbPortique { get => nbPortique; set => nbPortique = value; }
        public int NbQuaisPassager { get => nbQuaisPassager; set => nbQuaisPassager = value; }
        public int NbQuaisTanker { get => nbQuaisTanker; set => nbQuaisTanker = value; }
        public int NbQuaisSuperTanker { get => nbQuaisSuperTanker; set => nbQuaisSuperTanker = value; }
        internal Dictionary<string, Navire> NavireAttendus { get => navireAttendus; }
        internal Dictionary<string, Navire> NavireArrives { get => navireArrives; }
        internal Dictionary<string, Navire> NavirePartis { get => navirePartis; }
        internal Dictionary<string, Navire> NavireEnAttente { get => navireEnAttente; }
        internal List<Stockage> Stockages { get => stockages; }
    }
}
