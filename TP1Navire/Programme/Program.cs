using System;
using GestionNavire.Classesmetier;

namespace GestionNavire.Application
{
    class Program
    {
        static void Main()
        {
            //TesterInstanciations();
            //TesterEnregistrerArrivee();
            //TesterEnregistrerDepart();
            //TesterEstPresent();
            try
            {
                Instanciations();
                Console.WriteLine("Fin normale du programme");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void TesterInstanciations()
        {
            //déclaration de l'objet unNavire de la classe Navire
            Navire unNavire;
            // Instanciation de l'objet
            unNavire = new Navire("IMO9427639", "Copper Spirit", "Hydrocarbures", 156827);
            Affiche(unNavire);
            //Declaration ET instanciation d'un autre objet de la classe Navire
            Navire unAutreNavire = new Navire("IMO9427639", "MSC Isabella", "Porte-conteneurs", 197500);
            Affiche(unAutreNavire);
            // Instanciation de l'objet
            unAutreNavire = new Navire("IMO8715871", "MSC PILAR");
            Affiche(unAutreNavire);
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
        public static void TesterEnregistrerArrivee()
        {
            try
            {
                Port port = new Port("Toulon");
                port.EnregistrerArrivee(new Navire("IMO9427639", "Copper Spirit", "Hydrocarbures", 156827));
                port.EnregistrerArrivee(new Navire("IMO8715871", "MSC PILAR"));
                port.EnregistrerArrivee(new Navire("IMO9839272", "MSC Isabella", "Porte-conteneurs", 197500));
                port.EnregistrerArrivee(new Navire("IMO5548482", "MSC FRAER", "Porte-conteneurs", 198200));
                port.EnregistrerArrivee(new Navire("IMO3547957", "Silver Spirit", "Hydrocarbures", 176350));
                port.EnregistrerArrivee(new Navire("IMO1486784", "MSC TOWER"));
                Console.WriteLine("Navires bien enregistrés dans le port"); ;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void TesterEnregistrerDepart()
        {
            try
            {
                Port port = new Port("Toulon");
                port.EnregistrerArrivee(new Navire("IMO9427639", "Copper Spirit", "Hydrocarbures", 156827));
                port.EnregistrerArrivee(new Navire("IMO9839272", "MSC Isabella", "Porte-conteneurs", 197500));
                port.EnregistrerArrivee(new Navire("IMO8715871", "MSC PILAR"));
                port.EnregistrerDepart("IMO8715871");
                Console.WriteLine("Départ du navire IMO871871 enregistré");
                port.EnregistrerDepart("IMO1111111");
                Console.WriteLine("Départ du navire IMO1111111 enregistré");
                Console.WriteLine("fin des enregistrements des départs");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void TesterEstPresent()
        {
            Port port = new Port("Toulon");
            port.EnregistrerArrivee(new Navire("IMO9427639", "Copper Spirit", "Hydrocarbures", 156827));
            port.EnregistrerArrivee(new Navire("IMO9839272", "MSC Isabella", "Porte-conteneurs", 197500));
            port.EnregistrerArrivee(new Navire("IMO8715871", "MSC PILAR"));
            String imo = "IMO9427639";
            Console.WriteLine("Le navire " + imo + "est présent dans le port : " + port.EstPresent(imo));
            imo = "IMO1111111";
            Console.WriteLine("Le navire " + imo + "est présent dans le port : " + port.EstPresent(imo));
        }
        private static void Instanciations()
        {
            try
            {
                Navire navire = new Navire("IMO9427639", "Copper Spirit", "Hydrocarbures", 156827);
                navire = new Navire("IMO9839272", "MSC Isabella", "Porte-conteneurs", -197500);
                navire = new Navire("IMO8715871", "MSC PILAR", "Porte-conteneurs", 67183);
                navire = new Navire("IMO9235232", "FORTUNE TRADER", "Cargo", 74750);
                navire = new Navire("IMO9574004", "TRITON SEAHAWK", "Hydrocarbures", 51201);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
