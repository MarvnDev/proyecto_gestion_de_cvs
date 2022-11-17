using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// comentarear
using System;
using System.Data.SqlClient;

namespace Registros_RRHH
{
    public class cConexion
    {
        public SqlConnection ConexionServer()
        {
            SqlConnection conn;
            try
            {
                string cadenaConexion = "Data Source=MARVIN\\SQLEXPRESS; Initial Catalog = registros;Persist Security Info=True; User ID=sa; Password=Pa$$w0rd";
                conn = new SqlConnection(cadenaConexion);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error al conectar", ex);
            }
            return conn;
        }
    }
}
