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
        // GET: Module
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult ModuleRead()
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


    }

}