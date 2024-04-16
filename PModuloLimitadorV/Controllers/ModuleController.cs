using PModuloLimitadorV.Models;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PModuloLimitadorV.Controllers
{
    public class ModuleController : Controller
    {
        // Declarar un objeto SerialPort
        private SerialPort serialPort;

        // Constructor para inicializar el objeto SerialPort
        public ModuleController()
        {
            // Configura el puerto COM y la velocidad 
            serialPort = new SerialPort("COM3", 9600); // Ejemplo de configuración, ajusta según tu entorno
        }

        // GET: Module
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ModuleRead()
        {
            return View();
        }

        public ActionResult ModuleConfigure()
        {
            return View();
        }

        public ActionResult ConfigureArduino()
        {
            return View();
        }


        [HttpGet]
        public ActionResult ObtenerPuertosDisponibles()
        {
            var puertosDisponibles = SerialPort.GetPortNames();

            if (puertosDisponibles.Length > 0)
            {
                return Json(new { ConexionExitosa = true, Puerto = puertosDisponibles[0] }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { ConexionExitosa = false, Mensaje = "No se ha detectado el módulo" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult UpdateLimit(int limite)
        {
            // Obtener los puertos COM disponibles
            var puertosDisponibles = SerialPort.GetPortNames();

            if (puertosDisponibles.Length > 0)
            {
                // Seleccionar el primer puerto disponible
                string puerto = puertosDisponibles[0];

                // Abrir la conexión serial en el puerto seleccionado
                using (var serialPort = new SerialPort(puerto, 9600))
                {
                    serialPort.Open();

                    // Enviar el nuevo límite al Arduino
                    serialPort.Write("A" + limite);

                    // Cerrar la conexión serial
                    serialPort.Close();
                }

                // Redireccionar a la vista de configuración con un mensaje de éxito
                TempData["Message"] = "Límite de velocidad actualizado correctamente.";
                return RedirectToAction("ModuleConfigure");
            }
            else
            {
                TempData["ErrorMessage"] = "No se ha detectado el módulo. Asegúrese de que el módulo esté conectado correctamente.";
                return RedirectToAction("ModuleConfigure");
            }
        }


        [HttpPost]
        public ActionResult UpdateArduinoSettings(int? limite, int? umbral, float? FC, float? SetMAF, int? delayT_In, int? delayT_Out)
        {
            // Obtener los puertos COM disponibles
            var puertosDisponibles = SerialPort.GetPortNames();

            if (puertosDisponibles.Length > 0)
            {
                // Seleccionar el primer puerto disponible
                string puerto = puertosDisponibles[0];

                // Abrir la conexión serial en el puerto seleccionado
                using (var serialPort = new SerialPort(puerto, 9600))
                {
                    serialPort.Open();

                    // Verificar si se proporciona un valor para el límite
                    if (limite.HasValue)
                    {
                        // Enviar el nuevo límite al Arduino
                        serialPort.Write("A" + limite);
                    }

                    // Verificar si se proporciona un valor para el umbral
                    if (umbral.HasValue)
                    {
                        serialPort.Write("B" + umbral);
                    }

                    // Verificar si se proporciona un valor para el FC
                    if (FC.HasValue)
                    {
                        serialPort.Write("C" + FC.Value.ToString("F1"));
                    }

                    // Verificar si se proporciona un valor para el SetMAF
                    if (SetMAF.HasValue)
                    {
                        serialPort.Write("D" + SetMAF.Value.ToString("F1"));
                    }

                    // Verificar si se proporciona un valor para el delayT_In
                    if (delayT_In.HasValue)
                    {
                        serialPort.Write("E" + delayT_In);
                    }

                    // Verificar si se proporciona un valor para el delayT_Out
                    if (delayT_Out.HasValue)
                    {
                        serialPort.Write("F" + delayT_Out);
                    }

                    // Cerrar la conexión serial
                    serialPort.Close();
                }

                TempData["Message"] = "Configuración del Arduino actualizada correctamente.";
                return RedirectToAction("ConfigureArduino");
            }
            else
            {
                TempData["ErrorMessage"] = "No se ha detectado el módulo. Asegúrese de que el módulo esté conectado correctamente.";
                return RedirectToAction("ConfigureArduino");
            }
        }


    }
}
