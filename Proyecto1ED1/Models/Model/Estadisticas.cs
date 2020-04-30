using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto1ED1.Models.Model
{
    public class Estadisticas
    {
        public string nombre { get; set; }
        public int contagiadosIngresados { get; set; }
        public int sospechososIngresados { get; set; }
        public int sospechososPositivo { get; set; }
        public int egresados { get; set; }
    }
}