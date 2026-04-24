using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaLogica
{
    public class CLInventarios
    {

        public DateTime MtdFechaSistema()
        {
             return DateTime.Today;
        }
        public  int MtdDiasVigencia(DateTime fechaEntrada, DateTime fechaVencimiento)
        {
            int dias = (fechaVencimiento - fechaEntrada).Days;
            if (fechaEntrada > fechaVencimiento)
                throw new ArgumentException("La fecha de entrada no puede ser mayor que la fecha de vencimiento.");

            return dias;

        }
    }
}
