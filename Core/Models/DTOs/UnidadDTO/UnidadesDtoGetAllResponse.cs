using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.DTOs.UnidadDTO
{
    public class UnidadesDtoGetAllResponse
    {
        public int Id { get; set; }
        public string Unidad { get; set; }
        public string Abreviatura { get; set; }
    }
}
