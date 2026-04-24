using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaDatos;
using CapaLogica;
using CapaPresentacion.Seguridad;

namespace Sistema_Restaurante
{
    public partial class frmDetallesOrdenes: Form
    {
        CDdetallesOrdenes cd_detallesOrdenes = new CDdetallesOrdenes();
        CLdetallesOrdenes cl_detallesOrdenes = new CLdetallesOrdenes();

        public frmDetallesOrdenes()
        {
            InitializeComponent();
        }
        private void frmDetallesOrdenes_Load(object sender, EventArgs e)
        {
            mtdConsultarDetallesOrdenes();
            mtdMostrarMenus();
            lblFechaActual.Text = cl_detallesOrdenes.MtdFechaActual().ToString("dd/MM/yyyy");   
        }

        private void mtdConsultarDetallesOrdenes()
        {
            DataTable dtDetallesOrdenes = cd_detallesOrdenes.MtdConsultarDetallesOrdenes();
            dgvDetallesOrdenes.DataSource = dtDetallesOrdenes;
        }

        private void mtdMostrarMenus()
        {
            var ListaMenus = cd_detallesOrdenes.MtdListarMenus();

            foreach (var Menus in ListaMenus)
            {
                cboxCodigoMenu.Items.Add(Menus);
            }

            cboxCodigoMenu.DisplayMember = "Text";
            cboxCodigoMenu.ValueMember = "Value";
        }

        private void mtdLimpiarCampos()
        {
            txtCodigoOrdenDetalles.Text = "";
            cboxCodigoMenu.Text = "";
            txtCantidad.Text = "";
            lblPrecioUnitario.Text = "0.00";
            lblPrecioTotal.Text = "0.00";
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                int CodigoMenu = int.Parse(cboxCodigoMenu.Text.Split('-')[0].Trim());
                int Cantidad = int.Parse(txtCantidad.Text);
                decimal PrecioUnitario = cd_detallesOrdenes.MtdPrecioUnitario(CodigoMenu);
                decimal PrecioTotal = cl_detallesOrdenes.MtdPrecioTotal(Cantidad, PrecioUnitario);
                string UsuarioSistema = UserCache.NombreUsuario;
                DateTime FechaSistema = cl_detallesOrdenes.MtdFechaActual();

                cd_detallesOrdenes.MtdAgregarDetallesOrdenes(CodigoMenu, Cantidad, PrecioUnitario, PrecioTotal, UsuarioSistema, FechaSistema);
                MessageBox.Show("Detalles de la orden guardados correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                mtdConsultarDetallesOrdenes();
                mtdLimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar los detalles de la orden: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                int CodigoOrdenDet = int.Parse(txtCodigoOrdenDetalles.Text);
                int CodigoMenu = int.Parse(cboxCodigoMenu.Text.Split('-')[0].Trim());
                int Cantidad = int.Parse(txtCantidad.Text);
                decimal PrecioUnitario = cd_detallesOrdenes.MtdPrecioUnitario(CodigoMenu);
                decimal PrecioTotal = cl_detallesOrdenes.MtdPrecioTotal(Cantidad, PrecioUnitario);
                string UsuarioSistema = UserCache.NombreUsuario;
                DateTime FechaSistema = cl_detallesOrdenes.MtdFechaActual();

                cd_detallesOrdenes.MtdActualizarDetallesOrdenes(CodigoOrdenDet, CodigoMenu, Cantidad, PrecioUnitario, PrecioTotal, UsuarioSistema, FechaSistema);
                MessageBox.Show("Detalles de la orden actualizados correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                mtdConsultarDetallesOrdenes();
                mtdLimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar los detalles de la orden: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                int CodigoOrdenDet = int.Parse(txtCodigoOrdenDetalles.Text);

                if (cd_detallesOrdenes.MtdConsultarEncabezado(CodigoOrdenDet) == true)
                {
                    MessageBox.Show("La fila que intenta eliminar depende de una o más filas en otra tabla, por lo que no se podrá eliminar hasta que deje de depender de ellas", "Error al borrar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    cd_detallesOrdenes.MtdEliminarDetallesOrdenes(CodigoOrdenDet);
                    MessageBox.Show("Orden eliminada correctamente", "Eliminar Orden", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mtdConsultarDetallesOrdenes();
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

        private void dgvDetallesOrdenes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCodigoOrdenDetalles.Text = dgvDetallesOrdenes.SelectedCells[0].Value.ToString();
            cboxCodigoMenu.Text = dgvDetallesOrdenes.SelectedCells[1].Value.ToString();
            txtCantidad.Text = dgvDetallesOrdenes.SelectedCells[2].Value.ToString();
            lblPrecioUnitario.Text = dgvDetallesOrdenes.SelectedCells[3].Value.ToString();
            lblPrecioTotal.Text = dgvDetallesOrdenes.SelectedCells[4].Value.ToString();
        }
        private void cboxCodigoMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
                lblPrecioUnitario.Text = cd_detallesOrdenes.MtdPrecioUnitario(int.Parse(cboxCodigoMenu.Text.Split('-')[0].Trim())).ToString();
        }

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {
                if (txtCantidad.Text == "")
                {
                    lblPrecioTotal.Text = "0.00";
                }
            else
            {
                decimal PrecioUnitario = cd_detallesOrdenes.MtdPrecioUnitario(int.Parse(cboxCodigoMenu.Text.Split('-')[0].Trim().ToString()));
                lblPrecioTotal.Text = cl_detallesOrdenes.MtdPrecioTotal(int.Parse(txtCantidad.Text), PrecioUnitario).ToString();
            }
        }
    }
}
