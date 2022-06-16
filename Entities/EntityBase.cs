using Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class EntityBase : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public bool? Activo { get; set; }
    }
}
