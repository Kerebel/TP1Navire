using System;
using GestionNavire.Classesmetier;
using GestionNavire.ClassesMetier;
using GestionNavire.Exceptions;

namespace GestionNavire.Application
{
    class Program
    {
        private static Port port;
        static void Main()
        {
            try
            {
                port = new Port("Toulon");
                InitPort();
                ////Instanciations();
                try { TesterEnregistrerArrivee(); }
                catch (GestionPortException ex)
                { Console.WriteLine(ex.Message); }

                try { TesterEnregistrerArriveeV2(); }
                catch (GestionPortException ex)
                { Console.WriteLine(ex.Message); }

                Console.WriteLine("-----------------------------------------");
                Console.WriteLine("------- Début des déchargements -------");
                Console.WriteLine("-----------------------------------------");
                AjouterStockages();
                TesterDechargerNavires();
                Console.WriteLine("---------------------------------------");
                Console.WriteLine("------- fin des déchargements -------");
                Console.WriteLine("---------------------------------------");

                try { TesterEnregistrerDepart(); }
                catch (GestionPortException ex)
                { Console.WriteLine(ex.Message); }
                Console.WriteLine("Fin normale du programme");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { Console.ReadKey(); }
        }
        public static void InitPort()
        {
            Port port = new Port("Toulon");
            Navire unNavire;
            unNavire = new Navire("IMO9427639", "Copper Spirit", "Hydrocarbures", 156827, 120000);
            Navire unAutreNavire = new Navire("IMO9839272", "MSC Isabella", "Porte-conteneurs", 197500, 13000);
            unAutreNavire = new Navire("IMO8715871", "MSC PILAR");
            Console.WriteLine("Fin de chargement du port");
        }
        public static void Affiche(Navire navire)
        {
            //Console.WriteLine("Identification : " + navire.Imo);
            //Console.WriteLine("Nom : " + navire.Nom);
            //Console.WriteLine("Type de frêt : " + navire.LibelleFret);
            //Console.WriteLine("Quantité de frêt :        " + navire.QteFretMaxi);
            Console.WriteLine(navire);
            Console.WriteLine("--------------------------");
        }
        
        public static void TesterEnregistrerDepart()
        {
            try
            {
                port.EnregistrerDepart("IMO9427639");
                port.EnregistrerDepart("IMO9405423");
                port.EnregistrerDepart("IMO1111111");
            }
            catch (GestionPortException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void TesterEstPresent()
        {
            Port port = new Port("Toulon");
            port.EnregistrerArrivee(new Navire("IMO9427639", "Copper Spirit", "Hydrocarbures", 156827, 150000));
            port.EnregistrerArrivee(new Navire("IMO9839272", "MSC Isabella", "Porte-conteneurs", 197500, 150000));
            port.EnregistrerArrivee(new Navire("IMO8715871", "MSC PILAR"));
            String imo = "IMO9427639";
            Console.WriteLine("Le navire " + imo + "est présent dans le port : " + port.EstPresent(imo));
            imo = "IMO1111111";
            Console.WriteLine("Le navire " + imo + "est présent dans le port : " + port.EstPresent(imo));
        }
        public static void TesterInstanciationsStockage()
        {
            try
            { new Stockage(1, 15000);}
            catch (GestionPortException ex)
            { Console.WriteLine(ex.Message); }
            try
            { new Stockage(2, 12000, 10000); }
            catch (GestionPortException ex)
            { Console.WriteLine(ex.Message); }
            try
            { new Stockage(3, -25000, -10000); }
            catch (GestionPortException ex)
            { Console.WriteLine(ex.Message); }
            try
            { new Stockage(4, 15000, 20000); }
            catch (GestionPortException ex)
            { Console.WriteLine(ex.Message); }
        } 
        private static void TesterEnregistrerArrivee()
        {
            Navire navire = null;
            try
            {
                navire = new Navire("IMO9427639", "Copper Spirit", "Hydrocarbures", 156827, 150000); 
                port.EnregistrerArrivee(navire);
                navire = new Navire("IMO9427639", "Copper Spirit", "Hydrocarbures", 156827, 150000);
                port.EnregistrerArrivee(navire);
                Console.WriteLine("Navires bien enregistrés dans le port"); ;
            }
            catch (GestionPortException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private static void TesterEnregistrerArriveeV2()
        {
            Navire navire = null;
            try
            {
                port.EnregistrerArrivee(new Navire("IMO9839272", "MSC Isabella", "Porte-conteneurs", 197500, 150000));
                port.EnregistrerArrivee(new Navire("IMO8715871", "MSC PILAR"));
                port.EnregistrerArrivee(new Navire("IMO9235232", "FORTUNE TRADER", "Cargo", 74750, 70000));
                port.EnregistrerArrivee(new Navire("IMO9405423", "SERENEA", "Tanker", 158583, 150000));
                port.EnregistrerArrivee(new Navire("IMO9574004", "TRITON SEAHAWK", "Hydrocarbures", 51201, 50000));
            }
            catch (GestionPortException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentException)
            {
                throw new GestionPortException("Le navire " + navire.Imo + " est déjà enregistré");
            }
        }
        public static void AjouterStockages()
        {
            port.AjoutStockage(new Stockage(1, 160000));
            port.AjoutStockage(new Stockage(2, 12000));
            port.AjoutStockage(new Stockage(3, 25000));
            port.AjoutStockage(new Stockage(4, 15000));
            port.AjoutStockage(new Stockage(5, 15000));
            port.AjoutStockage(new Stockage(6, 15000));
            port.AjoutStockage(new Stockage(7, 15000));
            port.AjoutStockage(new Stockage(8, 15000));
            port.AjoutStockage(new Stockage(9, 35000));
            port.AjoutStockage(new Stockage(10, 19000));
        }
        public static void TesterDechargerNavires()
        {
            try
            {
                String imo = "IMO9839272";
                port.Dechargement(imo);
                Console.WriteLine("Navire " + imo + " déchargé");
                port.EnregistrerDepart(imo);
            }
            catch (GestionPortException ex) { Console.WriteLine(ex.Message); }
            try
            {
                String imo = "IMO1111111";
                port.Dechargement(imo);
                Console.WriteLine("Navire " + imo + " déchargé");
                port.EnregistrerDepart(imo);
            }
            catch (GestionPortException ex) { Console.WriteLine(ex.Message); }
            try
            {
                String imo = "IMO9574004";
                port.Dechargement(imo);
                Console.WriteLine("Navire " + imo + " déchargé");
                port.EnregistrerDepart(imo);
            }
            catch (GestionPortException ex) { Console.WriteLine(ex.Message); }
            try
            {
                port.EnregistrerArrivee(new Navire("IMO9786841", "EVER GLOBE", "Porte-conteneurs", 198937, 190000));
                String imo = "IMO9786841";
                port.Dechargement(imo);
                Console.WriteLine("Navire " + imo + " déchargé");
                port.EnregistrerDepart(imo);
            }
            catch (GestionPortException ex) { Console.WriteLine(ex.Message); }
            try
            {
                port.EnregistrerArrivee(new Navire("IMO9776432", "CMACGM LOUIS BLERIOT", "Porte-conteneurs", 202684, 190000));
                String imo = "IMO9776432";
                port.Dechargement(imo);
                Console.WriteLine("Navire " + imo + " déchargé");
                port.EnregistrerDepart(imo);
            }
            catch (GestionPortException ex) { Console.WriteLine(ex.Message); }
        }
        private static void Instanciations()
        {
            try
            {
                Navire navire = new Navire("IMO9427639", "Copper Spirit", "Hydrocarbures", 156827, 150000);
                navire = new Navire("IMO9839272", "MSC Isabella", "Porte-conteneurs", 197500, 150000);
                navire = new Navire("IMO8715871", "MSC PILAR", "Porte-conteneurs", 67183, 60000);
                navire = new Navire("IMZ9235232", "FORTUNE TRADER", "Cargo", 74750, 70000);
                navire = new Navire("IMO9574004", "TRITON SEAHAWK", "Hydrocarbures", 51201, 50000);

            }
            catch (GestionPortException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
