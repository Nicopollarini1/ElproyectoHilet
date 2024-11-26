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
        string cadena = "Data Source=DESKTOP-HK3C4G0\\SQLEXPRESS;Initial Catalog=ProyectoFinal; Integrated Security=true";
        //string cadena = "Data Source=192.168.0.100; Database=u4; User Id=u4; Password=u4";
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
