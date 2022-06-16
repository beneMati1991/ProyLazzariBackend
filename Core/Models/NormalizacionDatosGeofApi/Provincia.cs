using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.NormalizacionDatosGeofApi
{
    public class ProvinciasApiResponse
    {
        public List<Provincia> provincias { get; set; }
    }

    public class Provincia
    {
        public string id { get; set; }
        public string nombre { get; set; }
    }

}
