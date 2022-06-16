using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.DTOs.UsuarioDTO.Comercio
{
    public class DomicilioComercioDto
    {
        [Required]
        [MaxLength(255)]
        public string Provincia { get; set; }

        [Required]
        [MaxLength(255)]
        public string Municipio { get; set; }

        [Required]
        [MaxLength(255)]
        public string Localidad { get; set; }

        [Required]
        [MaxLength(255)]
        public string Calle { get; set; }

        [Required]
        public long Altura { get; set; }

    }
}
