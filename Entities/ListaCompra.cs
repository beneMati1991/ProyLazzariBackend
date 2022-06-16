using Abstractions;
using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Entities
{
    [NotMapped]
    public partial class ListaCompra : EntityBase
    {
        public ListaCompra()
        {
            Productos = new HashSet<Producto>();
        }

        [ForeignKey("Consumidor")]
        public int ConsumidorId { get; set; }

        [Required]
        public int ProductoId { get; set; }

        public int? Cantidad { get; set; }

        public virtual Consumidor Consumidor { get; set; }
        public virtual ICollection<Producto> Productos { get; set; }
    }
}
