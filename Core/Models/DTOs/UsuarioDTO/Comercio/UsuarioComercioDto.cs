using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.DTOs.UsuarioDTO.Comercio
{
    public class UsuarioComercioDto
    {
        public string NombreDeUsuario{get; set; }
        public string Email { get; set; }
        public string Nombre { get; set; }
        public string RazonSocial { get; set; }
        public string Cuit { get; set; }
        public string NombreContacto { get; set; }
        public string Telefono { get; set; }

        public DomicilioComercioDto Domicilio { get; set; }

    }
}
