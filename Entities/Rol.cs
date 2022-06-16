using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Rol : EntityBase
    {
        [Required]
        [MaxLength(255)]
        public string Nombre { get; set; }

        [MaxLength(255)]
        public string Descripción { get; set; }
    }
}
