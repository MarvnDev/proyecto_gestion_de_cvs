using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//comentar
using System;
using System.Data;
using System.Data.SqlClient;

namespace Registros_RRHH
{
    public class cContacto
    {
        // atributos
        private string nombre_completo;
        private int telefono;
        private string email;
        private string departamento;
        private string objetivo;
        private string foto;

        // el evento EventHandler no retorna datos
        public event EventHandler NotificarAdicionEnContacto;
        public event EventHandler NotificarModificacionEnContacto;
        public event EventHandler NotificarEliminacionEnContacto;

        // metodos constructor
        /// <summary>
        /// metodo constructor por defecto
        /// </summary>
        public cContacto() { }
        // metodo constructor con parametros
        public cContacto(string nombre_completo, int telefono, string email, string departamento, string objetivo, string foto)
        {
            this.nombre_completo = nombre_completo;
            this.telefono = telefono;
            this.email = email;
            this.departamento = departamento;
            this.objetivo = objetivo;
            this.foto = foto;
        }

        // propiedades
        public string Nombre_completo { get { return nombre_completo; } set { nombre_completo = value; } }
        public int Telefono { get { return telefono; } set { telefono = value; } }
        public string Email { get { return email; } set { email = value; } }
        public string Departamento { get { return departamento; } set {  departamento = value; }}
        public string Objetivo { get { return objetivo; } set { objetivo = value; } }
        public string Foto { get { return foto; } set { foto = value; } }

        /// <summary>
        /// obtiene todos los registros de la tabla comercial
        /// </summary>
        /// <returns>objeto dataTable</returns>
        // extrayendo registros
        public DataTable GetContactos()
        {
            DataTable mydt = new DataTable();
            cConexion conectar = new cConexion();
            SqlConnection conn = conectar.ConexionServer();

            string sql = "SELECT idContacto, nombre_completo, telefono, email, departamento, objetivo, foto, estado FROM contacto;";
            SqlCommand cmd = new SqlCommand(sql, conn);

            SqlDataReader mydr = null;
            try
            {
                conn.Open();
                mydr = cmd.ExecuteReader();
                mydt.Load(mydr);
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
            }
            return mydt;
        }
        public void agregarContacto()
        {
            cConexion conectar = new cConexion();

            SqlConnection conn = conectar.ConexionServer();

            string insertSql = "insert into contacto (foto) values (@foto); Select IDENT_CURRENT('contacto') as idContacto";

            SqlCommand cmd = new SqlCommand(insertSql, conn);

            //cmd.Parameters.AddWithValue("@nombre_completo", this.nombre_completo);
            cmd.Parameters.AddWithValue("@foto", this.foto);
      
            try
            {
                long id = 0;

                conn.Open();

                id = Convert.ToInt64(cmd.ExecuteScalar());

                if (id > 0)
                {
                    if (NotificarAdicionEnContacto != null)
                    {
                        NotificarAdicionEnContacto(this, EventArgs.Empty);
                    }
                }
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
            }

        }

    }

}

