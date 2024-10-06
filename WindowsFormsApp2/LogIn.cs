using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            txtContrasenia.UseSystemPasswordChar = true;
            ConexionLogin.Abrir();
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

            VariableGlobal.perfil = txtUsuario.Text;
            string password = txtContrasenia.Text;
            
            if (VariableGlobal.perfil == "admin" && password == "admin")
            {
                MenuPrincipal menuPrincipal = new MenuPrincipal();
                menuPrincipal.Show();
                VariableGlobal.entro = true;
                ConexionLogin.Cerrar();
                this.Close();
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
    }
}
