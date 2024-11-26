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
    public partial class BuscarAlumno : Form
    {
        private ConexionBdd Conexion;

        public string matricula {  get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public BuscarAlumno()
        {
            InitializeComponent();
            Conexion = new ConexionBdd();
        }
        public void BuscarAlumnoTabla()
        {
            string query = "select Matricula,Nombre,Apellido,Dni from Alumnos";
            using (SqlCommand command = new SqlCommand(query,Conexion.conectorClase))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView10.DataSource = dt;
                }
            }
        }
        private void BuscarAlumno_Load(object sender, EventArgs e)
        {
            Conexion.Abrir();
            BuscarAlumnoTabla();
        }

        private void dataGridView10_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                matricula = dataGridView10.Rows[e.RowIndex].Cells["Matricula"].Value.ToString();
                nombre = dataGridView10.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();
                apellido = dataGridView10.Rows[e.RowIndex].Cells["Apellido"].Value.ToString();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
