using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.NormalizacionDatosGeofApi
{
    public class DireccionNormalizada
    {
        public class Altura
        {
            public object unidad { get; set; }
            public int valor { get; set; }
        }

        public class Calle
        {
            public string categoria { get; set; }
            public string id { get; set; }
            public string nombre { get; set; }
        }

        public class CalleCruce1
        {
            public object categoria { get; set; }
            public object id { get; set; }
            public object nombre { get; set; }
        }

        public class CalleCruce2
        {
            public object categoria { get; set; }
            public object id { get; set; }
            public object nombre { get; set; }
        }

        public class Departamento
        {
            public string id { get; set; }
            public string nombre { get; set; }
        }

        public class Direccion
        {
            public Altura altura { get; set; }
            public List<string> calles { get; set; }
            public object piso { get; set; }
            public string tipo { get; set; }
        }

        public class DireccionApi
        {
            public Altura altura { get; set; }
            public Calle calle { get; set; }
            public CalleCruce1 calle_cruce_1 { get; set; }
            public CalleCruce2 calle_cruce_2 { get; set; }
            public Departamento departamento { get; set; }
            public LocalidadCensal localidad_censal { get; set; }
            public string nomenclatura { get; set; }
            public object piso { get; set; }
            public Provincia provincia { get; set; }
            public Ubicacion ubicacion { get; set; }
        }

        public class LocalidadCensal
        {
            public string id { get; set; }
            public string nombre { get; set; }
        }

        public class Parametros
        {
            public string departamento { get; set; }
            public Direccion direccion { get; set; }
            public string localidad { get; set; }
            public string provincia { get; set; }
        }

        public class DireccionNormalizadaApiResponse
        {
            public int cantidad { get; set; }
            public List<DireccionApi> direcciones { get; set; }
            public int inicio { get; set; }
            public Parametros parametros { get; set; }
            public int total { get; set; }
        }

        public class Ubicacion
        {
            public double lat { get; set; }
            public double lon { get; set; }
        }


    }
}
