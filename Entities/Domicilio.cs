using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public partial class Domicilio : EntityBase
    {

        [Required]
        [MaxLength(255)]
        public string Calle { get; set; }

        [Required]
        public long Altura { get; set; }

        [Required]
        [MaxLength(255)]
        public string Localidad { get; set; }

        [Required]
        [MaxLength(255)]
        public string Municipio { get; set; }

        [Required]
        [MaxLength(255)]
        public string Provincia { get; set; }

        [Required]
        public double Latitud {get;set;}

        [Required]
        public double Longitud { get; set; }


    }
}
