using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using POOI_T2_Garibay.Models;
using Newtonsoft.Json;

namespace POOI_T2_Garibay.Controllers
{
    public class AlumnoController : Controller
    {
        static string jAlumno = @"[]";
        public ActionResult Index()
        {
            string mensaje = string.Empty;

            try
            {
                List<Alumno> temporal = new List<Alumno>();

                if (!string.IsNullOrEmpty(jAlumno))
                {
                    temporal = JsonConvert.DeserializeObject<List<Alumno>>(jAlumno);
                }

                return View(temporal);
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }

            ViewBag.mensaje = mensaje;

            return View(new List<Alumno>());
        }

        public ActionResult Agregar()
        {
            return View(new Alumno());
        }

        [HttpPost][ValidateAntiForgeryToken]
        public ActionResult Agregar(Alumno reg)
        {
            string mensaje = string.Empty;

            if (!reg.validarCampos())
            {
                ViewBag.mensaje = "Complete correctamente todos los campos";
                return View(reg);
            }

            try
            {
                //Deserializar
                List<Alumno> temporal = JsonConvert.DeserializeObject<List<Alumno>>(jAlumno);

                if (temporal.Any(x => x.dni == reg.dni))
                {
                    ViewBag.mensaje = "El DNI ya se encuentra registrado";
                    return View(reg);
                }

                Alumno nuevo = new Alumno(
                    reg.dni,
                    reg.nombres,
                    reg.apellidos,
                    reg.carrera,
                    reg.ciclo
                );

                //Agregar
                temporal.Add(nuevo);

                //Serializar
                jAlumno = JsonConvert.SerializeObject(temporal);
                mensaje = "Alumno registrado";
            }
            catch (JsonException ex)
            {
                mensaje = ex.Message;
            }

            ViewBag.mensaje = mensaje;

            return View(reg);
        }

        public ActionResult Eliminar(string dni)
        {
            try
            {
                List<Alumno> temporal = JsonConvert.DeserializeObject<List<Alumno>>(jAlumno);
                Alumno alumno = temporal.FirstOrDefault(x => x.dni == dni);

                if (alumno != null)
                {
                    temporal.Remove(alumno);
                    jAlumno = JsonConvert.SerializeObject(temporal);
                }
            }
            catch (Exception ex)
            {
                TempData["mensaje"] = ex.Message;
            }

            return RedirectToAction("Index");
        }

        public ActionResult Detalles(string dni)
        {
            string mensaje = string.Empty;

            try
            {
                List<Alumno> temporal = JsonConvert.DeserializeObject<List<Alumno>>(jAlumno);
                Alumno reg = temporal.FirstOrDefault(x => x.dni == dni);

                if (reg == null)
                {
                    ViewBag.mensaje = "Alumno no encontrado";
                    return View(new Alumno());
                }

                return View(reg);
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }

            ViewBag.mensaje = mensaje;

            return View(new Alumno());
        }

        public ActionResult Actualizar(string dni)
        {
            try
            {
                List<Alumno> temporal = JsonConvert.DeserializeObject<List<Alumno>>(jAlumno);
                Alumno reg =temporal.FirstOrDefault(x => x.dni == dni);

                return View(reg);
            }
            catch (Exception ex)
            {
                ViewBag.mensaje = ex.Message;
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Actualizar(Alumno reg, string dniOriginal)
        {
            string mensaje = string.Empty;

            if (!reg.validarCampos())
            {
                ViewBag.mensaje = "Complete correctamente todos los campos";
                return View(reg);
            }

            try
            {
                List<Alumno> temporal = JsonConvert.DeserializeObject<List<Alumno>>(jAlumno);

                // Verificar DNI
                if (temporal.Any(x => x.dni == reg.dni &&
                                      x.dni != dniOriginal))
                {
                    ViewBag.mensaje =
                        "El DNI ya se encuentra registrado";

                    return View(reg);
                }

                // Buscar el registro original
                Alumno alumno =
                    temporal.FirstOrDefault(
                        x => x.dni == dniOriginal
                    );

                if (alumno != null)
                {
                    alumno.dni = reg.dni;
                    alumno.nombres = reg.nombres;
                    alumno.apellidos = reg.apellidos;
                    alumno.carrera = reg.carrera;
                    alumno.ciclo = reg.ciclo;

                    jAlumno =JsonConvert.SerializeObject(temporal);
                    mensaje = "Alumno actualizado correctamente";
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }

            ViewBag.mensaje = mensaje;

            return View(reg);
        }

    }
}