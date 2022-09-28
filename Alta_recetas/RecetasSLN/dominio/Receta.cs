using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecetasSLN.dominio
{
    internal class Receta
    {
  

        public int nroReceta { get; set; }

        public List<DetalleReceta> Detalles { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public string cheff { get; set; }

        public Receta()
        {
            Detalles = new List<DetalleReceta>();
        }


        public void AgregarDetalle(DetalleReceta detalle)
        {
            Detalles.Add(detalle);
        }

        public void QuitarDetalle(int indice)
        {
            Detalles.RemoveAt(indice);
        }
    }
}
