using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto1ED1.Models.Model
{
    public class Cama : IComparable
    {
        public string Key { get; set; }
        public int NumeroDeCama { get; set; }
        public bool Disponible { get; set; }
        public PatientInfo PacienteActual { get; set; }

        public int CompareTo(object obj)
        {
            return Key.CompareTo(obj);
        }
    }
}