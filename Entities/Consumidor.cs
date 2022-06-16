using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Entities
{
    public partial class Consumidor : EntityBase
    {
        public Consumidor()
        {
            ListasCompras = new HashSet<ListaCompra>();
        }

        [Required]
        [MinLength(2)]
        [MaxLength(255)]
        public string Nombre { get; set; }
        
        [Required]
        [MinLength(2)]
        [MaxLength(255)]
        public string Apellido { get; set; }
        
        [MaxLength(255)]
        public string Telefono { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(320)]
        public string Email { get; set; }

        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }

        public virtual Usuario Usuario { get; set; }

        public virtual ICollection<ListaCompra> ListasCompras { get; set; }
    }
}
