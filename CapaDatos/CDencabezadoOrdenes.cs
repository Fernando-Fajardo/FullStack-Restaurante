using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CDencabezadoOrdenes
    {
        Conexion cd_conexion = new Conexion();
        public List<dynamic> MtdListarCliente()
        {
            List<dynamic> ListaCliente = new List<dynamic>();
            string QueryListaClientes = "Select CodigoCliente, Nombre from tbl_clientes";
            SqlCommand cmd = new SqlCommand(QueryListaClientes, cd_conexion.MtdAbrirConexion());
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ListaCliente.Add(new
                {
                    Value = reader["CodigoCliente"],
                    Text = $"{reader["CodigoCliente"]} - {reader["Nombre"]}"
                });
            }

            cd_conexion.MtdCerrarConexion();
            return ListaCliente;
        }

        public List<dynamic> MtdListarMesas()
        {
            List<dynamic> ListaMesas = new List<dynamic>();
            string QueryListaMesas = "Select CodigoMesa, NumeroMesa from tbl_Mesas";
            SqlCommand cmd = new SqlCommand(QueryListaMesas, cd_conexion.MtdAbrirConexion());
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ListaMesas.Add(new
                {
                    Value = reader["CodigoMesa"],
                    Text = $"{reader["CodigoMesa"]} - Mesa numero {reader["NumeroMesa"]}"
                });
            }

            cd_conexion.MtdCerrarConexion();
            return ListaMesas;
        }

        public List<dynamic> MtdListarEmpleados()
        {
            List<dynamic> ListaEmpleados = new List<dynamic>();
            string QueryListaEmpleados = "Select CodigoEmpleado, Nombre from tbl_Empleados";
            SqlCommand cmd = new SqlCommand(QueryListaEmpleados, cd_conexion.MtdAbrirConexion());
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ListaEmpleados.Add(new
                {
                    Value = reader["CodigoEmpleado"],
                    Text = $"{reader["CodigoEmpleado"]} - {reader["Nombre"]}"
                });
            }

            cd_conexion.MtdCerrarConexion();
            return ListaEmpleados;
        }

        public List<dynamic> MtdListarDetallesOrdenes()
        {
            List<dynamic> ListaDetallesOrdenes = new List<dynamic>();
            string QueryListaDetallesOrdenes = "Select CodigoOrdenDet from tbl_DetallesOrdenes";
            SqlCommand cmd = new SqlCommand(QueryListaDetallesOrdenes, cd_conexion.MtdAbrirConexion());
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ListaDetallesOrdenes.Add(new
                {
                    Value = reader["CodigoOrdenDet"],
                    Text = $"Orden No. - {reader["CodigoOrdenDet"]}"
                });
            }

            cd_conexion.MtdCerrarConexion();
            return ListaDetallesOrdenes;
        }


        public DataTable MtdConsultarEncabezadoOrdenes()
        {
            string QueryConsultarEncabezadoOrdenes = "Select CodigoOrdenEnc, CodigoOrdenDet, CodigoCliente, CodigoMesa, CodigoEmpleado, FechaOrden, MontoTotalOrd, Estado, UsuarioSistema, FechaSistema from tbl_EncabezadoOrdenes";
            SqlDataAdapter sqlAdapter = new SqlDataAdapter(QueryConsultarEncabezadoOrdenes, cd_conexion.MtdAbrirConexion());
            DataTable dt_EncabezadoOrdenes = new DataTable();
            sqlAdapter.Fill(dt_EncabezadoOrdenes);

            cd_conexion.MtdCerrarConexion();
            return dt_EncabezadoOrdenes;
        }

        public Decimal MtdTotalOrd(int CodigoOrdenDet)
        {
            decimal preciototal = 0;

            string QueryConsultarPrecioOrden = "Select PrecioTotal from tbl_DetallesOrdenes where CodigoOrdenDet = @CodigoOrdenDet";
            SqlCommand CommandPrecioOrden = new SqlCommand(QueryConsultarPrecioOrden, cd_conexion.MtdAbrirConexion());
            CommandPrecioOrden.Parameters.AddWithValue("@CodigoOrdenDet", CodigoOrdenDet);
            SqlDataReader reader = CommandPrecioOrden.ExecuteReader();

            if (reader.Read())
            {
                preciototal = decimal.Parse(reader["PrecioTotal"].ToString());
            }
            else
            {
                preciototal = 0;
            }

            cd_conexion.MtdCerrarConexion();
            return preciototal;
        }


        public void MtdAgregarEncabezadoOrdenes(int CodigoOrdenDet, int CodigoCliente, int CodigoMesa, int CodigoEmpleado, DateTime FechaOrden, decimal MontoTotalOrd, string Estado, string UsuarioSistema, DateTime FechaSistema)
        {
            string QueryAgregarEncabezadoOrdenes = "Insert into tbl_EncabezadoOrdenes(CodigoOrdenDet, CodigoCliente, CodigoMesa, CodigoEmpleado, FechaOrden, MontoTotalOrd, Estado, UsuarioSistema, FechaSistema) values (@CodigoOrdenDet, @CodigoCliente, @CodigoMesa, @CodigoEmpleado, @FechaOrden, @MontoTotalOrd, @Estado, @UsuarioSistema, @FechaSistema)";
            SqlCommand CommandAgregarEncabezadoOrdenes = new SqlCommand(QueryAgregarEncabezadoOrdenes, cd_conexion.MtdAbrirConexion());
            CommandAgregarEncabezadoOrdenes.Parameters.AddWithValue("@CodigoOrdenDet", CodigoOrdenDet);
            CommandAgregarEncabezadoOrdenes.Parameters.AddWithValue("@CodigoCliente", CodigoCliente);
            CommandAgregarEncabezadoOrdenes.Parameters.AddWithValue("@CodigoMesa", CodigoMesa);
            CommandAgregarEncabezadoOrdenes.Parameters.AddWithValue("@CodigoEmpleado", CodigoEmpleado);
            CommandAgregarEncabezadoOrdenes.Parameters.AddWithValue("@FechaOrden", FechaOrden);
            CommandAgregarEncabezadoOrdenes.Parameters.AddWithValue("@MontoTotalOrd", MontoTotalOrd);
            CommandAgregarEncabezadoOrdenes.Parameters.AddWithValue("@Estado", Estado);
            CommandAgregarEncabezadoOrdenes.Parameters.AddWithValue("@UsuarioSistema", UsuarioSistema);
            CommandAgregarEncabezadoOrdenes.Parameters.AddWithValue("@FechaSistema", FechaSistema);
            CommandAgregarEncabezadoOrdenes.ExecuteNonQuery();
            cd_conexion.MtdCerrarConexion();
        }

        public void MtdActualizarEncabezadoOrdenes(int CodigoOrdenEnc, int CodigoOrdenDet, int CodigoCliente, int CodigoMesa, int CodigoEmpleado, DateTime FechaOrden, decimal MontoTotalOrd, string Estado, string UsuarioSistema, DateTime FechaSistema)
        {
            string QueryActualizarEncabezadoOrdenes = "Update tbl_EncabezadoOrdenes set CodigoOrdenDet = @CodigoOrdenDet, CodigoCliente = @CodigoCliente, CodigoMesa = @CodigoMesa, CodigoEmpleado = @CodigoEmpleado, FechaOrden = @FechaOrden, MontoTotalOrd = @MontoTotalOrd, Estado = @Estado, UsuarioSistema = @UsuarioSistema, FechaSistema = @FechaSistema where CodigoOrdenEnc = @CodigoOrdenEnc";
            SqlCommand CommandActualizarEncabezadoOrdenes = new SqlCommand(QueryActualizarEncabezadoOrdenes, cd_conexion.MtdAbrirConexion());
            CommandActualizarEncabezadoOrdenes.Parameters.AddWithValue("@CodigoOrdenEnc", CodigoOrdenEnc);
            CommandActualizarEncabezadoOrdenes.Parameters.AddWithValue("@CodigoOrdenDet", CodigoOrdenDet);
            CommandActualizarEncabezadoOrdenes.Parameters.AddWithValue("@CodigoCliente", CodigoCliente);
            CommandActualizarEncabezadoOrdenes.Parameters.AddWithValue("@CodigoMesa", CodigoMesa);
            CommandActualizarEncabezadoOrdenes.Parameters.AddWithValue("@CodigoEmpleado", CodigoEmpleado);
            CommandActualizarEncabezadoOrdenes.Parameters.AddWithValue("@FechaOrden", FechaOrden);
            CommandActualizarEncabezadoOrdenes.Parameters.AddWithValue("@MontoTotalOrd", MontoTotalOrd);
            CommandActualizarEncabezadoOrdenes.Parameters.AddWithValue("@Estado", Estado);
            CommandActualizarEncabezadoOrdenes.Parameters.AddWithValue("@UsuarioSistema", UsuarioSistema);
            CommandActualizarEncabezadoOrdenes.Parameters.AddWithValue("@FechaSistema", FechaSistema);
            CommandActualizarEncabezadoOrdenes.ExecuteNonQuery();
            cd_conexion.MtdCerrarConexion();
        }

        public bool MtdConsultarPago(int CodigoOrdenEnc)
        {
            string QueryConsultarPago = "SELECT 1 FROM tbl_PagoOrdenes WHERE CodigoOrdenEnc = @CodigoOrdenEnc";
            SqlCommand CommandEliminarEncabezadoOrdenes = new SqlCommand(QueryConsultarPago, cd_conexion.MtdAbrirConexion());
            CommandEliminarEncabezadoOrdenes.Parameters.AddWithValue("@CodigoOrdenEnc", CodigoOrdenEnc);
            cd_conexion.MtdAbrirConexion();
            object result = CommandEliminarEncabezadoOrdenes.ExecuteScalar(); // devuelve 1 o null
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

        public void MtdEliminarEncabezadoOrdenes(int CodigoOrdenEnc)
        {
            string QueryEliminarEncabezadoOrdenes = "Delete tbl_EncabezadoOrdenes where CodigoOrdenEnc = @CodigoOrdenEnc";
            SqlCommand CommandEliminarEncabezadoOrdenes = new SqlCommand(QueryEliminarEncabezadoOrdenes, cd_conexion.MtdAbrirConexion());
            CommandEliminarEncabezadoOrdenes.Parameters.AddWithValue("@CodigoOrdenEnc", CodigoOrdenEnc);
            CommandEliminarEncabezadoOrdenes.ExecuteNonQuery();
            cd_conexion.MtdCerrarConexion();
        }
    }
}
