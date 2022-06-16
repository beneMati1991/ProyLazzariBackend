using Abstractions;
using Core.Models.DTOs;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Business.Interfaces
{
    public interface IComercioBusiness
    {
        Task<bool> EsOwner(int usuarioId, int comercioId);
        IEnumerable<ProductoDtoGetAllResponse> GetProductosPorComercioPaginado(int comercioId, IPaginationFilter filter, string nombre, string orderBy);
        Task<ProductoDtoAlta> AltaProducto(int comercioId, ProductoDtoAlta producto);
        Task<ProductoDtoAlta> EditarProducto(int comercioId, int idProducto, ProductoDtoAlta producto);
        Task<bool?> DeleteProducto(int comercioId, int id);
        ProductoDtoGetAllResponse GetProductoPorIdPorComercio(int comercioId, int productoId);
        Task<int> CountProductosPorComercio(int comercioId, string nombre);
        
    }
}
