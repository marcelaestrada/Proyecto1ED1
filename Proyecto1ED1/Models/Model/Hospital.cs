using System;
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

        public int contagiadosCamilla;
        public HashTable<long,PrioridadCola> camillas = new HashTable<long,PrioridadCola>(10);
        public PriorityQueue<PrioridadCola> colaContagiados = new PriorityQueue<PrioridadCola>();
        public PriorityQueue<PrioridadCola> colaSospechosos = new PriorityQueue<PrioridadCola>();
    }
}