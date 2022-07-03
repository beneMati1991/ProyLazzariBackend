using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.MejoresOpcionesVVM
{
    public class ComercioMejorOpcionViewModel
    {
        public string Nombre { get; set; }
        public string Calle { get; set; }
        public long Altura { get; set; }
        public double Distancia { get; set; }
        public double PrecioTotal { get; set; }
        public bool CarritoCompleto { get; set; }
        public List<ProductoViewModel> Productos { get; set; }
      
    }
}
