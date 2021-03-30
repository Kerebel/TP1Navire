using System;
using System.Collections.Generic;
using System.Text;

namespace Station.Interfaces
{
    interface IStationnable
    {
        /// <summary>
        /// Méthode qui met à jour la liste des objets qui sont susceptibles d'arriver dans la station (port, aéroport,…)
        /// </summary>
        /// <param name=""></param>
        void EnregistrerArriveePrevue(object objet);
        /// <summary>
        /// Méthode qui enregistre l'arrivée réelle de l'objet. 
        /// </summary>
        /// <param name=""></param>
        void EnregistrerArrivee(string imo);
        void EnregistrerDepart(string imo);
        bool EstAttendu(string imo);
        bool EstPresent(string imo);
        bool EstParti(string imo);
        object getUnAttendu(string imo);
        object getUnArrive(string imo);
        object getUnParti(string imo);
    }
}
