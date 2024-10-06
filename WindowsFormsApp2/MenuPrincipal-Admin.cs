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
    public partial class MenuPrincipal : Form
    {
        private ConexionBdd ConexionMenuPrincipal;
        ConexionBdd conexionBdd = new ConexionBdd();

        public void CargaTablaAlumnos()
        {
            //ahora armo query
            string query = "select * from Alumnos";
            //preparo adaptador
            SqlDataAdapter adaptador = new SqlDataAdapter(query,conexionBdd.conectorClase);
            DataTable dataTable = new DataTable();
            adaptador.Fill(dataTable);
            dataGridView3.DataSource = dataTable;

        }

        public MenuPrincipal()
        {
            InitializeComponent();
            ConexionMenuPrincipal = new ConexionBdd();
            var tt = new ToolTip();
            tt.SetToolTip(btnAgregar, "Agregar");
            tt.SetToolTip(btnEditar, "Editar");
            tt.SetToolTip(btnEliminar, "Eliminar");
            tt.SetToolTip(btnSalir, "Salir del Programa");
        }

        private void MenuPrincipal_Load(object sender, EventArgs e)
        {
            ConexionMenuPrincipal.Abrir();
            CargaTablaAlumnos();


            switch (VariableGlobal.perfil) //cambiar username por tipo de permiso
            {
                case "admin":
                    lvlNombre.Text = "admin";

                break;
//--------------------------------------------------------------
                case "personal":
                    lvlNombre.Text = "personal";

                break;
//--------------------------------------------------------------
                case "profesor":
                    lvlNombre.Text = "profesor";

                break;
//--------------------------------------------------------------
                case "alumno":
                    lvlNombre.Text = "alumno";
                    tabControl1.TabPages.RemoveAt(1);

                break;
                }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            switch (tabControl1.TabIndex) //con esto doy orden segun donde este
            { 
                case 0:
                    agregar agregar = new agregar();
                    DialogResult inscripcion = agregar.ShowDialog();
                    if (inscripcion == DialogResult.OK)
                    {
                        /*
                        new Alumno() 
                        {
                            
                            //ingresar datos de alumno
                         
                        };
                        */
                    }
                    else if (inscripcion == DialogResult.Cancel)
                    {
                        
                    }
                    break;

                case 1:

                break;

                case 2:

                break;

                case 3:

                break;

                case 4:

                break;
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            switch (tabControl1.TabIndex) //con esto doy orden segun donde este
            {
                case 0:
                    agregar agregar = new agregar();
                    DialogResult inscripcion = agregar.ShowDialog();
                    if (inscripcion == DialogResult.OK)
                    {
                        /*
                        new Alumno() 
                        {
                            
                            //ingresar datos de alumno
                         
                        };
                        */
                    }
                    else if (inscripcion == DialogResult.Cancel)
                    {

                    }
                    break;

                case 1:

                    break;

                case 2:

                    break;

                case 3:

                    break;

                case 4:

                    break;
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            confirmar confirmar = new confirmar();
            DialogResult dialogResult = confirmar.ShowDialog();
            switch (tabControl1.TabIndex) //con esto doy orden segun donde este
            {
                case 0:
                    //eliminar usuario
                    break;

                case 1:

                    break;

                case 2:

                    break;

                case 3:

                    break;

                case 4:

                    break;
            }
        }
    }
}
