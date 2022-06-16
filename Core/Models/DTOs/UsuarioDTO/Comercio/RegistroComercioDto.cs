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
    public class RegistroComercioDto
    {
        [Required]
        [MaxLength(255)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(255)]
        public string RazonSocial { get; set; }

        [Required]
        [MaxLength(14)]
        [RegularExpression(@"\b(20|23|24|27|30|33|34)(\-)[0-9]{8}(\-)[0-9]",
        ErrorMessage = "Formato de CUIT inválido")]
        public string Cuit { get; set; }

        [Required]
        public DomicilioComercioDto Domicilio { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(320)]
        public string Email { get; set; }

        [Required]
        [MaxLength(255)]
        public string NombreContacto { get; set; }

        [Required]
        [MaxLength(255)]
        public string Telefono { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(255)]
        public string NombreUsuario { get; set; }

        [Required]
        [MinLength(8)]
        [MaxLength(100)]
        public string Password { get; set; }

        public static explicit operator Comercio(RegistroComercioDto dto)
        {
            return new Comercio
            {
                Nombre = dto.Nombre,
                RazonSocial = dto.RazonSocial,
                Cuit = dto.Cuit,
                Email = dto.Email,
                NombreContacto = dto.NombreContacto,
                Telefono = dto.Telefono,
                Activo = true
            };
        }

    }
}
