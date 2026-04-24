using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogica
{
    public class CLPagoPlanillas
    {
        // Bono, MontoTotal
        public DateTime MtdFechaSistema()
        {
            return DateTime.Today;
        }

        public double MtdBono (double Salario)
        {
            double bono = Salario * 0.10;
            return bono;
        }

        public double MtdHorasExtras (double hextras)
        {
            double horasextras = hextras * 15;
            return horasextras;
        }

        public double MtdMontoTotalSalario (double Salario, double hextras)
        {
            double montoTotal = Salario + MtdBono(Salario) + MtdHorasExtras(hextras);
            return montoTotal;
        }
    }
}
