using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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
    public partial class FrmPagoPlanillas: Form
    {
        CDPagoPlanillas cd_pagoplanillas = new CDPagoPlanillas();
        CLPagoPlanillas cl_pagoplanillas = new CLPagoPlanillas();
        public FrmPagoPlanillas()
        {
            InitializeComponent();
        }
        //primer paso CBOX
        private void MtdMostrarListaEmpleados()
        {
            var ListaMenus = cd_pagoplanillas.MtdListarEmpleados();

            foreach (var Menus in ListaMenus)
            {
                cboxCodigoEmpleado.Items.Add(Menus);
            }

            cboxCodigoEmpleado.DisplayMember = "Text";
            cboxCodigoEmpleado.ValueMember = "Value";

        }

        
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmPagoPlanillas_Load(object sender, EventArgs e)
        {
            MtdMostrarListaEmpleados();
            lblFechaSistema.Text = cl_pagoplanillas.MtdFechaSistema().ToString("dd/MM/yyyy");
            MtdConsultarPagoPlanilla();
        }

        private void cboxCodigoEmpleado_SelectedIndexChanged(object sender, EventArgs e)
        {
            var EmpleadoSeleccionado = cboxCodigoEmpleado.SelectedItem;
            int CodigoEmpleado = (int)EmpleadoSeleccionado.GetType().GetProperty("Value").GetValue(EmpleadoSeleccionado, null);
            lblSalario.Text = cd_pagoplanillas.MtdConsultarEmpleado(CodigoEmpleado);
            lblBono.Text = cl_pagoplanillas.MtdBono(double.Parse(lblSalario.Text)).ToString();
 
        }

        private void txtHorasExtras_TextChanged(object sender, EventArgs e)
        {
            if (txtHorasExtras.Text == "")
            {
                lblMontoTotal.Text = "0.00";
            }else
            {
                double salario = double.Parse(lblSalario.Text);
                double horasExtras = double.Parse(txtHorasExtras.Text);

                lblMontoTotal.Text = cl_pagoplanillas.MtdMontoTotalSalario(salario, horasExtras).ToString();
            }
                           
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void lblSalario_Click(object sender, EventArgs e)
        {

        }

        public void MtdConsultarPagoPlanilla()
        {
            DataTable dt_pagoplanilla = cd_pagoplanillas.MtdVerificarPagoPlanilla();
            dgvPagoPlanillas.DataSource = dt_pagoplanilla;
        }

        public void MtdLimpiarCampos()
        {
            txtPagoPlanilla.Text = "";
            cboxCodigoEmpleado.Text = "";
            lblSalario.Text = "";
            lblBono.Text = "";
            txtHorasExtras.Text = "";
            lblMontoTotal.Text = "";
            cboxEstado.Text = "";
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            
                int codigoEmpleado = int.Parse(cboxCodigoEmpleado.Text.Split('-')[0].Trim());
                DateTime Fechapago = dtpFechaPago.Value;
                double salario = double.Parse(lblSalario.Text);
                double bono = double.Parse(lblBono.Text);
                double horasExtras = double.Parse(txtHorasExtras.Text);
                double montoTotal = double.Parse(lblMontoTotal.Text);
                string estado = cboxEstado.Text;
                string usuarioSistema = UserCache.NombreUsuario;
                DateTime fechasistema = cl_pagoplanillas.MtdFechaSistema();

                cd_pagoplanillas.MtdAgregarPagoPlanilla(codigoEmpleado, Fechapago, salario, bono, horasExtras, montoTotal, estado, usuarioSistema, fechasistema);
                MessageBox.Show("Pago Planilla Realizado Correctamente", "Guardado exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MtdConsultarPagoPlanilla();
                MtdLimpiarCampos();
        }

        private void dgvPagoPlanillas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtPagoPlanilla.Text = dgvPagoPlanillas.SelectedCells[0].Value.ToString();
            cboxCodigoEmpleado.Text = dgvPagoPlanillas.SelectedCells[1].Value.ToString();
            dtpFechaPago.Text = dgvPagoPlanillas.SelectedCells[2].Value.ToString();
            lblSalario.Text = dgvPagoPlanillas.SelectedCells[3].Value.ToString();
            lblBono.Text = dgvPagoPlanillas.SelectedCells[4].Value.ToString();
            txtHorasExtras.Text = dgvPagoPlanillas.SelectedCells[5].Value.ToString();
            lblMontoTotal.Text = dgvPagoPlanillas.SelectedCells[6].Value.ToString();
            cboxEstado.Text = dgvPagoPlanillas.SelectedCells[7].Value.ToString();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            int codigoPagoPlanilla = int.Parse(txtPagoPlanilla.Text);
            int codigoEmpleado = int.Parse(cboxCodigoEmpleado.Text.Split('-')[0].Trim());
            DateTime Fechapago = dtpFechaPago.Value;
            double salario = double.Parse(lblSalario.Text);
            double bono = double.Parse(lblBono.Text);
            double horasExtras = double.Parse(txtHorasExtras.Text);
            double montoTotal = double.Parse(lblMontoTotal.Text);
            string estado = cboxEstado.Text;
            string usuarioSistema = UserCache.NombreUsuario;
            DateTime fechasistema = cl_pagoplanillas.MtdFechaSistema();

            cd_pagoplanillas.MtdActualizarPagoPlanilla(codigoPagoPlanilla, codigoEmpleado, Fechapago, salario, bono, horasExtras, montoTotal, estado, usuarioSistema, fechasistema);
            MessageBox.Show("Pago Planilla Actualizado Correctamente", "Guardado exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            MtdConsultarPagoPlanilla();
            MtdLimpiarCampos();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            MtdLimpiarCampos();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int CodigoPagoPlanilla = (int.Parse(txtPagoPlanilla.Text));

                cd_pagoplanillas.MtdEliminarPagoPlanilla(CodigoPagoPlanilla);
                MessageBox.Show("Pago Planilla Eliminado", "Eliminación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MtdConsultarPagoPlanilla();
                MtdLimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
