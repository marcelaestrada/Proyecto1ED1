using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Proyecto1ED1.Models;
using Proyecto1ED1.Models.Model;
using CustomGenerics.Structures;

namespace Proyecto1ED1
{
    static class Simulaciones
    {
        static Cama camas = new Cama();
        static Random rdm = new Random();

        /// <summary>
        /// Este método devuelve un bool para indicar si la prueba resulta positiva o negativa
        /// basado en un entero "probabilidad" al que se le asigna de base 5 y se le suma
        /// una probabilidad extra segun el tipo de posible contagio. 
        /// </summary>
        /// <param name="paciente"></param>
        /// <returns></returns>
        static bool PruebaContagio(PatientInfo paciente)
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
        static int ContagioValue(PatientInfo patient)
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

     
        static void AsignacionDeCama(HashTable<string, Cama> camas, PatientInfo paciente)
        {
            if (camas.datos<10)
            {

            }
            else
            {
                //Response.Write("<script>alert('No hay suficientes Camas, por favor espere...')</script>");
            }
        }

    }
}
