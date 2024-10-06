using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    internal class ConexionBdd
    {
        string cadena = "Data Source=DESKTOP-VBARPRG\\SQLEXPRESS;Initial Catalog=proyectoHilet; Integrated Security=true";
        //string cadena = "Data Source=192.168.0.100; Database=u5; User Id=u5; Password=u5";
        public SqlConnection conectorClase = new SqlConnection();
        public ConexionBdd()
        {
            conectorClase.ConnectionString = cadena;
        }
        public void Abrir()
        {
            conectorClase.Open();
        }
        public void Cerrar()
        {
            conectorClase.Close();
        }
    }
}
