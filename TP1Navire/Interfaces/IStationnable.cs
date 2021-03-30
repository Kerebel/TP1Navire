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
        /// <summary>
        /// Méthode qui enregistre le départ d'un objet présent dans la station
        /// </summary>
        /// <param name="imo"></param>
        void EnregistrerDepart(string imo);
        /// <summary>
        /// Retourne vrai si l'objet dont l'id est passé en paramètre fait partie des objets attendus dans la station 
        /// </summary>
        /// <param name="imo"></param>
        /// <returns></returns>
        bool EstAttendu(string imo);
        /// <summary>
        /// Retourne vrai si l'objet dont l'id est passé en paramètre fait partie des objets présents dans la station
        /// </summary>
        /// <param name="imo"></param>
        /// <returns></returns>
        bool EstPresent(string imo);
        /// <summary>
        /// Retourne vrai si l'objet dont l'id est passé en paramètre est parti de la station depuis peu de temps 
        /// </summary>
        /// <param name="imo"></param>
        /// <returns></returns>
        bool EstParti(string imo);
        /// <summary>
        /// Retourne l'objet dont l'id a été passé en paramètre ou une exception de type Exception 
        /// </summary>
        /// <param name="imo"></param>
        /// <returns></returns>
        object getUnAttendu(string imo);
        /// <summary>
        /// Retourne l'objet dont l'id a été passé en paramètre ou une exception de type Exception 
        /// </summary>
        /// <param name="imo"></param>
        /// <returns></returns>
        object getUnArrive(string imo);
        /// <summary>
        /// Retourne l'objet dont l'id a été passé en paramètre ou une exception de type Exception 
        /// </summary>
        /// <param name="imo"></param>
        /// <returns></returns>
        object getUnParti(string imo);
    }
}
