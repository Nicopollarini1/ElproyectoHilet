using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace WindowsFormsApp2
{
    public partial class agregar : Form
    {
        public agregar()
        {
            InitializeComponent();
            gbCarrera.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //btn aceptar
            if (ControlAgregar()) 
                { 
                    confirmar confirmar = new confirmar();
                    if (confirmar.ShowDialog() == DialogResult.OK)
                    this.Close();
                }
        }
        public bool ControlAgregar()
        {   //controles antes de agregar una nueva persona
            if (txtNombre.Text == "") { 
                errorProvider1.SetError(txtNombre,"Ingrese un nombre"); 
                return false;
            }
            if (txtApellido.Text == "") {
                errorProvider1.SetError(txtApellido, "Ingrese un apellido"); 
                return false;
            }
            if (txtDni.Text == "" || !int.TryParse(txtDni.Text,out int dni)) 
            {   errorProvider1.SetError(txtDni, "Ingrese un DNI valido"); 
                return false;
            }
            if (txtDireccion.Text == "") { 
                errorProvider1.SetError(txtDireccion, "Ingrese una direccion");
                return false;    
            }
            if (txtDireccionAltura.Text == "") { 
                errorProvider1.SetError(txtDireccionAltura, "Ingrese una numero de casa/edificio"); 
                return false;
            }
            if (txtTelefono.Text == "" || !int.TryParse(txtTelefono.Text ,out int telefono)) {
                errorProvider1.SetError(txtTelefono, "Ingrese un telefono"); 
                return false;
            }
            if (txtEmail.Text == "") { 
                errorProvider1.SetError(txtEmail, "Ingrese un Email valido"); 
                return false;
            }

            if (!rbAlumno.Checked && !rbPersonal.Checked && !rbProfesor.Checked && !rbAdministrador.Checked)
            {
                errorProvider1.SetError(gbPerfil, "Seleccione un perfil");
                return false;
            }

            if (rbAlumno.Checked && (!cbAnalista.Checked && !cbPublicidad.Checked)) 
            {
                errorProvider1.SetError(gbCarrera, "Seleccione una carrera");
                return false;
            }
            return true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
                this.Close();
        }

 

        private void rbAlumno_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAlumno.Focused) { gbCarrera.Visible = true; }
        }

        private void rbProfesor_CheckedChanged(object sender, EventArgs e)
        {
            gbCarrera.Visible = false;
        }

        private void rbPersonal_CheckedChanged(object sender, EventArgs e)
        {
            gbCarrera.Visible = false;
        }

        private void rbAdministrador_CheckedChanged(object sender, EventArgs e)
        {
            gbCarrera.Visible = false;
        }
    }
}
