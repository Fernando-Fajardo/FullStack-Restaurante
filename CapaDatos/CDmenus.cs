using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CDmenus
    {
        Conexion cd_conexion = new Conexion();
    
        public DataTable MtdConsultarMenu()
        {
            string QueryConsultarMenu = "Select * from tbl_Menus";
            SqlDataAdapter sqlAdapter = new SqlDataAdapter(QueryConsultarMenu, cd_conexion.MtdAbrirConexion());
            DataTable dt_menu = new DataTable();
            sqlAdapter.Fill(dt_menu);

            cd_conexion.MtdCerrarConexion();
            return dt_menu;
        }

        public void MtdAgregarMenu(string Nombre, string Ingredientes, string Categoria, decimal Precio, string Estado, string UsuarioSistema, DateTime FechaSistema)
        {
            string QueryAgregarMenu = "Insert into tbl_Menus(Nombre, Ingredientes, Categoria, Precio, Estado, UsuarioSistema, FechaSistema) values (@Nombre, @Ingredientes, @Categoria, @Precio, @Estado, @UsuarioSistema, @FechaSistema);";
            SqlCommand CommandAgregarMenu = new SqlCommand(QueryAgregarMenu, cd_conexion.MtdAbrirConexion());
            CommandAgregarMenu.Parameters.AddWithValue("@Nombre", Nombre);
            CommandAgregarMenu.Parameters.AddWithValue("@Ingredientes", Ingredientes);
            CommandAgregarMenu.Parameters.AddWithValue("@Categoria", Categoria);
            CommandAgregarMenu.Parameters.AddWithValue("@Precio", Precio);
            CommandAgregarMenu.Parameters.AddWithValue("@Estado", Estado);
            CommandAgregarMenu.Parameters.AddWithValue("UsuarioSistema", UsuarioSistema);
            CommandAgregarMenu.Parameters.AddWithValue("FechaSistema", FechaSistema);
            CommandAgregarMenu.ExecuteNonQuery();
            cd_conexion.MtdCerrarConexion();
        }

        public void MtdActualizarMenu(int CodigoMenu, string Nombre, string Ingredientes, string Categoria, decimal Precio, string Estado, string UsuarioSistema, DateTime FechaSistema)
        {
            string QueryActualizarMenu = "Update tbl_Menus set Nombre = @Nombre, Ingredientes = @Ingredientes, Categoria = @Categoria, Precio = @Precio, Estado = @Estado, UsuarioSistema = @UsuarioSistema, FechaSistema = @FechaSistema where CodigoMenu = @CodigoMenu";
            SqlCommand CommandActualizarMenu = new SqlCommand(QueryActualizarMenu, cd_conexion.MtdAbrirConexion());
            CommandActualizarMenu.Parameters.AddWithValue("@CodigoMenu", CodigoMenu);
            CommandActualizarMenu.Parameters.AddWithValue("@Nombre", Nombre);
            CommandActualizarMenu.Parameters.AddWithValue("@Ingredientes", Ingredientes);
            CommandActualizarMenu.Parameters.AddWithValue("@Categoria", Categoria);
            CommandActualizarMenu.Parameters.AddWithValue("@Precio", Precio);
            CommandActualizarMenu.Parameters.AddWithValue("@Estado", Estado);
            CommandActualizarMenu.Parameters.AddWithValue("@UsuarioSistema", UsuarioSistema);
            CommandActualizarMenu.Parameters.AddWithValue("@FechaSistema", FechaSistema);
            CommandActualizarMenu.ExecuteNonQuery();
            cd_conexion.MtdCerrarConexion();
        }

        public bool MtdConsultarInventario(int CodigoMenu)
        {
            string QueryConsultarInventario = "SELECT 1 FROM tbl_Inventarios WHERE CodigoMenu = @CodigoMenu";
            SqlCommand CommandEliminarMenu = new SqlCommand(QueryConsultarInventario, cd_conexion.MtdAbrirConexion());
            CommandEliminarMenu.Parameters.AddWithValue("@CodigoMenu", CodigoMenu);
            cd_conexion.MtdAbrirConexion();
            object result = CommandEliminarMenu.ExecuteScalar(); // devuelve 1 o null
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

        public bool MtdConsultarDetalles(int CodigoMenu)
        {
            string QueryConsultarDetalles = "SELECT 1 FROM tbl_DetallesOrdenes WHERE CodigoMenu = @CodigoMenu";
            SqlCommand CommandEliminarMenu = new SqlCommand(QueryConsultarDetalles, cd_conexion.MtdAbrirConexion());
            CommandEliminarMenu.Parameters.AddWithValue("@CodigoMenu", CodigoMenu);
            cd_conexion.MtdAbrirConexion();
            object result = CommandEliminarMenu.ExecuteScalar(); // devuelve 1 o null
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

        public void MtdEliminarMenu(int CodigoMenu)
        {
            string QueryEliminarMenu = "Delete tbl_Menus where CodigoMenu = @CodigoMenu";
            SqlCommand CommandEliminarMenu = new SqlCommand(QueryEliminarMenu, cd_conexion.MtdAbrirConexion());
            CommandEliminarMenu.Parameters.AddWithValue("@CodigoMenu", CodigoMenu);
            CommandEliminarMenu.ExecuteNonQuery();
            cd_conexion.MtdCerrarConexion();
        }
    }
}
