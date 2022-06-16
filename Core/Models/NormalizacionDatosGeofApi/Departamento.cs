using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.NormalizacionDatosGeofApi
{
    public class DepartamentosApiResponse
    {
        public List<Departamento> departamentos { get; set; }
    }

    public class Departamento
    {
        public string id { get; set; }
        public string nombre { get; set; }
        public Provincia provincia { get; set; }
    }
    
}
