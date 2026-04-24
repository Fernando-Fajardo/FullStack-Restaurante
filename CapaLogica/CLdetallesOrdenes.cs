using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogica
{
    public class CLdetallesOrdenes
    {
        public DateTime MtdFechaActual()
        {
            return DateTime.Today;
        }

        public decimal MtdPrecioTotal(int Cantidad, decimal PrecioUnitario)
        {
            return Cantidad * PrecioUnitario;
        }
    }
}
