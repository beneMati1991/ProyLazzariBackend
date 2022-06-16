using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.NormalizacionDatosGeofApi
{
    public class LocalidadesApiResponse
    {
        public List<Localidad> localidades { get; set; }
    }

    public class Localidad
    {
        public string id { get; set; }
        public string nombre { get; set; }
        public Departamento departamento { get; set; }

        public class Departamento
        {
            public string id { get; set; }
            public string nombre { get; set; }
        }
    }


}
