using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CDInventarios
    {
        Conexion cd_conexion = new Conexion();

        //primer paso LBL
        public string MtdConsultarCategoria(int CodigoMenu)
        {
            string categoria;

            string QueryConsultarCategorias = "Select Categoria from tbl_Menus where CodigoMenu = @CodigoMenu";
            SqlCommand CommandCategorias = new SqlCommand(QueryConsultarCategorias, cd_conexion.MtdAbrirConexion());
            CommandCategorias.Parameters.AddWithValue("@CodigoMenu", CodigoMenu);
            SqlDataReader reader = CommandCategorias.ExecuteReader();

            if (reader.Read())
            {
                categoria = reader["Categoria"].ToString();
                
            }
            else
            {
                categoria = "";
            }

            cd_conexion.MtdCerrarConexion();
            return categoria;
        }

        //primer paso CBOX
        public List<dynamic> MtdListarMenus()
        {
            List<dynamic> ListaMenu = new List<dynamic>();
            string QueryListaMenus = "Select CodigoMenu, Nombre from tbl_Menus";
            SqlCommand cmd = new SqlCommand(QueryListaMenus, cd_conexion.MtdAbrirConexion());
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ListaMenu.Add(new
                {
                    Value = reader["CodigoMenu"],
                    Text = $"{reader["CodigoMenu"]} - {reader["Nombre"]}"
                });
            }

            cd_conexion.MtdCerrarConexion();
            return ListaMenu;
        }
        public DataTable MtdConsultarInventario()
        {
            string QueryConsultarInventario = "Select * from tbl_Inventarios";
            SqlDataAdapter sqlAdapter = new SqlDataAdapter(QueryConsultarInventario, cd_conexion.MtdAbrirConexion());
            DataTable dt_inventario = new DataTable();
            sqlAdapter.Fill(dt_inventario);

            cd_conexion.MtdCerrarConexion();
            return dt_inventario;
        }

        public void MtdAgregarInventarios(int CodigoMenu, string Categoria, int Cantidad, DateTime FechaEntrada, DateTime FechaVencimiento, int DiasVigencia, string UsuarioSistema, DateTime FechaSistema)
         {
             string QueryAgregarInventarios = "Insert into tbl_Inventarios(CodigoMenu, Categoria, Cantidad, FechaEntrada, FechaVencimiento, DiasVigencia, UsuarioSistema, FechaSistema) values (@CodigoMenu, @Categoria, @Cantidad, @FechaEntrada, @FechaVencimiento, @DiasVigencia, @UsuarioSistema, @FechaSistema)";
             SqlCommand CommandAgregarInventarios = new SqlCommand(QueryAgregarInventarios, cd_conexion.MtdAbrirConexion());
            CommandAgregarInventarios.Parameters.AddWithValue("@CodigoMenu", CodigoMenu);
            CommandAgregarInventarios.Parameters.AddWithValue("@Categoria", Categoria);
            CommandAgregarInventarios.Parameters.AddWithValue("@Cantidad", Cantidad);
            CommandAgregarInventarios.Parameters.AddWithValue("@FechaEntrada", FechaEntrada);
            CommandAgregarInventarios.Parameters.AddWithValue("@FechaVencimiento", FechaVencimiento);
            CommandAgregarInventarios.Parameters.AddWithValue("@DiasVigencia", DiasVigencia);
            CommandAgregarInventarios.Parameters.AddWithValue("@UsuarioSistema", UsuarioSistema);
            CommandAgregarInventarios.Parameters.AddWithValue("@FechaSistema", FechaSistema);
            CommandAgregarInventarios.ExecuteNonQuery();
             cd_conexion.MtdCerrarConexion();
            
         }

        public void MtdActualizarInventario(int CodigoInventario, int CodigoMenu, string Categoria, int Cantidad, DateTime FechaEntrada, DateTime FechaVencimiento, int DiasVigencia, string UsuarioSistema, DateTime FechaSistema)
        {
            string QueryActualizarInventario = "Update tbl_Inventarios set CodigoMenu = @CodigoMenu, Categoria = @Categoria, Cantidad = @Cantidad, FechaEntrada = @FechaEntrada, FechaVencimiento = @FechaVencimiento, DiasVigencia = @DiasVigencia, UsuarioSistema = @UsuarioSistema, FechaSistema= @FechaSistema where CodigoInventario = @CodigoInventario";
            SqlCommand CommandActualizarInventarios = new SqlCommand(QueryActualizarInventario, cd_conexion.MtdAbrirConexion());
            CommandActualizarInventarios.Parameters.AddWithValue("@CodigoInventario", CodigoInventario);
            CommandActualizarInventarios.Parameters.AddWithValue("@CodigoMenu", CodigoMenu);
            CommandActualizarInventarios.Parameters.AddWithValue("@Categoria", Categoria);
            CommandActualizarInventarios.Parameters.AddWithValue("@Cantidad", Cantidad);
            CommandActualizarInventarios.Parameters.AddWithValue("@FechaEntrada", FechaEntrada);
            CommandActualizarInventarios.Parameters.AddWithValue("@FechaVencimiento", FechaVencimiento);
            CommandActualizarInventarios.Parameters.AddWithValue("@DiasVigencia", DiasVigencia);
            CommandActualizarInventarios.Parameters.AddWithValue("@UsuarioSistema", UsuarioSistema);
            CommandActualizarInventarios.Parameters.AddWithValue("@FechaSistema", FechaSistema);
            CommandActualizarInventarios.ExecuteNonQuery();
            cd_conexion.MtdCerrarConexion();
        }

        public void MtdEliminarInventario(int CodigoInventario)
        {
            string QueryEliminarInventario = "Delete tbl_Inventarios where CodigoInventario = @CodigoInventario";
            SqlCommand CommandEliminarInventario = new SqlCommand(QueryEliminarInventario, cd_conexion.MtdAbrirConexion());
            CommandEliminarInventario.Parameters.AddWithValue("@CodigoInventario", CodigoInventario);
            CommandEliminarInventario.ExecuteNonQuery();
            cd_conexion.MtdCerrarConexion();
        }

    }
}
