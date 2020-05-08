using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto1ED1.Models;
using Proyecto1ED1.Models.Model;
using Proyecto1ED1.Helpers;
using CustomGenerics.Structures;

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
            Estadisticas datosEstadisticas = Storage.Instance.datos;
            return View(datosEstadisticas);
        }
        public ActionResult EstadisticaGeneralHospital()
        {
            Storage.Instance.estadisticaGeneral.Capital = Storage.Instance.datosCapital;
            Storage.Instance.estadisticaGeneral.Escuintla = Storage.Instance.datosEscuintla;
            Storage.Instance.estadisticaGeneral.Oriente = Storage.Instance.datosOriente;
            Storage.Instance.estadisticaGeneral.Quetzaltenango = Storage.Instance.datosQuetzaltenango;
            Storage.Instance.estadisticaGeneral.Peten = Storage.Instance.datosPeten;
            return View(Storage.Instance.estadisticaGeneral);
        }
        public ActionResult EstadisticaPorHospital()
        {
            return View();
        }
        public ActionResult MenuBusquedas()
        {
            return View();
        }
        public ActionResult Busquedas()
        {
            return View();
        }
        public ActionResult Simulacion()
        {
            return View("Simulaciones");
        }
        public ActionResult Hospitales()
        {
            return View();
        }
        #endregion

        #region Registro
        [HttpPost]
        public ActionResult Registro(FormCollection collection)
        {
            //Si dpi existe, alertar e inflar la vista otra vez. 
            if (Storage.Instance.dataPacientes.Search(long.Parse(collection["DPI_Partida"])))
            {
                Response.Write("<script>alert('El DPI ingresado ya existe, intente ingresar otro paciente...')</script>");
                return View("Registro");
            }

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
            newPatient.Estado = newPatient.Categoria;
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
            if ((collection["Departamento"] == "Quetzaltenango") || (collection["Departamento"] == "SanMarcos") || (collection["Departamento"] == "Retalhuleu") || (collection["Departamento"] == "Totonicapán") || (collection["Departamento"] == "Sololá"))
            {
                hospitalCorrespondiente = Storage.Instance.hospitalQuetzaltenango;
                infoEstadisticas = "Quetzaltenango";
            }
            if ((collection["Departamento"] == "Petén") || (collection["Departamento"] == "Quiché") || (collection["Departamento"] == "AltaVerapaz") || (collection["Departamento"] == "Izabal") || (collection["Departamento"] == "Huehuetenango"))
            {
                hospitalCorrespondiente = Storage.Instance.hospitalPeten;
                infoEstadisticas = "Petén";
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
                else if (infoEstadisticas == "Petén")
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

                    //Enviar datos del Paciente a camilla.

                    //Encontrar primer camilla libre y sacar su código. 
                    Cama camaDisponible = hospitalCorrespondiente.camillas.AllDataLikeList().Find((dato) =>
                    {
                        return (dato.Disponible) ? true : false;
                    });

                    //Llamar al hashTable del hospital Correspondiente....
                    //Hacer un search de la camilla con ese código y al objeto de retorno ingresarle el paciente.
                    hospitalCorrespondiente.camillas.Search(camaDisponible.Codigo).PacienteActual = infoCola;
                    hospitalCorrespondiente.camillas.Search(camaDisponible.Codigo).Disponible = false;

                    hospitalCorrespondiente.CamillasDisponibles = hospitalCorrespondiente.CamasDisponibles();
                    hospitalCorrespondiente.CamillasOcupadas = hospitalCorrespondiente.CamasOcupadas();

                   

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
                else if (infoEstadisticas == "Petén")
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
            Storage.Instance.dataPacientes.Insert(newPatient.Nombre, newPatient.Apellido, newPatient.DPI_Partida, newPatient);
            return View("Index");
        }

        public long definirPrioridad(string categoria)
        {
            ///Definicion por categoria
            double porCategoria = 0.0;

            if (categoria == "TerceraEdad")
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

            string aux = "";
            int hora;
            int minutos;
            int segundo;
            int dia;
            int mes;
            int anio = DateTime.Now.Year;

            if (DateTime.Now.Hour < 10)
            {
                aux = "0" + DateTime.Now.Hour.ToString();
                hora = Convert.ToInt32(aux);
            }
            else
            {
                hora = DateTime.Now.Hour;
            }
            if (DateTime.Now.Minute < 10)
            {
                aux = "0" + DateTime.Now.Minute.ToString();
                minutos = Convert.ToInt32(aux);
            }
            else
            {
                minutos = DateTime.Now.Minute;
            }
            if (DateTime.Now.Second < 10)
            {
                aux = "0" + DateTime.Now.Second.ToString();
                segundo = Convert.ToInt32(aux);
            }
            else
            {
                segundo = DateTime.Now.Second;
            }
            if (DateTime.Now.Day < 10)
            {
                aux = "0" + DateTime.Now.Day.ToString();
                dia = Convert.ToInt32(aux);
            }
            else
            {
                dia = DateTime.Now.Day;
            }
            if (DateTime.Now.Month < 10)
            {
                aux = "0" + DateTime.Now.Month.ToString();
                mes = Convert.ToInt32(aux);
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
        #endregion

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

        public ActionResult CamasDisponibles()
        {
            switch (Storage.Instance.hospitalSeleccionado)
            {
                case "HospitalCapital":
                    return View("ListaCamasDisponibles", Storage.Instance.hospitalCapital.CamillasDisponibles);

                case "HospitalQuetzaltenango":
                    return View("ListaCamasDisponibles", Storage.Instance.hospitalQuetzaltenango.CamillasDisponibles);

                case "HospitalPeten":
                    return View("ListaCamasDisponibles", Storage.Instance.hospitalPeten.CamillasDisponibles);

                case "HospitalEscuintla":
                    return View("ListaCamasDisponibles", Storage.Instance.hospitalEscuintla.CamillasDisponibles);

                case "HospitalOriente":
                    return View("ListaCamasDisponibles", Storage.Instance.hospitalOriente.CamillasDisponibles);

                default:
                    return View();

            }
        }

        public ActionResult CamasOcupadas()
        {
            switch (Storage.Instance.hospitalSeleccionado)
            {
                case "HospitalCapital":
              
                    return View("CamasOcupadas", Storage.Instance.hospitalCapital.CamillasOcupadas );

                case "HospitalQuetzaltenango":
                    return View("CamasOcupadas", Storage.Instance.hospitalQuetzaltenango.CamillasOcupadas);

                case "HospitalPeten":
                    return View("CamasOcupadas", Storage.Instance.hospitalPeten.CamillasOcupadas);

                case "HospitalEscuintla":
                    return View("CamasOcupadas", Storage.Instance.hospitalEscuintla.CamillasOcupadas);

                case "HospitalOriente":
                    return View("CamasOcupadas", Storage.Instance.hospitalOriente.CamillasOcupadas);

                default:
                    return View();

            }
        }



        public Hospital HospitalCambioEstado()
        {
            //En los case se puede agregar las estadisticas.
            switch (Storage.Instance.hospitalSeleccionado)
            {
                case "HospitalCapital":
                    
                    return Storage.Instance.hospitalCapital;
                case "HospitalQuetzaltenango":
                    
                    return Storage.Instance.hospitalQuetzaltenango;
                case "HospitalPeten":
                    Storage.Instance.estadisticaGeneral.Peten.egresados++;
                    return Storage.Instance.hospitalPeten;
                case "HospitalEscuintla":
                    
                    return Storage.Instance.hospitalEscuintla;
                case "HospitalOriente":
                    
                    return Storage.Instance.hospitalOriente;
                default:
                    Hospital hospital = new Hospital("Default");
                    return hospital;
            }
        }

        public ActionResult CambiarEstado(long id)
        {
            Storage.Instance.datos.egresados++;
            
           long dpi =  HospitalCambioEstado().camillas.Search(id.ToString()).PacienteActual.dpi;
           
            NodeAVL<PatientInfo> nodoPaciente = Storage.Instance.dataPacientes.SearchOneValue(dpi);
            nodoPaciente.value.Estado = "Recuperado";

            HospitalCambioEstado().camillas.Search(id.ToString()).Disponible = true;
            HospitalCambioEstado().camillas.Search(id.ToString()).PacienteActual = null;

           HospitalCambioEstado().CamillasDisponibles = HospitalCambioEstado().CamasDisponibles();
           HospitalCambioEstado().CamillasOcupadas = HospitalCambioEstado().CamasOcupadas();

            return View("MenuHospital");

        }
        #endregion

        #region Estadisticas
        [HttpPost]
        public ActionResult Estadisticas(FormCollection collection)
        {
            if (collection["nombre"] == "Capital")
            {
                return View("EstadisticaPorHospital", (Storage.Instance.datosCapital));
            }
            else if (collection["nombre"] == "Quetzaltenango")
            {
                return View("EstadisticaPorHospital", (Storage.Instance.datosQuetzaltenango));
            }
            else if (collection["nombre"] == "Peten")
            {
                return View("EstadisticaPorHospital", (Storage.Instance.datosPeten));
            }
            else if (collection["nombre"] == "Escuintla")
            {
                return View("EstadisticaPorHospital", (Storage.Instance.datosEscuintla));
            }
            else if (collection["nombre"] == "Oriente")
            {
                return View("EstadisticaPorHospital", (Storage.Instance.datosOriente));
            }
            else
            {
                return View();
            }
        }
        #endregion

        #region Simulaciones
        public void PruebaPorHospital(Hospital hospital, Estadisticas data)
        {
            try
            {

                PrioridadCola pacienteSeleccionado = hospital.colaSospechosos.Peek();
                long dpi = pacienteSeleccionado.dpi;
                PatientInfo patient = Storage.Instance.dataPacientes.Busqueda("dpi", dpi.ToString())[0];
                bool prueba = hospital.PruebaContagio(patient);

                if (prueba)
                {
                    Storage.Instance.datos.sospechososPositivo++;
                    Storage.Instance.datos.contagiadosIngresados++;
                    data.sospechososPositivo++;
                    data.contagiadosIngresados++;


                    if (hospital.contagiadosCamilla < 10)
                    {
                        hospital.contagiadosCamilla++;

                        //Encontrar primer camilla libre y sacar su código. 
                        Cama camaDisponible = hospital.camillas.AllDataLikeList().Find((dato) =>
                        {
                            return (dato.Disponible) ? true : false;
                        });

                        //Llamar al hashTable del hospital Correspondiente....
                        //Hacer un search de la camilla con ese código y al objeto de retorno ingresarle el paciente.
                        hospital.camillas.Search(camaDisponible.Codigo).PacienteActual = pacienteSeleccionado;
                        hospital.camillas.Search(camaDisponible.Codigo).Disponible = false;

                        hospital.CamillasDisponibles = hospital.CamasDisponibles();



                    }
                    else
                    {
                        hospital.colaContagiados.Insert(pacienteSeleccionado.prioridad, pacienteSeleccionado);

                        hospital.colaSospechosos.Delete();
                    }
                    Response.Write("<script>alert('El paciente que era sospechoso y seguía en la cola ha resultado positivo para el Covid - 19')</script>");
                    //TODO: Revisar si es necesario este cambio de estado....
                    NodeAVL<PatientInfo> nodoPaciente = Storage.Instance.dataPacientes.SearchOneValue(dpi);
                    nodoPaciente.value.Estado = "No Recuperado";

                    NodeAVL<PatientInfo> nodoPacienteFlag = Storage.Instance.dataPacientes.SearchOneValue(dpi);

                }
                else
                {
                    Storage.Instance.datos.sospechososNegativo++;
                    data.sospechososNegativo++;

                    Response.Write("<script>alert('La prueba ha salido negativa')</script>");
                    hospital.colaSospechosos.Delete();
                    //TODO: Revisar el string de este estado...
                    NodeAVL<PatientInfo> nodoPaciente = Storage.Instance.dataPacientes.SearchOneValue(dpi);
                    nodoPaciente.value.Estado = "Sospechoso Negativo";

                    NodeAVL<PatientInfo> nodoPacienteFlag = Storage.Instance.dataPacientes.SearchOneValue(dpi);

                }

            }
            catch (Exception)
            {
                Response.Write("<script>alert('No se puede realizar la prueba, no hay sospechosos en la cola.')</script>");

            }


        }

        public ActionResult RealizarUnaPrueba()
        {
            switch (Storage.Instance.hospitalSeleccionado)
            {
                case "HospitalCapital":

                    PruebaPorHospital(Storage.Instance.hospitalCapital, Storage.Instance.datosCapital);

                    break;

                case "HospitalQuetzaltenango":
                    PruebaPorHospital(Storage.Instance.hospitalQuetzaltenango, Storage.Instance.datosQuetzaltenango);

                    break;

                case "HospitalPeten":
                    PruebaPorHospital(Storage.Instance.hospitalPeten, Storage.Instance.datosPeten);

                    break;

                case "HospitalEscuintla":
                    PruebaPorHospital(Storage.Instance.hospitalEscuintla, Storage.Instance.datosEscuintla);

                    break;

                case "HospitalOriente":
                    PruebaPorHospital(Storage.Instance.hospitalOriente, Storage.Instance.datosOriente);
                    break;
            }

            return View("MenuHospital");
        }

        #endregion

        #region Busquedas
        [HttpPost]
        public ActionResult MenuBusquedas(FormCollection collection)
        {
            List<PatientInfo> pacientesEncontrados = new List<PatientInfo>();
            pacientesEncontrados = null;
            PatientInfo estaVacio = new PatientInfo();

            estaVacio.Nombre = "No hay coincidencias";
            estaVacio.Apellido = "";
            estaVacio.DPI_Partida = 000;
            estaVacio.Departamento = "";
            estaVacio.Municipio = "";
            estaVacio.Sintomas = "";
            estaVacio.Contagio = "";
            estaVacio.Categoria = "";
            estaVacio.Caracteristica = "";
            estaVacio.Estado = "";

            if (collection["Parametro"] == "nombre")
            {
                pacientesEncontrados = Storage.Instance.dataPacientes.Busqueda("nombre", collection["Valor"]);
            }
            else if (collection["Parametro"] == "apellido")
            {
                pacientesEncontrados = Storage.Instance.dataPacientes.Busqueda("apellido", collection["Valor"]);
            }
            else if (collection["Parametro"] == "dpi")
            {
                pacientesEncontrados = Storage.Instance.dataPacientes.Busqueda("dpi", collection["Valor"]);
            }

            if (pacientesEncontrados == null)
            {
                pacientesEncontrados.Add(estaVacio);
            }

            return View("Busquedas", pacientesEncontrados);
        }
        #endregion


    }
}