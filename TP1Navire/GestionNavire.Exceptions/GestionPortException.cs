using System;
using System.Collections.Generic;
using System.Text;

namespace GestionNavire.Exceptions
{
    class GestionPortException : Exception
    {
        public GestionPortException(string message)
            : base("Erreur de : " + System.Environment.UserName + " le " + DateTime.Now.ToLocalTime() + "\n" + message) {}
    }
}
