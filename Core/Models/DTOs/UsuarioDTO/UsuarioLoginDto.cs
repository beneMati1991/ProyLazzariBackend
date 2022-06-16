using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.DTOs
{
    public class UsuarioLoginDto
    {
		[Required]
		[MaxLength(320)]
		public string NombreUsuario { get; set; }

		[Required]
		[MaxLength(20)]
		public string Password { get; set; }
	}
}
