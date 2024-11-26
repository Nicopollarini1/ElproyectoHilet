using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static WindowsFormsApp2.Materia;

namespace WindowsFormsApp2
{
    public partial class AgregarCarrera : Form
    {
        public AgregarCarrera()
        {
            InitializeComponent();
        }

        public Carrera CarreraCreada { get; private set; }
        public string nombrecarrera { get; set; } = string.Empty;
        public string nroresolucion { get; set; } = string.Empty;
        public string anioplandeestudio { get; set; } = string.Empty;
        public bool editar {  get; set; }=false;


        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (ControlAgregarCarrera())
            {
                string nombreCarrera = txtNombreCarrera.Text;
                string nroResolucion = txtNroResolucion.Text;
                int anioPlanEstudio = int.Parse(txtAnioPlanEstudio.Text);

                CarreraCreada = new Carrera(nombreCarrera, nroResolucion, anioPlanEstudio);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public bool ControlAgregarCarrera()
        {   //limpia los mensajes de error antes de validar
            errorProvider1.SetError(txtNombreCarrera, "");
            errorProvider1.SetError(txtNroResolucion, "");
            errorProvider1.SetError(txtAnioPlanEstudio, "");

            if (txtNombreCarrera.Text == "")
            {
                errorProvider1.SetError(txtNombreCarrera, "Ingrese una carrera");
                txtNombreCarrera.Focus();
                return false;
            }
            if (txtNroResolucion.Text == "")
            {
                errorProvider1.SetError(txtNroResolucion, "Ingrese el numero de resolucion valido");
                txtNroResolucion.Focus();
                return false;
            }
            if (txtAnioPlanEstudio.Text == "" || !int.TryParse(txtAnioPlanEstudio.Text, out int anioPlanEstudio))
            {
                errorProvider1.SetError(txtAnioPlanEstudio, "Ingrese el anio del plan de estudio valido");
                txtAnioPlanEstudio.Focus();
                return false;
            }
            return true;
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Abort;
            this.Close();
        }

        private void AgregarCarrera_Load(object sender, EventArgs e)
        {
            txtNombreCarrera.Text = nombrecarrera;
            txtNroResolucion.Text = nroresolucion;
            txtAnioPlanEstudio.Text = anioplandeestudio;
        }
    }
}

