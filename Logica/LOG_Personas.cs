using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Conexion;

namespace Logica
{
    public class LOG_Personas
    {
        private CL_Personas OBJ = new CL_Personas();

        public DataTable ListarPersonas()
        {
            DataTable tabla = new DataTable();
            tabla = OBJ.Listar();
            return tabla;
        }

        public bool ExistePersona(string documentoiden)
        {
            return OBJ.ExistePersona(documentoiden);
        }
        public bool CrearPersona(per persona)
        {
            return OBJ.CrearPersona(persona);
        }

        public bool BorrarPersona(string documentoiden)
        {
            return OBJ.BorrarPersona(documentoiden);
        }

        public bool ValidaInformacionContacto(string tipo, int personaid)
        {
            return OBJ.ValidaInformacionContacto(tipo, personaid);
        }

        public bool AgregarInfo(string tipo, string info, int personaid)
        {
            return OBJ.AgregarInfo(tipo, info, personaid);
        }

        public DataTable ListarInformacionPersona(int personaid)
        {
            DataTable tabla = new DataTable();
            tabla = OBJ.ListarInformacionPersona(personaid);
            return tabla;
        }
    }
}
