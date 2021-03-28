using GestionNavire.Exceptions;

namespace NavireHeritage.ClassesMetier
{
    class Stockage
    {
        private int numero;
        private int capaciteMaxi;
        private int capaciteDispo;

        public Stockage(int numero, int capaciteMaxi, int capaciteDispo)
        {
            this.numero = numero;
            this.CapaciteMaxi = capaciteMaxi;
            this.CapaciteDispo = capaciteDispo;
        }
        public Stockage(int numero, int capaciteMaxi) : this(numero, capaciteMaxi, capaciteMaxi) { }

        public void Stocker(int quantite)
        {
            if (quantite < 0)
            {
                throw new GestionPortException("la quantité à stocker dans un stockage ne peut être négative");
            }
            else if (quantite > this.capaciteDispo)
            {
                throw new GestionPortException("Impossible de stocker plus que la capacité disponible dans le stockage");
            }
            else
            {
                this.capaciteDispo -= quantite;
            }

        }
        public int Numero { get => numero; }
        public int CapaciteMaxi
        {
            get => capaciteMaxi;
            set
            {
                if (value > 0)
                {
                    this.capaciteMaxi = value;
                }
                else
                {
                    throw new GestionPortException("Impossible de créer un stockage avec une capacité négative");
                }
            }
        }
        public int CapaciteDispo
        {
            get => capaciteDispo;
            private set
            {
                if (value < 0)
                {
                    throw new GestionPortException("la quantité à stocker dans un stockage ne peut être négative ou nulle");
                }
                else if (this.capaciteMaxi >= value)
                {
                    // On peut stocker
                    this.capaciteDispo = value;
                }
                else
                {
                    throw new GestionPortException("Impossible de stocker plus que la capacité disponible dans le stockage ");
                }
            }
        }
    }
}
