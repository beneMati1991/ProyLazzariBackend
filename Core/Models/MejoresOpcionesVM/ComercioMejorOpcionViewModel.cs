using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.MejoresOpcionesVVM
{
    public class ComercioMejorOpcionViewModel
    {
        public List<ProductoViewModel> Productos { get; set; }
        public string Nombre { get; set; }
        public string Distancia { get; set; }
        public string Calle { get; set; }
        public long Altura { get; set; }
        public double PrecioTotal { get; set; }
    }
}
