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

        public void CargaTablaAlumnos()
        {
            //ahora armo query
            string query = "select * from Alumnos"; 
            //preparo adaptador
            SqlDataAdapter adaptador = new SqlDataAdapter(query, ConexionMenuPrincipal.conectorClase);
            DataTable dataTable = new DataTable();
            adaptador.Fill(dataTable);
            dataGridView3.DataSource = dataTable;

        }

        public void CargaTablaCarreras()
        {
            //ahora armo query
            string query = "select * from Carreras";
            //preparo adaptador
            SqlDataAdapter adaptador = new SqlDataAdapter(query, ConexionMenuPrincipal.conectorClase);
            DataTable dataTable = new DataTable();
            adaptador.Fill(dataTable);
            dataGridView2.DataSource = dataTable;
        }

        public void CargaTablaMaterias()
        {
            //ahora armo query
            string query = "select * from Materias";
            //preparo adaptador
            SqlDataAdapter adaptador = new SqlDataAdapter(query, ConexionMenuPrincipal.conectorClase);
            DataTable dataTable = new DataTable();
            adaptador.Fill(dataTable);
            dataGridView4.DataSource = dataTable;
        }

        public void CargaTablaExamenes()
        {
            //ahora armo query
            string query = "select * from NotasXAlumno";
            //preparo adaptador
            SqlDataAdapter adaptador = new SqlDataAdapter(query, ConexionMenuPrincipal.conectorClase);
            DataTable dataTable = new DataTable();
            adaptador.Fill(dataTable);
            dataGridView7.DataSource = dataTable;
        }
        public void CargaTablaUsuarios()
        {
            //ahora armo query
            string query = "select * from Usuarios";
            //preparo adaptador
            SqlDataAdapter adaptador = new SqlDataAdapter(query, ConexionMenuPrincipal.conectorClase);
            DataTable dataTable = new DataTable();
            adaptador.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
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
            tt.SetToolTip(btnConfig, "Configuracion");
            tt.SetToolTip(btnCerrarSesion, "Salir de la sesion actual");
        }

        private void MenuPrincipal_Load(object sender, EventArgs e)
        {
            ConexionMenuPrincipal.Abrir();
            CargaTablaAlumnos();
            //CargaTablaCarreras();


            switch (VariableGlobal.perfil) //cambiar username por tipo de permiso
            {
                case 1:
                    lvlNombre.Text = "admin";
                    tabControl1.TabPages.RemoveAt(12);
                    tabControl1.TabPages.RemoveAt(11);
                    tabControl1.TabPages.RemoveAt(10);
                    tabControl1.TabPages.RemoveAt(9);
                    tabControl1.TabPages.RemoveAt(8);
                    CargaTablaAlumnos();
                    CargaTablaCarreras();
                    CargaTablaMaterias();
                    CargaTablaExamenes();


                    break;
//--------------------------------------------------------------
                case 2:
                    lvlNombre.Text = "personal";
                    tabControl1.TabPages.RemoveAt(12);
                    tabControl1.TabPages.RemoveAt(11);
                    tabControl1.TabPages.RemoveAt(10);
                    tabControl1.TabPages.RemoveAt(9);
                    tabControl1.TabPages.RemoveAt(8);
                    tabControl1.TabPages.RemoveAt(7);
                    tabControl1.TabPages.RemoveAt(5);
                    CargaTablaAlumnos();
                    break;
//--------------------------------------------------------------
                case 3:
                    lvlNombre.Text = "profesor";
                    tabControl1.TabPages.RemoveAt(12);
                    tabControl1.TabPages.RemoveAt(11);
                    tabControl1.TabPages.RemoveAt(10);
                    tabControl1.TabPages.RemoveAt(9);
                    tabControl1.TabPages.RemoveAt(7);
                    tabControl1.TabPages.RemoveAt(6);
                    tabControl1.TabPages.RemoveAt(5);
                    tabControl1.TabPages.RemoveAt(4);
                    tabControl1.TabPages.RemoveAt(2);
                    tabControl1.TabPages.RemoveAt(1);
                    CargaTablaAlumnos();
                    break;
//--------------------------------------------------------------
                case 4:
                    lvlNombre.Text = "alumno";
                    tabControl1.TabPages.RemoveAt(8);
                    tabControl1.TabPages.RemoveAt(7);
                    tabControl1.TabPages.RemoveAt(6);
                    tabControl1.TabPages.RemoveAt(5);
                    tabControl1.TabPages.RemoveAt(4);
                    tabControl1.TabPages.RemoveAt(3);
                    tabControl1.TabPages.RemoveAt(2);
                    tabControl1.TabPages.RemoveAt(1);
                    break;
                }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            confirmar confirmar = new confirmar();
            if (confirmar.ShowDialog() == DialogResult.OK)
                this.Close();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex) //con esto doy orden segun donde este
            { 
                case 0:
                    
                    break;

                case 1:

                break;

                case 2:
                    agregarMateria agregarMateria = new agregarMateria();
                    DialogResult materia = agregarMateria.ShowDialog();
                    if (materia == DialogResult.OK)
                    {
                        /*
                        new Alumno() 
                        {
                            
                            //ingresar datos de alumno
                         
                        };
                        */
                    }
                    else if (materia == DialogResult.Cancel)
                    {

                    }
                    break;

                case 3:
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

                case 4:

                break;
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex) //con esto doy orden segun donde este
            {
                case 0:


                case 1:

                    break;

                case 2:
                    agregarMateria agregarMateria = new agregarMateria();
                    DialogResult materia = agregarMateria.ShowDialog();
                    if (materia == DialogResult.OK)
                    {
                        /*
                        new Alumno() 
                        {
                            
                            //ingresar datos de alumno
                         
                        };
                        */
                    }
                    else if (materia == DialogResult.Cancel)
                    {

                    }
                    break;

                case 3:
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

        private void btnRecargar_Click(object sender, EventArgs e)
        {
            CargaTablaAlumnos();
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            LogIn logIn = new LogIn();
            logIn.Show();
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            if (VariableGlobal.perfil == 1)
                tabControl1.SelectedIndex = 7;
            if (VariableGlobal.perfil == 4)
                tabControl1.SelectedIndex = 12;
        }
    }
}
