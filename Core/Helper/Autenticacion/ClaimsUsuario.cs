using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helper.Autenticacion
{
    public class ClaimsUsuario
    {
        public int UsuarioId { get; set; }
        public string Email { get; set; }
        public string Rol { get; set; } 
        public string NombreUsuario { get; set; }

        public int? CID { get; set; }

    }
}
