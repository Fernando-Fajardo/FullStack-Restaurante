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
    public partial class frmClientes: Form
    {

        CDClientes cdclientes = new CDClientes();
        CLclientes clclientes = new CLclientes();

        public frmClientes()
        {
            InitializeComponent();
        }

        public void mtdConsultarClientes()
        {

            DataTable dtclientes = cdclientes.MtdConsultarCliente();
            dgvClientes.DataSource = dtclientes;

        }

        private void frmClientes_Load(object sender, EventArgs e)
        {

            lblFechaActual.Text = clclientes.MtdFechaActual().ToString("dd/MM/yyyy");
            mtdConsultarClientes();

        }

        public void mtdLimpiarCampos()
        {

            txtNombre.Text = " ";
            txtNit.Text = " ";
            txtTelefono.Text = " ";
            cboxCategoria.Text = " ";
            cboxEstado.Text = " ";

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string Nombre = txtNombre.Text;
                string Nit = txtNit.Text;
                string Telefono = txtTelefono.Text;
                string Categoria = cboxCategoria.Text;
                string Estado = cboxEstado.Text;
                string UsuarioSistema = UserCache.NombreUsuario;
                DateTime FechaSistema = clclientes.MtdFechaActual();

                cdclientes.MtdAgregarCliente(Nombre, Nit, Telefono, Categoria, Estado, UsuarioSistema, FechaSistema);
                MessageBox.Show("Cliente agregado correctamente", "Confirmacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                mtdConsultarClientes();
                mtdLimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void dgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            txtCodigoCliente.Text = dgvClientes.SelectedCells[0].Value.ToString();
            txtNombre.Text = dgvClientes.SelectedCells[1].Value.ToString();
            txtNit.Text = dgvClientes.SelectedCells[2].Value.ToString();
            txtTelefono.Text = dgvClientes.SelectedCells[3].Value.ToString();
            cboxCategoria.Text = dgvClientes.SelectedCells[4].Value.ToString();
            cboxEstado.Text = dgvClientes.SelectedCells[5].Value.ToString();

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {

            try
            {
                int CodigoCliente = int.Parse(txtCodigoCliente.Text);
                string Nombre = txtNombre.Text;
                string Nit = txtNit.Text;
                string Telefono = txtTelefono.Text;
                string Categoria = cboxCategoria.Text;
                string Estado = cboxEstado.Text;
                string UsuarioSistema = UserCache.NombreUsuario;
                DateTime FechaSistema = clclientes.MtdFechaActual();

                cdclientes.MtdActualizarCliente(CodigoCliente, Nombre, Nit, Telefono, Categoria, Estado, UsuarioSistema, FechaSistema);
                MessageBox.Show("Cliente actualizado correctamente", "Confirmacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                mtdConsultarClientes();
                mtdLimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            mtdLimpiarCampos();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

            try
            {
                int CodigoCliente = int.Parse(txtCodigoCliente.Text);


                if (cdclientes.MtdConsultarEncabezado(CodigoCliente) == true)
                {
                    MessageBox.Show("La fila que intenta eliminar depende de una o más filas en otra tabla, por lo que no se podrá eliminar hasta que deje de depender de ellas", "Error al borrar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    cdclientes.MtdEliminarCliente(CodigoCliente);
                    MessageBox.Show("Cliente eliminado correctamente", "Confirmacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mtdConsultarClientes();
                    mtdLimpiarCampos();
                }
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
