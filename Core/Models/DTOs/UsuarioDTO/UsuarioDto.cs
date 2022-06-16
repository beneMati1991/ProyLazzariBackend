using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.DTOs.UsuarioDTO
{
    public class UsuarioDto
    {
        public string Nombre { get; set; }

        public string Usuario { get; set; }

        public string Email { get; set; }


        public static explicit operator UsuarioDto (RegistroComercioDto dtoComercio)
        {
            return new UsuarioDto
            {
                Nombre = dtoComercio.Nombre,
                Usuario = dtoComercio.NombreUsuario,
                Email = dtoComercio.Email
            };

        }

        public static explicit operator UsuarioDto (RegistroConsumidorDto dtoConsumidor)
        {
            return new UsuarioDto
            {
                Nombre = dtoConsumidor.Nombre,
                Usuario = dtoConsumidor.NombreUsuario,
                Email = dtoConsumidor.Email
            };
        }
    }
}
