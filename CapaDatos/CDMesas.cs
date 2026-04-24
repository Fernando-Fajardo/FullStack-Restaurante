using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CapaDatos
{
    public class CDMesas
    {
        Conexion cd_conexion = new Conexion();

        public DataTable MtdConsultarMesa()
        {
            string QueryConsultarMesa = "Select * from tbl_mesas";
            SqlDataAdapter sqlAdapter = new SqlDataAdapter(QueryConsultarMesa, cd_conexion.MtdAbrirConexion());
            DataTable dt_Mesa = new DataTable();
            sqlAdapter.Fill(dt_Mesa);

            cd_conexion.MtdCerrarConexion();
            return dt_Mesa;
        }

        public void MtdAgregarMesa(int NumeroMesa, int CantidadSillas, string Ubicacion, string TipoMesa, string Estado, string UsuarioSistema, DateTime FechaSistema)
        {
            string QueryAgregarMesa = "Insert into tbl_mesas(NumeroMesa, CantidadSillas, Ubicacion, TipoMesa, Estado, UsuarioSistema, Fechasistema) values (@NumeroMesa, @CantidadSillas, @Ubicacion, @TipoMesa, @Estado, @UsuarioSistema, @FechaSistema)";
            SqlCommand CommandAgregarMesa = new SqlCommand(QueryAgregarMesa, cd_conexion.MtdAbrirConexion());
            CommandAgregarMesa.Parameters.AddWithValue("@NumeroMesa", NumeroMesa);
            CommandAgregarMesa.Parameters.AddWithValue("@CantidadSillas", CantidadSillas);
            CommandAgregarMesa.Parameters.AddWithValue("@Ubicacion", Ubicacion);
            CommandAgregarMesa.Parameters.AddWithValue("@TipoMesa", TipoMesa);
            CommandAgregarMesa.Parameters.AddWithValue("@Estado", Estado);
            CommandAgregarMesa.Parameters.AddWithValue("UsuarioSistema", UsuarioSistema);
            CommandAgregarMesa.Parameters.AddWithValue("FechaSistema", FechaSistema);
            CommandAgregarMesa.ExecuteNonQuery();
            cd_conexion.MtdCerrarConexion();
        }

        public void MtdActualizarMesa(int CodigoMesa, int NumeroMesa, int CantidadSillas, string Ubicacion, string TipoMesa, string Estado, string UsuarioSistema, DateTime FechaSistema)
        {
            string QueryActualizarMesa = "Update tbl_mesas set NumeroMesa = @NumeroMesa, CantidadSillas = @CantidadSillas, Ubicacion = @Ubicacion, TipoMesa = @TipoMesa, Estado = @Estado, UsuarioSistema = @UsuarioSistema, FechaSistema = @FechaSistema where CodigoMesa = @CodigoMesa";
            SqlCommand CommandActualizarMesa = new SqlCommand(QueryActualizarMesa, cd_conexion.MtdAbrirConexion());
            CommandActualizarMesa.Parameters.AddWithValue("@CodigoMesa", CodigoMesa);
            CommandActualizarMesa.Parameters.AddWithValue("@NumeroMesa", NumeroMesa);
            CommandActualizarMesa.Parameters.AddWithValue("@CantidadSillas", CantidadSillas);
            CommandActualizarMesa.Parameters.AddWithValue("@Ubicacion", Ubicacion);
            CommandActualizarMesa.Parameters.AddWithValue("@TipoMesa", TipoMesa);
            CommandActualizarMesa.Parameters.AddWithValue("@Estado", Estado);
            CommandActualizarMesa.Parameters.AddWithValue("@UsuarioSistema", UsuarioSistema);
            CommandActualizarMesa.Parameters.AddWithValue("@FechaSistema", FechaSistema);
            CommandActualizarMesa.ExecuteNonQuery();
            cd_conexion.MtdCerrarConexion();


        }

        public bool MtdConsultarEncabezado(int CodigoMesa)
        {
            string QueryConsultarEncabezado = "SELECT 1 FROM tbl_EncabezadoOrdenes WHERE CodigoMesa = @CodigoMesa";
            SqlCommand CommandEliminarMesa = new SqlCommand(QueryConsultarEncabezado, cd_conexion.MtdAbrirConexion());
            CommandEliminarMesa.Parameters.AddWithValue("@CodigoMesa", CodigoMesa);
            cd_conexion.MtdAbrirConexion();
            object result = CommandEliminarMesa.ExecuteScalar(); // devuelve 1 o null
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

        public void MtdEliminarMesa(int CodigoMesa)
        {
            string QueryEliminarMesa = "Delete tbl_mesas where CodigoMesa = @CodigoMesa";
            SqlCommand CommandEliminarMesa = new SqlCommand(QueryEliminarMesa, cd_conexion.MtdAbrirConexion());
            CommandEliminarMesa.Parameters.AddWithValue("@CodigoMesa", CodigoMesa);
            CommandEliminarMesa.ExecuteNonQuery();
            cd_conexion.MtdCerrarConexion();
        }
    }
}
