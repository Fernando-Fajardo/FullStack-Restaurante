using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class Conexion
    {
        private SqlConnection db_conexion = new SqlConnection("Data Source=db-restaurante-server.database.windows.net;Initial Catalog=db_restaurante;User ID=Administrador;Password=Pass$2025;Encrypt=False;MultipleActiveResultSets=true");
    
        public SqlConnection MtdAbrirConexion()
        {
            if (db_conexion.State == System.Data.ConnectionState.Closed)
            {
                db_conexion.Open();
            }

            return db_conexion;
        }

        public SqlConnection MtdCerrarConexion()
        {
            if (db_conexion.State == ConnectionState.Open)
            {
                db_conexion.Close();
            }

            return db_conexion;
        }
    }
}
