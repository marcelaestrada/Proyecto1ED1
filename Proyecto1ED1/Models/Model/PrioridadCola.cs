using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto1ED1.Models.Model
{
    public class PrioridadCola : IComparable
    {
        public long prioridad { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public long dpi { get; set; }

        int IComparable.CompareTo(object obj)
        {
            throw new NotImplementedException();
        }
    }
}