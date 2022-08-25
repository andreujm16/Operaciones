using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Conexion
{
    public class CL_Conexion
    {
        private SqlConnection Conexion = new SqlConnection("Server=localhost\\SQLEXPRESS;DataBase= operaciones;Integrated Security=true");
        public SqlConnection Conectar()
        {
            if (Conexion.State == ConnectionState.Closed)
                Conexion.Open();
            return Conexion;
        }
        public SqlConnection Desconectar()
        {
            if (Conexion.State == ConnectionState.Open)
                Conexion.Close();
            return Conexion;
        }
    }

}
