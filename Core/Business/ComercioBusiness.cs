using Abstractions;
using AutoMapper;
using Core.Business.Interfaces;
using Core.Helper;
using Core.Models.DTOs;
using Entities;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Business
{
    public class ComercioBusiness : IComercioBusiness
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public ComercioBusiness(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> EsOwner(int usuarioId, int comercioId)
        {
            var comercio = await _repository.GetById<Comercio>(comercioId);
            if (comercio is null) throw new NullReferenceException(Constantes.ComercioInexistente);
            return comercio.UsuarioId.Equals(usuarioId);
        }

        public IEnumerable<ProductoDtoGetAllResponse> GetProductosPorComercioPaginado(int comercioId, IPaginationFilter filter, string nombre, string orderBy)
        {
            var productosComercio = new List<ProductoDtoGetAllResponse>();

            if (_repository.ExistsEntity<Comercio>(c => c.Id.Equals(comercioId)))
            {
                var productos = _repository.GetQueryPaginado<Producto>(p =>
                p.Activo.Equals(true) && p.ComercioId.Equals(comercioId), filter, p => p.UnidadMedida);

                if(!string.IsNullOrEmpty(nombre))
                {
                    productos = productos.Where(p => p.Nombre.ToLower().Contains(nombre.ToLower()));
                }

                foreach (var producto in productos)
                {
                    var productoResponse = _mapper.Map<ProductoDtoGetAllResponse>(producto);

                    productoResponse.UnidadDeMedida = producto.UnidadMedida?.Abreviatura;

                    productosComercio.Add(productoResponse);
                }

                if (orderBy is not null)
                {
                    switch (orderBy)
                    {
                        case "precio":
                            productosComercio = productosComercio.OrderBy(p => p.Precio).ToList();
                            break;
                        case "nombre":
                            productosComercio = productosComercio.OrderBy(p => p.Nombre).ToList();
                            break;
                    }
                }
            }

            return productosComercio;
        }

        public ProductoDtoGetAllResponse GetProductoPorIdPorComercio(int comercioId, int productoId)
        {
            var producto = _repository.FindFirst<Producto>(p => 
            p.Id.Equals(productoId) && p.ComercioId.Equals(comercioId) && p.Activo.Equals(true),
            p => p.UnidadMedida);

            if (producto is not null)
            {
                var response = _mapper.Map<ProductoDtoGetAllResponse>(producto);

                response.UnidadDeMedida = producto.UnidadMedida.Abreviatura;

                return response;
            }
            return null;
        }


        public async Task<ProductoDtoAlta> AltaProducto(int comercioId, ProductoDtoAlta productoDto)
        {
            var producto = _mapper.Map<Producto>(productoDto);
            
            var comercio = _repository.FindFirst<Comercio>(c => c.Id.Equals(comercioId));
            
            producto.Activo = true;

            producto.Comercio = comercio;

            producto.ComercioId = comercio.Id;

            await _repository.Save(producto);

            comercio.Productos.Add(producto);

            await _repository.Update(comercio);

            return productoDto;

        }

        public async Task<ProductoDtoAlta> EditarProducto(int comercioId, int productoId, ProductoDtoAlta productoDto)
        {
            var producto = _repository.FindFirst<Producto>(p =>
            p.Id.Equals(productoId) && p.ComercioId.Equals(comercioId) && p.Activo.Equals(true),
            p => p.UnidadMedida);

            if (producto is not null)
            {
                producto.Nombre = productoDto.Nombre;
                producto.Marca = productoDto.Marca;
                producto.UnidadMedidaId = productoDto.UnidadMedidaId;
                producto.Detalle = productoDto.Detalle;
                producto.Precio = productoDto.Precio;
                producto.Cantidad = productoDto.Cantidad;
                producto.Imagen = productoDto.Imagen;

                await _repository.Update(producto);

                return productoDto;
            }

            return null;
        }

        public async Task<bool?> DeleteProducto(int comercioId, int productoId)
        {
            var producto = _repository.FindFirst<Producto>(p =>
            p.Id.Equals(productoId) && p.ComercioId.Equals(comercioId) && p.Activo.Equals(true),
            p => p.UnidadMedida);

            if (producto is not null)
            {
                await _repository.Delete<Producto>(producto.Id);
                return producto.Activo;
            }

            return null;
        }

        public async Task<int> CountProductosPorComercio(int comercioId, string nombre)
        {
            var total = await _repository.CountWhere<Producto>(p =>
            p.ComercioId.Equals(comercioId));

            if (!string.IsNullOrEmpty(nombre))
            {
                total = await _repository.CountWhere<Producto>(p =>
                p.Nombre.Contains(nombre) && p.ComercioId.Equals(comercioId));
            }

            return total;
        }


    }
}
