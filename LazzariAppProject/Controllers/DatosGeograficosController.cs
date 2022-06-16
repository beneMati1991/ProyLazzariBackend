using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.NormalizacionDatosGeofApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LazzariAppProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatosGeograficosController : ControllerBase
    {
        private readonly INormalizacionDatosGeograficos _datosGeograficosService;

        public DatosGeograficosController(INormalizacionDatosGeograficos datosGeograficosService)
        {
            _datosGeograficosService = datosGeograficosService;
        }

        [HttpGet]
        [Route("/provincias")]
        public async Task<IActionResult> GetProvincias(string provincia)
        {
            var provincias = await _datosGeograficosService.GetProvinciasAsync(provincia);

            if (provincias is null) return NotFound();

            return Ok(provincias);
        }

        [HttpGet]
        [Route("/provincias/{provincia}/departamentos")]
        public async Task<IActionResult> GetMunicipios(string provincia, string departamento)
        {
            var departamentos = await _datosGeograficosService.GetDepartamentosPorProvinciaAsync(provincia, departamento);

            if (departamentos is null) return NotFound();

            return Ok(departamentos);
        }

        [HttpGet]
        [Route("/provincias/{provincia}/departamentos/{departamento}/localidades")]
        public async Task<IActionResult> GetLocalidades(string provincia, string departamento, string localidad)
        {
            var localidades = await _datosGeograficosService.GetLocalidadesPorProvinciaYDepartamentoAsync(provincia, departamento, localidad);

            if (localidades is null) return NotFound();

            return Ok(localidades);
        }
    }
}
