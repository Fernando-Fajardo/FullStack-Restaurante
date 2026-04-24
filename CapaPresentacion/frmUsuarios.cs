using CapaDatos;
using CapaLogica;
using CapaPresentacion.Seguridad;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_Restaurante
{
    public partial class frmUsuarios: Form
    {

        CLusuarios cLusuarios = new CLusuarios();
        CDUsuarios cdusuarios = new CDUsuarios();
        public frmUsuarios()
        {
            InitializeComponent();
        }


        private void mtdMostrarListaEmpleados()
        {
            var ListaEmpleados = cdusuarios.MtdListarEmpleados();

            foreach (var Empleados in ListaEmpleados)
            {
                cboxCodigoEmpleado.Items.Add(Empleados);
            }

            cboxCodigoEmpleado.DisplayMember = "Text";
            cboxCodigoEmpleado.ValueMember = "Value";
        }

        public void mtdLimpiarCampos()
        {

            cboxCodigoEmpleado.Text = " ";
            txtNombreUsuario.Text = " ";
            txtContrasenia.Text = " ";
            cboxRol.Text = " ";
            cboxEstado.Text = " ";

        }

        public void mtdConsultarUsuario()
        {
            DataTable dtUsuarios = cdusuarios.MtdConsultarUsuario();
            dgvUsuarios.DataSource = dtUsuarios;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {

                int CodigoEmpleado = int.Parse(cboxCodigoEmpleado.Text.Split('-')[0].Trim());
                string NombreUsuario = txtNombreUsuario.Text;
                string Contrasenia = txtContrasenia.Text;
                string Rol = cboxRol.Text;
                string Estado = cboxEstado.Text;
                string UsuarioSistema = UserCache.NombreUsuario;
                DateTime FechaSistema = cLusuarios.MtdFechaActual();

                cdusuarios.MtdAgregarUsuario(CodigoEmpleado, NombreUsuario, Contrasenia, Rol, Estado, UsuarioSistema, FechaSistema);
                MessageBox.Show("Usuario agregado correctamente", "Confirmacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                mtdConsultarUsuario();
                mtdLimpiarCampos();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void frmUsuarios_Load(object sender, EventArgs e)
        {
            lblFechaActual.Text = cLusuarios.MtdFechaActual().ToString("dd/MM/yyyy");
            mtdMostrarListaEmpleados();
            mtdConsultarUsuario();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {

                int CodigoUsuario = int.Parse(txtCodigoUsuario.Text);
                int CodigoEmpleado = int.Parse(cboxCodigoEmpleado.Text.Split('-')[0].Trim());
                string NombreUsuario = txtNombreUsuario.Text;
                string Contrasenia = txtContrasenia.Text;
                string Rol = cboxRol.Text;
                string Estado = cboxEstado.Text;
                string UsuarioSistema = UserCache.NombreUsuario;
                DateTime FechaSistema = cLusuarios.MtdFechaActual();

                cdusuarios.MtdActualizarUsuario(CodigoUsuario, CodigoEmpleado, NombreUsuario, Contrasenia, Rol, Estado, UsuarioSistema, FechaSistema);
                MessageBox.Show("Usuario actualizado correctamente", "Confirmacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                mtdConsultarUsuario();
                mtdLimpiarCampos();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void dgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            txtCodigoUsuario.Text = dgvUsuarios.SelectedCells[0].Value.ToString();
            cboxCodigoEmpleado.Text = dgvUsuarios.SelectedCells[1].Value.ToString();
            txtNombreUsuario.Text = dgvUsuarios.SelectedCells[2].Value.ToString();
            txtContrasenia.Text = dgvUsuarios.SelectedCells[3].Value.ToString();
            cboxRol.Text = dgvUsuarios.SelectedCells[4].Value.ToString();
            cboxEstado.Text = dgvUsuarios.SelectedCells[5].Value.ToString();

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            mtdLimpiarCampos();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int CodigoUsuario = int.Parse(txtCodigoUsuario.Text);
                cdusuarios.MtdEliminarUsuario(CodigoUsuario);
                MessageBox.Show("Usuario eliminado correctamente", "Confirmacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                mtdConsultarUsuario();
                mtdLimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
