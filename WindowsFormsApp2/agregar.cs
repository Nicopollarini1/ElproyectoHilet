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
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace WindowsFormsApp2
{
    public partial class agregar : Form
    {
        private ConexionBdd ConexionAlumno { get; set; }
        public Alumno AlumnoCreado { get; private set; }
        public Personal PersonalCreado { get; private set; }
        public Profesor ProfesorCreado { get; private set; }
        public Administrador adminCreado { get; private set; }

        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Dire_calle { get; set; }
        public string Dire_numero { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public DateTime Fecha_nacimiento { get; set; }
        public string Dni { get; set; }
        public bool Editar { get; set; } = false;
        public bool MiEditar { get; set; } =false;
        public int Perfil { get; set; }
        public int Id_Carrera { get; set; }
        public agregar()
        {
            InitializeComponent();
            ConexionAlumno = new ConexionBdd();
            gbPerfil.Enabled = false;
        }

        public void CargarComboCarrera()
        {
            if (VariableGlobal.perfil == 2 || VariableGlobal.perfil == 1)
            {
                using (SqlCommand command = new SqlCommand("sp_ObtenerTodasCarreras", ConexionAlumno.conectorClase))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);
                        cmbCarrera.DataSource = dataTable;
                        cmbCarrera.DisplayMember = "Nombre_carrera";
                        cmbCarrera.ValueMember = "Id_Carrera";
                    }
                }
            }
        }

        private void agregar_Load(object sender, EventArgs e)
        {
            ConexionAlumno.Abrir();
            CargarComboCarrera();

            Perfil = 0;
            if (MenuPrincipal.indice == 3)
            {
                rbAlumno.Select();
                Perfil = 4;
                gbCarrera.Visible = true;
            }
            if (MenuPrincipal.indice == 6)
            {
                rbProfesor.Select();
                Perfil = 3;
                gbCarrera.Visible = false;
            }
            if (MenuPrincipal.indice == 7)
            {
                rbPersonal.Select();
                Perfil = 2;
                gbCarrera.Visible = false;
            }

            if (Editar == true)
            {
                if (MenuPrincipal.indice == 6 || MenuPrincipal.indice == 7)
                {
                    gbCarrera.Visible = false;
                }
                else
                {
                    gbCarrera.Visible = true;
                }
                txtNombre.Text = Nombre;
                txtApellido.Text = Apellido;
                txtDireccion.Text = Dire_calle;
                txtDireccionAltura.Text = Dire_numero;
                txtTelefono.Text = Telefono;
                txtEmail.Text = Email;
                dateTimePicker1.Value = Fecha_nacimiento;
                txtDni.Text = Dni;

                cmbCarrera.SelectedValue = Id_Carrera;

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
            if (txtTelefono.Text == "" /*|| !int.TryParse(txtTelefono.Text ,out int telefono)*/) {
                errorProvider1.SetError(txtTelefono, "Ingrese un telefono"); 
                return false;
            }
            if (txtEmail.Text == "") { 
                errorProvider1.SetError(txtEmail, "Ingrese un Email valido"); 
                return false;
            }
            /*
            if (!rbAlumno.Checked && !rbPersonal.Checked && !rbProfesor.Checked && !rbAdministrador.Checked)
            {
                errorProvider1.SetError(gbPerfil, "Seleccione un perfil");
                return false;
            }
            */
            if (rbAlumno.Checked && (cmbCarrera.Text == "")) 
            {
                errorProvider1.SetError(gbCarrera, "Seleccione una carrera");
                return false;
            }
            return true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            //btn aceptar
            if (ControlAgregar()) 
                {
                
                if (Perfil==4) 
                {
                    AlumnoCreado = new Alumno(txtNombre.Text, txtApellido.Text, txtDireccion.Text, txtDireccionAltura.Text, txtTelefono.Text, txtEmail.Text, dateTimePicker1.Value, txtDni.Text, Perfil, txtDni.Text, txtDni.Text, DateTime.Now);
                    this.DialogResult = DialogResult.OK;
                    Id_Carrera = int.Parse(cmbCarrera.SelectedValue.ToString());
                    ///******


                    this.Close();
                }
                if (Perfil == 2)
                {
                    /*----------------CAMBIOS ELIAS--------------*/ //FIJATE LOS PARAMETRO QUE ONDA SI ESTAN BIEN COMPARADO CON LOS QUE PIDE LA CLASE
                    PersonalCreado = new Personal(txtNombre.Text, txtApellido.Text, txtDireccion.Text, txtDireccionAltura.Text, txtTelefono.Text, txtEmail.Text, dateTimePicker1.Value, txtDni.Text, Perfil, txtDni.Text, txtDni.Text);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                if (Perfil == 3)
                {
                    /*----------------CAMBIOS ELIAS--------------*/ //FIJATE LOS PARAMETRO QUE ONDA SI ESTAN BIEN COMPARADO CON LOS QUE PIDE LA CLASE
                    ProfesorCreado = new Profesor(txtNombre.Text, txtApellido.Text, txtDireccion.Text, txtDireccionAltura.Text, txtTelefono.Text, txtEmail.Text, dateTimePicker1.Value, txtDni.Text, Perfil, txtDni.Text, txtDni.Text,0);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                if (Perfil == 4) 
                {
                    adminCreado = new Administrador(txtNombre.Text, txtApellido.Text, txtDireccion.Text, txtDireccionAltura.Text, txtTelefono.Text, txtEmail.Text, dateTimePicker1.Value, txtDni.Text, Perfil, txtDni.Text, txtDni.Text);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }     
            }
            if (MiEditar == true)//esto indica q esta en la pestaña de esitarse a si mismo
            {
                //puse personal pero podria haber sido cualquier clase por que lo importante es generar una persona
                PersonalCreado = new Personal(txtNombre.Text, txtApellido.Text, txtDireccion.Text, txtDireccionAltura.Text, txtTelefono.Text, txtEmail.Text, dateTimePicker1.Value, txtDni.Text, Perfil, txtDni.Text, txtDni.Text);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
