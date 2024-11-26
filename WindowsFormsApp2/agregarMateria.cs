using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class agregarMateria : Form
    {
        private ConexionBdd ConexioMateria;
        public Materia MateriaCreada { get; private set; }
        public bool Editar { get; set; } = false;
        public string NombreCarrera { get; set; }
        public int AnioCursada { get; set; }
        public int Id_Carrera { get; set; }
        public agregarMateria()
        {
            InitializeComponent();
            ConexioMateria = new ConexionBdd();
        }

        public void CargarComboCarrera()
        {
            if (VariableGlobal.perfil == 2 || VariableGlobal.perfil == 1)
            {
                using (SqlCommand command = new SqlCommand("sp_ObtenerTodasCarreras", ConexioMateria.conectorClase))
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

        private void agregarMateria_Load(object sender, EventArgs e)
        {
            ConexioMateria.Abrir();
            CargarComboCarrera();
            if (Editar == true)
            {
                txtNombreMateria.Text = NombreCarrera;
                cmbCarrera.SelectedValue = Id_Carrera;
                cmbAnioCursada.Text = AnioCursada.ToString();
            }
        }
        bool comprobar()
        {
            bool comprobado=true;
            if (txtNombreMateria.Text == "")
            {
                errorProvider1.SetError(txtNombreMateria, "Ingrese una materia");
                txtNombreMateria.Focus();
                comprobado=false;
                return comprobado;
            }
            if (cmbAnioCursada.Text == "Seleccione un año de cursada...") 
            {
                errorProvider1.SetError(cmbAnioCursada, "Ingrese una año al que se adjunta la materia");
                cmbAnioCursada.Focus();
                comprobado = false;
                return comprobado;
            }
            if (cmbCarrera.Text == "Seleccione la carrera a la que pertenece...")
            {
                errorProvider1.SetError(cmbCarrera, "Ingrese una carrera a la cual pertenecera");
                cmbCarrera.Focus();
                comprobado = false;
                return comprobado;
            }
            errorProvider1.SetError(txtNombreMateria, ""); //limpia mensaje
            return comprobado;
        }
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (comprobar())
            {
                string nombre = txtNombreMateria.Text;
                int anioCursada = int.Parse(cmbAnioCursada.Text);
                int carrera = int.Parse(cmbCarrera.SelectedValue.ToString());

                MateriaCreada = new Materia(nombre, anioCursada, carrera);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
