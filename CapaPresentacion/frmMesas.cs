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
    public partial class frmMesas: Form
    {
        CDMesas cdmesas = new CDMesas();
        CLmesas clmesas = new CLmesas();
        public frmMesas()
        {

            InitializeComponent();

        }

        private void frmMesas_Load(object sender, EventArgs e)
        {

            lblFechaActual.Text = clmesas.MtdFechaActual().ToString("dd/MM/yyyy");
            mtdConsultarMesas();

        }

        public void mtdConsultarMesas()
        {

            DataTable dtMesas = cdmesas.MtdConsultarMesa();
            dgvMesas.DataSource = dtMesas;

        }

        private void dgvMesas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        public void mtdLimpiarCampos()
        {

            cboxNumeroMesa.Text = " ";
            cboxCantidadSillas.Text = " ";
            txtUbicacion.Text = " ";
            cboxTipoMesa.Text = " ";
            cboxEstado.Text = " ";

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            try
            {
                
                int NumeroMesa = int.Parse(cboxNumeroMesa.Text);
                int CantidadSillas = int.Parse(cboxCantidadSillas.Text);
                string Ubicacion = txtUbicacion.Text;
                string TipoMesa = cboxTipoMesa.Text;
                string Estado = cboxEstado.Text;
                string UsuarioSistema = UserCache.NombreUsuario;
                DateTime FechaSistema = clmesas.MtdFechaActual();

                cdmesas.MtdAgregarMesa(NumeroMesa, CantidadSillas, Ubicacion, TipoMesa, Estado, UsuarioSistema, FechaSistema);
                MessageBox.Show("Mesa agregada correctamente", "Confirmacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                mtdConsultarMesas();
                mtdLimpiarCampos();
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
                int CodigoMesa = int.Parse(txtCodigoMesa.Text);
                int NumeroMesa = int.Parse(cboxNumeroMesa.Text);
                int CantidadSillas = int.Parse(cboxCantidadSillas.Text);
                string Ubicacion = txtUbicacion.Text;
                string TipoMesa = cboxTipoMesa.Text;
                string Estado = cboxEstado.Text;
                string UsuarioSistema = UserCache.NombreUsuario;
                DateTime FechaSistema = clmesas.MtdFechaActual();

                cdmesas.MtdActualizarMesa(CodigoMesa, NumeroMesa, CantidadSillas, Ubicacion, TipoMesa, Estado, UsuarioSistema, FechaSistema);
                MessageBox.Show("Mesa actulizada correctamente", "Confirmacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                mtdConsultarMesas();
                mtdLimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void dgvMesas_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            txtCodigoMesa.Text = dgvMesas.SelectedCells[0].Value.ToString();
            cboxNumeroMesa.Text = dgvMesas.SelectedCells[1].Value.ToString();
            cboxCantidadSillas.Text = dgvMesas.SelectedCells[2].Value.ToString();
            txtUbicacion.Text = dgvMesas.SelectedCells[3].Value.ToString();
            cboxTipoMesa.Text = dgvMesas.SelectedCells[4].Value.ToString();
            cboxEstado.Text = dgvMesas.SelectedCells[5].Value.ToString();

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

            mtdLimpiarCampos();

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

            try
            {
                int CodigoMesa = int.Parse(txtCodigoMesa.Text);


                if (cdmesas.MtdConsultarEncabezado(CodigoMesa) == true)
                {
                    MessageBox.Show("La fila que intenta eliminar depende de una o más filas en otra tabla, por lo que no se podrá eliminar hasta que deje de depender de ellas", "Error al borrar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    cdmesas.MtdEliminarMesa(CodigoMesa);
                    MessageBox.Show("Mesa eliminada correctamente", "Confirmacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mtdConsultarMesas();
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
