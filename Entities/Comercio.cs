using Abstractions;
using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Entities
{
    public partial class Comercio : EntityBase
    {
        public Comercio()
        {
            Productos = new HashSet<Producto>();
        }

        [Required]
        [MaxLength(255)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(255)]
        public string RazonSocial { get; set; }

        [Required]
        [MaxLength(14)]
        public string Cuit { get; set; }

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

        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }

        [ForeignKey("Domicilio")]
        public int DomicilioId { get; set; }

        public virtual Usuario Usuario { get; set; }
        public virtual Domicilio Domicilio { get; set; }
        public virtual ICollection<Producto> Productos { get; set; }
    }
}
