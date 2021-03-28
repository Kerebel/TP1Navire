using System;

namespace GestionNavire.Exceptions
{
    class GestionPortException : Exception
    {
        public GestionPortException(string message)
            : base("Erreur de : " + System.Environment.UserName + " le " + DateTime.Now.ToLocalTime() + "\n" + message) {}
    }
}
