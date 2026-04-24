using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogica
{
    public class CLempleado
    {
        public DateTime mtdFechaActual()
        {
            return DateTime.Today;
        }

        public decimal mtdSalarioEmpleado(string CargoEmpleado)
        {

            if (CargoEmpleado == "Gerente")
            {
                return 25000;
            }
            else if (CargoEmpleado == "Cocinero")
            {
                return 15000;
            }
            else if (CargoEmpleado == "Mesero")
            {
                return 6000;
            }
            else if (CargoEmpleado == "Cajero")
            {
                return 7000;
            }
            else if (CargoEmpleado == "Bodeguero")
            {
                return 7500;
            }
            else
            {
                return 0;
            }
        }

    }
}
