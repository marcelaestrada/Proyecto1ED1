using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Proyecto1ED1.Models.Model;

namespace Proyecto1ED1.Helpers
{
    public class Storage
    {
        private static Storage _instance = null;
        public static Storage Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Storage();

                return _instance;
            }
        }

        /// <summary>
        /// Informacion de cada hospital 
        /// </summary>
        public Hospital hospitalCapital = new Hospital("Hospital Capital");
        public Hospital hospitalQuetzaltenango = new Hospital("Hospital Quetzaltenango");
        public Hospital hospitalPeten = new Hospital("Hospital Peten");
        public Hospital hospitalEscuintla = new Hospital("Hospital Escuintla");
        public Hospital hospitalOriente = new Hospital("Hospital Oriente");

        // Estadisticas general y por cada hospital 
        public Estadisticas datos = new Estadisticas();
        public Estadisticas datosCapital = new Estadisticas();
        public Estadisticas datosQuetzaltenango = new Estadisticas();
        public Estadisticas datosPeten = new Estadisticas();
        public Estadisticas datosEscuintla = new Estadisticas();
        public Estadisticas datosOriente = new Estadisticas();

        //Hospital seleccionado a administrar. 
        public string hospitalSeleccionado = " ";
    }
}