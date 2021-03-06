﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CustomGenerics.Structures;

namespace Proyecto1ED1.Models.Model
{
    public class Hospital
    {
        /// <summary>
        /// Prueba para evaluar funcionamiento de hashtable con ese formato
        /// </summary>
        public string Nombre { get; set; }
        public int contagiadosCamilla;
        public HashTable<string, Cama> camillas;
        public PriorityQueue<PrioridadCola> colaContagiados;
        public PriorityQueue<PrioridadCola> colaSospechosos;

         
        public List<Cama> CamillasDisponibles = new List<Cama>();

        public List<Cama> CamillasOcupadas = new List<Cama>();



        public Hospital(string NombreHospital)
        {//Crear las 10 camas e incertarlas con su codigo y numero a la hashtable. 
            this.Nombre = NombreHospital;
            this.camillas = new HashTable<string, Cama>(10);
            this.colaContagiados = new PriorityQueue<PrioridadCola>();
            this.colaSospechosos = new PriorityQueue<PrioridadCola>();

            //Inicializar las camillas
            for (int i = 0; i < 10; i++)
            {
                Cama nuevaCama = new Cama(this.Nombre, i + 1);
                camillas.Insert(nuevaCama.Codigo, nuevaCama);
                //Setear como false la disponibilidad de las camillas. 
                CamillasDisponibles = this.camillas.AllDataLikeList().FindAll((cama) =>
                {
                    return (cama.Disponible) ? true : false;
                });

                CamillasOcupadas = this.camillas.AllDataLikeList().FindAll((cama)=> {
                    return (!cama.Disponible) ? true : false;
                });
            }
        }



        private Random rdm = new Random();

        /// <summary>
        /// Este método devuelve un bool para indicar si la prueba resulta positiva o negativa
        /// basado en un entero "probabilidad" al que se le asigna de base 5 y se le suma
        /// una probabilidad extra segun el tipo de posible contagio. 
        /// </summary>
        /// <param name="paciente"></param>
        /// <returns></returns>
        public bool PruebaContagio(PatientInfo paciente)
        {
            int probabilidad = 5 + ContagioValue(paciente);
            int porcentaje = rdm.Next(101);
            return (porcentaje <= probabilidad) ? true : false;
        }
        /// <summary>
        /// Convierte el string de Contagio en un entero 
        /// equivalente al porcentage qe indica el función 
        /// de simulación solicitada en el proyecto.
        /// 5, 10 , 15 o 30.
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        private int ContagioValue(PatientInfo patient)
        {
            string contagio = patient.Contagio;
            int retorno = 0;
            switch (contagio)
            {
                case "ReunionConSospechosos":
                    retorno = 5;
                    break;
                case "Viaje":
                    retorno = 10;
                    break;
                case "ConocidoContagiado":
                    retorno = 15;
                    break;
                case "FamiliarContagiado":
                    retorno = 30;
                    break;
                default:
                    retorno = 0;
                    break;
            }
            return retorno;
        }

        /// <summary>
        /// Retorna el listado de camas que están disponibles. 
        /// </summary>
        /// <returns></returns>
        public List<Cama> CamasDisponibles()
        {
            return this.camillas.AllDataLikeList().FindAll((cama) =>
            {
                return (cama.Disponible) ? true : false;
            });
        }

        public List<Cama> CamasOcupadas()
        {
            return this.camillas.AllDataLikeList().FindAll((cama)=> {

                return (!cama.Disponible) ? true : false;
            });
        }

        /// <summary>
        /// Devuelve un listado de pacientes en cama, para en la ui cambiar el 
        /// estado de los pacientes a recuperado asignar al siguiente en la cola
        /// </summary>
        /// <returns></returns>
        public List<Cama> ListadoDePacientesEnCama()
        {
            return this.camillas.AllDataLikeList().FindAll((cama) =>
            {
                return (cama.Disponible) ? true : false;
            });
        }
       



    }
}