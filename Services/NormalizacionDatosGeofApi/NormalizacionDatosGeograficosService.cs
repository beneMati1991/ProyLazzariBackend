using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using static Services.NormalizacionDatosGeofApi.DireccionNormalizada;

namespace Services.NormalizacionDatosGeofApi
{
    public interface INormalizacionDatosGeograficos
    {
        Task<IEnumerable<Provincia>> GetProvinciasAsync(string provincia);
        Task<IEnumerable<Departamento>> GetDepartamentosPorProvinciaAsync(string provincia, string municipio);
        Task<IEnumerable<Localidad>> GetLocalidadesPorProvinciaYDepartamentoAsync(string provincia, string municipio, string localidad);
        Task<Ubicacion> GetCoordenadas(Domicilio domicilio);
    }

    public class NormalizacionDatosGeograficosService : INormalizacionDatosGeograficos
    {
        private static HttpClient _HttpClient;
        private readonly string baseUrl;

        public NormalizacionDatosGeograficosService()
        {
            _HttpClient = new HttpClient();
            baseUrl = "https://apis.datos.gob.ar/georef/api/";
            _HttpClient.BaseAddress = new Uri(baseUrl);
            _HttpClient.DefaultRequestHeaders.Accept.Clear();
            _HttpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// Servicio para obtener todas las provincias de Argentina. Opcionalmente se puede filtrar por nombre
        /// </summary>
        /// <param name="nombreProvincia"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Provincia>> GetProvinciasAsync(string nombreProvincia)
        {
            var provinciasResponse = new List<Provincia>();

            var path = "provincias?campos=id,nombre&max=24";

            if(!string.IsNullOrEmpty(nombreProvincia)) path = $"provincias?nombre={nombreProvincia}&campos=id,nombre&max=24";

            try
            {
                HttpResponseMessage response = await _HttpClient.GetAsync(path);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsAsync<ProvinciasApiResponse>();

                    foreach (var provincia in content.provincias)
                    {
                        provinciasResponse.Add(provincia);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return provinciasResponse.OrderBy(p => p.id);
        }

        /// <summary>
        /// Servicio para obtener los municipios de una provincia. Opcionalmente se puede filtrar por nombre de municipio
        /// </summary>
        /// <param name="nombreProvincia"></param>
        /// <param name="nombreMunicipio"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Departamento>> GetDepartamentosPorProvinciaAsync(string nombreProvincia, string nombreDepartamento)
        {
            var municipiosResponse = new List<Departamento>();

            var path = $"departamentos?provincia={nombreProvincia}&campos=id,nombre,provincia&max=500";

            if (!string.IsNullOrEmpty(nombreDepartamento)) path = $"departamentos?provincia={nombreProvincia}&nombre={nombreDepartamento}&campos=id,nombre,provincia&max=500";

            try
            {
                HttpResponseMessage response = await _HttpClient.GetAsync(path);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsAsync<DepartamentosApiResponse>();

                    foreach (var municipio in content.departamentos)
                    {
                        municipiosResponse.Add(municipio);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return municipiosResponse.OrderBy(m => m.id);
        }

        /// <summary>
        /// Servicio para obtener las localidades a partir de un municipio y provincia. Opcionalmente se puede filtrar por nombre de localidad
        /// </summary>
        /// <param name="nombreProvincia"></param>
        /// <param name="nombreMunicipio"></param>
        /// <param name="nombreLocalidad"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Localidad>> GetLocalidadesPorProvinciaYDepartamentoAsync(string nombreProvincia, string nombreDepartamento, string nombreLocalidad)
        {
            var localidadesResponse = new List<Localidad>();

            var path = $"localidades?provincia={nombreProvincia}&departamento={nombreDepartamento}&campos=id,nombre,departamento&max=500";

            if (!string.IsNullOrEmpty(nombreLocalidad)) path = $"localidades?provincia={nombreProvincia}&departamento={nombreDepartamento}&nombre={nombreLocalidad}&campos=id,nombre,departamento&max=500";

            try
            {
                HttpResponseMessage response = await _HttpClient.GetAsync(path);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsAsync<LocalidadesApiResponse>();

                    foreach (var localidad in content.localidades)
                    {
                        localidadesResponse.Add(localidad);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return localidadesResponse.OrderBy(l => l.id);
        }

        /// <summary>
        /// Servicio para obtener las coordenadas de un domicilio
        /// </summary>
        /// <param name="domicilio"></param>
        /// <returns></returns>
        public async Task<Ubicacion> GetCoordenadas(Domicilio domicilio)
        {
            var coordenadas = new Ubicacion();

            var path = $"direcciones?direccion={domicilio.Calle + " " + domicilio.Altura}&departamento={domicilio.Municipio}&localidad={domicilio.Localidad}&provincia={domicilio.Provincia}";

            try
            {
                HttpResponseMessage response = await _HttpClient.GetAsync(path);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsAsync<DireccionNormalizadaApiResponse>();
                    var direccion = content.direcciones.FirstOrDefault();
                    coordenadas = direccion.ubicacion;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return coordenadas;
        }

    }
}
