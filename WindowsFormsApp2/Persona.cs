using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
        public abstract class Persona
        {
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public string DireccionCalle { get; set; }
            public int DireccionNro { get; set; }
            public string Telefono { get; set; }
            public string Email { get; set; }
            public DateTime FechaNacimiento { get; set; }
            public int DNI { get; set; }

            public Persona(string nombre, string apellido, string direccionCalle, int direccionNro, string telefono, string email, DateTime fechaNacimiento, int dni)
            {
                Nombre = nombre;
                Apellido = apellido;
                DireccionCalle = direccionCalle;
                DireccionNro = direccionNro;
                Telefono = telefono;
                Email = email;
                FechaNacimiento = fechaNacimiento;
                DNI = dni;
            }
        }

        public class Alumno : Persona
        {
            public Alumno(string nombre, string apellido, string direccionCalle, int direccionNro, string telefono, string email, DateTime fechaNacimiento, int dni)
               : base(nombre, apellido, direccionCalle, direccionNro, telefono, email, fechaNacimiento, dni)
            {
            }
        }

        public class Profesor : Persona
        {
            public Profesor(string nombre, string apellido, string direccionCalle, int direccionNro, string telefono, string email, DateTime fechaNacimiento, int dni)
               : base(nombre, apellido, direccionCalle, direccionNro, telefono, email, fechaNacimiento, dni)
            {
            }
        }

        public class Personal : Persona
        {
            public Personal(string nombre, string apellido, string direccionCalle, int direccionNro, string telefono, string email, DateTime fechaNacimiento, int dni)
               : base(nombre, apellido, direccionCalle, direccionNro, telefono, email, fechaNacimiento, dni)
            {
            }
        }

        public class Administrador : Persona
        {
            public Administrador(string nombre, string apellido, string direccionCalle, int direccionNro, string telefono, string email, DateTime fechaNacimiento, int dni)
               : base(nombre, apellido, direccionCalle, direccionNro, telefono, email, fechaNacimiento, dni)
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

            public bool VerificarContraseña(string contrasenia)
            {
                return Contrasenia == contrasenia;
            }
        }
}
