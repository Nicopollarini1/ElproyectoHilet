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
    public partial class AgregarExamen : Form
    {
        private ConexionBdd Conexion;
        public Examen examenCreado {  get; private set; }
        public bool Editar { get; set; } = false;
        public int Matricula { get; set; }
        public int IdMateria { get; set; }
        public decimal Nota { get; set; }
        public DateTime Fecha { get; set; }
        public string TipoExamen { get; set; }
        public AgregarExamen()
        {
            InitializeComponent();
            Conexion = new ConexionBdd();
            cbTipoExamen.Items.Add("Parcial 1");
            cbTipoExamen.Items.Add("Parcial 2");
            cbTipoExamen.Items.Add("Recuperatorio 1");
            cbTipoExamen.Items.Add("Recuperatorio 2");
            cbTipoExamen.Items.Add("Final");
            cbTipoExamen.SelectedIndex = 0;
        }

        public void CargarCombo()
        {
            if (VariableGlobal.perfil == 1 || VariableGlobal.perfil == 2)
            {
                using (SqlCommand command = new SqlCommand("sp_ObtenerTodasMaterias", Conexion.conectorClase))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        cbMateria.DataSource = dt;
                        cbMateria.DisplayMember = "Nombre_Materia";
                        cbMateria.ValueMember = "Id_Materia";
                    }
                }
            }
            else if (VariableGlobal.perfil == 3)
            {
                if (VariableGlobal.DatosUsuario is Profesor profesor)
                {
                    using (SqlCommand command = new SqlCommand("sp_ObtenerMateriasProfesor", Conexion.conectorClase))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Idlegajo", profesor.ID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            DataTable dt = new DataTable();
                            dt.Load(reader);
                            cbMateria.DataSource = dt;
                            cbMateria.DisplayMember = "Nombre_Materia";
                            cbMateria.ValueMember = "Id_Materia";
                        }
                    }
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Abort;
            this.Close();
        }

        private void btnBuscarAlumno_Click(object sender, EventArgs e)
        {
            BuscarAlumno alumno = new BuscarAlumno();
            DialogResult resultado = alumno.ShowDialog();
            if (resultado == DialogResult.OK)
            {
                label13.Visible = true;
                label14.Visible = true;
                lblNombre.Visible = true;
                lblApellido.Visible = true;

                lblNombre.Text = alumno.nombre;
                lblApellido.Text = alumno.apellido;
                txtMatricula.Text = alumno.matricula;
                txtMatricula.Enabled = false;
            }
        }

        private void AgregarExamen_Load(object sender, EventArgs e)
        {
            Conexion.Abrir();
            CargarCombo();
            if (Editar == true)
            {
                txtMatricula.Text = Convert.ToString(Matricula);
                txtMatricula.Enabled = false;
                dateTimePicker1.Value = Fecha;
                txtNota.Text = Convert.ToString(Nota);
                cbTipoExamen.Text = TipoExamen;
                cbMateria.SelectedValue = IdMateria;
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMatricula.Text == "" || !int.TryParse(txtMatricula.Text, out int matricula))
                {
                    errorProvider1.SetError(txtMatricula, "Ingrese Matricula valida");
                    return;
                }
                using (SqlCommand cmd = new SqlCommand("select count(*) from Alumnos where Matricula = @Matricula", Conexion.conectorClase))
                {
                    cmd.Parameters.AddWithValue("@Matricula",matricula);
                    
                    int existe = Convert.ToInt32(cmd.ExecuteScalar());
                    if (existe == 0)
                    {
                        errorProvider1.SetError(txtMatricula, "Ingrese Matricula valida");
                        return;
                    }
                }

                    int id_materia = Convert.ToInt32(cbMateria.SelectedValue);
                if (txtNota.Text == "" || !decimal.TryParse(txtNota.Text, out decimal nota) || nota < 0 || nota > 10)
                {
                    errorProvider1.SetError(txtNota, "Ingrese Nota valida");
                    return;
                }
                DateTime fecha = Convert.ToDateTime(dateTimePicker1.Value);
                string tipo = cbTipoExamen.Text;
                examenCreado = new Examen(matricula, id_materia, nota, fecha, tipo);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch
            {
                MessageBox.Show("La matricula no existe");
            }
        }

        private void txtMatricula_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
