using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto1ED1.Models.Model
{
    public class Cama : IComparable
    {
       
        public string Codigo { get; set; }
        public int id { get; set; }
        public bool Disponible { get; set; }
        public PrioridadCola PacienteActual { get; set; }

        public Cama(string nombreHospital, int numeroCama)
        {
            id = numeroCama;
            Disponible = true;
            Codigo = CreateHash(nombreHospital,numeroCama).ToString();
            PacienteActual = null;
        }

        private string CreateHash(string nombreHospital, int numeroCama)
        {
            string retorno = "";
            int sumaDeElementos = 0;

            for (int i = 0; i < nombreHospital.Length; i++)
                sumaDeElementos += Convert.ToInt32(nombreHospital[i]) * numeroCama;

            return retorno = "001" + sumaDeElementos.ToString();
        }

        public int CompareTo(object obj)
        {
            return Codigo.CompareTo(obj);
        }
    }
}