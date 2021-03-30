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
        private Dictionary<string, Navire> navireAttendus = new Dictionary<string, Navire>();
        /// <summary>
        /// Dictionnaire des navires arrivés, c’est-à-dire présents dans le port.String = id du navire
        /// </summary>
        private Dictionary<string, Navire> navireArrives = new Dictionary<string, Navire>();
        /// <summary>
        /// Dictionnaire des navires partis récemment. String = id du navire
        /// </summary>
        private Dictionary<string, Navire> navirePartis = new Dictionary<string, Navire>();
        /// <summary>
        /// Dictionnaire des navires en attente d'avoir un quai libre pour stationner.String = id du navire
        /// </summary>
        private Dictionary<string, Navire> navireEnAttente = new Dictionary<string, Navire>();
        private Dictionary<int,Stockage> stockages=  new Dictionary<int, Stockage>();
        public Port(string nom, string latitude, string longitude, int nbPortique, int nbQuaisPassager, int nbQuaisTanker, int nbQuaisSuperTanker)
        {
            this.nom = nom;
            this.latitude = latitude;
            this.longitude = longitude;
            this.nbPortique = nbPortique;
            this.nbQuaisPassager = nbQuaisPassager;
            this.nbQuaisTanker = nbQuaisTanker;
            this.nbQuaisSuperTanker = nbQuaisSuperTanker;
        }
        /// <summary>
        /// Enregistrement de l'arrivée proche d'un navire
        /// </summary>
        /// <param name="navire"></param>
        public void EnregistrerArriveePrevue(Navire navire)
        {
            if (!EstPresent(navire.Imo) && !EstAttendu(navire.Imo) && !EstEnAttente(navire.Imo))
            {
                navireAttendus.Add(navire.Imo, navire);
            }
            else
            {
                if (EstPresent(navire.Imo))
                {
                    throw new GestionPortException("Le navire est déjà présent dans le port");
                }
                else if (EstAttendu(navire.Imo))
                {
                    throw new GestionPortException("L'arrivée du navire est déjà prévue");
                }
                else
                {
                    throw new GestionPortException("La navire est en attente d'un quai dans le port");
                }
            }
        }
        /// <summary>
        ///  Enregistrement de l'arrivée d'un navire enregistré en tant que arrivée prévue
        /// </summary>
        /// <param name="navire"></param>
        public void EnregistrerArrivee(Navire navire)
        {
            if (navire is Cargo)
            {
                if (this.NbPortique > navireArrives.Count)
                {
                    navireArrives.Add(navire.Imo, navire);
                    navireAttendus.Remove(navire.Imo);
                }
                else
                {
                    AjoutNavireEnAttente(navire);
                    navireAttendus.Remove(navire.Imo);
                    Console.WriteLine("Navire mis en attente, il ne reste plus de quais libres pour les cargos");
                }
            }
            else if (navire is Croisiere)
            {
                if (this.NbQuaisPassager > navireArrives.Count)
                {
                    navireArrives.Add(navire.Imo, navire);
                    navireAttendus.Remove(navire.Imo);
                }
            }
            else if (navire is Tanker)
            {
                if (navire.TonnageDWT <= 130000)
                {
                    if (this.nbQuaisTanker < navireArrives.Count)
                    {
                        navireArrives.Add(navire.Imo, navire);
                        navireAttendus.Remove(navire.Imo);
                    }
                    else
                    {
                        AjoutNavireEnAttente(navire);
                        navireAttendus.Remove(navire.Imo);
                        Console.WriteLine("Navire mis en attente, il ne reste plus de quais libres pour les tankers");
                    }
                }
                else
                {
                    if (this.NbQuaisSuperTanker < navireArrives.Count)
                    {
                        navireArrives.Add(navire.Imo, navire);
                        navireAttendus.Remove(navire.Imo);
                    }
                    else
                    {
                        AjoutNavireEnAttente(navire);
                        navireAttendus.Remove(navire.Imo);
                        Console.WriteLine("Navire mis en attente, il ne reste plus de quais libres pour les super tankers");
                    }
                }
            }
        }
        public void EnregistrerArrivee(string imo)
        {
            if (navireAttendus.ContainsKey(imo))
            {
                foreach (Navire navire in navireAttendus.Values)
                {
                    {
                        EnregistrerArrivee(navire);
                    }
                }
            }
        }
        public void EnregistrerDepart(Navire navire)
        {
            EnregistrerDepart(navire.Imo);
        }
        /// <summary>
        /// Enregistrement du départ d'un navire présent dans le port
        /// </summary>
        /// <param name="imo"></param>
        public void EnregistrerDepart(string imo)
        {
            if (EstPresent(imo))
            {
                this.navireArrives.Remove(imo);
                this.navirePartis.Add(GetNavire(imo).Imo, GetNavire(imo));
            }
            else
            {
                throw new GestionPortException("Impossible d'enregistrer le navire " + imo + " , il n'est pas dans le port");
            }
        }
        /// <summary>
        /// Ajout du navire passé en paramètre dans le dictionnaire des navires en attente d'un quai dans le port
        /// </summary>
        /// <param name="navire"></param>
        public void AjoutNavireEnAttente(Navire navire)
        {
            navireEnAttente.Add(navire.Imo, navire);
        }
        /// <summary>
        /// Retourne vrai si le navire est dans le port
        /// </summary>
        /// <param name="imo"></param>
        /// <returns></returns>
        public bool EstPresent(string imo)
        {
            return navireArrives.ContainsKey(imo);
        }
        /// <summary>
        /// Retourne vrai si le navire est attendu.
        /// </summary>
        /// <param name="imo"></param>
        /// <returns></returns>
        public bool EstAttendu(string imo)
        {
            return navireAttendus.ContainsKey(imo);
        }
        /// <summary>
        /// Retourne vrai si le navire est en attente d'un quai dans le port
        /// </summary>
        /// <param name="imo"></param>
        /// <returns></returns>
        public bool EstEnAttente(string imo)
        {
            return navireEnAttente.ContainsKey(imo);
        }
        /// <summary>
        /// Chargement du navire dont l'id et la quantité sont passés en paramètre 
        /// </summary>
        /// <param name="imo"></param>
        /// <param name="quantite"></param>
        public void Chargement(string imo, int quantite)
        {
            Navire navire = GetNavire(imo);
            if (navire != null && navire is Cargo cargo)
            {
                int i = 0;
                while (i < this.stockages.Count)
                {

                }
            }
        }
        /// <summary>
        /// Déhargement du navire dont l'id et la quantité sont passés en paramètre 
        /// </summary>
        /// <param name="imo"></param>
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
        /// <summary>
        /// Retourne l'objet dont l'id a été passé en paramètre ou une exception de type Exception 
        /// </summary>
        /// <param name="imo"></param>
        /// <returns></returns>
        public Navire getUnAttendu(string imo)
        {
            if (this.navireAttendus.TryGetValue(imo, out Navire navire))
            {
                return navire;
            }
            return null;
        }
        /// <summary>
        /// Retourne l'objet dont l'id a été passé en paramètre ou une exception de type Exception 
        /// </summary>
        /// <param name="imo"></param>
        /// <returns></returns>
        public Navire getUnArrive(string imo)
        {
            if (this.navireArrives.TryGetValue(imo, out Navire navire))
            {
                return navire;
            }
            return null;
        }
        /// <summary>
        /// Retourne l'objet dont l'id a été passé en paramètre ou une exception de type Exception 
        /// </summary>
        /// <param name="imo"></param>
        /// <returns></returns>
        public Navire getUnParti(string imo)
        {
            if (!this.navirePartis.TryGetValue(imo, out Navire navire))
            {
                return navire;
            }
            return null;
        }
        /// <summary>
        /// retourne le nombre de tankers (tonnageGT <= 130000) présents dans le port 
        /// </summary>
        /// <returns></returns>
        public int NbTankerArrives()
        {
            int cpt = 0;
            foreach (Navire navire in navireArrives.Values)
            {
                if (navire is Tanker && navire.TonnageGT <= 130000)
                {
                    cpt+= 1;
                }
            }
            return cpt;
        }
        /// <summary>
        /// Retourne le nombre de super tankers (tonnageGT>130000) présents dans le port 
        /// </summary>
        /// <returns></returns>
        public int GetSuperTankerArrives()
        {
            int cpt = 0;
            foreach(Navire navire in navireArrives.Values)
            {
                if (navire is Tanker && navire.TonnageGT > 130000)
                {
                    cpt += 1;
                }
            }
            return cpt;
        }
        /// <summary>
        /// Retourne le nombre de cargos présents dans le port 
        /// </summary>
        /// <returns></returns>
        public int getNbCargoArrives()
        {
            int cpt = 0;
            foreach(Navire navire in navireArrives.Values)
            {
                if (navire is Cargo)
                {
                    cpt += 1;
                }
            }
            return cpt;
        }
        public void AjoutStockage(Stockage stockage)
        {
            this.stockages.Add(stockage.Numero, stockage);
        }
        public Navire GetNavire(String imo)
        {
            if (this.navireArrives.TryGetValue(imo, out Navire navire))
            {
                return navire;
            }
            else
            {
                return null;
            }
        }
        private void PermutEtatNavire(Dictionary<string, Navire> ancienEtat, Dictionary<string, Navire> nouvetat)
        {
            ancienEtat.Remove
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
        internal Dictionary<int,Stockage> Stockages { get => stockages; }
    }
}
