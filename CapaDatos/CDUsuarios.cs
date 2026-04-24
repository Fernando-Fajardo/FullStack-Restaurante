using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace CapaDatos
{
    public class CDUsuarios
    {
        Conexion cd_conexion = new Conexion();

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

        public DataTable MtdConsultarUsuario()
        {
            string QueryConsultarMesa = "Select * from tbl_usuarios";
            SqlDataAdapter sqlAdapter = new SqlDataAdapter(QueryConsultarMesa, cd_conexion.MtdAbrirConexion());
            DataTable dt_Mesa = new DataTable();
            sqlAdapter.Fill(dt_Mesa);

            return dt_Mesa;

            cd_conexion.MtdCerrarConexion();
        }

        public void MtdAgregarUsuario(int CodigoEmpleado, string NombreUsuario, string Contrasenia, string Rol, string Estado, string UsuarioSistema, DateTime FechaSistema)
        {
            string QueryAgregarUsuario = "Insert into tbl_usuarios(CodigoEmpleado, NombreUsuario, Contrasenia, Rol, Estado, UsuarioSistema, Fechasistema) values (@CodigoEmpleado, @NombreUsuario, @Contrasenia, @Rol, @Estado, @UsuarioSistema, @FechaSistema);";
            SqlCommand CommandAgregarUsuario = new SqlCommand(QueryAgregarUsuario, cd_conexion.MtdAbrirConexion());
            CommandAgregarUsuario.Parameters.AddWithValue("@CodigoEmpleado", CodigoEmpleado);
            CommandAgregarUsuario.Parameters.AddWithValue("@NombreUsuario", NombreUsuario);
            CommandAgregarUsuario.Parameters.AddWithValue("@Contrasenia", Contrasenia);
            CommandAgregarUsuario.Parameters.AddWithValue("@Rol", Rol);
            CommandAgregarUsuario.Parameters.AddWithValue("@Estado", Estado);
            CommandAgregarUsuario.Parameters.AddWithValue("UsuarioSistema", UsuarioSistema);
            CommandAgregarUsuario.Parameters.AddWithValue("FechaSistema", FechaSistema);
            CommandAgregarUsuario.ExecuteNonQuery();
            cd_conexion.MtdCerrarConexion();
        }

        public void MtdActualizarUsuario(int CodigoUsuario, int CodigoEmpleado, string NombreUsuario, string Contrasenia, string Rol, string Estado, string UsuarioSistema, DateTime FechaSistema)
        {
            string QueryActualizarUsuario = "Update tbl_usuarios set CodigoEmpleado = @CodigoEmpleado, NombreUsuario = @NombreUsuario, Contrasenia = @Contrasenia, Rol = @Rol, Estado = @Estado, UsuarioSistema = @UsuarioSistema, FechaSistema = @FechaSistema where CodigoUsuario = @CodigoUsuario";
            SqlCommand CommandActualizarUsuario = new SqlCommand(QueryActualizarUsuario, cd_conexion.MtdAbrirConexion());
            CommandActualizarUsuario.Parameters.AddWithValue("@CodigoUsuario", CodigoUsuario);
            CommandActualizarUsuario.Parameters.AddWithValue("@CodigoEmpleado", CodigoEmpleado);
            CommandActualizarUsuario.Parameters.AddWithValue("@NombreUsuario", NombreUsuario);
            CommandActualizarUsuario.Parameters.AddWithValue("@Contrasenia", Contrasenia);
            CommandActualizarUsuario.Parameters.AddWithValue("@Rol", Rol);
            CommandActualizarUsuario.Parameters.AddWithValue("@Estado", Estado);
            CommandActualizarUsuario.Parameters.AddWithValue("UsuarioSistema", UsuarioSistema);
            CommandActualizarUsuario.Parameters.AddWithValue("FechaSistema", FechaSistema);
            CommandActualizarUsuario.ExecuteNonQuery();
            cd_conexion.MtdCerrarConexion();


        }

        public void MtdEliminarUsuario(int CodigoUsuario)
        {
            string QueryEliminarUsuario = "Delete tbl_usuarios where CodigoUsuario = @Codigousuario";
            SqlCommand CommandEliminarUsuario = new SqlCommand(QueryEliminarUsuario, cd_conexion.MtdAbrirConexion());
            CommandEliminarUsuario.Parameters.AddWithValue("@CodigoUsuario", CodigoUsuario);
            CommandEliminarUsuario.ExecuteNonQuery();
            cd_conexion.MtdCerrarConexion();
      
        }

    }
}
