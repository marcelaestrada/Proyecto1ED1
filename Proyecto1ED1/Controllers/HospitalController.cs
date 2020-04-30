using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto1ED1.Models;
using Proyecto1ED1.Models.Model;
using Proyecto1ED1.Helpers;

namespace Proyecto1ED1.Controllers
{
    public class HospitalController : Controller
    {
       
        #region Metodos GET
        // GET: Hospital
        public ActionResult Index()
        {
            Storage.Instance.hospitalCapital.Nombre = "Hospital Capital";
            Storage.Instance.hospitalQuetzaltenango.Nombre = "Hospital Quetzaltenango";
            Storage.Instance.hospitalPeten.Nombre = "Hospital Peten";
            Storage.Instance.hospitalEscuintla.Nombre = "Hospital Escuintla";
            Storage.Instance.hospitalOriente.Nombre = "Hospital Oriente";



            return View();
        }
        public ActionResult Registro()
        {
            return View();
        }
        public ActionResult Simulaciones()
        {
            return View();
        }
        public ActionResult Estadisticas()
        {
            return View(Storage.Instance.datos);
        }
        #endregion 

        [HttpPost]
        public ActionResult Registro(FormCollection collection)
        {
            PatientInfo newPatient = new PatientInfo();
            Hospital hospitalCorrespondiente = new Hospital("Hospital");
            PrioridadCola infoCola = new PrioridadCola();
            string infoEstadisticas = "";

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
            infoCola.prioridad = definirPrioridad(newPatient.Caracteristica);
            #endregion

            #region Definicion de hospital correspondiente
            if ((collection["Departamento"] == "Guatemala") || (collection["Departamento"] == "BajaVerapaz") || (collection["Departamento"] == "Chimaltenango") || (collection["Departamento"] == "ElProgreso"))
            {
                hospitalCorrespondiente = Storage.Instance.hospitalCapital;
                infoEstadisticas = "Capital";

            }
            if ((collection["Departamento"] == "Quetzaltenango") || (collection["Departamento"] == "SanMarcos") || (collection["Departamento"] == "Retalhuleu") || (collection["Departamento"] == "Totonicapan") || (collection["Departamento"] == "Solola"))
            {
                hospitalCorrespondiente = Storage.Instance.hospitalQuetzaltenango;
                infoEstadisticas = "Quetzaltenango";
            }
            if ((collection["Departamento"] == "Peten") || (collection["Departamento"] == "Quiche") || (collection["Departamento"] == "AltaVerapaz") || (collection["Departamento"] == "Izabal") || (collection["Departamento"] == "Huehuetenango"))
            {
                hospitalCorrespondiente = Storage.Instance.hospitalPeten;
                infoEstadisticas = "Peten";
            }
            if ((collection["Departamento"] == "Escuintla") || (collection["Departamento"] == "Suchitepequez") || (collection["Departamento"] == "Sacatepequez") || (collection["Departamento"] == "SantaRosa"))
            {
                hospitalCorrespondiente = Storage.Instance.hospitalEscuintla;
                infoEstadisticas = "Escuintla";
            }
            if ((collection["Departamento"] == "Jalapa") || (collection["Departamento"] == "Jutiapa") || (collection["Departamento"] == "Chiquimula") || (collection["Departamento"] == "Zacapa"))
            {
                hospitalCorrespondiente = Storage.Instance.hospitalOriente;
                infoEstadisticas = "Oriente";
            }
            #endregion

            #region Envio a cola
            if (newPatient.Categoria == "Contagiado")
            {
                Storage.Instance.datos.contagiadosIngresados++;
                #region Agregar datos para estadisticas
                if (infoEstadisticas == "Capital")
                {
                    Storage.Instance.datosCapital.contagiadosIngresados++;
                }
                else if (infoEstadisticas == "Quetzaltenango")
                {
                    Storage.Instance.datosQuetzaltenango.contagiadosIngresados++;
                }
                else if (infoEstadisticas == "Escuintla")
                {
                    Storage.Instance.datosEscuintla.contagiadosIngresados++;
                }
                else if (infoEstadisticas == "Peten")
                {
                    Storage.Instance.datosPeten.contagiadosIngresados++;
                }
                else if (infoEstadisticas == "Oriente")
                {
                    Storage.Instance.datosOriente.contagiadosIngresados++;
                }
                #endregion

                if (hospitalCorrespondiente.contagiadosCamilla < 10)
                {
                    hospitalCorrespondiente.contagiadosCamilla++;


                    //Encontrar primer camilla libre y sacar su código. 
                    Cama camaDisponible = hospitalCorrespondiente.camillas.AllDataLikeList().Find((dato) =>
                    {
                        return (dato.Disponible) ? true : false;
                    });


                    //Llamar al hashTable del hospital Correspondiente....
                    //Hacer un search de la camilla con ese código y al objeto de retorno ingresarle el paciente.
                    hospitalCorrespondiente.camillas.Search(camaDisponible.Codigo).PacienteActual = newPatient;
                    hospitalCorrespondiente.camillas.Search(camaDisponible.Codigo).Disponible = false;

                    hospitalCorrespondiente.CamillasDisponibles = hospitalCorrespondiente.CamasDisponibles();
                    var s = Storage.Instance.hospitalCapital;

                    int flag = 0;
                    // Hospital hospitalBandera = Storage.Instance.hospitalCapital;

                }
                else
                {
                    hospitalCorrespondiente.colaContagiados.Insert(infoCola.prioridad, infoCola);
                }
            }
            else if (newPatient.Categoria == "Sospechoso")
            {
                Storage.Instance.datos.sospechososIngresados++;
                #region Agregar datos para estadisticas
                if (infoEstadisticas == "Capital")
                {
                    Storage.Instance.datosCapital.sospechososIngresados++;
                }
                else if (infoEstadisticas == "Quetzaltenango")
                {
                    Storage.Instance.datosQuetzaltenango.sospechososIngresados++;
                }
                else if (infoEstadisticas == "Escuintla")
                {
                    Storage.Instance.datosEscuintla.sospechososIngresados++;
                }
                else if (infoEstadisticas == "Peten")
                {
                    Storage.Instance.datosPeten.sospechososIngresados++;
                }
                else if (infoEstadisticas == "Oriente")
                {
                    Storage.Instance.datosOriente.sospechososIngresados++;
                }
                #endregion

                hospitalCorrespondiente.colaSospechosos.Insert(infoCola.prioridad, infoCola);
            }
            #endregion

            return View("Registro");
        }

