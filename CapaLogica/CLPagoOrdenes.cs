using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogica
{
    public class CLPagoOrdenes
    {
        //METODOS PARA HACER: PROPINA, IMPUESTOS, DESCUENTO, TOTALPAGO

        public DateTime MtdFechaActual()
        {
            return DateTime.Today;
        }

        public double MtdPropina (double MontoTotalOrd)
        {
            Double propina = 0.10 * MontoTotalOrd;

            return propina;
        }

        public double MtdImpuesto(double MontoTotalOrd)
        {
            Double impuesto = 0.12 * MontoTotalOrd;
            return impuesto;
        }

        public double MtdDescuento (double MontoTotalOrd)
        {
            Double descuento = 0;
            if (MontoTotalOrd >=0 && MontoTotalOrd <= 100)
            {
                descuento = MontoTotalOrd * 0.02;
            }
            else if (MontoTotalOrd >= 100 && MontoTotalOrd <= 300)
            {
                descuento = MontoTotalOrd * 0.03;
            }
            else if (MontoTotalOrd >= 500)
            {
                descuento = MontoTotalOrd * 0.05;
            }
            return descuento;
        }

        public double MtdTotalPago(double MontoTotalOrd)
        {
            double totalpago = MontoTotalOrd + MtdPropina(MontoTotalOrd) + MtdImpuesto(MontoTotalOrd) - MtdDescuento(MontoTotalOrd);
            return totalpago;
        }

    }
}
