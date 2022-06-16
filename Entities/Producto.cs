using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Entities
{
    public partial class Producto : EntityBase
    {
        [Required]
        [MaxLength(255)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(255)]
        public string Marca { get; set; }

        [Required]
        public decimal Precio { get; set; }

        [MaxLength(255)]
        public string Detalle { get; set; }

        public int? Cantidad { get; set; }

        [MaxLength(320)]
        public string Imagen { get; set; }

        [ForeignKey("UnidadDeMedida")]
        public int UnidadMedidaId { get; set; }

        [ForeignKey("Comercio")]
        public int ComercioId { get; set; }

        public virtual Comercio Comercio { get; set; }
        public virtual UnidadDeMedida UnidadMedida { get; set; }

    }
}
