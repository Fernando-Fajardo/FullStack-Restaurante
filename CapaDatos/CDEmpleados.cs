using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace CapaDatos
{
    public class CDEmpleados
    {
        Conexion cd_conexion = new Conexion();

        public DataTable MtdConsultarEmpleado()
        {
            string QueryConsultarEmpleado = "Select * from tbl_empleados";
            SqlDataAdapter sqlAdapter = new SqlDataAdapter(QueryConsultarEmpleado, cd_conexion.MtdAbrirConexion());
            DataTable dt_Empleado = new DataTable();
            sqlAdapter.Fill(dt_Empleado);

            return dt_Empleado;

            cd_conexion.MtdCerrarConexion();
        }

        public void MtdAgregarEmpleado(string Nombre, string Cargo, decimal Salario, DateTime FechaContratacion, string Estado, string UsuarioSistema, DateTime FechaSistema)
        {
            string QueryAgregarMenu = "Insert into tbl_empleados(Nombre, Cargo, Salario, FechaContratacion, Estado, UsuarioSistema, Fechasistema) values (@Nombre, @Cargo, @Salario, @FechaContratacion, @Estado, @UsuarioSistema, @FechaSistema)";
            SqlCommand CommandAgregarMenu = new SqlCommand(QueryAgregarMenu, cd_conexion.MtdAbrirConexion());
            CommandAgregarMenu.Parameters.AddWithValue("@Nombre", Nombre);
            CommandAgregarMenu.Parameters.AddWithValue("@Cargo", Cargo);
            CommandAgregarMenu.Parameters.AddWithValue("@Salario", Salario);
            CommandAgregarMenu.Parameters.AddWithValue("@FechaContratacion", FechaContratacion);
            CommandAgregarMenu.Parameters.AddWithValue("@Estado", Estado);
            CommandAgregarMenu.Parameters.AddWithValue("UsuarioSistema", UsuarioSistema);
            CommandAgregarMenu.Parameters.AddWithValue("FechaSistema", FechaSistema);
            CommandAgregarMenu.ExecuteNonQuery();
            cd_conexion.MtdCerrarConexion();
        }

        public void MtdActualizarEmpleado(int CodigoEmpleado, string Nombre, string Cargo, decimal Salario, DateTime FechaContratacion, string Estado, string UsuarioSistema, DateTime FechaSistema)
        {
            string QueryActualizarEmpleado = "Update tbl_empleados set Nombre = @Nombre, Cargo = @Cargo, Salario = @Salario, FechaContratacion = @FechaContratacion, Estado = @Estado, UsuarioSistema = @UsuarioSistema, FechaSistema = @FechaSistema where CodigoEmpleado = @CodigoEmpleado";
            SqlCommand CommandActualizarEmpleado = new SqlCommand(QueryActualizarEmpleado, cd_conexion.MtdAbrirConexion());
            CommandActualizarEmpleado.Parameters.AddWithValue("@CodigoEmpleado", CodigoEmpleado);
            CommandActualizarEmpleado.Parameters.AddWithValue("@Nombre", Nombre);
            CommandActualizarEmpleado.Parameters.AddWithValue("@Cargo", Cargo);
            CommandActualizarEmpleado.Parameters.AddWithValue("@Salario", Salario);
            CommandActualizarEmpleado.Parameters.AddWithValue("@FechaContratacion", FechaContratacion);
            CommandActualizarEmpleado.Parameters.AddWithValue("@Estado", Estado);
            CommandActualizarEmpleado.Parameters.AddWithValue("@UsuarioSistema", UsuarioSistema);
            CommandActualizarEmpleado.Parameters.AddWithValue("@FechaSistema", FechaSistema);
            CommandActualizarEmpleado.ExecuteNonQuery();
            cd_conexion.MtdCerrarConexion();


        }

        public bool MtdConsultarUsuario(int CodigoEmpleado)
        {
            string QueryConsultarUsuario = "SELECT 1 FROM tbl_Usuarios WHERE CodigoEmpleado = @CodigoEmpleado";
            SqlCommand CommandEliminarEmpleado = new SqlCommand(QueryConsultarUsuario, cd_conexion.MtdAbrirConexion());
            CommandEliminarEmpleado.Parameters.AddWithValue("@CodigoEmpleado", CodigoEmpleado);
            cd_conexion.MtdAbrirConexion();
            object result = CommandEliminarEmpleado.ExecuteScalar(); // devuelve 1 o null
            cd_conexion.MtdCerrarConexion();

            if (result != null){
                    
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool MtdConsultarEncabezado(int CodigoEmpleado)
        {
            string QueryConsultarEncabezado = "SELECT 1 FROM tbl_EncabezadoOrdenes WHERE CodigoEmpleado = @CodigoEmpleado";
            SqlCommand CommandEliminarEmpleado = new SqlCommand(QueryConsultarEncabezado, cd_conexion.MtdAbrirConexion());
            CommandEliminarEmpleado.Parameters.AddWithValue("@CodigoEmpleado", CodigoEmpleado);
            cd_conexion.MtdAbrirConexion();
            object result = CommandEliminarEmpleado.ExecuteScalar(); // devuelve 1 o null
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

        public bool MtdConsultarPagoPlanillas(int CodigoEmpleado)
        {
            string QueryConsultarPagoPlanillas = "SELECT 1 FROM tbl_PagoPlanillas WHERE CodigoEmpleado = @CodigoEmpleado";
            SqlCommand CommandEliminarEmpleado = new SqlCommand(QueryConsultarPagoPlanillas, cd_conexion.MtdAbrirConexion());
            CommandEliminarEmpleado.Parameters.AddWithValue("@CodigoEmpleado", CodigoEmpleado);
            cd_conexion.MtdAbrirConexion();
            object result = CommandEliminarEmpleado.ExecuteScalar(); // devuelve 1 o null
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

        public void MtdEliminarEmpleado(int CodigoEmpleado)
        {
            string QueryEliminarEmpleado = "Delete tbl_empleados where CodigoEmpleado = @CodigoEmpleado";
            SqlCommand CommandEliminarEmpleado = new SqlCommand(QueryEliminarEmpleado, cd_conexion.MtdAbrirConexion());
            CommandEliminarEmpleado.Parameters.AddWithValue("@CodigoEmpleado", CodigoEmpleado);
            CommandEliminarEmpleado.ExecuteNonQuery();
            cd_conexion.MtdCerrarConexion();
        }
    }
}



