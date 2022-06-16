using Core.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LazzariAppProject.Controllers
{
    [Route("api/unidades")]
    [ApiController]
    public class UnidadDeMedidaController : ControllerBase
    {
        private readonly IUnidadDeMedidaBusiness _business;

        public UnidadDeMedidaController(IUnidadDeMedidaBusiness business)
        {
            _business = business;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetUnidades() {
            var unidades = await _business.GetAllUnidades();
            if (unidades is null)
            {
                return NotFound();
            }

            return Ok(unidades);
        }
    }
}
