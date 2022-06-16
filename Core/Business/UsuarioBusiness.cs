using AutoMapper;
using Core.Business.Interfaces;
using Core.Helper;
using Core.Helper.Autenticacion;
using Core.Mapper;
using Core.Models;
using Core.Models.DTOs;
using Core.Models.DTOs.UsuarioDTO;
using Core.Models.DTOs.UsuarioDTO.Comercio;
using Core.Models.DTOs.UsuarioDTO.Consumidor;
using Entities;
using Repositories;
using Services.NormalizacionDatosGeofApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Core.Business
{
    public class UsuarioBusiness : IUsuarioBusiness
    {
        private readonly IRepository _repository;
        private readonly ITokenHandler _tokenHandler;
        private readonly IUsuarioMapper _mapper;
        private readonly INormalizacionDatosGeograficos _datosGeograficosService;

        public UsuarioBusiness(IRepository repository, ITokenHandler tokenHandler, IUsuarioMapper mapper, INormalizacionDatosGeograficos datosGeograficosService)
        {
            _repository = repository;
            _tokenHandler = tokenHandler;
            _mapper = mapper;
            _datosGeograficosService = datosGeograficosService;
        }

        public bool ComercioExistente(string email, string nombreUsuario, string cuit)
        {
            return _repository.GetQuery<Comercio>(c => c.Usuario).ToList().Exists(c =>
            c.Cuit.Equals(cuit) ||
            c.Email.ToLower().Equals(email.ToLower()) ||
            c.Usuario.Nombre.ToLower().Equals(nombreUsuario.ToLower()));
        }

        public bool ConsumidorExistente(string email, string nombreUsuario)
        {
            return _repository.GetQuery<Consumidor>(c => c.Usuario).ToList().Exists(c =>
            c.Email.ToLower().Equals(email.ToLower()) ||
            c.Usuario.Nombre.ToLower().Equals(nombreUsuario.ToLower()));
        }

        public async Task<UsuarioDto> RegistrarComercio(RegistroComercioDto comercio)
        {
            try
            {
                using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

                var rol = _repository.GetWhere<Rol>(r =>
                r.Nombre.Equals(Enum.GetName(ERoles.Comerciante))).Result.FirstOrDefault();

                var usuarioComercio = _mapper.MapDtoUsuarioRegistroComercio(comercio, rol.Id);

                var domicilioComercio = _mapper.MapDtoDomicilioRegistroComercio(comercio);

                usuarioComercio.Password = EncriptarPassword.EncryptPassSha25(usuarioComercio.Password);

                await _repository.Save(usuarioComercio);

                var nuevoComercio = (Comercio)comercio;

                nuevoComercio.UsuarioId = usuarioComercio.Id;

                nuevoComercio.Usuario = usuarioComercio;

                var coordenadas = _datosGeograficosService.GetCoordenadas(domicilioComercio);

                domicilioComercio.Latitud = coordenadas.Result.lat;

                domicilioComercio.Longitud = coordenadas.Result.lon;

                await _repository.Save(domicilioComercio);

                nuevoComercio.DomicilioId = domicilioComercio.Id;

                nuevoComercio.Domicilio = domicilioComercio;

                await _repository.Save(nuevoComercio);

                scope.Complete();
            }
            catch (TransactionAbortedException ex)
            {
                throw new Exception(ex.Message);
            }

            return (UsuarioDto)comercio;
        }

        public async Task<UsuarioDto> RegistrarConsumidor(RegistroConsumidorDto consumidor)
        {
            try
            {
                using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

                var rol = _repository.GetWhere<Rol>(r =>
                    r.Nombre.Equals(Enum.GetName(ERoles.Consumidor))).Result.FirstOrDefault();

                var nuevoUsuario = _mapper.MapDtoUsuarioRegistroConsumidor(consumidor, rol.Id);

                nuevoUsuario.Password = EncriptarPassword.EncryptPassSha25(nuevoUsuario.Password);

                await _repository.Save(nuevoUsuario);

                var nuevoConsumidor = (Consumidor)consumidor;

                nuevoConsumidor.UsuarioId = nuevoUsuario.Id;

                nuevoConsumidor.Usuario = nuevoUsuario;

                await _repository.Save(nuevoConsumidor);

                scope.Complete();

            }
            catch (TransactionAbortedException ex)
            {
                throw new Exception(ex.Message);
            }

            return (UsuarioDto)consumidor;
        }

        public async Task<string> LoginComercio(UsuarioLoginDto usuarioDto)
        {
            var usuario = _repository.GetQuery<Usuario>().Where(u =>
            u.Email.ToLower().Equals(usuarioDto.NombreUsuario.ToLower()) ||
            u.Nombre.ToLower().Equals(usuarioDto.NombreUsuario.ToLower())).FirstOrDefault();

            if (usuario is not null)
            {
                var rol = await _repository.GetById<Rol>(usuario.RolId);

                if (rol.Nombre != Enum.GetName(ERoles.Comerciante)) return null;

                var comercio = _repository.GetQuery<Comercio>()
                    .Where(c => c.UsuarioId.Equals(usuario.Id)).FirstOrDefault();

                var tokenParameter = new TokenParameter
                {
                    Id = usuario.Id,
                    Email = usuario.Email,
                    Usuario = usuario.Nombre,
                    Password = usuario.Password,
                    Rol = rol.Nombre,
                    CID = comercio.Id
                };

                if (!EncriptarPassword.EncryptPassSha25(usuarioDto.Password).Equals(usuario.Password))
                {
                    return Constantes.PasswordIncorrecto;
                }

                else return _tokenHandler.GenerateTokenJWT(tokenParameter);
            }

            return null;
        }

        public async Task<string> LoginConsumidor(UsuarioLoginDto usuarioDto)
        {
            var usuario = _repository.GetQuery<Usuario>().Where(u =>
            u.Email.ToLower().Equals(usuarioDto.NombreUsuario.ToLower()) ||
            u.Nombre.ToLower().Equals(usuarioDto.NombreUsuario.ToLower())).FirstOrDefault();

            if (usuario is not null)
            {
                var rol = await _repository.GetById<Rol>(usuario.RolId);

                if (rol.Nombre != Enum.GetName(ERoles.Consumidor)) return null;

                var consumidor = _repository.GetQuery<Consumidor>()
                    .Where(c => c.UsuarioId.Equals(usuario.Id)).FirstOrDefault();

                var tokenParameter = new TokenParameter
                {
                    Id = usuario.Id,
                    Email = usuario.Email,
                    Usuario = usuario.Nombre,
                    Password = usuario.Password,
                    Rol = rol.Nombre,
                    CID = consumidor.Id
                };

                if (!EncriptarPassword.EncryptPassSha25(usuarioDto.Password).Equals(usuario.Password))
                {
                    return Constantes.PasswordIncorrecto;
                }

                else return _tokenHandler.GenerateTokenJWT(tokenParameter);
            }

            return null;
        }

        public async Task<UsuarioComercioDto> GetPerfilComercio(int idUsuario, int idComercio)
        {
            var comercio = _repository.GetQuery<Comercio>
                (p => p.Usuario).Where(p => p.UsuarioId.Equals(idUsuario) && p.Id.Equals(idComercio)).FirstOrDefault();
            if (comercio is not null)
            {
                var domicilio = _repository.GetQuery<Domicilio>().Where(d => d.Id.Equals(comercio.DomicilioId)).FirstOrDefault();
                var usuarioComercioDto = new UsuarioComercioDto
                {
                    NombreDeUsuario = comercio.Usuario?.Nombre,
                    Email = comercio.Email,
                    Nombre = comercio.Nombre,
                    RazonSocial = comercio.RazonSocial,
                    Cuit = comercio.Cuit,
                    NombreContacto = comercio.NombreContacto,
                    Telefono = comercio.Telefono,
                    Domicilio = new DomicilioComercioDto
                    {
                        Altura = domicilio.Altura,
                        Calle = domicilio.Calle,
                        Localidad = domicilio.Localidad,
                        Municipio = domicilio.Municipio,
                        Provincia = domicilio.Provincia
                    }
                };

                return usuarioComercioDto;
            }

            return null;
        }
        public async Task<UsuarioConsumidorDto> GetPerfilConsumidor(int idUsuario, int idConsumidor)
        {
            var consumidor = _repository.GetQuery<Consumidor>
                (p => p.Usuario).Where(p => p.UsuarioId.Equals(idUsuario) && p.Id.Equals(idConsumidor)).FirstOrDefault();

            if (consumidor is not null)
            {
                var usuarioConsumidorDto = new UsuarioConsumidorDto
                {
                    NombreUsuario = consumidor.Usuario?.Nombre,
                    Email = consumidor.Usuario.Email,
                    Nombre = consumidor.Nombre,
                    Apellido = consumidor.Apellido,
                    Telefono = consumidor.Telefono
                };
                return usuarioConsumidorDto;
            }
            return null;
        }

        public async Task<UsuarioComercioDto> EditarPerfilComercio(UsuarioComercioDto comercioDto, int idUsuario, int idComercio)
        {
            var comercio = _repository.GetQuery<Comercio>(p => p.Domicilio).Where(c => c.Id.Equals(idComercio)).FirstOrDefault();
            var usuario = await _repository.GetById<Usuario>(idUsuario);


            if (comercio is not null && usuario is not null)
            {
                //var domicilio = await _repository.GetById<Domicilio>(comercio.DomicilioId);

                comercio.Nombre = comercioDto.Nombre;
                comercio.RazonSocial = comercioDto.RazonSocial;
                comercio.Cuit = comercioDto.Cuit;
                comercio.Email = comercio.Email;
                comercio.NombreContacto = comercioDto.NombreContacto;
                comercio.Telefono = comercioDto.Telefono;
                usuario.Nombre = comercioDto.NombreDeUsuario;

                try
                {
                    var domicilioComercio = _mapper.MapDtoDomicilioEditarComercio(comercioDto);
                    var coordenadas = _datosGeograficosService.GetCoordenadas(domicilioComercio);
                    comercio.Domicilio.Calle = comercioDto.Domicilio.Calle;
                    comercio.Domicilio.Altura = comercioDto.Domicilio.Altura;
                    comercio.Domicilio.Localidad = comercioDto.Domicilio.Localidad;
                    comercio.Domicilio.Municipio = comercioDto.Domicilio.Municipio;
                    comercio.Domicilio.Provincia = comercioDto.Domicilio.Provincia;
                    comercio.Domicilio.Latitud = coordenadas.Result.lat;
                    comercio.Domicilio.Longitud = coordenadas.Result.lon;

                }
                catch (TransactionAbortedException ex)
                {
                    throw new Exception(ex.Message);
                }

                await _repository.Update(comercio);
                await _repository.Update(usuario);
                return comercioDto;
            }

            return null;

        }

        public async Task<UsuarioConsumidorDto> EditarPerfilConsumidor(UsuarioConsumidorDto consumidorDto, int idUsuario, int idConsumidor)
        {
            var consumidor = await _repository.GetById<Consumidor>(idConsumidor);
            var usuario = await _repository.GetById<Usuario>(idUsuario);

            if (consumidor is not null && usuario is not null)
            {
                consumidor.Nombre = consumidorDto.Nombre;
                consumidor.Apellido = consumidorDto.Apellido;
                consumidor.Email = consumidorDto.Email;
                consumidor.Telefono = consumidorDto.Telefono;
                usuario.Nombre = consumidorDto.NombreUsuario;

                await _repository.Update(consumidor);
                await _repository.Update(usuario);
                return consumidorDto;
            }

            return null;
        }

        public async Task<bool?> DeleteComercio(int idUsuario, int idComercio)
        {
            var comercio = _repository.GetQuery<Comercio>
                (p => p.Usuario).Where(p => p.UsuarioId.Equals(idUsuario) && p.Id.Equals(idComercio)).FirstOrDefault();
            var usuario = await _repository.GetById<Usuario>(idUsuario);

            if (comercio is not null)
            {
                await _repository.Remove(comercio);
                await _repository.Remove(usuario);

                return true;
            }
            return null;

        }
        public async Task<bool?> DeleteConsumidor(int idUsuario, int idConsumidor)
        {
            var consumidor = _repository.GetQuery<Consumidor>
                (p => p.Usuario).Where(p => p.UsuarioId.Equals(idUsuario) && p.Id.Equals(idConsumidor)).FirstOrDefault();
            var usuario = await _repository.GetById<Usuario>(idUsuario);

            if (consumidor is not null)
            {
                await _repository.Remove(consumidor);
                await _repository.Remove(usuario);

                return true;
            }
            return null;
        }
    }
}
