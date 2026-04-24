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
    public partial class FrmInventarios: Form
    {
        CDInventarios cd_inventarios = new CDInventarios();
        CLInventarios cl_inventarios = new CLInventarios();

        public FrmInventarios()
        {
            InitializeComponent();
        }

        private void FrmInventarios_Load(object sender, EventArgs e)
        {
            MtdMostrarListaMenus();
            lblFechaSistema.Text = cl_inventarios.MtdFechaSistema().ToString("dd/MM/yyyy");
            MtdConsultarInventario();
        }

        //primer paso CBOX
        private void MtdMostrarListaMenus()
        {
            var ListaMenus = cd_inventarios.MtdListarMenus();

            foreach (var Menus in ListaMenus)
            {
                cboxCodigoMenu.Items.Add(Menus);
            }

            cboxCodigoMenu.DisplayMember = "Text";
            cboxCodigoMenu.ValueMember = "Value";
            
        }

        
        private void txtCodigoMenu_TextChanged(object sender, EventArgs e)
        {
            
        }

        //primer paso LBL
        private void cboxCodigoMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*lblCategoria.Text = cd_inventarios.MtdConsultarCategoria(int.Parse(cboxCodigoMenu.Text.Split('-')[0].Trim()));*/

            /*
            var MenuSeleccionado = (dynamic)cboxCodigoMenu.SelectedItem;
            int CodigoMenu = (int)MenuSeleccionado.Value;
            lblCategoria.Text = cd_inventarios.MtdConsultarCategoria(CodigoMenu);
            */

            var MenuSeleccionado = cboxCodigoMenu.SelectedItem;
            int CodigoMenu = (int)MenuSeleccionado.GetType().GetProperty("Value").GetValue(MenuSeleccionado, null);
            lblCategoria.Text = cd_inventarios.MtdConsultarCategoria(CodigoMenu);
        }

        
        private void ActualizacionDiasVigencia()
        {
            try
            {
                DateTime fechaEntrada = dtpFechaEntrada.Value.Date;
                DateTime fechaVencimiento = dtpFechaVencimiento.Value.Date;
     
                int dias = cl_inventarios.MtdDiasVigencia(fechaEntrada, fechaVencimiento);
                lblDiasVigencia.Text = $"{dias}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void dtpFechaVencimiento_ValueChanged(object sender, EventArgs e)
        {
            ActualizacionDiasVigencia();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblCategoria_Click(object sender, EventArgs e)
        {

        }

        private void lblDiasVigencia_Click(object sender, EventArgs e)
        {

        }
        public void MtdConsultarInventario()
        {
            DataTable dt_inventario = cd_inventarios.MtdConsultarInventario();
            dgvInventarios.DataSource = dt_inventario;
        }

        public void MtdLimpiarCampos()
        {
            txtCodigoInventario.Text = "";
            cboxCodigoMenu.Text = "";
            lblCategoria.Text = "";
            txtCantidad.Text = "";
            dtpFechaEntrada.Value = DateTime.Now;
            dtpFechaVencimiento.Value = DateTime.Now;
            lblDiasVigencia.Text = "0";
        }
        /*private void btnAgregar_Click(object sender, EventArgs e, DateTime DateTimePicker)
        {
            
        }*/

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                int codigoinventario = int.Parse(txtCodigoInventario.Text);
                int codigomenu = int.Parse(cboxCodigoMenu.Text.Split('-')[0].Trim());
                string categorias = lblCategoria.Text;
                int cantidad = int.Parse(txtCantidad.Text);
                DateTime Fechaentrada = dtpFechaEntrada.Value;
                DateTime Fechavencimiento = dtpFechaVencimiento.Value;
                int diasvigencia = int.Parse(lblDiasVigencia.Text);
                string usuarioSistema = UserCache.NombreUsuario;
                DateTime fechasistema = cl_inventarios.MtdFechaSistema();

                cd_inventarios.MtdActualizarInventario(codigoinventario, codigomenu, categorias, cantidad, Fechaentrada, Fechavencimiento, diasvigencia, usuarioSistema, fechasistema);
                MessageBox.Show("Inventario Actualizado Correctamente", "Guardado exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MtdConsultarInventario();
                MtdLimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            MtdLimpiarCampos();
        }

        private void lblFechaSistema_Click(object sender, EventArgs e)
        {

        }

        private void dgvInventarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCodigoInventario.Text = dgvInventarios.SelectedCells[0].Value.ToString();
            cboxCodigoMenu.Text = dgvInventarios.SelectedCells[1].Value.ToString();
            lblCategoria.Text = dgvInventarios.SelectedCells[2].Value.ToString();
            txtCantidad.Text = dgvInventarios.SelectedCells[3].Value.ToString();
            dtpFechaEntrada.Text = dgvInventarios.SelectedCells[4].Value.ToString();
            dtpFechaVencimiento.Text = dgvInventarios.SelectedCells[5].Value.ToString();
            lblDiasVigencia.Text = dgvInventarios.SelectedCells[6].Value.ToString();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                int codigomenu = int.Parse(cboxCodigoMenu.Text.Split('-')[0].Trim());
                string categorias = lblCategoria.Text;
                int cantidad = int.Parse(txtCantidad.Text);
                DateTime Fechaentrada = dtpFechaEntrada.Value;
                DateTime Fechavencimiento = dtpFechaVencimiento.Value;
                int diasvigencia = int.Parse(lblDiasVigencia.Text);
                string usuarioSistema = UserCache.NombreUsuario;
                DateTime fechasistema = cl_inventarios.MtdFechaSistema();

                cd_inventarios.MtdAgregarInventarios(codigomenu, categorias, cantidad, Fechaentrada, Fechavencimiento, diasvigencia, usuarioSistema, fechasistema);
                MessageBox.Show("Inventario Agregado Correctamente", "Guardado exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MtdConsultarInventario();
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
                int CodigoInventario = (int.Parse(txtCodigoInventario.Text));

                cd_inventarios.MtdEliminarInventario(CodigoInventario);
                MessageBox.Show("Inventario Eliminado", "Eliminación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MtdConsultarInventario();
                MtdLimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
