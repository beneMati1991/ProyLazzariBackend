using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.DTOs
{
    public class ProductoDtoGetAllResponse
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Marca { get; set; }
        public string UnidadDeMedida { get; set; }
        public string Detalle { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public string Imagen { get; set; }

        public static explicit operator ProductoDtoGetAllResponse(Producto producto) 
        {
            return new ProductoDtoGetAllResponse
            {
                Nombre = producto.Nombre,
                Marca = producto.Marca,
                UnidadDeMedida = producto.UnidadMedida.Abreviatura,
            };
        }
    }
}
