using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Conexion
{
    public class per
    {
        public string documentoiden { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string fechanac { get; set; }
    }

    public class CL_Personas
    {
        private CL_Conexion conexion = new CL_Conexion();

        SqlCommand cmd = new SqlCommand();

        public DataTable Listar()
        {
            string sql = "SELECT * FROM persona";
            cmd.Connection = conexion.Conectar();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;

            SqlDataReader reader;
            DataTable tabla = new DataTable();

            try
            {
                reader = cmd.ExecuteReader();
                tabla.Load(reader);
                conexion.Desconectar();
            }
            catch (Exception ex)
            {
                conexion.Desconectar();
            }

            return tabla;
        }

        public bool ExistePersona(string documentoiden)
        {
            bool result = false;

            string sql = "SELECT * FROM persona WHERE documentoiden = " + documentoiden;

            cmd.Connection = conexion.Conectar();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;

            SqlDataReader reader;

            try
            {
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    result = true;
                }
                else
                {
                    result = false;
                }

                conexion.Desconectar();
            }
            catch (Exception ex)
            {
                conexion.Desconectar();
            }

            return result;
        }

        public bool CrearPersona(per persona)
        {
            bool result = false;

            string sql = String.Format("INSERT INTO persona (documentoiden, nombres, apellidos, fechanac) " +
                "VALUES ('{0}', '{1}', '{2}', '{3}')", persona.documentoiden, persona.nombres, persona.apellidos, persona.fechanac);

            cmd.Connection = conexion.Conectar();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;

            try
            {
                int estado = cmd.ExecuteNonQuery();

                if(estado > 0)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
                conexion.Desconectar();
            }
            catch (Exception ex)
            {
                conexion.Desconectar();
            }

            return result;
        }

        public bool BorrarPersona(string documentoiden)
        {
            bool result = false;

            string sql = String.Format("DELETE FROM persona WHERE documentoiden = '{0}'", documentoiden);

            cmd.Connection = conexion.Conectar();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;

            try
            {
                int estado = cmd.ExecuteNonQuery();

                if (estado > 0)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
                conexion.Desconectar();
            }
            catch (Exception ex)
            {
                conexion.Desconectar();
            }

            return result;
        }

        public bool ValidaInformacionContacto(string tipo, int personaid)
        {
            bool result = false;

            string sql = "SELECT COUNT(id) as cantidad FROM informacioncontacto WHERE personaid = " + personaid + " and tipo = " + tipo;

            cmd.Connection = conexion.Conectar();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;

            SqlDataReader reader;

            try
            {
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    int cantidad = Convert.ToInt32(reader[0].ToString());

                    if(cantidad < 2)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
                else
                {
                    result = false;
                }

                conexion.Desconectar();
            }
            catch (Exception ex)
            {
                conexion.Desconectar();
            }

            return result;
        }

        public bool AgregarInfo(string tipo, string info, int personaid)
        {
            bool result = false;

            string sql = String.Format("INSERT INTO informacioncontacto (personaid, tipo, valor) " +
                "VALUES ({0}, {1}, '{2}')", personaid, tipo, info);

            cmd.Connection = conexion.Conectar();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;

            try
            {
                int estado = cmd.ExecuteNonQuery();

                if (estado > 0)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
                conexion.Desconectar();
            }
            catch (Exception ex)
            {
                conexion.Desconectar();
            }

            return result;
        }

        public DataTable ListarInformacionPersona(int personaid)
        {
            string sql = "SELECT id, personaid, CASE tipo WHEN 1 THEN 'Telefono' WHEN 2 THEN 'Correo' WHEN 3 THEN 'Direccion' END as tipo, valor as informacion  FROM informacioncontacto WHERE personaid = " + personaid;
            cmd.Connection = conexion.Conectar();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;

            SqlDataReader reader;
            DataTable tabla = new DataTable();

            try
            {
                reader = cmd.ExecuteReader();
                tabla.Load(reader);
                conexion.Desconectar();
            }
            catch (Exception ex)
            {
                conexion.Desconectar();
            }

            return tabla;
        }

    }
}
