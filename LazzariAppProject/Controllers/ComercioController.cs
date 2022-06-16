using Core.Business.Interfaces;
using Core.Helper;
using Core.Helper.Autenticacion;
using Core.Helper.Paginacion;
using Core.Models;
using Core.Models.DTOs;
using Core.Models.DTOs.UsuarioDTO.Consumidor;
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
    [Authorize(Roles = "Comerciante, Administrador")]
    [Route("api/comercios/")]
    [ApiController]
    public class ComercioController : ControllerBase
    {
        private readonly IComercioBusiness _business;
        private readonly IUriService _uriService;

        public ComercioController(IComercioBusiness business, IUriService uriService) 
        {
            _business = business;
            _uriService = uriService;
        }

        /// <summary>
        /// Endpoint para obtener todos los productos paginado de un comercio. Opcionalmente se puede filtrar por nombre
        /// y ordenar por "nombre", "precio" o "sucursal". 
        /// Tamaño y número de página del paginado son opcionales, por default el tamaño es 20 y la página 1.
        /// </summary>
        /// <param name="comercioId"></param>
        /// <param name="nombre"></param>
        /// <param name="orderBy"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{comercioId}/productos")]
        public async Task<IActionResult> GetProductos(int comercioId, string nombre, string orderBy, [FromQuery] PaginationFilter filter)
        {
            var token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");

            var claims = UsuarioOwnerHandler.GetClaimsUsuario(token);

            if (!await _business.EsOwner(claims.UsuarioId, comercioId)) return Unauthorized();

            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);

            var route = Request.Path.Value;

            var productos = _business.GetProductosPorComercioPaginado(comercioId, validFilter, nombre, orderBy);

            var totalRecords = await _business.CountProductosPorComercio(comercioId, nombre);

            if (!string.IsNullOrEmpty(nombre) || !string.IsNullOrEmpty(orderBy))
            {
                var url = Request.HttpContext.Request.GetEncodedUrl();
                var uri = new Uri(url);
                var queryParams = uri.Query.ToString().Split("&Page");
                var query = string.Empty;
                foreach(var item in queryParams)
                {
                    if (item.Contains("Number")) break;
                    query += item;  
                }
                route = Request.Path.Value + query;
            }

            if (productos is null) return NotFound(Constantes.ComercioSinProductos);

            var pagedResponse = PaginationHelper.CreatePagedReponse(productos.ToList(), validFilter,
            totalRecords, _uriService, route);

            return Ok(pagedResponse);
        }

        [HttpGet]
        [Route("{comercioId}/productos/{idProducto}")]
        public async Task<IActionResult> GetProductoPorId(int comercioId, int idProducto)
        {
            var token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");

            var claims = UsuarioOwnerHandler.GetClaimsUsuario(token);

            if (!await _business.EsOwner(claims.UsuarioId, comercioId)) return Unauthorized();

            var producto = _business.GetProductoPorIdPorComercio(comercioId, idProducto);

            if (producto is null) return NotFound(Constantes.ProductoInexistente);

            return Ok(producto);
        }
      
        [HttpPost]
        [Route("{comercioId}/productos")]
        public async Task<ActionResult> CargarProducto(int comercioId, [FromBody] ProductoDtoAlta productoDto)
        {
            var token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");

            var claims = UsuarioOwnerHandler.GetClaimsUsuario(token);

            if (!await _business.EsOwner(claims.UsuarioId, comercioId)) return Unauthorized();

            var result = await _business.AltaProducto(comercioId, productoDto);

            if (result is not null)
            {
                return Ok(new GenericResponse
                {
                    StatusCode = HttpStatusCode.OK.GetHashCode(),
                    Data = result,
                    Message = Constantes.ProductoOk
                });
            }

            else return BadRequest(new GenericResponse
            {
                StatusCode = HttpStatusCode.BadRequest.GetHashCode(),
                Data = null,
                Message = Constantes.Error
            });
        }


        [HttpPut]
        [Route("{comercioId}/productos/{productoId}")]
        public async Task<ActionResult> EditarProducto([FromBody] ProductoDtoAlta productoDto, int comercioId, int productoId)
        {
            var token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");

            var claims = UsuarioOwnerHandler.GetClaimsUsuario(token);

            if (!await _business.EsOwner(claims.UsuarioId, comercioId)) return Unauthorized();

            var result = await _business.EditarProducto(comercioId, productoId, productoDto);

            if (result is not null)
            {
                return Ok(new GenericResponse
                {
                    StatusCode = HttpStatusCode.OK.GetHashCode(),
                    Data = result,
                    Message = Constantes.ProductoOk
                });
            }

            else return NotFound(Constantes.EntidadNoEncontrada); 
        }

   
        [HttpDelete]
        [Route("{comercioId}/productos/{productoId}")]
        public async Task<ActionResult> BorrarProducto(int comercioId, int productoId)
        {
            var token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");

            var claims = UsuarioOwnerHandler.GetClaimsUsuario(token);

            if (!await _business.EsOwner(claims.UsuarioId, comercioId)) return Unauthorized();

            var entidadBorrada = await _business.DeleteProducto(comercioId, productoId);

            if (entidadBorrada is null) return NotFound(Constantes.EntidadNoEncontrada);

            return Ok(new GenericResponse
            {
                StatusCode = HttpStatusCode.OK.GetHashCode(),
                Data = null,
                Message = Constantes.EntidadEliminada + !entidadBorrada
            });
        }


    }
}
