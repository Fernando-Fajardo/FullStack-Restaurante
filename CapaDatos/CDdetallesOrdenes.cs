using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CDdetallesOrdenes
    {
        Conexion cd_conexion = new Conexion();

        public List<dynamic> MtdListarMenus()
        {
            List<dynamic> ListaMenus = new List<dynamic>();
            string QueryListaMenus = "Select CodigoMenu, Nombre from tbl_Menus";
            SqlCommand cmd = new SqlCommand(QueryListaMenus, cd_conexion.MtdAbrirConexion());
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ListaMenus.Add(new
                {
                    Value = reader["CodigoMenu"],
                    Text = $"{reader["CodigoMenu"]} - {reader["Nombre"]}"
                });
            }

            cd_conexion.MtdCerrarConexion();
            return ListaMenus;
        }

        public Decimal MtdPrecioUnitario(int CodigoMenu)
        {
            decimal PrecioUnitario = 0;

            string QueryConsultarPrecioUnitario = "Select Precio from tbl_Menus where CodigoMenu = @CodigoMenu";
            SqlCommand CommandPrecioUnitario = new SqlCommand(QueryConsultarPrecioUnitario, cd_conexion.MtdAbrirConexion());
            CommandPrecioUnitario.Parameters.AddWithValue("@CodigoMenu", CodigoMenu);
            SqlDataReader reader = CommandPrecioUnitario.ExecuteReader();

            if (reader.Read())
            {
                PrecioUnitario = decimal.Parse(reader["Precio"].ToString());
            }
            else
            {
                PrecioUnitario = 0;
            }

            cd_conexion.MtdCerrarConexion();
            return PrecioUnitario;
        }

        public DataTable MtdConsultarDetallesOrdenes()
        {
            string QueryConsultarDetallesOrdenes = "Select * from tbl_DetallesOrdenes";
            SqlDataAdapter dt_adapter = new SqlDataAdapter(QueryConsultarDetallesOrdenes, cd_conexion.MtdAbrirConexion());
            DataTable dt_detallesOrdenes = new DataTable();
            dt_adapter.Fill(dt_detallesOrdenes);
            cd_conexion.MtdCerrarConexion();
            return dt_detallesOrdenes;
        }

        public void MtdAgregarDetallesOrdenes(int CodigoMenu, int Cantidad, decimal PrecioUnitario, decimal PrecioTotal, string UsuarioSistema, DateTime FechaSistema)
        {
            string QueryAgregarDetallesOrdenes = "Insert into tbl_DetallesOrdenes(CodigoMenu, Cantidad, PrecioUnitario, PrecioTotal, UsuarioSistema, FechaSistema) values (@CodigoMenu, @Cantidad, @PrecioUnitario, @PrecioTotal, @UsuarioSistema, @FechaSistema)";
            SqlCommand CommandAgregarDetallesOrdenes = new SqlCommand(QueryAgregarDetallesOrdenes, cd_conexion.MtdAbrirConexion());
            CommandAgregarDetallesOrdenes.Parameters.AddWithValue("@CodigoMenu", CodigoMenu);
            CommandAgregarDetallesOrdenes.Parameters.AddWithValue("@Cantidad", Cantidad);
            CommandAgregarDetallesOrdenes.Parameters.AddWithValue("@PrecioUnitario", PrecioUnitario);
            CommandAgregarDetallesOrdenes.Parameters.AddWithValue("@PrecioTotal", PrecioTotal);
            CommandAgregarDetallesOrdenes.Parameters.AddWithValue("@UsuarioSistema", UsuarioSistema);
            CommandAgregarDetallesOrdenes.Parameters.AddWithValue("@FechaSistema", FechaSistema);
            CommandAgregarDetallesOrdenes.ExecuteNonQuery();
            cd_conexion.MtdCerrarConexion();
        }

        public void MtdActualizarDetallesOrdenes(int CodigoOrdenDet, int CodigoMenu, int Cantidad, decimal PrecioUnitario, decimal PrecioTotal, string UsuarioSistema, DateTime FechaSistema)
        {
            string QueryActualizarDetallesOrdenes = "Update tbl_DetallesOrdenes set CodigoMenu = @CodigoMenu, Cantidad = @Cantidad, PrecioUnitario = @PrecioUnitario, PrecioTotal = @PrecioTotal, UsuarioSistema = @UsuarioSistema, FechaSistema = @FechaSistema where CodigoOrdenDet = @CodigoOrdenDet";
            SqlCommand CommandActualizarDetallesOrdenes = new SqlCommand(QueryActualizarDetallesOrdenes, cd_conexion.MtdAbrirConexion());
            CommandActualizarDetallesOrdenes.Parameters.AddWithValue("@CodigoOrdenDet", CodigoOrdenDet);
            CommandActualizarDetallesOrdenes.Parameters.AddWithValue("@CodigoMenu", CodigoMenu);
            CommandActualizarDetallesOrdenes.Parameters.AddWithValue("@Cantidad", Cantidad);
            CommandActualizarDetallesOrdenes.Parameters.AddWithValue("@PrecioUnitario", PrecioUnitario);
            CommandActualizarDetallesOrdenes.Parameters.AddWithValue("@PrecioTotal", PrecioTotal);
            CommandActualizarDetallesOrdenes.Parameters.AddWithValue("@UsuarioSistema", UsuarioSistema);
            CommandActualizarDetallesOrdenes.Parameters.AddWithValue("@FechaSistema", FechaSistema);
            CommandActualizarDetallesOrdenes.ExecuteNonQuery();
            cd_conexion.MtdCerrarConexion();
        }

        public bool MtdConsultarEncabezado(int CodigoOrdenDet)
        {
            string QueryConsultarEncabezado = "SELECT 1 FROM tbl_EncabezadoOrdenes WHERE CodigoOrdenDet = @CodigoOrdenDet";
            SqlCommand CommandEliminarDetalle = new SqlCommand(QueryConsultarEncabezado, cd_conexion.MtdAbrirConexion());
            CommandEliminarDetalle.Parameters.AddWithValue("@CodigoOrdenDet", CodigoOrdenDet);
            cd_conexion.MtdAbrirConexion();
            object result = CommandEliminarDetalle.ExecuteScalar(); // devuelve 1 o null
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

        public void MtdEliminarDetallesOrdenes(int CodigoOrdenDet)
        {
            string QueryEliminarDetallesOrdenes = "Delete tbl_DetallesOrdenes where CodigoOrdenDet = @CodigoOrdenDet";
            SqlCommand CommandEliminarDetallesOrdenes = new SqlCommand(QueryEliminarDetallesOrdenes, cd_conexion.MtdAbrirConexion());
            CommandEliminarDetallesOrdenes.Parameters.AddWithValue("@CodigoOrdenDet", CodigoOrdenDet);
            CommandEliminarDetallesOrdenes.ExecuteNonQuery();
            cd_conexion.MtdCerrarConexion();
        }
    }
}
