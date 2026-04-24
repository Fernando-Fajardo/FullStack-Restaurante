using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace CapaPresentacion.Seguridad
{
    public abstract class UserConnectionToSql
    {
        private readonly string connectionString;
        public UserConnectionToSql()
        {
            connectionString = "Data Source=db-restaurante-server.database.windows.net;Initial Catalog=db_restaurante;User ID=Administrador;Password=Pass$2025;Encrypt=False;MultipleActiveResultSets=true";
        }
        protected SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

    }
}
