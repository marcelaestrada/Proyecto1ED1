using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto1ED1.Models.Model
{
    public class PrioridadCola : IComparable
    {
        int prioridad { get; set; }
        string nombre { get; set; }
        string apellido { get; set; }
        long dpi { get; set; }

        int IComparable.CompareTo(object obj)
        {
            throw new NotImplementedException();
        }
    }
}