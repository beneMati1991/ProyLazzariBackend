using Core.Models.DTOs.UsuarioDTO.Comercio;
using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.DTOs
{
    public class RegistroConsumidorDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(255)]
        public string Nombre { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(255)]
        public string Apellido { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(320)]
        public string Email { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(255)]
        public string NombreUsuario { get; set; }

        [Required]
        [MinLength(8)]
        [MaxLength(100)]
        public string Password { get; set; }

        public static explicit operator Consumidor (RegistroConsumidorDto dto)
        {
            return new Consumidor
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Telefono = "",
                Email = dto.Email,
                Activo = true,
                Usuario = new Usuario(),
                ListasCompras = new List<ListaCompra>()
            };
        }
             
    }
}
