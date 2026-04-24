using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CDPagoPlanillas
    {
        Conexion cd_conexion = new Conexion();

        //primer paso LBL
        public string MtdConsultarEmpleado(int CodigoEmpleado)
        {
            string empleado;

            string QueryConsultarEmpleado = "Select Salario from tbl_empleados where CodigoEmpleado = @CodigoEmpleado";
            SqlCommand CommandEmpleados = new SqlCommand(QueryConsultarEmpleado, cd_conexion.MtdAbrirConexion());
            CommandEmpleados.Parameters.AddWithValue("@CodigoEmpleado", CodigoEmpleado);
            SqlDataReader reader = CommandEmpleados.ExecuteReader();

            if (reader.Read())
            {
                empleado = reader["Salario"].ToString();

            }
            else
            {
                empleado = "";
            }

            cd_conexion.MtdCerrarConexion();
            return empleado;
        }

        //primer paso CBOX
        public List<dynamic> MtdListarEmpleados()
        {
            List<dynamic> ListaEmpleado = new List<dynamic>();
            string QueryListaEmpleado = "Select CodigoEmpleado, Nombre from tbl_empleados";
            SqlCommand cmd = new SqlCommand(QueryListaEmpleado, cd_conexion.MtdAbrirConexion());
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ListaEmpleado.Add(new
                {
                    Value = reader["CodigoEmpleado"],
                    Text = $"{reader["CodigoEmpleado"]} - {reader["Nombre"]}"
                });
            }

            cd_conexion.MtdCerrarConexion();
            return ListaEmpleado;
        }

        public DataTable MtdVerificarPagoPlanilla()
        {
            string QueryConsultarPagoPlanilla = "Select * from tbl_PagoPlanillas";
            SqlDataAdapter sqlAdapter = new SqlDataAdapter(QueryConsultarPagoPlanilla, cd_conexion.MtdAbrirConexion());
            DataTable dt_pagoplanilla = new DataTable();
            sqlAdapter.Fill(dt_pagoplanilla);

            return dt_pagoplanilla;

            cd_conexion.MtdCerrarConexion();
        }

        public void MtdAgregarPagoPlanilla(int CodigoEmpleado, DateTime FechaPago, double Salario, double Bono, double HorasExtras,  double MontoTotal, string Estado, string UsuarioSistema, DateTime FechaSistema)
        {
            string QueryAgregarPagoPlanillas = "Insert into tbl_PagoPlanillas (CodigoEmpleado, FechaPago, Salario,  Bono, HorasExtras, MontoTotal, Estado, UsuarioSistema, FechaSistema) values (@CodigoEmpleado, @FechaPago, @Salario,  @Bono, @HorasExtras, @MontoTotal, @Estado, @UsuarioSistema, @FechaSistema)";
            SqlCommand CommandAgregarPagoPlanilla = new SqlCommand(QueryAgregarPagoPlanillas, cd_conexion.MtdAbrirConexion());
            CommandAgregarPagoPlanilla.Parameters.AddWithValue("@CodigoEmpleado", CodigoEmpleado);
            CommandAgregarPagoPlanilla.Parameters.AddWithValue("@FechaPago", FechaPago);
            CommandAgregarPagoPlanilla.Parameters.AddWithValue("@Salario", Salario);
            CommandAgregarPagoPlanilla.Parameters.AddWithValue("@Bono", Bono);
            CommandAgregarPagoPlanilla.Parameters.AddWithValue("@HorasExtras", HorasExtras);
            CommandAgregarPagoPlanilla.Parameters.AddWithValue("@MontoTotal", MontoTotal);
            CommandAgregarPagoPlanilla.Parameters.AddWithValue("@Estado", Estado);
            CommandAgregarPagoPlanilla.Parameters.AddWithValue("@UsuarioSistema", UsuarioSistema);
            CommandAgregarPagoPlanilla.Parameters.AddWithValue("@FechaSistema", FechaSistema);
            CommandAgregarPagoPlanilla.ExecuteNonQuery();
            cd_conexion.MtdCerrarConexion();
        }

        public void MtdActualizarPagoPlanilla(int CodigoPagoPlanilla, int CodigoEmpleado, DateTime FechaPago, double Salario, double Bono, double HorasExtras, double MontoTotal, string Estado, string UsuarioSistema, DateTime FechaSistema)
        {
            string QueryActualizarPagoPlanillas = "Update tbl_PagoPlanillas set CodigoEmpleado = @CodigoEmpleado, FechaPago = @FechaPago, Salario = @Salario,  Bono = @Bono, HorasExtras = @HorasExtras, MontoTotal = @MontoTotal, Estado = @Estado, UsuarioSistema = @UsuarioSistema, FechaSistema = @FechaSistema where CodigoPagoPlanilla = @CodigoPagoPlanilla";
            SqlCommand CommandActPagoPlanilla = new SqlCommand(QueryActualizarPagoPlanillas, cd_conexion.MtdAbrirConexion());
            CommandActPagoPlanilla.Parameters.AddWithValue("@CodigoPagoPlanilla", CodigoPagoPlanilla);
            CommandActPagoPlanilla.Parameters.AddWithValue("@CodigoEmpleado", CodigoEmpleado);
            CommandActPagoPlanilla.Parameters.AddWithValue("@FechaPago", FechaPago);
            CommandActPagoPlanilla.Parameters.AddWithValue("@Salario", Salario);
            CommandActPagoPlanilla.Parameters.AddWithValue("@Bono", Bono);
            CommandActPagoPlanilla.Parameters.AddWithValue("@HorasExtras", HorasExtras);
            CommandActPagoPlanilla.Parameters.AddWithValue("@MontoTotal", MontoTotal);
            CommandActPagoPlanilla.Parameters.AddWithValue("@Estado", Estado);
            CommandActPagoPlanilla.Parameters.AddWithValue("@UsuarioSistema", UsuarioSistema);
            CommandActPagoPlanilla.Parameters.AddWithValue("@FechaSistema", FechaSistema);
            CommandActPagoPlanilla.ExecuteNonQuery();
            cd_conexion.MtdCerrarConexion();
        }

        public void MtdEliminarPagoPlanilla(int CodigoPagoPlanilla)
        {
            string QueryEliminarPagoPlanillas = "Delete tbl_PagoPlanillas where CodigoPagoPlanilla = @CodigoPagoPlanilla";
            SqlCommand CommandEliminarPago = new SqlCommand(QueryEliminarPagoPlanillas, cd_conexion.MtdAbrirConexion());
            CommandEliminarPago.Parameters.AddWithValue("@CodigoPagoPlanilla", CodigoPagoPlanilla);
            CommandEliminarPago.ExecuteNonQuery();
            cd_conexion.MtdCerrarConexion();
        }
    }
}
