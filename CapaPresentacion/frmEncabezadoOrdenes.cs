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
    public partial class frmEncabezadoOrdenes: Form
    {
        CDencabezadoOrdenes cd_encabezadoOrdenes = new CDencabezadoOrdenes();
        CLencabezadoOrdenes cl_encabdezadoOrdenes = new CLencabezadoOrdenes();

        public frmEncabezadoOrdenes()
        {
            InitializeComponent();
        }
        private void frmEncabezadoOrdenes_Load(object sender, EventArgs e)
        {
            MtdMostrarListaDetallesOrdenes();
            MtdMostrarListaClientes();
            MtdMostrarListaMesas();
            MtdMostrarListaEmpleados();
            mtdConsultarEncabezadoOrdenes();
            lblFechaActual.Text = cl_encabdezadoOrdenes.MtdFechaActual().ToString("dd/MM/yyyy");
        }

        private void MtdMostrarListaClientes()
        {
            var ListaClientes = cd_encabezadoOrdenes.MtdListarCliente();

            foreach (var Clientes in ListaClientes)
            {
                cboxCodigoCliente.Items.Add(Clientes);
            }

            cboxCodigoCliente.DisplayMember = "Text";
            cboxCodigoCliente.ValueMember = "Value";
        }

        private void MtdMostrarListaMesas()
        {
            var ListaMesas = cd_encabezadoOrdenes.MtdListarMesas();
            foreach (var Mesas in ListaMesas)
            {
                cboxCodigoMesa.Items.Add(Mesas);
            }

            cboxCodigoMesa.DisplayMember = "Text";
            cboxCodigoMesa.ValueMember = "Value";
        }

        private void MtdMostrarListaEmpleados()
        {
            var ListaEmpleados = cd_encabezadoOrdenes.MtdListarEmpleados();
            foreach (var Empleados in ListaEmpleados)
            {
                cboxCodigoEmpleado.Items.Add(Empleados);
            }

            cboxCodigoEmpleado.DisplayMember = "Text";
            cboxCodigoEmpleado.ValueMember = "Value";
        }

        private void MtdMostrarListaDetallesOrdenes()
        {
            var ListaDetallesOrdenes = cd_encabezadoOrdenes.MtdListarDetallesOrdenes();
            foreach (var Detalles in ListaDetallesOrdenes)
            {
                cboxDetallesOrdenes.Items.Add(Detalles);
            }
            cboxDetallesOrdenes.DisplayMember = "Text";
            cboxDetallesOrdenes.ValueMember = "Value";
        }

        private void mtdConsultarEncabezadoOrdenes()
        {
            DataTable dtEncabezadoOrdenes = cd_encabezadoOrdenes.MtdConsultarEncabezadoOrdenes();
            dgvEncabezadoOrdenes.DataSource = dtEncabezadoOrdenes;
        }

        public void mtdLimpiarCampos()
        {
            txtCodigoOrdenEncabezado.Text = "";
            cboxDetallesOrdenes.Text = "";
            cboxCodigoCliente.Text = "";
            cboxCodigoMesa.Text = "";
            cboxCodigoEmpleado.Text = "";
            dtpFechaOrden.Value = DateTime.Now;
            lblMontoTotalOrden.Text = "0.00";
            cboxEstado.Text = "";
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                int CodigoOrdenDet = int.Parse(cboxDetallesOrdenes.Text.Split('-')[1].Trim());
                int CodigoCliente = int.Parse(cboxCodigoCliente.Text.Split('-')[0].Trim());
                int CodigoMesa = int.Parse(cboxCodigoMesa.Text.Split('-')[0].Trim());
                int CodigoEmpleado = int.Parse(cboxCodigoEmpleado.Text.Split('-')[0].Trim());
                DateTime FechaOrden = dtpFechaOrden.Value;
                decimal MontoTotalOrden = cd_encabezadoOrdenes.MtdTotalOrd(CodigoOrdenDet);
                string Estado = cboxEstado.Text;
                string UsuarioSistema = UserCache.NombreUsuario;
                DateTime FechaSistema = cl_encabdezadoOrdenes.MtdFechaActual();

                cd_encabezadoOrdenes.MtdAgregarEncabezadoOrdenes(CodigoOrdenDet, CodigoCliente, CodigoMesa, CodigoEmpleado, FechaOrden, MontoTotalOrden, Estado, UsuarioSistema, FechaSistema);
                MessageBox.Show("Orden agregada", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                mtdLimpiarCampos();
                mtdConsultarEncabezadoOrdenes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void txtCodigoOrdenEncabezado_TextChanged(object sender, EventArgs e)
        {
            if(txtCodigoOrdenEncabezado.Text == "")
            {
                lblMontoTotalOrden.Text = "0.00";
            }
            else
            {
                lblMontoTotalOrden.Text = cd_encabezadoOrdenes.MtdTotalOrd(int.Parse(txtCodigoOrdenEncabezado.Text)).ToString();
            }
        }

        private void dgvEncabezadoOrdenes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCodigoOrdenEncabezado.Text = dgvEncabezadoOrdenes.SelectedCells[0].Value.ToString();
            cboxDetallesOrdenes.Text = dgvEncabezadoOrdenes.SelectedCells[1].Value.ToString();
            cboxCodigoCliente.Text = dgvEncabezadoOrdenes.SelectedCells[2].Value.ToString();
            cboxCodigoMesa.Text = dgvEncabezadoOrdenes.SelectedCells[3].Value.ToString();
            cboxCodigoEmpleado.Text = dgvEncabezadoOrdenes.SelectedCells[4].Value.ToString();
            dtpFechaOrden.Text = dgvEncabezadoOrdenes.SelectedCells[5].Value.ToString();
            lblMontoTotalOrden.Text = dgvEncabezadoOrdenes.SelectedCells[6].Value.ToString();
            cboxEstado.Text = dgvEncabezadoOrdenes.SelectedCells[7].Value.ToString();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            mtdLimpiarCampos();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboxDetallesOrdenes_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMontoTotalOrden.Text = cd_encabezadoOrdenes.MtdTotalOrd(int.Parse(cboxDetallesOrdenes.Text.Split('-')[1].Trim())).ToString("0.00");
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int CodigoOrdenEnc = int.Parse(txtCodigoOrdenEncabezado.Text);


                if (cd_encabezadoOrdenes.MtdConsultarPago(CodigoOrdenEnc) == true)
                {
                    MessageBox.Show("La fila que intenta eliminar depende de una o más filas en otra tabla, por lo que no se podrá eliminar hasta que deje de depender de ellas", "Error al borrar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    cd_encabezadoOrdenes.MtdEliminarEncabezadoOrdenes(CodigoOrdenEnc);
                    MessageBox.Show("Encabezado eliminado correctamente", "Confirmacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mtdConsultarEncabezadoOrdenes();
                    mtdLimpiarCampos();
                }            
        }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                int CodigoOrdenEnc = int.Parse(txtCodigoOrdenEncabezado.Text);
                int CodigoOrdenDet = int.Parse(cboxDetallesOrdenes.Text);
                int CodigoCliente = int.Parse(cboxCodigoCliente.Text.Split('-')[0].Trim()); 
                int CodigoMesa = int.Parse(cboxCodigoMesa.Text.Split('-')[0].Trim());
                int CodigoEmpleado = int.Parse(cboxCodigoEmpleado.Text.Split('-')[0].Trim());
                DateTime FechaOrden = dtpFechaOrden.Value;
                decimal MontoTotalOrden = cd_encabezadoOrdenes.MtdTotalOrd(CodigoOrdenDet);
                string Estado = cboxEstado.Text;
                string UsuarioSistema = UserCache.NombreUsuario;
                DateTime FechaSistema = cl_encabdezadoOrdenes.MtdFechaActual();

                cd_encabezadoOrdenes.MtdActualizarEncabezadoOrdenes(CodigoOrdenEnc, CodigoOrdenDet, CodigoCliente, CodigoMesa, CodigoEmpleado, FechaOrden, MontoTotalOrden, Estado, UsuarioSistema, FechaSistema);
                MessageBox.Show("Orden actualizada", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                mtdLimpiarCampos();
                mtdConsultarEncabezadoOrdenes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
