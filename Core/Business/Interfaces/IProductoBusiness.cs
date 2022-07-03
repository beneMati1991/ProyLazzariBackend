using Abstractions;
using Core.Models.DTOs;
using Core.Models.MejoresOpcionesVVM;
using Entities;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Business.Interfaces
{
    public interface IProductoBusiness
    {
        Task<IEnumerable<ProductoDtoGetAllResponse>> GetAllProductos(string nombre);
        IEnumerable<ProductoDtoGetAllResponse> GetAllProductosPaginado(string name, IPaginationFilter filter);
        Task<ProductoDtoGetAllResponse> GetProductoPorId(int productoId);
        Task<int> CountProductos(string nombre);
        Task<IEnumerable<ComercioMejorOpcionViewModel>> GetMejoresOpciones(MejoresOpcionesProductosRequest request, string orderby);

    }
}