        public long definirPrioridad(string categoria)
        {
            ///Definicion por categoria
            double porCategoria = 0.0;

            if (categoria == "3eraEdad")
            {
                porCategoria = 0.1;
            }
            if (categoria == "RecienNacido")
            {
                porCategoria = 0.2;
            }
            if (categoria == "Adulto")
            {
                porCategoria = 0.3;
            }
            if (categoria == "Ninio_Joven")
            {
                porCategoria = 0.4;
            }

            ///Definicion de fecha y hora de registro de paciente 

            int hora;
            int minutos;
            int segundo;
            int dia;
            int mes;
            int anio = DateTime.Now.Year;

            if (DateTime.Now.Hour < 10)
            {
                hora = int.Parse(0.ToString() + DateTime.Now.Hour.ToString());
            }
            else
            {
                hora = DateTime.Now.Hour;
            }
            if (DateTime.Now.Minute < 10)
            {
                minutos = int.Parse(0.ToString() + DateTime.Now.Minute.ToString());
            }
            else
            {
                minutos = DateTime.Now.Minute;
            }
            if (DateTime.Now.Second < 10)
            {
                segundo = int.Parse(0.ToString() + DateTime.Now.Second.ToString());
            }
            else
            {
                segundo = DateTime.Now.Second;
            }
            if (DateTime.Now.Day < 10)
            {
                dia = int.Parse(0.ToString() + DateTime.Now.Day.ToString());
            }
            else
            {
                dia = DateTime.Now.Day;
            }
            if (DateTime.Now.Month < 10)
            {
                mes = int.Parse(0.ToString() + DateTime.Now.Month.ToString());
            }
            else
            {
                mes = DateTime.Now.Month;
            }

            //Para evitar System.FormatException
            porCategoria *= 10;
            ///Concatenacion de fecha y hora para conseguir valor de la prioridad
            long prioridad = long.Parse(porCategoria.ToString() + anio.ToString() + mes.ToString() + dia.ToString() + hora.ToString() + minutos.ToString() + segundo.ToString());

            return prioridad;
        }
        public ActionResult Simulacion()
        {
            return View("Simulaciones");
        }

        public ActionResult Hospitales()
        {
            return View();
        }

        #region ActionResults a menu de hospitales

        public ActionResult HospitalCapital()
        {
            Storage.Instance.hospitalSeleccionado = "HospitalCapital";

            return
                View("MenuHospital", Storage.Instance.hospitalCapital);
        }
        public ActionResult HospitalQuetzaltenango()
        {
             Storage.Instance.hospitalSeleccionado = "HospitalQuetzaltenango";
            return View("MenuHospital", Storage.Instance.hospitalQuetzaltenango);
        }
        public ActionResult HospitalPeten()
        {
            Storage.Instance.hospitalSeleccionado = "HospitalPeten";
            return View("MenuHospital", Storage.Instance.hospitalPeten);
        }
        public ActionResult HospitalEscuintla()
        {
             Storage.Instance.hospitalSeleccionado = "HospitalEscuintla";
            return View("MenuHospital", Storage.Instance.hospitalEscuintla);
        }
        public ActionResult HospitalOriente()
        {
             Storage.Instance.hospitalSeleccionado = "HospitalOriente";
            return View("MenuHospital", Storage.Instance.hospitalOriente);
        }


        #endregion

        public ActionResult CamasDisponibles()
        {
            switch (Storage.Instance.hospitalSeleccionado)
            {
                case "HospitalCapital":
                    return View("ListaCamasDisponibles", Storage.Instance.hospitalCapital.CamillasDisponibles);

                case "HospitalQuetzaltenango":
                    return View("ListaCamasDisponibles", Storage.Instance.hospitalQuetzaltenango.CamillasDisponibles);

                case "HospitalPeten":
                    return View("ListaCamasDisponibles", Storage.Instance.hospitalEscuintla.CamillasDisponibles);

                case "HospitalEscuintla":
                    return View("ListaCamasDisponibles", Storage.Instance.hospitalEscuintla.CamillasDisponibles);

                case "HospitalOriente":
                    return View("ListaCamasDisponibles", Storage.Instance.hospitalOriente.CamillasDisponibles);

                default:
                    return View();

            }
        }

    }
}