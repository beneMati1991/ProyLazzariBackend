using Core.Business.Interfaces;
using Core.Helper;
using Core.Helper.Autenticacion;
using Core.Models;
using Core.Models.DTOs;
using Core.Models.DTOs.UsuarioDTO.Comercio;
using Core.Models.DTOs.UsuarioDTO.Consumidor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LazzariAppProject.Controllers
{
    [Authorize]
    [Route("api/usuarios/")]
    [ApiController]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioBusiness _usuarioBusiness;
        private readonly IComercioBusiness _comercioBusiness;
        private readonly IConsumidorBusiness _consumidorBusiness;

        public UsuarioController(IUsuarioBusiness usuarioBusiness, IComercioBusiness comercioBusiness, IConsumidorBusiness consumidorBusiness)
        {
            _usuarioBusiness = usuarioBusiness;
            _comercioBusiness = comercioBusiness;
            _consumidorBusiness = consumidorBusiness;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("comercios/registro")]
        public async Task<IActionResult> RegistrarComercio([FromBody] RegistroComercioDto comercio)
        {

            if (_usuarioBusiness.ComercioExistente(comercio.Email, comercio.NombreUsuario, comercio.Cuit))
            {
                return BadRequest(new GenericResponse
                {
                    StatusCode = HttpStatusCode.BadRequest.GetHashCode(),
                    Data = null,
                    Message = Constantes.ComercioExistente
                });
            }

            var response = await _usuarioBusiness.RegistrarComercio(comercio);

            return Ok(new GenericResponse
            {
                StatusCode = HttpStatusCode.OK.GetHashCode(),
                Data = response,
                Message = Constantes.ComercioRegistrado
            });
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("consumidores/registro")]
        public async Task<IActionResult> RegistrarConsumidor([FromBody] RegistroConsumidorDto consumidor)
        {
            if (_usuarioBusiness.ConsumidorExistente(consumidor.Email, consumidor.NombreUsuario))
            {
                return BadRequest(new GenericResponse
                {
                    StatusCode = HttpStatusCode.BadRequest.GetHashCode(),
                    Data = null,
                    Message = Constantes.ConsumidorExistente
                });
            }

            var response = await _usuarioBusiness.RegistrarConsumidor(consumidor);

            return Ok(new GenericResponse
            {
                StatusCode = HttpStatusCode.OK.GetHashCode(),
                Data = response,
                Message = Constantes.UsuarioRegistrado
            });
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("consumidores/login")]
        public async Task<IActionResult> LoginConsumidor(UsuarioLoginDto usuario)
        {
            var response = await _usuarioBusiness.LoginConsumidor(usuario);

            switch (response)
            {
                case null:
                    return NotFound(Constantes.UsuarioInexistente);
                case Constantes.PasswordIncorrecto:
                    return BadRequest(response);
                default: return new JsonResult(new { Token = response }) { StatusCode = 201 };
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("comercios/login")]
        public async Task<IActionResult> LoginComercio(UsuarioLoginDto usuario)
        {
            var response = await _usuarioBusiness.LoginComercio(usuario);

            switch (response)
            {
                case null:
                    return NotFound(Constantes.UsuarioInexistente);
                case Constantes.PasswordIncorrecto:
                    return BadRequest(response);
                default: return new JsonResult(new { Token = response }) { StatusCode = 201 };
            }
        }

        [Authorize(Roles = "Comerciante, Administrador")]
        [HttpGet]
        [Route("{idUsuario}/comercios/{idComercio}")]
        public async Task<IActionResult> GetPerfilComercio(int idUsuario, int idComercio)
        {
            var token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            
            var claims = UsuarioOwnerHandler.GetClaimsUsuario(token);

            if (!await _comercioBusiness.EsOwner(claims.UsuarioId, idComercio)) return Unauthorized();

            var perfilComercio = await _usuarioBusiness.GetPerfilComercio(idUsuario, idComercio);

            if (perfilComercio is null) return NotFound(Constantes.ComercioInexistente);
            return Ok(perfilComercio);

        }
    

        [Authorize(Roles = "Consumidor, Administrador")]
        [HttpGet]
        [Route("{idUsuario}/consumidores/{idConsumidor}")]
        public async Task<IActionResult> GetPerfilConsumidor(int idUsuario, int idConsumidor)
        {
            var token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var claims = UsuarioOwnerHandler.GetClaimsUsuario(token);

            if (!await _consumidorBusiness.EsOwner(claims.UsuarioId, idConsumidor)) return Unauthorized();

            var perfilConsumidor = await _usuarioBusiness.GetPerfilConsumidor(idUsuario, idConsumidor);

            if (perfilConsumidor is null) return NotFound(Constantes.ConsumidorInexistente);
            return Ok(perfilConsumidor);
        }

        [Authorize(Roles = "Comerciante, Administrador")]
        [HttpPut]
        [Route("{idUsuario}/comercios/{idComercio}")]
        public async Task<IActionResult> EditarPerfilComercio([FromBody]UsuarioComercioDto comercio,int idUsuario, int idComercio)
        {
            var token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var claims = UsuarioOwnerHandler.GetClaimsUsuario(token);

            if (!await _comercioBusiness.EsOwner(claims.UsuarioId, idComercio)) return Unauthorized();

            var result = await _usuarioBusiness.EditarPerfilComercio(comercio,idUsuario, idComercio);

            if (result is not null)
            {
                return Ok(result);
            }
            else return NotFound(Constantes.EntidadNoEncontrada);
        }

        [Authorize(Roles = "Consumidor, Administrador")]
        [HttpPut]
        [Route("{idUsuario}/consumidores/{idConsumidor}")]
        public async Task<IActionResult> EditarPerfilConsumidor([FromBody] UsuarioConsumidorDto consumidor, int idUsuario, int idConsumidor)
        {
            var token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var claims = UsuarioOwnerHandler.GetClaimsUsuario(token);

            if (!await _consumidorBusiness.EsOwner(claims.UsuarioId, idConsumidor)) return Unauthorized();

            var result = await _usuarioBusiness.EditarPerfilConsumidor(consumidor, idUsuario, idConsumidor);

            if (result is not null)
            {
                return Ok(result);
            }
            else return NotFound(Constantes.EntidadNoEncontrada);

        }

        [Authorize(Roles = "Comerciante, Administrador")]
        [HttpDelete]
        [Route("{idUsuario}/comercios/{idComercio}")]
        public async Task<IActionResult> EliminarPerfilComercio(int idUsuario, int idComercio)
        {
            var token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var claims = UsuarioOwnerHandler.GetClaimsUsuario(token);

            if (!await _comercioBusiness.EsOwner(claims.UsuarioId, idComercio)) return Unauthorized();

            var entidadBorrada = await _usuarioBusiness.DeleteComercio(idUsuario, idComercio);

            if (entidadBorrada is null) return NotFound(Constantes.EntidadNoEncontrada);

            return Ok(Constantes.EntidadEliminada + entidadBorrada);
        }

        [Authorize(Roles = "Consumidor, Administrador")]
        [HttpDelete]
        [Route("{idUsuario}/consumidores/{idConsumidor}")]
        public async Task<IActionResult> EliminarPerfilConsumidor(int idUsuario, int idConsumidor)
        {
            var token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var claims = UsuarioOwnerHandler.GetClaimsUsuario(token);

            if (!await _consumidorBusiness.EsOwner(claims.UsuarioId, idConsumidor)) return Unauthorized();

            var entidadBorrada = await _usuarioBusiness.DeleteConsumidor(idUsuario, idConsumidor);

            if (entidadBorrada is null) return NotFound(Constantes.EntidadNoEncontrada);

            return Ok(Constantes.EntidadEliminada + entidadBorrada);
        }


    }
}
