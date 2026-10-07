using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POOI_T2_Garibay.Models
{
    public class Alumno
    {
        public string dni { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string carrera { get; set; }
        public int ciclo { get; set; } 

        //Constructores
        //constructor vacío
        public Alumno()
        {

        }

        //constructor con parámetros
        public Alumno(string dni, string nombres, string apellidos, string carrera, int ciclo)
        {
            this.dni = dni;
            this.nombres = nombres;
            this.apellidos = apellidos;
            this.carrera = carrera;
            this.ciclo = ciclo;
        }

        public bool validarCampos()
        {
            if (!string.IsNullOrEmpty(this.dni) && !string.IsNullOrEmpty(this.nombres) &&
                !string.IsNullOrEmpty(this.apellidos) && !string.IsNullOrEmpty(this.carrera) && this.ciclo > 0)
            {
                return true;
            }
            return false;
        }
    }
}