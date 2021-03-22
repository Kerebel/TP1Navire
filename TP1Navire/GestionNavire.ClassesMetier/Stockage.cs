using GestionNavire.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionNavire.ClassesMetier
{
    class Stockage
    {
        private int numero;
        private int capacitéMaxi;
        private int capaciteDispo;

        public Stockage(int numero, int capacitéMaxi, int capaciteDispo)
        {
            this.numero = numero;
            this.CapacitéMaxi = capacitéMaxi;
            this.CapaciteDispo = capaciteDispo;
        }
        public Stockage(int numero, int capacitéMaxi) : this(numero, capacitéMaxi, 0) { }

        public int Stocker(int quantite)
        {
            if (quantite <= 0)
            {
                throw new GestionPortException("la quantité à stocker dans un stockage ne peut être négative ou nulle");
            }
            else if (quantite > this.capaciteDispo)
            {
                throw new GestionPortException("Impossible de stocker plus que la capacité disponible dans le stockage");
            }
            else
            {
                return this.capaciteDispo - quantite;
            }

        }
        public int Numero { get => numero; }
        public int CapacitéMaxi
        {
            get => capacitéMaxi;
            set
            {
                if (value > 0)
                {
                    this.capacitéMaxi = value;
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
            set
            {
                if (value <= 0)
                {
                    throw new GestionPortException("la quantité à stocker dans un stockage ne peut être négative ou nulle");
                }
                else if (this.capacitéMaxi >= value)
                {
                    // On peut stocker
                    this.capaciteDispo -= value;
                }
                else
                {
                    throw new GestionPortException("Impossible de stocker plus que la capacité disponible dans le stockage ");
                }
            }
        }
    }
}
