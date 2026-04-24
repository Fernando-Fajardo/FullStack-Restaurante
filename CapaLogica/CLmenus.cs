using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogica
{
    public class CLmenus
    {
        public DateTime MtdFechaActual()
        {
            return DateTime.Today;
        }

        public decimal MtdPrecioMenu(string categoria)
        {
            decimal precio = 0;

            if (categoria == "Desayunos")
            {
                precio = 50;
            }
            else if (categoria == "Almuerzos")
            {
                precio = 100;
            }
            else if (categoria == "Cenas")
            {
                precio = 75;
            }
            else if (categoria == "Postres")
            {
                precio = 35;
            }
            else if (categoria == "Bebidas")
            {
                precio = 25;
            }

            return precio;
        }
    }
}
