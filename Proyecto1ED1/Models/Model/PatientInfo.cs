using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Proyecto1ED1.Models;

namespace Proyecto1ED1.Models
{
    public class PatientInfo
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public long DPI_Partida { get; set; }
        public string Departamento { get; set; }
        public string Municipio { get; set; }
        public string Sintomas { get; set; }
        public string Contagio { get; set; }
        public string Categoria { get; set; }
        public string Caracteristica { get; set; }
    }
}