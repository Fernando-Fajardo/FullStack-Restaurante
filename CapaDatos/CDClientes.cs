using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CapaDatos
{
    public class CDClientes
    {
        Conexion cd_conexion = new Conexion();

        public DataTable MtdConsultarCliente()
        {
            string QueryConsultarCliente = "Select * from tbl_clientes";
            SqlDataAdapter sqlAdapter = new SqlDataAdapter(QueryConsultarCliente, cd_conexion.MtdAbrirConexion());
            DataTable dt_Cliente = new DataTable();
            sqlAdapter.Fill(dt_Cliente);

            cd_conexion.MtdCerrarConexion();
            return dt_Cliente;
        }

        public void MtdAgregarCliente(string Nombre, string Nit, string Telefono, string Categoria, string Estado, string UsuarioSistema, DateTime FechaSistema)
        {
            string QueryAgregarMenu = "Insert into tbl_clientes(Nombre, Nit, Telefono, Categoria, Estado, UsuarioSistema, Fechasistema) values (@Nombre, @Nit, @Telefono, @Categoria, @Estado, @UsuarioSistema, @FechaSistema)";
            SqlCommand CommandAgregarMenu = new SqlCommand(QueryAgregarMenu, cd_conexion.MtdAbrirConexion());
            CommandAgregarMenu.Parameters.AddWithValue("@Nombre", Nombre);
            CommandAgregarMenu.Parameters.AddWithValue("@Nit", Nit);
            CommandAgregarMenu.Parameters.AddWithValue("@Telefono", Telefono);
            CommandAgregarMenu.Parameters.AddWithValue("@Categoria", Categoria);
            CommandAgregarMenu.Parameters.AddWithValue("@Estado", Estado);
            CommandAgregarMenu.Parameters.AddWithValue("UsuarioSistema", UsuarioSistema);
            CommandAgregarMenu.Parameters.AddWithValue("FechaSistema", FechaSistema);
            CommandAgregarMenu.ExecuteNonQuery();
            cd_conexion.MtdCerrarConexion();
        }

        public void MtdActualizarCliente(int CodigoCliente, string Nombre, string Nit, string Telefono, string Categoria, string Estado, string UsuarioSistema, DateTime FechaSistema)
        {
            string QueryActualizarEmpleado = "Update tbl_clientes set Nombre = @Nombre, Nit = @Nit, Telefono = @Telefono, Categoria = @Categoria, Estado = @Estado, UsuarioSistema = @UsuarioSistema, FechaSistema = @FechaSistema where CodigoCliente = @CodigoCliente";
            SqlCommand CommandActualizarEmpleado = new SqlCommand(QueryActualizarEmpleado, cd_conexion.MtdAbrirConexion());
            CommandActualizarEmpleado.Parameters.AddWithValue("@CodigoCliente", CodigoCliente);
            CommandActualizarEmpleado.Parameters.AddWithValue("@Nombre", Nombre);
            CommandActualizarEmpleado.Parameters.AddWithValue("@Nit", Nit);
            CommandActualizarEmpleado.Parameters.AddWithValue("@Telefono", Telefono);
            CommandActualizarEmpleado.Parameters.AddWithValue("@Categoria", Categoria);
            CommandActualizarEmpleado.Parameters.AddWithValue("@Estado", Estado);
            CommandActualizarEmpleado.Parameters.AddWithValue("@UsuarioSistema", UsuarioSistema);
            CommandActualizarEmpleado.Parameters.AddWithValue("@FechaSistema", FechaSistema);
            CommandActualizarEmpleado.ExecuteNonQuery();
            cd_conexion.MtdCerrarConexion();


        }
        public bool MtdConsultarEncabezado(int CodigoCliente)
        {
            string QueryConsultarEncabezado = "SELECT 1 FROM tbl_EncabezadoOrdenes WHERE CodigoCliente = @CodigoCliente";
            SqlCommand CommandEliminarCliente = new SqlCommand(QueryConsultarEncabezado, cd_conexion.MtdAbrirConexion());
            CommandEliminarCliente.Parameters.AddWithValue("@CodigoCliente", CodigoCliente);
            cd_conexion.MtdAbrirConexion();
            object result = CommandEliminarCliente.ExecuteScalar(); // devuelve 1 o null
            cd_conexion.MtdCerrarConexion();

            if (result != null)
            {

                return true;
            }
            else
            {
                return false;
            }
        }

        public void MtdEliminarCliente(int CodigoCliente)
        {
            string QueryEliminarCliente = "Delete tbl_clientes where CodigoCliente = @CodigoCliente";
            SqlCommand CommandEliminarCliente = new SqlCommand(QueryEliminarCliente, cd_conexion.MtdAbrirConexion());
            CommandEliminarCliente.Parameters.AddWithValue("@CodigoCliente", CodigoCliente);
            CommandEliminarCliente.ExecuteNonQuery();
            cd_conexion.MtdCerrarConexion();
        }
    }
}
