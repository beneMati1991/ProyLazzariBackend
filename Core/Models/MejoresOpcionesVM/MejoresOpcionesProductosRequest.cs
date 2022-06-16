using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.Models.NormalizacionDatosGeofApi.DireccionNormalizada;

namespace Core.Models.MejoresOpcionesVVM
{
    public class MejoresOpcionesProductosRequest
    {
        public List<ProductoViewModel> Productos { get; set; }
        public double DistanciaMaxima { get; set; } 
        public Ubicacion UbicacionUsuario { get; set; }  
    }
}
