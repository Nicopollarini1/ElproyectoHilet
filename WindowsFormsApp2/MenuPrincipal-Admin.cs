using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace WindowsFormsApp2
{
    public partial class MenuPrincipal : Form
    {
        private ConexionBdd ConexionMenuPrincipal;

        public void CargaTablaAlumnos()
        {
            if (VariableGlobal.perfil == 3)
            {
                using (SqlCommand cmd = new SqlCommand("sp_AlumnosDelProfesor", ConexionMenuPrincipal.conectorClase))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (VariableGlobal.DatosUsuario is Profesor profesor)
                    {
                        int Id_Profesor = profesor.ID;
                        cmd.Parameters.AddWithValue("@Legajo", Id_Profesor);
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            dataGridView3.DataSource = dataTable;
                        
                    }
                }
            }
            else
            {
                SqlDataAdapter adaptador = new SqlDataAdapter("sp_CargarAlumnosTabla", ConexionMenuPrincipal.conectorClase);
                DataTable dataTable = new DataTable();
                adaptador.Fill(dataTable);
                dataGridView3.DataSource = dataTable;

                dataGridView3.Columns["Id_Carrera"].Visible = false;
            }

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
            SqlDataAdapter adaptador = new SqlDataAdapter("sp_CargarMateriaTabla", ConexionMenuPrincipal.conectorClase);
            DataTable dataTable = new DataTable();
            adaptador.Fill(dataTable);
            dataGridView4.DataSource = dataTable;

            dataGridView4.Columns["Id_Carrera"].Visible = false;
        }
        public void CargaTablaProfesores()
        {
            //ahora armo query
            string query = "select * from profesores";
            //preparo adaptador
            SqlDataAdapter adaptador = new SqlDataAdapter(query, ConexionMenuPrincipal.conectorClase);
            DataTable dataTable = new DataTable();
            adaptador.Fill(dataTable);
            dataGridView8.DataSource = dataTable;
        }
        /*----------------CAMBIOS ELIAS--------------*/
        public void CargaTablaExamenes()
        {
            if (VariableGlobal.perfil == 1 || VariableGlobal.perfil == 2)
            {
                using (SqlCommand command = new SqlCommand("sp_TablaNotasAdmin", ConexionMenuPrincipal.conectorClase))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridView7.DataSource = dt;

                        dataGridView7.Columns["Id_NotasXAlumno"].Visible = false;
                        dataGridView7.Columns["Id_Materia"].Visible = false;
                    }
                }
            }
            else if (VariableGlobal.perfil == 3)
            {
                if (VariableGlobal.DatosUsuario is Profesor profesor)
                {
                    using (SqlCommand command = new SqlCommand("sp_TablaNotasParaProfesores", ConexionMenuPrincipal.conectorClase))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@IdLegajo", profesor.ID);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dataGridView7.DataSource = dt;

                            dataGridView7.Columns["Id_NotasXAlumno"].Visible = false;
                            dataGridView7.Columns["Id_Materia"].Visible = false;
                        }

                    }
                }
            }
        }

        /*----------------CAMBIOS ELIAS--------------*/
        public void CargarComboProfesor(int idProfesor)
        {
            using (SqlCommand command = new SqlCommand("sp_ObtenerMateriasProfesor", ConexionMenuPrincipal.conectorClase))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Idlegajo", idProfesor);
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

        public void CargarComboAdminPersonal()
        {
            if (VariableGlobal.perfil == 2 || VariableGlobal.perfil == 1)
            {
                using (SqlCommand command = new SqlCommand("sp_ObtenerTodasMaterias", ConexionMenuPrincipal.conectorClase))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        cbMateria.DataSource = dt;
                        cbMateria.DisplayMember = "Nombre_Materia";
                        cbMateria.ValueMember = "Id_Materia";
                        cmbMateriasProfes.DataSource = dt;
                        cmbMateriasProfes.ValueMember= "Id_Materia";
                        cmbMateriasProfes.DisplayMember = "Nombre_Materia";
                        cmbMateriaParaAlumno.DataSource = dt;
                        cmbMateriaParaAlumno.ValueMember = "Id_Materia";
                        cmbMateriaParaAlumno.DisplayMember = "Nombre_Materia";
                    }
                }
            }
        }

        public void CargaTablaUsuarios()
        {
            if (VariableGlobal.perfil == 1)
            {
                using (SqlCommand cmd = new SqlCommand("sp_TablaUsuario", ConexionMenuPrincipal.conectorClase))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridView1.DataSource = dt;
                    }
                }
            }
        }
        public void CargaTablaPersonal()
        {
            //ahora armo query

            string query = "select * from Personal";
            //preparo adaptador
            SqlDataAdapter adaptador = new SqlDataAdapter(query, ConexionMenuPrincipal.conectorClase);
            DataTable dataTable = new DataTable();
            adaptador.Fill(dataTable);
            dataGridView9.DataSource = dataTable;

        }

        public void CargaTablaMisMaterias()
        {
            string query = $"sp_TraerMateriasDeProfe @legajo = {VariableGlobal.DatosUsuario.Usuario}";
            SqlDataAdapter adaptador = new SqlDataAdapter(query, ConexionMenuPrincipal.conectorClase);
            DataTable dataTable = new DataTable();
            adaptador.Fill(dataTable);
            dataGridView5.DataSource = dataTable;

        }

        public void CargaTablaMisExamenes()
        {
            string query = $"sp_TraerMisExamenes @usuario = {VariableGlobal.DatosUsuario.Usuario}";
            SqlDataAdapter adaptador = new SqlDataAdapter(query, ConexionMenuPrincipal.conectorClase);
            DataTable dataTable = new DataTable();
            adaptador.Fill(dataTable);
            dataGridView6.DataSource = dataTable;

        }

        public void Cambiarcontrasenia()
        {
            using (SqlCommand command = new SqlCommand("sp_CambiarContrasenia", ConexionMenuPrincipal.conectorClase))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Usuario", VariableGlobal.DatosUsuario.Usuario);
                command.Parameters.AddWithValue("@contraseniaNueva", txtContrasena1.Text);
                command.ExecuteNonQuery();
            }
        }

        public MenuPrincipal()
        {
            InitializeComponent();
            ConexionMenuPrincipal = new ConexionBdd();
            this.FormClosing += MenuPrincipal_FormClosing;
            var tt = new ToolTip();
            tt.SetToolTip(btnAgregar, "Agregar");
            tt.SetToolTip(btnEditar, "Editar");
            tt.SetToolTip(btnEliminar, "Eliminar");
            tt.SetToolTip(btnSalir, "Salir del Programa");
            tt.SetToolTip(btnConfig, "Mi informacion");
            tt.SetToolTip(btnCerrarSesion, "Salir de la sesion actual");
            tt.SetToolTip(btnGestionAlumnos, "Gestionar alumnos");
            tt.SetToolTip(btnGestionExamen, "Gestionar Examenes");
            tt.SetToolTip(btnGestionMaterias, "Gestionar Materias");
            tt.SetToolTip(btnbuscar, "Buscar por materia");
            tt.SetToolTip(btnBuscarAlumno, "Buscar por nombre y apellido");
            tt.SetToolTip(btnLimpiar, "Limpiar campos");
            tt.SetToolTip(btnGuardar, "Guardar Reporte");
        }

        private void MenuPrincipal_Load(object sender, EventArgs e)
        {
            ConexionMenuPrincipal.Abrir();

            pnlSeleccionParaAlumno.Visible = false;
            
            pnlAsignarMaterias.Visible = false;
            
            btnAsignarMateria.Visible = false;
            pnlAlumno.Visible = false;
            pnlCarrera.Visible = false;
            pnlMateria.Visible = false;

            btnAgregarExamenProfe.Hide();//boton agregar examen profe
            btnEditarExamenProfesor.Hide();//boton editar examen profesor

            if (tabControl1.SelectedIndex == 0)
            {
                btnAgregar.Hide();
                btnEditar.Hide();
                btnEliminar.Hide();
            }
            using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM alumnos", ConexionMenuPrincipal.conectorClase))
            {
                cmd.CommandType = CommandType.Text;
                int cantidadAlumnos = (int)cmd.ExecuteScalar();
                lblEstudiantes.Text = cantidadAlumnos.ToString();
            }
            using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM profesores", ConexionMenuPrincipal.conectorClase))
            {
                cmd.CommandType = CommandType.Text;
                int cantidadProfesores = (int)cmd.ExecuteScalar();
                lblProfesores.Text = cantidadProfesores.ToString();
            }

            lvlNombre.Text = VariableGlobal.DatosUsuario.Nombre + " " + VariableGlobal.DatosUsuario.Apellido; //setea el nombre

            //cargar tabla de informacion personal (miinformacion)
            panelContrasena.Hide();
            lblNom.Text = VariableGlobal.DatosUsuario.Nombre;
            lblApe.Text = VariableGlobal.DatosUsuario.Apellido;
            lblDirec.Text = VariableGlobal.DatosUsuario.DireccionCalle;
            lblDicreccionN.Text = VariableGlobal.DatosUsuario.DireccionNro;
            lblTele.Text = VariableGlobal.DatosUsuario.Telefono;
            lblEma.Text = VariableGlobal.DatosUsuario.Email;
            lblDn.Text = VariableGlobal.DatosUsuario.DNI;
            dateTimePicker1.Value = VariableGlobal.DatosUsuario.FechaNacimiento;
            dateTimePicker1.Enabled = false;

            switch (VariableGlobal.perfil) //cambiar username por tipo de permiso
            {
                case 1:
                    //admin
                    tabControl1.TabPages.RemoveAt(12);
                    tabControl1.TabPages.RemoveAt(10);
                    tabControl1.TabPages.RemoveAt(8);
                    CargaTablaAlumnos();
                    CargaTablaCarreras();
                    CargaTablaMaterias();
                    CargaTablaExamenes();
                    CargaTablaProfesores();
                    CargaTablaPersonal();
                    CargaTablaUsuarios();
                    CargarComboAdminPersonal();
                    btnAsignarMateria.Visible = true;
                    btnEditarInfo.Hide(); //el boton de editar info personal sacado

                    break;
                //--------------------------------------------------------------
                case 2:
                    //personal
                    tabControl1.TabPages.RemoveAt(12);
                    tabControl1.TabPages.RemoveAt(11);
                    tabControl1.TabPages.RemoveAt(10);
                    tabControl1.TabPages.RemoveAt(8);
                    tabControl1.TabPages.RemoveAt(7);
                    tabControl1.TabPages.RemoveAt(5);
                    CargaTablaAlumnos();
                    CargaTablaProfesores();
                    CargaTablaCarreras();
                    CargaTablaMaterias();
                    CargaTablaExamenes();
                    CargarComboAdminPersonal();
                    btnAsignarMateria.Visible = true;
                    btnConfig.Hide();

                    break;
                //--------------------------------------------------------------
                case 3:
                    //profesor
                    if (VariableGlobal.DatosUsuario is Profesor profesor)
                    {
                        int Id_Profesor = profesor.ID;
                        CargarComboProfesor(Id_Profesor);
                    }
                    tabControl1.TabPages.RemoveAt(12);
                    tabControl1.TabPages.RemoveAt(11);
                    tabControl1.TabPages.RemoveAt(10);
                    tabControl1.TabPages.RemoveAt(7);
                    tabControl1.TabPages.RemoveAt(6);
                    tabControl1.TabPages.RemoveAt(5);
                    tabControl1.TabPages.RemoveAt(2);
                    tabControl1.TabPages.RemoveAt(1);
                    CargaTablaAlumnos();
                    CargaTablaExamenes();
                    CargaTablaMisMaterias();
                    btnConfig.Hide();
                    btnGestionAlumnos.Hide();
                    txtAlum.Hide();
                    btnGestionExamen.Hide();
                    btnAsignarAlumnoAMateria.Hide();
                    txtExam.Hide();
                    break;
                //--------------------------------------------------------------
                case 4:
                    //alumno
                    tabControl1.TabPages.RemoveAt(12);
                    tabControl1.TabPages.RemoveAt(11);
                    tabControl1.TabPages.RemoveAt(8);
                    tabControl1.TabPages.RemoveAt(7);
                    tabControl1.TabPages.RemoveAt(6);
                    tabControl1.TabPages.RemoveAt(5);
                    tabControl1.TabPages.RemoveAt(4);
                    tabControl1.TabPages.RemoveAt(3);
                    tabControl1.TabPages.RemoveAt(2);
                    tabControl1.TabPages.RemoveAt(1);
                    btnGestionAlumnos.Hide();
                    txtAlum.Hide();
                    btnGestionMaterias.Hide();
                    btnAsignarAlumnoAMateria.Hide();
                    txtMat.Hide();
                    CargaTablaMisExamenes();

                    break;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            confirmar confirmar = new confirmar();
            if (confirmar.ShowDialog() == DialogResult.OK)
                this.Close();
        }
        public static int indice;
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            indice = tabControl1.SelectedIndex;
            switch (tabControl1.SelectedIndex) //con esto doy orden segun donde este
            {
             
                case 1:
                    AgregarCarrera agregarCarrera = new AgregarCarrera();
                    DialogResult carreraa = agregarCarrera.ShowDialog();

                    if (carreraa == DialogResult.OK)
                    {
                        
                        Carrera carreraAgregada = agregarCarrera.CarreraCreada;

                        using (SqlCommand command = new SqlCommand("sp_InsertarCarrera", ConexionMenuPrincipal.conectorClase))
                        {
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@nombre_Carrera", carreraAgregada.NombreCarrera);//modificar el @perfil por lo que corresponde
                            command.Parameters.AddWithValue("@Num_de_Resolucion", carreraAgregada.NroResolucion);
                            command.Parameters.AddWithValue("@Anio_Plan_Estudio", carreraAgregada.AnioPlanEstudio);

                            command.ExecuteNonQuery();
                            MessageBox.Show("Carrera Cargada");
                            CargaTablaCarreras();
                        }
                    }
                    break;
                case 2:
                    agregarMateria agregarMateria = new agregarMateria();
                    DialogResult materiaa = agregarMateria.ShowDialog();

                    if (materiaa == DialogResult.OK)
                    {
                        Materia nuevaMateria = agregarMateria.MateriaCreada;

                        using (SqlCommand command = new SqlCommand("sp_InsertarMateriaYCarrera", ConexionMenuPrincipal.conectorClase))
                        {
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@nombre_Materia", nuevaMateria.NombreMateria);//modificar el @perfil por lo que corresponde
                            command.Parameters.AddWithValue("@Anio_Cursada", nuevaMateria.AnioCursada);
                            command.Parameters.AddWithValue("@Id_Carrera", nuevaMateria.ID_Carrera);

                            command.ExecuteNonQuery();
                            MessageBox.Show("Materia Cargada");
                            CargaTablaMaterias();
                        }
                    }
                    break;

                case 3:
                    agregar agregar = new agregar();
                    DialogResult inscripcion = agregar.ShowDialog();
                    if (inscripcion == DialogResult.OK)
                    {
                        Alumno nuevoAlumno = agregar.AlumnoCreado;

                        //Fijarse el store para aca, tendria que ser como el de MateriaYCarrera **************
                        using (SqlCommand command = new SqlCommand("sp_InsertarAlumnoYCarrera", ConexionMenuPrincipal.conectorClase))
                        {
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("Id_Carrera",agregar.Id_Carrera);
                            command.Parameters.AddWithValue("@Apellido", nuevoAlumno.Apellido);
                            command.Parameters.AddWithValue("@Nombre", nuevoAlumno.Nombre);
                            command.Parameters.AddWithValue("@Dire_Calle", nuevoAlumno.DireccionCalle);
                            command.Parameters.AddWithValue("@Dire_Numero", nuevoAlumno.DireccionNro);
                            command.Parameters.AddWithValue("@Telefono", nuevoAlumno.Telefono);
                            command.Parameters.AddWithValue("@Email", nuevoAlumno.Email);
                            command.Parameters.AddWithValue("@Fecha_Nacimiento", nuevoAlumno.FechaNacimiento);
                            command.Parameters.AddWithValue("@Dni", nuevoAlumno.DNI);
                            command.Parameters.AddWithValue("@Fecha_Inscripcion", nuevoAlumno.FechaInscripcion);
                            command.Parameters.AddWithValue("@usuario", nuevoAlumno.Usuario);
                            command.Parameters.AddWithValue("@contraseña", nuevoAlumno.Contrasena);
                            command.Parameters.AddWithValue("@Id_Perfil", nuevoAlumno.Perfil);
                            //para que ande falta agregar carrera
                            command.ExecuteNonQuery();
                            MessageBox.Show("Alumno Cargado");
                            CargaTablaAlumnos();
                        }
                    }
                    else if (inscripcion == DialogResult.Cancel)
                    {

                    }
                    break;
                case 4:
                    AgregarExamen agregarExamen = new AgregarExamen();
                    DialogResult resultado = agregarExamen.ShowDialog();
                    if (resultado == DialogResult.OK)
                    {
                        Examen nuevoExamen = agregarExamen.examenCreado;
                        using (SqlCommand command = new SqlCommand("sp_InsertarNotasXAlumno", ConexionMenuPrincipal.conectorClase))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@Matricula",nuevoExamen.Matricula);
                            command.Parameters.AddWithValue("@Id_Materia", nuevoExamen.Id_Materia);
                            command.Parameters.AddWithValue("@Nota", nuevoExamen.Nota);
                            command.Parameters.AddWithValue("@Fecha", nuevoExamen.Fecha);
                            command.Parameters.AddWithValue("@Tipo_Examen", nuevoExamen.TipoExamen);

                            command.ExecuteNonQuery();
                            MessageBox.Show("Examen Cargado");
                            CargaTablaExamenes();
                        }
                    }
                    break;
                case 6:
                    agregar agregarProfesor = new agregar();
                    DialogResult inscript = agregarProfesor.ShowDialog();
                    if (inscript == DialogResult.OK)
                    {
                        Profesor nuevoProfe = agregarProfesor.ProfesorCreado;

                        using (SqlCommand command = new SqlCommand("sp_InsertarProfesor", ConexionMenuPrincipal.conectorClase))
                        {
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@Apellido", nuevoProfe.Apellido);
                            command.Parameters.AddWithValue("@Nombre", nuevoProfe.Nombre);
                            command.Parameters.AddWithValue("@Dire_Calle", nuevoProfe.DireccionCalle);
                            command.Parameters.AddWithValue("@Dire_Numero", nuevoProfe.DireccionNro);
                            command.Parameters.AddWithValue("@Telefono", nuevoProfe.Telefono);
                            command.Parameters.AddWithValue("@Email", nuevoProfe.Email);
                            command.Parameters.AddWithValue("@Fecha_Nacimiento", nuevoProfe.FechaNacimiento);
                            command.Parameters.AddWithValue("@Dni", nuevoProfe.DNI);
                            command.Parameters.AddWithValue("@usuario", nuevoProfe.Usuario);
                            command.Parameters.AddWithValue("@contraseña", nuevoProfe.Contrasena);
                            command.Parameters.AddWithValue("@Id_Perfil", nuevoProfe.Perfil);

                            //para que ande falta agregar carrera
                            command.ExecuteNonQuery();
                            MessageBox.Show("Profesor Cargado");
                            CargaTablaProfesores();
                        }
                    }
                    break;
                case 7:
                    agregar agregarPersonal = new agregar();
                    DialogResult inscrip = agregarPersonal.ShowDialog();
                    if (inscrip == DialogResult.OK)
                    {
                        Personal nuevoPersonal = agregarPersonal.PersonalCreado;

                        using (SqlCommand command = new SqlCommand("sp_InsertarPersonal", ConexionMenuPrincipal.conectorClase))
                        {
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@Apellido", nuevoPersonal.Apellido);
                            command.Parameters.AddWithValue("@Nombre", nuevoPersonal.Nombre);
                            command.Parameters.AddWithValue("@Dire_Calle", nuevoPersonal.DireccionCalle);
                            command.Parameters.AddWithValue("@Dire_Numero", nuevoPersonal.DireccionNro);
                            command.Parameters.AddWithValue("@Telefono", nuevoPersonal.Telefono);
                            command.Parameters.AddWithValue("@Email", nuevoPersonal.Email);
                            command.Parameters.AddWithValue("@Fecha_Nacimiento", nuevoPersonal.FechaNacimiento);
                            command.Parameters.AddWithValue("@Dni", nuevoPersonal.DNI);
                            command.Parameters.AddWithValue("@usuario", nuevoPersonal.Usuario);
                            command.Parameters.AddWithValue("@contraseña", nuevoPersonal.Contrasena);
                            command.Parameters.AddWithValue("@Id_Perfil", nuevoPersonal.Perfil);
                            //para que ande falta agregar carrera
                            command.ExecuteNonQuery();
                            MessageBox.Show("Personal Cargado");
                            CargaTablaPersonal();
                        }
                    }
                    break;
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            indice = tabControl1.SelectedIndex;
            switch (tabControl1.SelectedIndex) //con esto doy orden segun donde este
            {
                case 1:
                    if (dataGridView2.SelectedRows.Count > 0)
                    {
                        var filaseleccionada = dataGridView2.SelectedRows[0];

                        string nombrecarrera = filaseleccionada.Cells["Nombre_carrera"].Value.ToString();
                        string anioPlanStudio = filaseleccionada.Cells["Anio_Plan_Estudio"].Value.ToString();
                        string NumRes = filaseleccionada.Cells["Num_de_Resolucion"].Value.ToString();

                        int id_carrera = Convert.ToInt32(filaseleccionada.Cells["Id_Carrera"].Value);

                        AgregarCarrera agregarCarrera = new AgregarCarrera()
                        {
                            editar = true,
                            nombrecarrera = nombrecarrera,
                            anioplandeestudio = anioPlanStudio,
                            nroresolucion = NumRes
                        };
                        DialogResult resultado = agregarCarrera.ShowDialog();
                        if (resultado == DialogResult.OK)
                        {
                            Carrera carreraEditada = agregarCarrera.CarreraCreada;
                            using (SqlCommand command = new SqlCommand("sp_ActualizarCarrera", ConexionMenuPrincipal.conectorClase))
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@Id_Carrera", id_carrera);
                                command.Parameters.AddWithValue("@Nombre_carrera", carreraEditada.NombreCarrera);
                                command.Parameters.AddWithValue("@Anio_Plan_Estudio", carreraEditada.AnioPlanEstudio);
                                command.Parameters.AddWithValue("@Num_Resolucion", carreraEditada.NroResolucion);


                                command.ExecuteNonQuery();
                                CargaTablaCarreras();
                            }
                        }
                    }
                        break;

                case 2:
                    if (dataGridView4.SelectedRows.Count > 0)
                    {
                        var filaseleccionada = dataGridView4.SelectedRows[0];

                        int idMateria = Convert.ToInt32(filaseleccionada.Cells["Id_Materia"].Value);
                        string NombreMateria = filaseleccionada.Cells["Nombre_Materia"].Value.ToString();
                        int anioCursada = Convert.ToInt32(filaseleccionada.Cells["Anio_Cursada"].Value);
                        int ID_carrera = Convert.ToInt32(filaseleccionada.Cells["Id_Carrera"].Value);



                        agregarMateria agregarMateria = new agregarMateria()
                        {
                            Editar = true,
                            Id_Carrera = ID_carrera,
                            AnioCursada = anioCursada,
                            NombreCarrera = NombreMateria
                        };
                        DialogResult materia = agregarMateria.ShowDialog();
                        if (materia == DialogResult.OK)
                        {
                            Materia MateriaEditada = agregarMateria.MateriaCreada;
                            using (SqlCommand cmd = new SqlCommand("sp_ActualizarMateriaYCarrera", ConexionMenuPrincipal.conectorClase))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@Id_Carrera", MateriaEditada.ID_Carrera);
                                cmd.Parameters.AddWithValue("@Id_Materia", idMateria);
                                cmd.Parameters.AddWithValue("@Nombre_Materia", MateriaEditada.NombreMateria);
                                cmd.Parameters.AddWithValue("@Anio_Cursada", MateriaEditada.AnioCursada);


                                cmd.ExecuteNonQuery();
                                CargaTablaMaterias();
                            }
                        }
                    }

                    break;

                case 3:
                    if (dataGridView3.SelectedRows.Count > 0)
                    {
                        var filaseleccionada = dataGridView3.SelectedRows[0];

                        int matricula = Convert.ToInt32(filaseleccionada.Cells["Matricula"].Value);
                        string apellido = filaseleccionada.Cells["Apellido"].Value.ToString();
                        string nombre = filaseleccionada.Cells["nombre"].Value.ToString();
                        string direccion_calle = filaseleccionada.Cells["Dire_Calle"].Value.ToString();
                        string dreccion_numero= filaseleccionada.Cells["Dire_Numero"].Value.ToString();
                        string telefono = filaseleccionada.Cells["Telefono"].Value.ToString();
                        string email = filaseleccionada.Cells["Email"].Value.ToString();
                        DateTime fecha_nac = Convert.ToDateTime( filaseleccionada.Cells["Fecha_Nacimiento"].Value);
                        string dni = filaseleccionada.Cells["Dni"].Value.ToString();

                        int Id_Carrera = Convert.ToInt32(filaseleccionada.Cells["Id_Carrera"].Value);


                        agregar EditarAlumno = new agregar()
                        {
                            Editar = true,
                            Nombre = nombre,
                            Apellido = apellido,
                            Dni = dni,
                            Dire_calle = direccion_calle,
                            Dire_numero = dreccion_numero,
                            Telefono=telefono,
                            Email=email,
                            Fecha_nacimiento= fecha_nac,
                            Id_Carrera= Id_Carrera
                        };
                        DialogResult resultado = EditarAlumno.ShowDialog();
                        if (resultado == DialogResult.OK)
                        {
                            Alumno alumnoEditado = EditarAlumno.AlumnoCreado;
                            using (SqlCommand command = new SqlCommand("sp_ActualizarAlumnoYCarrera", ConexionMenuPrincipal.conectorClase))
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@Matricula", matricula);
                                command.Parameters.AddWithValue("@Id_Carrera",EditarAlumno.Id_Carrera);
                                command.Parameters.AddWithValue("@Apellido", alumnoEditado.Apellido);
                                command.Parameters.AddWithValue("@Nombre", alumnoEditado.Nombre);
                                command.Parameters.AddWithValue("@Dire_Calle", alumnoEditado.DireccionCalle);
                                command.Parameters.AddWithValue("@Dire_Numero",alumnoEditado.DireccionNro);
                                command.Parameters.AddWithValue("@Telefono", alumnoEditado.Telefono);
                                command.Parameters.AddWithValue("@Email", alumnoEditado.Email);
                                command.Parameters.AddWithValue("@Fecha_Nacimiento", alumnoEditado.FechaNacimiento);
                                command.Parameters.AddWithValue("@Dni", alumnoEditado.DNI);
                                command.Parameters.AddWithValue("@Id_Perfil", alumnoEditado.Perfil);
                                command.Parameters.AddWithValue("@Usuario",alumnoEditado.Usuario);
                                command.Parameters.AddWithValue("@Contraseña", alumnoEditado.Contrasena);
                                command.Parameters.AddWithValue("@Fecha_Inscripcion", alumnoEditado.FechaInscripcion);

                                command.ExecuteNonQuery();
                                CargaTablaAlumnos();
                            }
                        }
                    }
                    break;
                case 5:
                    var filaselecionada = dataGridView1.SelectedRows[0];

                    string contraVieja = filaselecionada.Cells["Contraseña"].Value.ToString();

                    lblContra.Visible = true;
                    txtCambiarContra.Visible = true;
                    lblContraVieja.Visible = true;
                    lblContraVieja1.Text = contraVieja;
                    lblContraVieja1.Visible = true;
                    btnCambiarContraseña.Visible = true;


                    break;

                case 4:
                    if (dataGridView7.SelectedRows.Count > 0)
                    {
                        var filaseleccionada = dataGridView7.SelectedRows[0];

                        int matricula = Convert.ToInt32(filaseleccionada.Cells["Matricula"].Value);
                        int idMateria = Convert.ToInt32(filaseleccionada.Cells["Id_Materia"].Value); 
                        decimal nota = Convert.ToDecimal(filaseleccionada.Cells["Nota"].Value);
                        DateTime fecha = Convert.ToDateTime(filaseleccionada.Cells["Fecha"].Value);
                        string tipo = Convert.ToString(filaseleccionada.Cells["Tipo_Examen"].Value);

                        int Idnota = Convert.ToInt32(filaseleccionada.Cells["Id_NotasXAlumno"].Value);

                        AgregarExamen agregarExamen = new AgregarExamen()
                        {
                            Editar = true,
                            Matricula = matricula,
                            IdMateria = idMateria,
                            Nota = nota,
                            Fecha = fecha,
                            TipoExamen = tipo,
                        };
                        DialogResult resultado = agregarExamen.ShowDialog();
                        if (resultado == DialogResult.OK)
                        {
                            Examen examenEditado = agregarExamen.examenCreado;
                            using (SqlCommand command = new SqlCommand("sp_ActualizarNotaXAlumno", ConexionMenuPrincipal.conectorClase))
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@Id_NotasXAlumno",Idnota);
                                command.Parameters.AddWithValue("@Matricula", examenEditado.Matricula);
                                command.Parameters.AddWithValue("@Id_Materia", examenEditado.Id_Materia);
                                command.Parameters.AddWithValue("@Nota", examenEditado.Nota);
                                command.Parameters.AddWithValue("@Fecha", examenEditado.Fecha);
                                command.Parameters.AddWithValue("@Tipo_Examen", examenEditado.TipoExamen);

                                command.ExecuteNonQuery();
                                CargaTablaExamenes();
                            }
                        }
                    }
                    break;
                case 6:
                    if (dataGridView8.SelectedRows.Count > 0)
                    {
                        var filaseleccionada = dataGridView8.SelectedRows[0];

                        int legajo = Convert.ToInt32(filaseleccionada.Cells["Legajo"].Value);
                        string apellido = filaseleccionada.Cells["Apellido"].Value.ToString();
                        string nombre = filaseleccionada.Cells["nombre"].Value.ToString();
                        string direccion_calle = filaseleccionada.Cells["Dire_Calle"].Value.ToString();
                        string dreccion_numero = filaseleccionada.Cells["Dire_Numero"].Value.ToString();
                        string telefono = filaseleccionada.Cells["Telefono"].Value.ToString();
                        string email = filaseleccionada.Cells["Email"].Value.ToString();
                        DateTime fecha_nac = Convert.ToDateTime(filaseleccionada.Cells["Fecha_Nacimiento"].Value);
                        string dni = filaseleccionada.Cells["Dni"].Value.ToString();

                        agregar EditarProfe = new agregar()
                        {
                            Editar = true,
                            Nombre = nombre,
                            Apellido = apellido,
                            Dni = dni,
                            Dire_calle = direccion_calle,
                            Dire_numero = dreccion_numero,
                            Telefono = telefono,
                            Email = email,
                            Fecha_nacimiento = fecha_nac
                        };
                        
                        DialogResult resultado = EditarProfe.ShowDialog();
                        if (resultado == DialogResult.OK)
                        {
                            Profesor ProfesorEditado = EditarProfe.ProfesorCreado;
                            using (SqlCommand command = new SqlCommand("sp_ActualizarProfesor", ConexionMenuPrincipal.conectorClase))
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@legajo", legajo);
                                command.Parameters.AddWithValue("@Apellido", ProfesorEditado.Apellido);
                                command.Parameters.AddWithValue("@Nombre", ProfesorEditado.Nombre);
                                command.Parameters.AddWithValue("@Dire_Calle", ProfesorEditado.DireccionCalle);
                                command.Parameters.AddWithValue("@Dire_Numero", ProfesorEditado.DireccionNro);
                                command.Parameters.AddWithValue("@Telefono", ProfesorEditado.Telefono);
                                command.Parameters.AddWithValue("@Email", ProfesorEditado.Email);
                                command.Parameters.AddWithValue("@Fecha_Nacimiento", ProfesorEditado.FechaNacimiento);
                                command.Parameters.AddWithValue("@Dni", ProfesorEditado.DNI);
                                command.Parameters.AddWithValue("@Id_Perfil", ProfesorEditado.Perfil);
                                command.Parameters.AddWithValue("@Usuario", ProfesorEditado.Usuario);
                                command.Parameters.AddWithValue("@Contraseña", ProfesorEditado.Contrasena);

                                command.ExecuteNonQuery();
                                CargaTablaProfesores();
                            }
                        }
                    }
                break;
                case 7:
                    if (dataGridView9.SelectedRows.Count > 0)
                    {
                        var filaseleccionada = dataGridView9.SelectedRows[0];

                        int id_personal = Convert.ToInt32(filaseleccionada.Cells["id_personal"].Value);
                        string apellido = filaseleccionada.Cells["Apellido"].Value.ToString();
                        string nombre = filaseleccionada.Cells["nombre"].Value.ToString();
                        string direccion_calle = filaseleccionada.Cells["Dire_Calle"].Value.ToString();
                        string dreccion_numero = filaseleccionada.Cells["Dire_Numero"].Value.ToString();
                        string telefono = filaseleccionada.Cells["Telefono"].Value.ToString();
                        string email = filaseleccionada.Cells["Email"].Value.ToString();
                        DateTime fecha_nac = Convert.ToDateTime(filaseleccionada.Cells["Fecha_Nacimiento"].Value);
                        string dni = filaseleccionada.Cells["Dni"].Value.ToString();

                        agregar EditarPersonal = new agregar()
                        {
                            Editar = true,
                            Nombre = nombre,
                            Apellido = apellido,
                            Dni = dni,
                            Dire_calle = direccion_calle,
                            Dire_numero = dreccion_numero,
                            Telefono = telefono,
                            Email = email,
                            Fecha_nacimiento = fecha_nac
                        };

                        DialogResult resultado = EditarPersonal.ShowDialog();
                        if (resultado == DialogResult.OK)
                        {
                            Personal PersonalEditado = EditarPersonal.PersonalCreado;
                            using (SqlCommand command = new SqlCommand("sp_ActualizarPersonal", ConexionMenuPrincipal.conectorClase))
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@id_personal", id_personal);
                                command.Parameters.AddWithValue("@Apellido", PersonalEditado.Apellido);
                                command.Parameters.AddWithValue("@Nombre", PersonalEditado.Nombre);
                                command.Parameters.AddWithValue("@Dire_Calle", PersonalEditado.DireccionCalle);
                                command.Parameters.AddWithValue("@Dire_Numero", PersonalEditado.DireccionNro);
                                command.Parameters.AddWithValue("@Telefono", PersonalEditado.Telefono);
                                command.Parameters.AddWithValue("@Email", PersonalEditado.Email);
                                command.Parameters.AddWithValue("@Fecha_Nacimiento", PersonalEditado.FechaNacimiento);
                                command.Parameters.AddWithValue("@Dni", PersonalEditado.DNI);
                                command.Parameters.AddWithValue("@Id_Perfil", PersonalEditado.Perfil);
                                command.Parameters.AddWithValue("@Usuario", PersonalEditado.Usuario);
                                command.Parameters.AddWithValue("@Contraseña", PersonalEditado.Contrasena);

                                command.ExecuteNonQuery();
                                CargaTablaPersonal();
                            }
                        }
                    }
                    break;
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

            confirmar confirmar = new confirmar();
            DialogResult dialogResult = confirmar.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                switch (tabControl1.SelectedIndex) //con esto doy orden segun donde este
                {
                    case 1:
                        int idCarrera = Convert.ToInt32(rowCarrera.Cells["id_carrera"].Value);

                        using (SqlCommand cmd = new SqlCommand("sp_EliminarCarrera", ConexionMenuPrincipal.conectorClase))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@id_Carrera", idCarrera);
                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("La Carrera con ID " + idCarrera + " fue eliminada.");
                        CargaTablaCarreras();
                        break;
                    case 2:

                        int idMateria = Convert.ToInt32(row.Cells["id_materia"].Value);

                        using (SqlCommand cmd = new SqlCommand("sp_EliminarMateria", ConexionMenuPrincipal.conectorClase))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@id_materia", idMateria);
                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("La materia con ID " + idMateria + " fue eliminada.");
                        CargaTablaMaterias();
                        break;

                    case 3:
                        
                        int idAlumno = Convert.ToInt32(rowAlumno.Cells["Matricula"].Value);
                        using (SqlCommand cmd = new SqlCommand("sp_EliminarAlumno", ConexionMenuPrincipal.conectorClase))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@matricula", idAlumno.ToString());
                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("El alumno con ID " + idAlumno + " fue eliminado.");
                        CargaTablaAlumnos();
                        break;

                    case 4:
                        int idNotaxAlumno = Convert.ToInt32(dataGridView7.SelectedRows[0].Cells["Id_NotasXAlumno"].Value); //obetiene el id de la notaxalumno, la cual esta no visible

                        using (SqlCommand cmd = new SqlCommand("sp_EliminarNotaxAlumno", ConexionMenuPrincipal.conectorClase))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@id_NotasxAlumno", idNotaxAlumno);
                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("El examen de ID : " + idNotaxAlumno + " fue eliminado.");
                        CargaTablaExamenes();
                        break;
                    case 6:
                        int idProfesor = Convert.ToInt32(dataGridView8.SelectedRows[0].Cells["Legajo"].Value);

                        using (SqlCommand cmd = new SqlCommand("sp_EliminarProfesor", ConexionMenuPrincipal.conectorClase))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@legajo", idProfesor);
                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("El Profesor de ID : " + idProfesor + " fue eliminado.");
                        CargaTablaProfesores();
                        break;
                    case 7:
                        int idPersonal = Convert.ToInt32(rowPersonal.Cells["id_Personal"].Value);

                        using (SqlCommand cmd = new SqlCommand("sp_EliminarPersonal", ConexionMenuPrincipal.conectorClase))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@id_Personal", idPersonal);
                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("El Personal de ID : " + idPersonal + " fue eliminado.");
                        CargaTablaPersonal();
                        break;
                        
                }
            }
        }

        private void btnRecargar_Click(object sender, EventArgs e)
        {
            CargaTablaAlumnos();
            CargaTablaExamenes();
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            VariableGlobal.entro = false;
            VariableGlobal.perfil = 0;
            VariableGlobal.DatosUsuario = null;

            this.Hide();
            LogIn logIn = new LogIn();
            logIn.Show();
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            if (VariableGlobal.perfil == 1)
                tabControl1.SelectedIndex = 8;
            if (VariableGlobal.perfil == 4)
                tabControl1.SelectedIndex = 1;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            pnlAsignarMaterias.Visible = false;
            idProfe = -1;
            switch (VariableGlobal.perfil) //cambiar username por tipo de permiso
            {
                case 1:
                    if (tabControl1.SelectedIndex == 0)
                    {
                        btnAgregar.Hide();
                        btnEditar.Hide();
                        btnEliminar.Hide();
                    }
                    if (tabControl1.SelectedIndex == 1)
                    {
                        btnAgregar.Show();
                        btnEditar.Show();
                        btnEliminar.Show();
                    }

                    if (tabControl1.SelectedIndex == 2)
                    {
                        btnAgregar.Show();
                        btnEditar.Show();
                        btnEliminar.Show();
                    }
                    if (tabControl1.SelectedIndex == 3)
                    {
                        btnAgregar.Show();
                        btnEditar.Show();
                        btnEliminar.Show();
                    }
                    if (tabControl1.SelectedIndex == 4)
                    {
                        btnAgregar.Show();
                        btnEditar.Show();
                        btnEliminar.Show();
                    }
                    if (tabControl1.SelectedIndex == 5)
                    {
                        btnAgregar.Hide();
                        btnEditar.Show();
                        btnEliminar.Hide();
                    }
                    if (tabControl1.SelectedIndex == 6)
                    {
                        btnAgregar.Show();
                        btnEditar.Show();
                        btnEliminar.Show();
                    }
                    if (tabControl1.SelectedIndex == 7)
                    {
                        btnAgregar.Show();
                        btnEditar.Show();
                        btnEliminar.Show();
                    }
                    if (tabControl1.SelectedIndex == 8)
                    {
                        btnAgregar.Hide();
                        btnEditar.Hide();
                        btnEliminar.Hide();
                    }
                    if (tabControl1.SelectedIndex == 9)
                    {
                        btnAgregar.Hide();
                        btnEditar.Hide();
                        btnEliminar.Hide();
                    }


                    break;

                case 2:
                    if (tabControl1.SelectedIndex == 0)
                    {
                        btnAgregar.Hide();
                        btnEditar.Hide();
                        btnEliminar.Hide();
                    }
                    if (tabControl1.SelectedIndex == 1)
                    {
                        btnAgregar.Show();
                        btnEditar.Show();
                        btnEliminar.Hide();
                    }

                    if (tabControl1.SelectedIndex == 2)
                    {
                        btnAgregar.Show();
                        btnEditar.Show();
                        btnEliminar.Show();
                    }
                    if (tabControl1.SelectedIndex == 3)
                    {
                        btnAgregar.Show();
                        btnEditar.Show();
                        btnEliminar.Show();
                    }
                    if (tabControl1.SelectedIndex == 4)
                    {
                        btnAgregar.Show();
                        btnEditar.Show();
                        btnEliminar.Show();
                    }
                    if (tabControl1.SelectedIndex == 5)
                    {
                        btnAgregar.Hide();
                        btnEditar.Hide();
                        btnEliminar.Hide();
                    }

                    break;

                case 3: 
                    if (tabControl1.SelectedIndex == 0)
                    {
                        btnAgregar.Hide();
                        btnEditar.Hide();
                        btnEliminar.Hide();
                    }
                    if (tabControl1.SelectedIndex == 1)
                    {
                        btnAgregar.Hide();
                        btnEditar.Hide();
                        btnEliminar.Hide();
                    }

                    if (tabControl1.SelectedIndex == 2)
                    {
                        btnAgregar.Hide();
                        btnEditar.Hide();
                        btnEliminar.Hide();
                    }
                    if (tabControl1.SelectedIndex == 3)
                    {
                        btnAgregar.Hide();
                        btnEditar.Hide();
                        btnEliminar.Hide();
                    }
                    //botones nuevos editar
                    if (tabControl1.SelectedIndex == 2)
                    {
                        btnAgregarExamenProfe.Show();
                        btnEditarExamenProfesor.Show();
                    }
                    else
                    {
                        btnAgregarExamenProfe.Hide();
                        btnEditarExamenProfesor.Hide();
                    }
                    

                    break;

                case 4:
                    if (tabControl1.SelectedIndex == 0)
                    {
                        btnAgregar.Hide();
                        btnEditar.Hide();
                        btnEliminar.Hide();
                    }
                    if (tabControl1.SelectedIndex == 1)
                    {
                        btnAgregar.Hide();
                        btnEditar.Hide();
                        btnEliminar.Hide();
                    }

                    if (tabControl1.SelectedIndex == 2)
                    {
                        btnAgregar.Hide();
                        btnEditar.Hide();
                        btnEliminar.Hide();
                    }
                    if (tabControl1.SelectedIndex == 3)
                    {
                        btnAgregar.Hide();
                        btnEditar.Hide();
                        btnEliminar.Hide();
                    }

                    break;

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            switch (VariableGlobal.perfil) //cambiar username por tipo de permiso
            {
                case 1:
                    if (tabControl1.SelectedIndex == 2) { CargaTablaMaterias(); }
                    if (tabControl1.SelectedIndex == 3) { CargaTablaAlumnos(); }
                    if (tabControl1.SelectedIndex == 4) { CargaTablaExamenes(); }

                    break;
                case 2:
                    if (tabControl1.SelectedIndex == 2) { CargaTablaMaterias(); }
                    if (tabControl1.SelectedIndex == 3) { CargaTablaAlumnos(); }
                    if (tabControl1.SelectedIndex == 4) { CargaTablaExamenes(); }
                    break;
                case 3:
                    if (tabControl1.SelectedIndex == 2) { CargaTablaMaterias(); }
                    break;
            }
        }
        DataGridViewRow row;
        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            row = dataGridView4.Rows[e.RowIndex];
        }
        DataGridViewRow rowAlumno;
        private void dataGridView3_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            rowAlumno = dataGridView3.Rows[e.RowIndex];
        }
        DataGridViewRow rowExamen;
        private void dataGridView7_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            rowExamen = dataGridView4.Rows[e.RowIndex];
        }
        DataGridViewRow rowPersonal;
        private void dataGridView9_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            rowPersonal = dataGridView9.Rows[e.RowIndex];
        }
        DataGridViewRow rowCarrera;
        private void dataGridView2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            rowCarrera = dataGridView2.Rows[e.RowIndex];
        }
        private void tpInicioGral_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            //materias
            switch (VariableGlobal.perfil) //cambiar username por tipo de permiso
            {
                case 1:
                    tabControl1.SelectedIndex = 2;
                    break;
                case 2:
                    tabControl1.SelectedIndex = 2;
                    break;
                case 3:
                    tabControl1.SelectedIndex = 2;
                    break;
                case 4:
                    tabControl1.SelectedIndex = 2;
                    break;
            }
        }

        private void btnGestionAlumnos_Click(object sender, EventArgs e)
        {
            switch (VariableGlobal.perfil) //cambiar username por tipo de permiso
            {
                case 1:
                    tabControl1.SelectedIndex = 3;
                    break;
                case 2:
                    tabControl1.SelectedIndex = 3;
                    break;
                case 3:
                    tabControl1.SelectedIndex = 2;
                    break;
                case 4:
                    tabControl1.SelectedIndex = 2;
                    break;
            }
        }

        private void btnGestionExamen_Click(object sender, EventArgs e)
        {
            switch (VariableGlobal.perfil) //cambiar username por tipo de permiso
            {
                case 1:
                    tabControl1.SelectedIndex = 4;
                    break;
                case 2:
                    tabControl1.SelectedIndex = 4;
                    break;
                case 4:
                    tabControl1.SelectedIndex = 2;
                    break;
            }
        }

        private void btnBuscarAlumno_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text;
            string apellido = txtApellido.Text;
            string materia = cbMateria.Text;
            using (SqlCommand cmd = new SqlCommand("sp_BuscarAlumnoExamen", ConexionMenuPrincipal.conectorClase))
            {
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.AddWithValue("@Nombre", string.IsNullOrEmpty(nombre) ? (object)DBNull.Value : nombre);
               cmd.Parameters.AddWithValue("@Apellido", string.IsNullOrEmpty(apellido) ? (object)DBNull.Value : apellido);
               cmd.Parameters.AddWithValue("@Materia",string.IsNullOrEmpty(materia) ? (object)DBNull.Value : materia);
               using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
               {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView7.DataSource = dt;

                    dataGridView7.Columns["Id_NotasXAlumno"].Visible = false;
                    dataGridView7.Columns["Id_Materia"].Visible = false;
                }
            }
            
        }

        private void panel11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnRecargarExamenes_Click(object sender, EventArgs e)
        {
            CargaTablaExamenes();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //btn buscar por materia
            btnBuscarAlumno_Click(sender, e);
        }

        private void MenuPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //btnreCargarPersonal
            CargaTablaPersonal();
        }

        private void btnCargarCarrera_Click(object sender, EventArgs e)
        {
            CargaTablaCarreras();
        }

        private void btnCambiarcontrasena_Click(object sender, EventArgs e)
        {
            panelContrasena.Visible = true;
        }

        private void btnAceptarcontra_Click(object sender, EventArgs e)
        {
            if(txtContrasena1.Text==txtContrasena2.Text && txtContrasena1.Text != "")
            {
                Cambiarcontrasenia();
                MessageBox.Show("Contrseña actualizada");
                panelContrasena.Visible = false;
            }
            else
            {
                errorProvider1.SetError(txtContrasena2, "Compruebe haber escrito la misma contraseña en los dos campos (NO SE PERMITE CONTRASEÑA VACIA)");
                return;
            }
            
        }

        private void button4_Click_2(object sender, EventArgs e)
        {
            panelContrasena.Visible = false;
        }

        private void btnEditarInfo_Click(object sender, EventArgs e)
        {

            agregar formulario = new agregar();

            formulario.Nombre = VariableGlobal.DatosUsuario.Nombre;
            formulario.Apellido = VariableGlobal.DatosUsuario.Apellido;
            formulario.Dni = VariableGlobal.DatosUsuario.DNI;
            formulario.Dire_calle = VariableGlobal.DatosUsuario.DireccionCalle;
            formulario.Dire_numero = VariableGlobal.DatosUsuario.DireccionNro;
            formulario.Telefono = VariableGlobal.DatosUsuario.Telefono;
            formulario.Email = VariableGlobal.DatosUsuario.Email;
            formulario.Fecha_nacimiento = VariableGlobal.DatosUsuario.FechaNacimiento;
            //edicion propia
            formulario.Editar= true;
            formulario.MiEditar= true; //con esto indico q me edito a mi mismo

            switch (VariableGlobal.DatosUsuario.Perfil)
            {
                case 1:
                    formulario.Perfil = 1;
                    break;
                case 2:
                    formulario.Perfil = 2;
                    break;
                case 3:
                    formulario.Perfil = 3;
                    break;
                case 4:
                    formulario.Perfil = 4;
                    
                    break;
            }


            formulario.ShowDialog();
            if (formulario.DialogResult == DialogResult.OK)
            {
                //acomodar todo lo que falta aca
                using (SqlCommand command = new SqlCommand("sp_ActualizarInfo", ConexionMenuPrincipal.conectorClase))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Apellido", formulario.PersonalCreado.Apellido);
                    command.Parameters.AddWithValue("@Nombre", formulario.PersonalCreado.Nombre);
                    command.Parameters.AddWithValue("@Dire_Calle", formulario.PersonalCreado.DireccionCalle);
                    command.Parameters.AddWithValue("@Dire_Numero", formulario.PersonalCreado.DireccionNro);
                    command.Parameters.AddWithValue("@Telefono", formulario.PersonalCreado.Telefono);
                    command.Parameters.AddWithValue("@Email", formulario.PersonalCreado.Email);
                    command.Parameters.AddWithValue("@Fecha_Nacimiento", formulario.PersonalCreado.FechaNacimiento);
                    command.Parameters.AddWithValue("@Dni", formulario.PersonalCreado.DNI);
                    command.Parameters.AddWithValue("@usuario",VariableGlobal.DatosUsuario.Usuario);
                    //deshabilitar o modificar esta opcion para el admin por que su usuario es admin y no un numero!!
                    command.ExecuteNonQuery();
                    MessageBox.Show("Cierre sesion y vuelva a ingresar para registrar los cambios!");
                }
            }
        }

        private void dataGridView3_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEditar_Click(sender, e);
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEditar_Click(sender, e);
        }

        private void dataGridView7_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (VariableGlobal.perfil == 1 || VariableGlobal.perfil == 2)
            {
                btnEditar_Click(sender, e);
            }
            else
            {
                btnEditarExamenProfesor_Click(sender, e);
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //lo aprete sin querer jeje
        }

        private void AgregarExamenProfe_Click(object sender, EventArgs e)
        {
            AgregarExamen agregarExamen = new AgregarExamen();
            DialogResult resultado = agregarExamen.ShowDialog();
            if (resultado == DialogResult.OK)
            {
                Examen nuevoExamen = agregarExamen.examenCreado;
                using (SqlCommand command = new SqlCommand("sp_InsertarNotasXAlumno", ConexionMenuPrincipal.conectorClase))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Matricula", nuevoExamen.Matricula);
                    command.Parameters.AddWithValue("@Id_Materia", nuevoExamen.Id_Materia);
                    command.Parameters.AddWithValue("@Nota", nuevoExamen.Nota);
                    command.Parameters.AddWithValue("@Fecha", nuevoExamen.Fecha);
                    command.Parameters.AddWithValue("@Tipo_Examen", nuevoExamen.TipoExamen);

                    command.ExecuteNonQuery();
                    MessageBox.Show("Examen Cargado");
                    CargaTablaExamenes();
                }
            }
        }

        private void btnEditarExamenProfesor_Click(object sender, EventArgs e)
        {
            if (dataGridView7.SelectedRows.Count > 0)
            {
                var filaseleccionada = dataGridView7.SelectedRows[0];

                int matricula = Convert.ToInt32(filaseleccionada.Cells["Matricula"].Value);
                int idMateria = Convert.ToInt32(filaseleccionada.Cells["Id_Materia"].Value);
                decimal nota = Convert.ToDecimal(filaseleccionada.Cells["Nota"].Value);
                DateTime fecha = Convert.ToDateTime(filaseleccionada.Cells["Fecha"].Value);
                string tipo = Convert.ToString(filaseleccionada.Cells["Tipo_Examen"].Value);

                int Idnota = Convert.ToInt32(filaseleccionada.Cells["Id_NotasXAlumno"].Value);

                AgregarExamen agregarExamen = new AgregarExamen()
                {
                    Editar = true,
                    Matricula = matricula,
                    IdMateria = idMateria,
                    Nota = nota,
                    Fecha = fecha,
                    TipoExamen = tipo,
                };
                DialogResult resultado = agregarExamen.ShowDialog();
                if (resultado == DialogResult.OK)
                {
                    Examen examenEditado = agregarExamen.examenCreado;
                    using (SqlCommand command = new SqlCommand("sp_ActualizarNotaXAlumno", ConexionMenuPrincipal.conectorClase))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id_NotasXAlumno", Idnota);
                        command.Parameters.AddWithValue("@Matricula", examenEditado.Matricula);
                        command.Parameters.AddWithValue("@Id_Materia", examenEditado.Id_Materia);
                        command.Parameters.AddWithValue("@Nota", examenEditado.Nota);
                        command.Parameters.AddWithValue("@Fecha", examenEditado.Fecha);
                        command.Parameters.AddWithValue("@Tipo_Examen", examenEditado.TipoExamen);

                        command.ExecuteNonQuery();
                        CargaTablaExamenes();
                    }
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            CargaTablaProfesores();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
        }

        private void cmbBusqueda_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBusqueda.SelectedIndex == 0) //alumno
            {
                pnlAlumno.Visible = true;
                pnlCarrera.Visible = true;
                pnlMateria.Visible = false;
            }
            else
            {
                pnlAlumno.Visible = false;
                pnlCarrera.Visible = false;
                pnlMateria.Visible = false;
            }

            if (cmbBusqueda.SelectedIndex == 1) //profesores
            {
                pnlCarrera.Visible = true;
                    using (SqlCommand cmd = new SqlCommand("execute sp_TODOSprofesorxCarrera", ConexionMenuPrincipal.conectorClase))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                        using (SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dataTable = new DataTable();
                            dataAdapter.Fill(dataTable);
                            dataGridView10.DataSource = dataTable;
                        }
                    }
            }


            if (cmbBusqueda.SelectedIndex == 2) //personal
                using(SqlCommand cmd = new SqlCommand("select Apellido,Nombre,Telefono,Dire_Calle,Dire_Numero,Email from Personal", ConexionMenuPrincipal.conectorClase))
                { 
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    using(SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        da.Fill(dataTable);
                        dataGridView10.DataSource = dataTable;
                    }
                }

        }

        private void cmbCarreraReporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBusqueda.SelectedIndex == 1 && (cmbCarreraReporte.SelectedIndex == 0))
            {
                using (SqlCommand cmd = new SqlCommand("execute sp_TODOSprofesorxCarrera", ConexionMenuPrincipal.conectorClase))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);
                        dataGridView10.DataSource = dataTable;
                    }
                }
            }
            if (cmbBusqueda.SelectedIndex == 1 && (cmbCarreraReporte.SelectedIndex == 1 || cmbCarreraReporte.SelectedIndex == 2))
            {
                string cadena = $"execute sp_profesorxCarrera @nombre_carrera = '{cmbCarreraReporte.Text}'";

                using (SqlCommand cmd = new SqlCommand(cadena, ConexionMenuPrincipal.conectorClase))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);
                        dataGridView10.DataSource = dataTable;
                    }
                }
            }
            if (cmbBusqueda.SelectedIndex == 0 && cmbCarreraReporte.SelectedIndex == 0 )
            {
                string cadena = $"execute sp_AlumnosAll";

                using (SqlCommand cmd = new SqlCommand(cadena, ConexionMenuPrincipal.conectorClase))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);
                        dataGridView10.DataSource = dataTable;
                    }
                }
            }
            if (cmbBusqueda.SelectedIndex==0 && (cmbCarreraReporte.SelectedIndex == 1 || cmbCarreraReporte.SelectedIndex == 2))
            {
                string cadena = $"execute sp_AlumnosRespectivaCarrera @nombre_carrera = '{cmbCarreraReporte.Text}'";

                using (SqlCommand cmd = new SqlCommand(cadena, ConexionMenuPrincipal.conectorClase))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);
                        dataGridView10.DataSource = dataTable;
                    }
                }
            }
        }

        private void GuardarReporte(DataGridView dataGridView, string RutaArchivo)
        {
            using (StreamWriter sw = new StreamWriter(RutaArchivo))
            {
                for (int i = 0; i < dataGridView.Columns.Count; i++)
                {
                    sw.Write(dataGridView.Columns[i].HeaderText);
                    if (i < dataGridView.Columns.Count - 1)
                    {
                        sw.Write('\t');
                    }
                }
                sw.WriteLine();

                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                        if (row.Cells[i].Value != null)
                        {
                            sw.Write(row.Cells[i].Value.ToString());
                        }
                        if (i < row.Cells.Count - 1)
                        {
                            sw.Write('\t');
                        }
                    }
                    sw.WriteLine();
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //btnGuardar

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                GuardarReporte(dataGridView10,saveFileDialog1.FileName);
            }
        }

        private void btnAsignarMateria_Click(object sender, EventArgs e)
        {
            if (idProfe != -1)
                pnlAsignarMaterias.Visible = true;
            else
                MessageBox.Show("Primero seleccione un profesor");
        }

        private void btnAsignarCarrera_Click(object sender, EventArgs e)
        {
            
        }
        //obtener datos de datagrids
        int idProfe = -1;
        string NombreProfe;
        private void dataGridView8_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow fila = dataGridView8.Rows[e.RowIndex];
            idProfe = Convert.ToInt32(fila.Cells["Legajo"].Value);
            NombreProfe = fila.Cells["Nombre"].Value.ToString() + fila.Cells["Apellido"].Value;
        }
        int idAlumno = -1;
        string NombreAlumno;
        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow fila = dataGridView3.Rows[e.RowIndex];
            idAlumno = Convert.ToInt32(fila.Cells["Matricula"].Value);
            NombreAlumno = fila.Cells["Nombre"].Value.ToString() + fila.Cells["Apellido"].Value;
        }

        //fin

        private void btnAceptarAsignacionCarrera_Click(object sender, EventArgs e)
        {
            
            MessageBox.Show("Carrera asignada a " + NombreProfe);
            
        }

        private void btnAsignarProfeCarrera_Click(object sender, EventArgs e)
        {
            //asignar materia
            using (SqlCommand cmd = new SqlCommand("sp_InsertarProfesorXMateria", ConexionMenuPrincipal.conectorClase))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_materia", cmbMateriasProfes.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@legajo", idProfe);

                cmd.ExecuteNonQuery();
            }
            MessageBox.Show("Materia asignada a " + NombreProfe);
            pnlAsignarMaterias.Visible = false;
        }

        private void btnCerrarAsignacionCarrera_Click(object sender, EventArgs e)
        {
            
        }

        private void btnCerrarAsignacionMateria_Click(object sender, EventArgs e)
        {
            pnlAsignarMaterias.Visible=false;
        }

        private async void textBox2_TextChanged(object sender, EventArgs e)
        {
            //si cambia el texto del apellido del alumno
            using (SqlCommand cmd = new SqlCommand($"execute sp_AlumnosYCarrera @apellido = '{textBox2.Text}'", ConexionMenuPrincipal.conectorClase))
            {
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                using (SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd))
                {
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    dataGridView10.DataSource = dataTable;
                }
            }
        }

        private void btnImprimirHistorialExamenes_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                GuardarReporte(dataGridView6, saveFileDialog1.FileName);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string nombre = txtNombreUsuario.Text;
            string apellido = txtApellidoUsuario.Text;

            using (SqlCommand cmd = new SqlCommand("sp_BuscarContraUsario", ConexionMenuPrincipal.conectorClase))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nombre", nombre);
                cmd.Parameters.AddWithValue("@Apellido", apellido);
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            txtNombreUsuario.Text = string.Empty;
            txtApellidoUsuario.Text = string.Empty;
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CambiarContraseña_Click(object sender, EventArgs e)
        {
            string contraseñaNueva = txtCambiarContra.Text;

            string Usuario = dataGridView1.SelectedRows[0].Cells["Usuario"].Value.ToString(); 

            using (SqlCommand cmd = new SqlCommand("sp_CambiarContraseñaUsuario", ConexionMenuPrincipal.conectorClase))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Usuario",Usuario);
                cmd.Parameters.AddWithValue("@Contrasena", contraseñaNueva);

                cmd.ExecuteNonQuery();
                CargaTablaUsuarios();
            }

            MessageBox.Show("Se cambio la contraseña");
            lblContra.Visible = false;
            txtCambiarContra.Visible = false;
            txtCambiarContra.Text = "";
            lblContraVieja.Visible = false;
            lblContraVieja1.Visible = false;
            btnCambiarContraseña.Visible = false;

        }

        private void btnAsignarAlumnoAMateria_Click(object sender, EventArgs e)
        {
            if (idAlumno != -1)
                pnlSeleccionParaAlumno.Visible = true;
            else
                MessageBox.Show("Primero seleccione un alumno");
        }

        private void btnCancelarAlumnoEnMateria_Click(object sender, EventArgs e)
        {
            pnlSeleccionParaAlumno.Visible = false;
        }
        
        private void btnAceptarAlumnoEnMateria_Click(object sender, EventArgs e)
        {
            //asignar materia
            using (SqlCommand cmd = new SqlCommand("sp_InsertarAlumnoXMateria", ConexionMenuPrincipal.conectorClase))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_materia", cmbMateriaParaAlumno.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@matricula", idAlumno);

                cmd.ExecuteNonQuery();
            }
            MessageBox.Show("Materia asignada a " + NombreAlumno);
            pnlAsignarMaterias.Visible = false;
            pnlSeleccionParaAlumno.Visible = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string nombre = txtNombreTotal.Text;
            string apellido = txtApelldioTotal.Text;

            using (SqlCommand cmd = new SqlCommand("sp_BuscarAlumnoTotal", ConexionMenuPrincipal.conectorClase))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nombre",nombre);
                cmd.Parameters.AddWithValue("@Apellido",apellido);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView3.DataSource = dataTable;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            txtNombreTotal.Text = string.Empty;
            txtApelldioTotal.Text = string.Empty;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string nombre = txtNombreProfe.Text;
            string apellido = txtApellidoProfe.Text;

            using (SqlCommand cmd = new SqlCommand("sp_BuscarProfesorTotal", ConexionMenuPrincipal.conectorClase))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nombre", nombre);
                cmd.Parameters.AddWithValue("@Apellido", apellido);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView8.DataSource = dataTable;
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            string nombre = txtNombrePersonal.Text;
            string apellido = txtApellidoPersonal.Text;

            using (SqlCommand cmd = new SqlCommand("sp_BuscarPersonalTotal", ConexionMenuPrincipal.conectorClase))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nombre", nombre);
                cmd.Parameters.AddWithValue("@Apellido", apellido);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView9.DataSource = dataTable;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            txtNombreProfe.Text = string.Empty;
            txtApellidoProfe.Text = string.Empty;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            txtNombrePersonal.Text = string.Empty;
            txtApellidoPersonal.Text = string.Empty;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            CargaTablaUsuarios();
        }

        private void dataGridView4_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEditar_Click(sender, e);
        }

        private void dataGridView8_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (VariableGlobal.perfil == 1)
            {
                btnEditar_Click(sender, e);
            }
            
        }

        private void dataGridView9_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEditar_Click(sender, e);
        }
    }
}
