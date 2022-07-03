using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.DTOs
{
    public class ProductoDtoAlta
    {
        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Marca { get; set; }

        [Required]
        public int UnidadMedidaId { get; set; }

        [Required]
        public string Detalle { get; set; }

        [Required]
        public decimal Precio { get; set; }

        public int? Cantidad { get; set; }

        public string Imagen { get; set; }



    }
}
