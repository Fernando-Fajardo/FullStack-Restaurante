using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaDatos;
using CapaLogica;
using CapaPresentacion.Seguridad;


namespace Sistema_Restaurante
{
    public partial class FrmPagoOrdenes: Form
    {
        CDPagoOrdenes cd_pagoordenes = new CDPagoOrdenes();
        CLPagoOrdenes cl_pagoordenes = new CLPagoOrdenes();

        public FrmPagoOrdenes()
        {
            InitializeComponent();
        }

        private void dgvMenus_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            
            try
            {
                int CodigoPago = (int.Parse(txtCodigoPago.Text));

                cd_pagoordenes.MtdEliminarPagoOrden(CodigoPago);
                MessageBox.Show("Pago Orden Eliminado", "Eliminación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MtdConsultarPagoOrdenes();
                MtdLimpiarCampos();
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

        private void FrmPagoOrdenes_Load(object sender, EventArgs e)
        {
            MtdMostrarListaOrdenEnc();
            lblFechaSistema.Text = cl_pagoordenes.MtdFechaActual().ToString("dd/MM/yyyy");
            MtdConsultarPagoOrdenes();
        }

        //lga CBOX
        private void MtdMostrarListaOrdenEnc()
        {
            var Listaordenes = cd_pagoordenes.MtdListarOrdenEnc();

            foreach (var Ordenes in Listaordenes)
            {
                cboxOrdenEncabezado.Items.Add(Ordenes);
            }

            cboxOrdenEncabezado.DisplayMember = "Text";
            cboxOrdenEncabezado.ValueMember = "Value";

        }
         //lga LBL
        private void cboxOrdenEncabezado_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*double MontoTotalOrd = double.Parse((lblMontoOrden.Text.Trim()).ToString());*/
           

            var PagoSeleccionado = cboxOrdenEncabezado.SelectedItem;
            int CodigoOrdenEnc = int.Parse(cboxOrdenEncabezado.Text);//ERROR
            if(cboxOrdenEncabezado == null)
            {
                lblMontoOrden.Text = "0.00";
                lblPropina.Text = "0.00";
                lblImpuesto.Text = "0.00";
                lblDescuento.Text = "0.00";
                lblTotalPago.Text = "0.00";
            }else
            {
                lblMontoOrden.Text = cd_pagoordenes.MtdMontoPagoOrdenes(CodigoOrdenEnc);
                lblPropina.Text = cl_pagoordenes.MtdPropina(double.Parse(lblMontoOrden.Text)).ToString("c");
                lblImpuesto.Text = cl_pagoordenes.MtdImpuesto(double.Parse(lblMontoOrden.Text)).ToString("c");
                lblDescuento.Text = cl_pagoordenes.MtdDescuento(double.Parse(lblMontoOrden.Text)).ToString("c");
                lblTotalPago.Text = cl_pagoordenes.MtdTotalPago(double.Parse(lblMontoOrden.Text)).ToString("c");
            }
            
        }

        private void lblMontoOrden_Click(object sender, EventArgs e)
        {
            
        }
        private void MtdConsultarPagoOrdenes()
        {
            DataTable dtPagoOrdenes = cd_pagoordenes.MtdConsultarPagoOrdenes();
            dgvMenus.DataSource = dtPagoOrdenes;
        }
        

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                int CodigoOrdenEnc = int.Parse(cboxOrdenEncabezado.Text.Split('-')[0].Trim());
                decimal MontoOrden = decimal.Parse(lblMontoOrden.Text);
                decimal Propina = (decimal)cl_pagoordenes.MtdPropina((double)MontoOrden);
                decimal Impuesto = (decimal)cl_pagoordenes.MtdImpuesto((double)MontoOrden);
                decimal Descuento = (decimal)cl_pagoordenes.MtdDescuento((double)MontoOrden);
                decimal TotalPago = (decimal)cl_pagoordenes.MtdTotalPago((double)MontoOrden);
                string MetodoPago = cboxMetodoPago.Text;
                string Estado = cboxEstado.Text;
                DateTime FechaPago = dtpFechaPago.Value;
                string UsuarioSistema = UserCache.NombreUsuario;
                DateTime FechaSistema = cl_pagoordenes.MtdFechaActual();

                cd_pagoordenes.MtdAgregarPagoOrdenes(CodigoOrdenEnc, MontoOrden, Propina, Impuesto, Descuento, TotalPago, MetodoPago, Estado, FechaPago, UsuarioSistema, FechaSistema);
                MessageBox.Show("Pago Orden Guardado Correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MtdConsultarPagoOrdenes();
                MtdLimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar el pago de la orden: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MtdLimpiarCampos()
        {
            txtCodigoPago.Clear();
            cboxOrdenEncabezado.Text = "";
            lblMontoOrden.Text = "0.00";
            lblPropina.Text = "0.00";
            lblImpuesto.Text = "0.00";
            lblDescuento.Text = "0.00";
            lblTotalPago.Text = "0.00";
            cboxEstado.Text = "";
            cboxMetodoPago.Text = "";
        }

        private void dgvMenus_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCodigoPago.Text = dgvMenus.SelectedCells[0].Value.ToString();
            cboxOrdenEncabezado.Text = dgvMenus.SelectedCells[1].Value.ToString();
            lblMontoOrden.Text = dgvMenus.SelectedCells[2].Value.ToString();
            lblPropina.Text = dgvMenus.SelectedCells[3].Value.ToString();
            lblImpuesto.Text = dgvMenus.SelectedCells[4].Value.ToString();
            lblDescuento.Text = dgvMenus.SelectedCells[5].Value.ToString();
            lblTotalPago.Text = dgvMenus.SelectedCells[6].Value.ToString();
            cboxMetodoPago.Text = dgvMenus.SelectedCells[7].Value.ToString();
            cboxEstado.Text = dgvMenus.SelectedCells[8].Value.ToString();
            lblFechaSistema.Text = dgvMenus.SelectedCells[9].Value.ToString();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            MtdLimpiarCampos();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                int CodigoPago = int.Parse(txtCodigoPago.Text);
                int CodigoOrdenEnc = int.Parse(cboxOrdenEncabezado.Text.Split('-')[0].Trim());
                decimal MontoOrden = decimal.Parse(lblMontoOrden.Text);
                decimal Propina = (decimal)cl_pagoordenes.MtdPropina((double)MontoOrden);
                decimal Impuesto = (decimal)cl_pagoordenes.MtdImpuesto((double)MontoOrden);
                decimal Descuento = (decimal)cl_pagoordenes.MtdDescuento((double)MontoOrden);
                decimal TotalPago = (decimal)cl_pagoordenes.MtdTotalPago((double)MontoOrden);
                string MetodoPago = cboxMetodoPago.Text;
                string Estado = cboxEstado.Text;
                DateTime FechaPago = dtpFechaPago.Value;
                string UsuarioSistema = UserCache.NombreUsuario;
                DateTime FechaSistema = cl_pagoordenes.MtdFechaActual();

                cd_pagoordenes.MtdActualizarPagoOrdenes(CodigoPago, CodigoOrdenEnc, MontoOrden, Propina, Impuesto, Descuento, TotalPago, MetodoPago, Estado, FechaPago, UsuarioSistema, FechaSistema);
                MessageBox.Show("Pago Orden Actualizado Correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MtdConsultarPagoOrdenes();
                MtdLimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar el pago de la orden: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
