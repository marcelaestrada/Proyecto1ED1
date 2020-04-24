using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto1ED1.Models;

namespace Proyecto1ED1.Controllers
{
    public class HospitalController : Controller
    {
        #region Metodos GET
        // GET: Hospital
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Registro()
        {
            return View();
        }
        #endregion 

        [HttpPost]
        public ActionResult Registro(FormCollection collection)
        {
            PatientInfo newPatient = new PatientInfo();
            string hospitalCorrespondiente = "";
            int prioridad = 0;

            newPatient.Nombre = collection["Nombre"];
            newPatient.Apellido = collection["Apellido"];
            newPatient.DPI_Partida = Convert.ToInt32(collection["DPI_Partida"]);
            newPatient.Departamento = collection["Departamento"];
            newPatient.Municipio = collection["Municipio"];
            newPatient.Contagio = collection["Contagio"];
            newPatient.Sintomas = collection["Sintomas"];
            newPatient.Categoria = collection["Categoria"];
            newPatient.Caracteristica = collection["Caracteristica"];

            #region Hospital Correspondiente
            if ((collection["Departamento"] == "Guatemala") || (collection["Departamento"]=="BajaVerapaz")|| (collection["Departamento"] == "Chimaltenango")|| (collection["Departamento"] == "ElProgreso"))
            {
                hospitalCorrespondiente = "Capital";
            }
            if((collection["Departamento"] == "Quetzaltenango")|| (collection["Departamento"] == "SanMarcos")|| (collection["Departamento"] == "Retalhuleu")|| (collection["Departamento"] == "Totonicapan")|| (collection["Departamento"] == "Solola"))
            {
                hospitalCorrespondiente = "Quetzaltenango";
            }
            if((collection["Departamento"] == "Peten")|| (collection["Departamento"] == "Quiche")|| (collection["Departamento"] == "AltaVerapaz")|| (collection["Departamento"] == "Izabal")|| (collection["Departamento"] == "Huehuetenango"))
            {
                hospitalCorrespondiente = "Peten";
            }
            if((collection["Departamento"] == "Escuintla")|| (collection["Departamento"] == "Suchitepequez")|| (collection["Departamento"] == "Sacatepequez")|| (collection["Departamento"] == "SantaRosa"))
            {
                hospitalCorrespondiente = "Escuintla";
            }
            if((collection["Departamento"] == "Jalapa")|| (collection["Departamento"] == "Jutiapa")|| (collection["Departamento"] == "Chiquimula")|| (collection["Departamento"] == "Zacapa"))
            {
                hospitalCorrespondiente = "Oriente";
            }
            #endregion

            #region Definición de prioridad
            //Definir si es contagiado o sospechoso
            //Definir prioridad como la hora y fecha de ingreso
            //Asignarse a la cola de prioridad correspondiente al hospital correspondiente (puede ser a la de contagiados o sospechosos)
            #endregion

            return View("Index");
        }
    }
}