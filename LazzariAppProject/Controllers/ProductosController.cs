using Core.Business.Interfaces;
using Core.Helper;
using Core.Helper.Autenticacion;
using Core.Helper.Paginacion;
using Core.Models;
using Core.Models.MejoresOpcionesVVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LazzariAppProject.Controllers
{
    [Authorize(Roles = "Consumidor, Administrador")]
    [Route("api/productos/")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoBusiness _business;
        private readonly IUriService _uriService;

        public ProductosController(IProductoBusiness business, IUriService uriService)
        {
            _business = business;
            _uriService = uriService;
        }

        
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetProductos(string nombre, [FromQuery] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            
            var route = Request.Path.Value;

            var productos = await _business.GetAllProductos(nombre); 

            var totalRecords = await _business.CountProductos(nombre);
                      
            if (!string.IsNullOrEmpty(nombre))
            {
                var url = Request.HttpContext.Request.GetEncodedUrl();
                var uri = new Uri(url);
                var queryParams = uri.Query.ToString().Split("&Page");
                var query = string.Empty;
                foreach (var item in queryParams)
                {
                    if (item.Contains("Number")) break;
                    query += item;
                }
                route = Request.Path.Value + query;
            }

            if (productos is null)
            {
                return NotFound();
            }

            var pagedResponse = PaginationHelper.CreatePagedReponse(productos.ToList(), validFilter,
            totalRecords, _uriService, route);

            return Ok(pagedResponse);
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetProductosPorId(int id)
        {
            var result = await _business.GetProductoPorId(id);

            if (result is not null)
            {
                return Ok(result);
            }

            else return NotFound();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("lazzari")]
        public async Task<IActionResult> GetMejorOpcionProductos([FromBody] MejoresOpcionesProductosRequest request, string orderby)
        {
            var result = await _business.GetMejoresOpciones(request, orderby);

            return Ok(new GenericResponse
            {
                StatusCode = HttpStatusCode.OK.GetHashCode(),
                Data = result,
                Message = null
            });
        }


    }
}
