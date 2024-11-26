using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    /*----------------CAMBIOS ELIAS--------------*/
    public abstract class Clases
        {
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public string DireccionCalle { get; set; }
            public string DireccionNro { get; set; }
            public string Telefono { get; set; }
            public string Email { get; set; }
            public DateTime FechaNacimiento { get; set; }
            public string DNI { get; set; }
            public int Perfil { get; set; }
            public string Usuario { get; set; }
            public string Contrasena { get; set; }

            public Clases(string nombre, string apellido, string direccionCalle, string direccionNro, string telefono, string email, DateTime fechaNacimiento, string dni,int perfil,string usuario,string contrasena)
            {
                Nombre = nombre;
                Apellido = apellido;
                DireccionCalle = direccionCalle;
                DireccionNro = direccionNro;
                Telefono = telefono;
                Email = email;
                FechaNacimiento = fechaNacimiento;
                DNI = dni;
                Perfil = perfil;
                Usuario = usuario;
                Contrasena = contrasena;
            }
        }

        public class Alumno : Clases
        {
            public DateTime FechaInscripcion { get; set; }
            public Alumno(string nombre, string apellido, string direccionCalle, string direccionNro, string telefono, string email, DateTime fechaNacimiento, string dni, int perfil, string usuario, string contrasena,DateTime fechaInscripcion)
               : base(nombre, apellido, direccionCalle, direccionNro, telefono, email, fechaNacimiento, dni,perfil,usuario,contrasena)
            {
                FechaInscripcion = fechaInscripcion;
            }
        }

        public class Profesor : Clases
        {
            public int ID { get; set; }
            public Profesor(string nombre, string apellido, string direccionCalle, string direccionNro, string telefono, string email, DateTime fechaNacimiento, string dni, int perfil, string usuario, string contrasena,int id)
               : base(nombre, apellido, direccionCalle, direccionNro, telefono, email, fechaNacimiento, dni, perfil, usuario, contrasena)
            {
                ID = id;
            }
        }

        public class Personal : Clases
        {
            public Personal(string nombre, string apellido, string direccionCalle, string direccionNro, string telefono, string email, DateTime fechaNacimiento, string dni, int perfil, string usuario, string contrasena)
               : base(nombre, apellido, direccionCalle, direccionNro, telefono, email, fechaNacimiento, dni, perfil, usuario, contrasena)
        {
            }
        }

        public class Administrador : Clases
        {
            public Administrador(string nombre, string apellido, string direccionCalle, string direccionNro, string telefono, string email, DateTime fechaNacimiento, string dni, int perfil, string usuario, string contrasena)
               : base(nombre, apellido, direccionCalle, direccionNro, telefono, email, fechaNacimiento, dni, perfil, usuario, contrasena)
            {
            }
        }

        //Clase Usuario
        public class Usuario
        {
            public string NombreUsuario { get; set; }
            public string Contrasenia { get; set; }


            public Usuario(string nombreUsuario, string contrasenia)
            {
                NombreUsuario = nombreUsuario;
                Contrasenia = contrasenia;
            }
        }

    //clase materia
    public class Materia
    {
        public string NombreMateria { get; set; }
        public int AnioCursada { get; set; }
        public int ID_Carrera { get; set; }

        public Materia(string nombreMateria, int anioCursada, int carrera)
        {
            NombreMateria = nombreMateria;
            AnioCursada = anioCursada;
            ID_Carrera = carrera;
        } 
    }

        //clase carrera
        public class Carrera
        {
            public string NombreCarrera { get; set; }
            public string NroResolucion { get; set; }
            public int AnioPlanEstudio { get; set; }

            public Carrera(string nombreCarrera, string nroResolucion, int anioPlanEstudio)
            {
                NombreCarrera = nombreCarrera;
                NroResolucion = nroResolucion;
                AnioPlanEstudio = anioPlanEstudio;
            }
        }
        public class Examen
        {
            public int Matricula {  get; set; }
            public int Id_Materia { get; set; }
            public decimal Nota { get; set; }
            public DateTime Fecha { get; set; }
            public string TipoExamen { get; set; }
            public Examen(int matricula, int id_materia, decimal nota,DateTime fecha, string tipoexamen)
            {
                Matricula = matricula;
                Id_Materia = id_materia;
                Nota = nota;
                Fecha = fecha;
                TipoExamen = tipoexamen;
            }

        }
}
