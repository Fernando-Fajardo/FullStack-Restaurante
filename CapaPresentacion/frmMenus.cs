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
    public partial class frmMenus: Form
    {

        CDmenus cd_menus = new CDmenus();
        CLmenus cl_menus = new CLmenus();

        public frmMenus()
        {
            InitializeComponent();
        }

        public void MtdConsultarMenu()
        {
            DataTable dt_menus = cd_menus.MtdConsultarMenu();
            dgvMenus.DataSource = dt_menus;
        }

        private void MtdLimpiarCampos()
        {
            txtCodigoMenu.Text = "";
            txtNombre.Text = "";
            txtIngredientes.Text = "";
            cboxCategoria.Text = "";
            cboxEstado.Text = "";
            lblPrecio.Text = "";
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string nombre = txtNombre.Text;
                string ingredientes = txtIngredientes.Text;
                string categoria = cboxCategoria.Text;
                decimal precio = cl_menus.MtdPrecioMenu(cboxCategoria.Text);
                string estado = cboxEstado.Text;
                string usuarioSistema = UserCache.NombreUsuario;
                DateTime fecha = cl_menus.MtdFechaActual();

                cd_menus.MtdAgregarMenu(nombre, ingredientes, categoria, precio, estado, usuarioSistema, fecha);
                MessageBox.Show("Menú agregado exitosamente", "Guardado exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MtdConsultarMenu();
                MtdLimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void frmMenus_Load(object sender, EventArgs e)
        {
            lblFechaActual.Text = cl_menus.MtdFechaActual().ToString("dd/MM/yyyy");
            MtdConsultarMenu();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                int CodigoMenu = int.Parse(txtCodigoMenu.Text);
                string nombre = txtNombre.Text;
                string ingredientes = txtIngredientes.Text;
                string categoria = cboxCategoria.Text;
                decimal precio = cl_menus.MtdPrecioMenu(cboxCategoria.Text);
                string estado = cboxEstado.Text;
                string usuarioSistema = UserCache.NombreUsuario;
                DateTime fecha = cl_menus.MtdFechaActual();

                cd_menus.MtdActualizarMenu(CodigoMenu, nombre, ingredientes, categoria, precio, estado, usuarioSistema, fecha);
                MessageBox.Show("Menú actualizado exitosamente", "Actualización", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MtdConsultarMenu();
                MtdLimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int CodigoMenu = (int.Parse(txtCodigoMenu.Text));

                if (cd_menus.MtdConsultarInventario(CodigoMenu) == true || cd_menus.MtdConsultarDetalles(CodigoMenu) == true)
                {
                    MessageBox.Show("La fila que intenta eliminar depende de una o más filas en otra tabla, por lo que no se podrá eliminar hasta que deje de depender de ellas", "Error al borrar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    cd_menus.MtdEliminarMenu(CodigoMenu);
                    MessageBox.Show("Menu Eliminado", "Eliminación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MtdConsultarMenu();
                    MtdLimpiarCampos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            MtdLimpiarCampos();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvMenus_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCodigoMenu.Text = dgvMenus.SelectedCells[0].Value.ToString();
            txtNombre.Text = dgvMenus.SelectedCells[1].Value.ToString();
            txtIngredientes.Text = dgvMenus.SelectedCells[2].Value.ToString();
            cboxCategoria.Text = dgvMenus.SelectedCells[3].Value.ToString();
            lblPrecio.Text = dgvMenus.SelectedCells[4].Value.ToString();
            cboxEstado.Text = dgvMenus.SelectedCells[5].Value.ToString();
        }

        private void cboxCategoria_TextChanged(object sender, EventArgs e)
        {
            lblPrecio.Text = cl_menus.MtdPrecioMenu(cboxCategoria.Text).ToString();
        }

        private void dgvMenus_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
