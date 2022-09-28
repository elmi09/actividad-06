using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecetasSLN.dominio
{
    internal class Ingrediente
    {
       

        public int ingredienteid { get; set; }
        public string nombre { get; set; }

        public string unidad { get; set; }


        public Ingrediente(int ingredienteid, string nombre, string unidad)
        {
            this.ingredienteid = ingredienteid;
            this.nombre = nombre;
            this.unidad = unidad;
        }

    }
}
