using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecetasSLN.dominio
{
    internal class DetalleReceta
    {
    

        public Ingrediente oingrediente { get; set; }
        public int cantidad { get; set; }

        public DetalleReceta(Ingrediente oingrediente, int cantidad)
        {
            this.oingrediente = oingrediente;
            this.cantidad = cantidad;
        }

    }
}
