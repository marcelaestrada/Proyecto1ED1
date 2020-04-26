using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto1ED1.Models;
using Proyecto1ED1.Models.Model;
using Proyecto1ED1.Helpers;
using CustomGenerics.Interfaces;

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
            Hospital hospitalCorrespondiente = new Hospital();
            PrioridadCola infoCola = new PrioridadCola();

            #region Informacion de registro de paciente
            newPatient.Nombre = collection["Nombre"];
            newPatient.Apellido = collection["Apellido"];
            newPatient.DPI_Partida = long.Parse(collection["DPI_Partida"]);
            newPatient.Departamento = collection["Departamento"];
            newPatient.Municipio = collection["Municipio"];
            newPatient.Sintomas = collection["Sintomas"];
            newPatient.Contagio = collection["Contagio"];
            newPatient.Categoria = collection["Categoria"];
            newPatient.Caracteristica = collection["Caracteristica"];
            #endregion

            #region Informacion de registro para cola 
            infoCola.nombre = newPatient.Nombre;
            infoCola.apellido = newPatient.Apellido;
            infoCola.dpi = newPatient.DPI_Partida;
            infoCola.prioridad = definirPrioridad();
            #endregion

            #region Definicion de hospital correspondiente
            if ((collection["Departamento"] == "Guatemala") || (collection["Departamento"]=="BajaVerapaz")|| (collection["Departamento"] == "Chimaltenango")|| (collection["Departamento"] == "ElProgreso"))
            {
                hospitalCorrespondiente = Storage.Instance.hospitalCapital;
            }
            if((collection["Departamento"] == "Quetzaltenango")|| (collection["Departamento"] == "SanMarcos")|| (collection["Departamento"] == "Retalhuleu")|| (collection["Departamento"] == "Totonicapan")|| (collection["Departamento"] == "Solola"))
            {
                hospitalCorrespondiente = Storage.Instance.hospitalQuetzaltenango;
            }
            if((collection["Departamento"] == "Peten")|| (collection["Departamento"] == "Quiche")|| (collection["Departamento"] == "AltaVerapaz")|| (collection["Departamento"] == "Izabal")|| (collection["Departamento"] == "Huehuetenango"))
            {
                hospitalCorrespondiente = Storage.Instance.hospitalPeten;
            }
            if((collection["Departamento"] == "Escuintla")|| (collection["Departamento"] == "Suchitepequez")|| (collection["Departamento"] == "Sacatepequez")|| (collection["Departamento"] == "SantaRosa"))
            {
                hospitalCorrespondiente = Storage.Instance.hospitalEscuintla;
            }
            if((collection["Departamento"] == "Jalapa")|| (collection["Departamento"] == "Jutiapa")|| (collection["Departamento"] == "Chiquimula")|| (collection["Departamento"] == "Zacapa"))
            {
                hospitalCorrespondiente = Storage.Instance.hospitalOriente;
            }
            #endregion

            #region Envio a cola
            if (newPatient.Categoria == "Contagiado")
            {
                if (hospitalCorrespondiente.contagiadosCamilla < 10)
                {
                    hospitalCorrespondiente.contagiadosCamilla++;
                    hospitalCorrespondiente.camillas.Insert(infoCola.prioridad, infoCola);
                }
                else
                {
                    hospitalCorrespondiente.colaContagiados.Insert(infoCola.prioridad, infoCola.nombre, infoCola.apellido, infoCola.prioridad);
                }
            }
            else if (newPatient.Categoria == "Sospechoso")
            {
                hospitalCorrespondiente.colaSospechosos.Insert(infoCola.prioridad, infoCola.nombre, infoCola.apellido, infoCola.prioridad);
            }
            #endregion

            return View("Registro");
        }

        public long definirPrioridad()
        {
            ///Definicion de fecha y hora de registro de paciente 
            int hora = DateTime.Now.Hour;
            int minutos = DateTime.Now.Minute;
            int segundo = DateTime.Now.Second;
            int dia = DateTime.Now.Day;
            int mes = DateTime.Now.Month;
            int anio = DateTime.Now.Year;

            ///Concatenacion de fecha y hora para conseguir valor de la prioridad
            long prioridad = long.Parse(anio.ToString() + mes.ToString() + dia.ToString() + hora.ToString() + minutos.ToString() + segundo.ToString());

            return prioridad;
        }
    }
}