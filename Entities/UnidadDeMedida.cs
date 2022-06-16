using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Entities
{
    public partial class UnidadDeMedida : EntityBase
    {
        public UnidadDeMedida()
        {
            Productos = new HashSet<Producto>();
        }

        [Required]
        [MaxLength(255)]
        public string Unidad { get; set; }

        [Required]
        [MaxLength(6)]
        public string Abreviatura { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
