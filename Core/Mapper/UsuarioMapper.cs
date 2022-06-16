using Core.Models;
using Core.Models.DTOs;
using Core.Models.DTOs.UsuarioDTO;
using Core.Models.DTOs.UsuarioDTO.Comercio;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Mapper
{
    public interface IUsuarioMapper
    {

        Usuario MapDtoUsuarioRegistroComercio(RegistroComercioDto comercio, int rolId);
        Domicilio MapDtoDomicilioRegistroComercio(RegistroComercioDto comercio);

        Usuario MapDtoUsuarioRegistroConsumidor(RegistroConsumidorDto user, int rolId);

        Domicilio MapDtoDomicilioEditarComercio(UsuarioComercioDto comercio);

    }

    public class UsuarioMapper : IUsuarioMapper
    {
        public Usuario MapDtoUsuarioRegistroComercio (RegistroComercioDto comercio, int rolId)
        {
            return new Usuario
            {
                Nombre = comercio.NombreUsuario,
                Email = comercio.Email,
                Password = comercio.Password,
                RolId = rolId,
                Activo = true
            };
        }

        public Domicilio MapDtoDomicilioRegistroComercio(RegistroComercioDto comercio)
        {
            return new Domicilio
            {
                Activo = true,
                Provincia = comercio.Domicilio.Provincia,
                Municipio = comercio.Domicilio.Municipio,
                Localidad = comercio.Domicilio.Localidad,
                Calle = comercio.Domicilio.Calle,
                Altura = comercio.Domicilio.Altura
            };              
        }

        public Domicilio MapDtoDomicilioEditarComercio(UsuarioComercioDto comercio)
        {
            return new Domicilio
            {
                Activo = true,
                Provincia = comercio.Domicilio.Provincia,
                Municipio = comercio.Domicilio.Municipio,
                Localidad = comercio.Domicilio.Localidad,
                Calle = comercio.Domicilio.Calle,
                Altura = comercio.Domicilio.Altura
            };
        }

        public Usuario MapDtoUsuarioRegistroConsumidor (RegistroConsumidorDto consumidor, int rolId)
        {
            return new Usuario
            {
                Nombre = consumidor.NombreUsuario,
                Email = consumidor.Email,
                Password = consumidor.Password,
                RolId = rolId,
                Activo = true
            };
        }







    }
}
