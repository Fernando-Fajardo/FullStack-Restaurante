using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CDPagoOrdenes
    {
        Conexion cd_conexion = new Conexion();

        //primer paso LBL
        public string MtdMontoPagoOrdenes(int CodigoOrdenEnc)//MTDMONTOORDEN
        {
            string montoorden;


            string QueryConsultarPagoOrdenes = "Select MontoTotalOrd from tbl_EncabezadoOrdenes where CodigoOrdenEnc = @CodigoOrdenEnc";
            SqlCommand CommandPagoOrdenes = new SqlCommand(QueryConsultarPagoOrdenes, cd_conexion.MtdAbrirConexion());
            CommandPagoOrdenes.Parameters.AddWithValue("@CodigoOrdenEnc", CodigoOrdenEnc);
            SqlDataReader reader = CommandPagoOrdenes.ExecuteReader();

            if (reader.Read())
            {
                montoorden = reader["MontoTotalOrd"].ToString();

            }
            else
            {
                montoorden = "";
            }

            cd_conexion.MtdCerrarConexion();
            return montoorden;
        }

        //primer paso CBOX
        public List<dynamic> MtdListarOrdenEnc()
        {
            List<dynamic> ListaOrdenEnc = new List<dynamic>();
            string QueryListaOrdenEnc = "Select CodigoOrdenEnc, MontoTotalOrd from tbl_EncabezadoOrdenes";
            SqlCommand cmd = new SqlCommand(QueryListaOrdenEnc, cd_conexion.MtdAbrirConexion());
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ListaOrdenEnc.Add(new
                {
                    Value = reader["CodigoOrdenEnc"],
                    Text = $"{reader["CodigoOrdenEnc"]}"
                });

            }

            cd_conexion.MtdCerrarConexion();
            return ListaOrdenEnc;
        }

        public DataTable MtdConsultarPagoOrdenes()
        {
            string QueryConsultarPagoOrdenes = "Select * from tbl_PagoOrdenes";
            SqlDataAdapter dt_adapter = new SqlDataAdapter(QueryConsultarPagoOrdenes, cd_conexion.MtdAbrirConexion());
            DataTable dt_pagoOrdenes = new DataTable();
            dt_adapter.Fill(dt_pagoOrdenes);
            cd_conexion.MtdCerrarConexion();
            return dt_pagoOrdenes;
        }

        public void MtdAgregarPagoOrdenes(int CodigoOrdenEnc, decimal MontoOrden, decimal Propina, decimal Impuesto, decimal Descuento, decimal TotalPago, string MetodoPago, string Estado, DateTime FechaPago, string UsuarioSistema, DateTime FechaSistema)
        {
            string QueryAgregarPagoOrdenes = "Insert into tbl_PagoOrdenes(CodigoOrdenEnc, MontoOrden, Propina, Impuesto, Descuento, TotalPago, MetodoPago, Estado, FechaPago, UsuarioSistema, FechaSistema) values (@CodigoOrdenEnc, @MontoOrden, @Propina, @Impuesto, @Descuento, @TotalPago, @MetodoPago, @Estado, @FechaPago, @UsuarioSistema, @FechaSistema)";
            SqlCommand CommandAgregarPagoOrdenes = new SqlCommand(QueryAgregarPagoOrdenes, cd_conexion.MtdAbrirConexion());
            CommandAgregarPagoOrdenes.Parameters.AddWithValue("@CodigoOrdenEnc", CodigoOrdenEnc);
            CommandAgregarPagoOrdenes.Parameters.AddWithValue("@MontoOrden", MontoOrden);
            CommandAgregarPagoOrdenes.Parameters.AddWithValue("@Propina", Propina);
            CommandAgregarPagoOrdenes.Parameters.AddWithValue("@Impuesto", Impuesto);
            CommandAgregarPagoOrdenes.Parameters.AddWithValue("@Descuento", Descuento);
            CommandAgregarPagoOrdenes.Parameters.AddWithValue("@TotalPago", TotalPago);
            CommandAgregarPagoOrdenes.Parameters.AddWithValue("@MetodoPago", MetodoPago);
            CommandAgregarPagoOrdenes.Parameters.AddWithValue("@Estado", Estado);
            CommandAgregarPagoOrdenes.Parameters.AddWithValue("@FechaPago", FechaPago);
            CommandAgregarPagoOrdenes.Parameters.AddWithValue("@UsuarioSistema", UsuarioSistema);
            CommandAgregarPagoOrdenes.Parameters.AddWithValue("@FechaSistema", FechaSistema);
            CommandAgregarPagoOrdenes.ExecuteNonQuery();
            cd_conexion.MtdCerrarConexion();
        }

        public void MtdActualizarPagoOrdenes(int CodigoPago, int CodigoOrdenEnc, decimal MontoOrden, decimal Propina, decimal Impuesto, decimal Descuento, decimal TotalPago, string MetodoPago, string Estado, DateTime FechaPago, string UsuarioSistema, DateTime FechaSistema)
        {
            string QueryActualizarPagoOrdenes = "Update tbl_PagoOrdenes set CodigoOrdenEnc = @CodigoOrdenEnc, MontoOrden = @MontoOrden, Propina = @Propina, Impuesto = @Impuesto, Descuento = @Descuento, TotalPago = @TotalPago, MetodoPago = @MetodoPago, Estado= @Estado, FechaPago= @FechaPago, UsuarioSistema= @UsuarioSistema, FechaSistema= @FechaSistema where CodigoPago = @CodigoPago";
            SqlCommand CommanActualizarPagoOrdenes = new SqlCommand(QueryActualizarPagoOrdenes, cd_conexion.MtdAbrirConexion());
            CommanActualizarPagoOrdenes.Parameters.AddWithValue("@CodigoPago", CodigoPago);
            CommanActualizarPagoOrdenes.Parameters.AddWithValue("@CodigoOrdenEnc", CodigoOrdenEnc);
            CommanActualizarPagoOrdenes.Parameters.AddWithValue("@MontoOrden", MontoOrden);
            CommanActualizarPagoOrdenes.Parameters.AddWithValue("@Propina", Propina);
            CommanActualizarPagoOrdenes.Parameters.AddWithValue("@Impuesto", Impuesto);
            CommanActualizarPagoOrdenes.Parameters.AddWithValue("@Descuento", Descuento);
            CommanActualizarPagoOrdenes.Parameters.AddWithValue("@TotalPago", TotalPago);
            CommanActualizarPagoOrdenes.Parameters.AddWithValue("@MetodoPago", MetodoPago);
            CommanActualizarPagoOrdenes.Parameters.AddWithValue("@Estado", Estado);
            CommanActualizarPagoOrdenes.Parameters.AddWithValue("@FechaPago", FechaPago);
            CommanActualizarPagoOrdenes.Parameters.AddWithValue("@UsuarioSistema", UsuarioSistema);
            CommanActualizarPagoOrdenes.Parameters.AddWithValue("@FechaSistema", FechaSistema);
            CommanActualizarPagoOrdenes.ExecuteNonQuery();
            cd_conexion.MtdCerrarConexion();
        }

        public void MtdEliminarPagoOrden(int CodigoPago)
        {
            string QueryEliminarpago = "Delete tbl_PagoOrdenes where CodigoPago = @CodigoPago";
            SqlCommand CommandEliminarPago = new SqlCommand(QueryEliminarpago, cd_conexion.MtdAbrirConexion());
            CommandEliminarPago.Parameters.AddWithValue("@CodigoPago", CodigoPago);
            CommandEliminarPago.ExecuteNonQuery();
            cd_conexion.MtdCerrarConexion();
        }
    }
}
