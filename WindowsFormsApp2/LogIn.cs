using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class LogIn : Form
    {
        public int intentos = 3;
        private ConexionBdd ConexionLogin;
        public LogIn()
        {
            InitializeComponent();
            ConexionLogin = new ConexionBdd();
            this.FormClosing += LogIn_FormClosing;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            txtContrasenia.UseSystemPasswordChar = true;
            ConexionLogin.Abrir();
        }

       
        public Clases ObtenerDatosUsuario(string Usuario)
        {
            Clases usuario = null;
            using (SqlCommand command = new SqlCommand("sp_BuscarDatosUsuario", ConexionLogin.conectorClase))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Usuario", Usuario);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string nombre = reader.GetString(0);
                        string apellido = reader.GetString(1);
                        string direccionCalle = reader.GetString(2);
                        string direccionNro = reader.GetString(3);
                        string telefono = reader.GetString(4);
                        string email = reader.GetString(5);
                        DateTime fechaNacimiento = reader.GetDateTime(6);
                        string dni = reader.GetString(7);
                        int perfil = reader.GetInt32(8);
                        string usuarioLogin = reader.GetString(9);
                        string contrasena = reader.GetString(10);
                        switch (perfil)
                        {
                            case 4:
                                DateTime fechaInscripcion = reader.GetDateTime(11);
                                usuario = new Alumno(nombre, apellido, direccionCalle, direccionNro, telefono, email, fechaNacimiento, dni, perfil, usuarioLogin, contrasena,fechaInscripcion);
                                break;
                            case 3:
                                int id = reader.GetInt32(11);
                                usuario = new Profesor(nombre, apellido, direccionCalle, direccionNro, telefono, email, fechaNacimiento, dni, perfil, usuarioLogin, contrasena,id);
                                break;
                            case 2:
                                usuario = new Personal(nombre, apellido, direccionCalle, direccionNro, telefono, email, fechaNacimiento, dni, perfil, usuarioLogin, contrasena);
                                break;
                            case 1:
                                usuario = new Administrador(nombre, apellido, direccionCalle, direccionNro, telefono, email, fechaNacimiento, dni, perfil, usuarioLogin, contrasena);
                                break;
                        }
                    }
                }
            }

            return usuario;
        }

        public int Validar(string Usuario, string Contrasena)
        {
            int Perfil = 0;
            using (SqlCommand command = new SqlCommand("VerificacionLogin", ConexionLogin.conectorClase))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Usuario", Usuario);
                command.Parameters.AddWithValue("@Contrasena", Contrasena);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Perfil = reader.GetInt32(0);
                    }
                }
            }
            return Perfil;
        }
        
        
        private void btnToggleView_Click(object sender, EventArgs e)
        {
            if (txtContrasenia.UseSystemPasswordChar)
            {
                txtContrasenia.UseSystemPasswordChar = false;
            }
            else
            {
                txtContrasenia.UseSystemPasswordChar= true;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "")
            {
                errorProvider1.SetError(txtUsuario, "Ingrese un usuario");
                return;
            }


            string usuario = txtUsuario.Text;
            string password = txtContrasenia.Text;

            

            VariableGlobal.perfil = Validar(usuario, password);
            
            VariableGlobal.DatosUsuario = ObtenerDatosUsuario(usuario);
            if (VariableGlobal.perfil > 0)
            {
                MenuPrincipal menuPrincipal = new MenuPrincipal();
                menuPrincipal.Show();
                VariableGlobal.entro = true;
                ConexionLogin.Cerrar();
                this.Hide();
            }
            else
            {
                intentos--;
                MessageBox.Show($"Usuario o contraseña incorrectos.\nIntentos restantes : {intentos}");
                if (intentos == 0)
                    this.Close();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Por favor, pongase en contacto con el administrador");
        }

        private void LogIn_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
