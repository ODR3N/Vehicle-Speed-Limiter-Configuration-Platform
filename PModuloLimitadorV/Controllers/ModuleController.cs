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
            // Abrir la conexión serial
            serialPort.Open();

            // Enviar el nuevo límite al Arduino
            serialPort.Write("A" + limite);

            // Cerrar la conexión serial
            serialPort.Close();

            // Redireccionar a la vista de configuración con un mensaje de éxito
            TempData["Message"] = "Límite de velocidad actualizado correctamente.";
            return RedirectToAction("ModuleConfigure");
        }

        [HttpPost]
        public ActionResult UpdateArduinoSettings(int limite, int umbral, float FC, float SetMAF, int delayT_In, int delayT_Out)
        {
            serialPort.Open();
            serialPort.Write("A" + limite);
            serialPort.Write("B" + umbral);
            serialPort.Write("C" + FC);
            serialPort.Write("D" + SetMAF);
            serialPort.Write("E" + delayT_In);
            serialPort.Write("F" + delayT_Out);
            serialPort.Close();

            TempData["Message"] = "Configuración del Arduino actualizada correctamente.";
            return RedirectToAction("ConfigureArduino");
        }
    }
}
