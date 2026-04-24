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
    public partial class frmEmpleados: Form
    {
        CDEmpleados cdempleados = new CDEmpleados();
        CLempleado clempleados = new CLempleado();

        public frmEmpleados()
        {
            InitializeComponent();
        }

        public void mtdConsultarEmpleados()
        {
            DataTable dtEmpleados = cdempleados.MtdConsultarEmpleado();
            dgvEmpleados.DataSource = dtEmpleados;

        }

        private void frmEmpleados_Load(object sender, EventArgs e)
        {

            lblFechaActual.Text = clempleados.mtdFechaActual().ToString("dd/MM/yyyy");
            mtdConsultarEmpleados();

        }

        public void mtdLimpiarCampos()
        {

            txtNombre.Text = " ";
            cboxCargo.Text = " ";
            lblSalario.Text = " ";
            cboxEstado.Text = " ";

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            try
            {
                string Nombre = txtNombre.Text;
                string Cargo = cboxCargo.Text;
                decimal Salario = clempleados.mtdSalarioEmpleado(Cargo);
                DateTime FechaContratacion = DateTime.Parse(dtpFechaContratacion.Text);
                string Estado = cboxEstado.Text;
                string UsuarioSistema = UserCache.NombreUsuario;
                DateTime FechaSistema = clempleados.mtdFechaActual();

                cdempleados.MtdAgregarEmpleado(Nombre, Cargo, Salario, FechaContratacion, Estado, UsuarioSistema, FechaSistema);
                MessageBox.Show("Empleado agregado correctamente", "Confirmacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                mtdConsultarEmpleados();
                mtdLimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void cboxCargo_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblSalario.Text = clempleados.mtdSalarioEmpleado(cboxCargo.Text).ToString();
        }

        private void dgvEmpleados_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            txtCodigoEmpleado.Text = dgvEmpleados.SelectedCells[0].Value.ToString();
            txtNombre.Text = dgvEmpleados.SelectedCells[1].Value.ToString();
            cboxCargo.Text = dgvEmpleados.SelectedCells[2].Value.ToString();
            lblSalario.Text = dgvEmpleados.SelectedCells[3].Value.ToString();
            dtpFechaContratacion.Text = dgvEmpleados.SelectedCells[4].Value.ToString();
            cboxEstado.Text = dgvEmpleados.SelectedCells[5].Value.ToString();

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {

            try
            {

                int CodigoEmpleado = int.Parse(txtCodigoEmpleado.Text);
                string Nombre = txtNombre.Text;
                string Cargo = cboxCargo.Text;
                decimal Salario = decimal.Parse(lblSalario.Text);
                DateTime FechaContratacion = DateTime.Parse(dtpFechaContratacion.Text);
                string Estado = cboxEstado.Text;
                string UsuarioSistema = UserCache.NombreUsuario;
                DateTime FechaSistema = clempleados.mtdFechaActual();

                cdempleados.MtdActualizarEmpleado(CodigoEmpleado, Nombre, Cargo, Salario, FechaContratacion, Estado, UsuarioSistema, FechaSistema);
                MessageBox.Show("Empleado actualizado correctamente", "Confirmacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                mtdConsultarEmpleados();
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
                int CodigoEmpleado = int.Parse(txtCodigoEmpleado.Text);

                if(cdempleados.MtdConsultarUsuario(CodigoEmpleado) == true || cdempleados.MtdConsultarEncabezado(CodigoEmpleado) == true || cdempleados.MtdConsultarPagoPlanillas(CodigoEmpleado) == true)
                {
                    MessageBox.Show("La fila que intenta eliminar depende de una o más filas en otra tabla, por lo que no se podrá eliminar hasta que deje de depender de ellas", "Error al borrar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    cdempleados.MtdEliminarEmpleado(CodigoEmpleado);
                    MessageBox.Show("Cliente eliminado correctamente", "Confirmacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mtdConsultarEmpleados();
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
