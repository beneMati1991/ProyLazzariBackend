using Abstractions;
using AutoMapper;
using Core.Business.Interfaces;
using Core.Helper;
using Core.Models.DTOs;
using Core.Models.MejoresOpcionesVVM;
using Entities;
using FluentValidation;
using FluentValidation.Results;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.Models.NormalizacionDatosGeofApi.DireccionNormalizada;

namespace Core.Business
{
    public class ProductoBusiness : IProductoBusiness
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;
        private readonly IValidator<Producto> _validatorProducto;

        //Inyección de dependencia.
        public ProductoBusiness(IRepository repository, IMapper mapper, IValidator<Producto> validatorProducto)
        {
            _repository = repository;
            _mapper = mapper;
            _validatorProducto = validatorProducto;
        }

        public async Task<IEnumerable<ProductoDtoGetAllResponse>> GetAllProductos()
        {
            var productos = _repository.GetQuery<Producto>(p => p.UnidadMedida).Where(p 
                =>p.Activo.Equals(true));

            var productosComercio = new List<ProductoDtoGetAllResponse>();

            foreach (var producto in productos)
            {
                var productoResponse = _mapper.Map<ProductoDtoGetAllResponse>(producto);

                productoResponse.UnidadDeMedida = producto.UnidadMedida?.Abreviatura;

                productosComercio.Add(productoResponse);
            }
            return productosComercio;
        }

      
        public IEnumerable<ProductoDtoGetAllResponse> GetAllProductosPaginado(string nombre, IPaginationFilter filter)
        {
            var response = new List<ProductoDtoGetAllResponse>();

            var productos = _repository.GetQueryPaginado<Producto>(p =>
            p.Activo.Equals(true), filter, p => p.UnidadMedida);

            if(!string.IsNullOrEmpty(nombre))
            {
                productos = _repository.GetQueryPaginado<Producto>(p =>
            p.Nombre.ToLower().Contains(nombre.ToLower()) && p.Activo.Equals(true),
            filter, p => p.UnidadMedida);
            }

            foreach (var producto in productos)
            {
                var productoResponse = _mapper.Map<ProductoDtoGetAllResponse>(producto);

                productoResponse.UnidadDeMedida = producto.UnidadMedida?.Abreviatura;

                response.Add(productoResponse);
            }
            return response.Distinct();
        }


        public async Task<ProductoDtoGetAllResponse> GetProductoPorId(int productoId)
        {
            var producto = await _repository.GetById<Producto>(productoId);
            
            if (producto is not null && producto.Activo.Equals(true)) 
            {
               var unidadMedida = await _repository.GetById<UnidadDeMedida>(producto.UnidadMedidaId);

                var productoResponse = _mapper.Map<ProductoDtoGetAllResponse>(producto);

                productoResponse.UnidadDeMedida = unidadMedida.Abreviatura;

                return productoResponse;
            }
            return null;
        }


        public async Task<int> CountProductos(string nombre)
        {
            var total = await _repository.Count<Producto>();

            if (!string.IsNullOrEmpty(nombre))
            {
                total = await _repository.CountWhere<Producto>(p =>
                p.Nombre.Contains(nombre));
            }

            return total;
        }

        /// <summary>
        /// Método que recibe una lista de productos view model y devuelve un listado de productos
        /// </summary>
        /// <param name="productos"></param>
        /// <returns></returns>
        private async Task<IEnumerable<Producto>> BuscarPorNombre(List<ProductoViewModel> productos) 
        {
            var productosResponse = new List<Producto>();

            foreach (var producto in productos)
            {
                var resultadoBusquedaProducto = await _repository.GetWhere<Producto>(p =>
                p.Nombre.ToLower().Contains(producto.Nombre.ToLower()));

                foreach (var resultado in resultadoBusquedaProducto)
                {
                    productosResponse.Add(resultado);
                }
            }

            return productosResponse;
        }

        private async Task<IEnumerable<Producto>> FiltrarProductosPorDistanciaComercio (List<ProductoViewModel> productosVM, Ubicacion ubicacionUsuario, double distanciaMaxima)
        {
            var productos = await BuscarPorNombre(productosVM);

            var productosResponse = new List<Producto>();

            foreach (var producto in productos)
            {
                var ubicacionComercio = new Ubicacion
                {
                    lat = producto.Comercio.Domicilio.Latitud,
                    lon = producto.Comercio.Domicilio.Longitud
                };

                var distanciaComercio = Calcular.DistanciaEntreDosPuntosEnKm(ubicacionUsuario, ubicacionComercio);

                if (distanciaComercio <= distanciaMaxima)
                {
                    productosResponse.Add(producto);
                }
            }

            return productosResponse;
        }

        public async Task<MejoresOpcionesResponse> GetMejoresOpciones(MejoresOpcionesProductosRequest request)
        {
            /*
            MejoresOpcionesResponse response = new MejoresOpcionesResponse();

            var comercios = new List<Comercio>();

            var productos = await FiltrarProductosPorDistanciaComercio(request.Productos, request.UbicacionUsuario, request.DistanciaMaxima);

            foreach(var producto in productos)
            {
                comercios.Add(producto.Comercio);
            }


            foreach(var comercio in comercios)
            {
                var productosComercio = productos.Where(p => p.ComercioId.Equals(comercio.Id))
                    .Select(p => new ProductoViewModel
                    {
                        Id = p.Id,
                        Nombre = p.Nombre,
                        Presentacion = p.UnidadMedida.Abreviatura,
                        Detalle = p.Detalle
                    });

                var comercioResponse = new ComercioMejorOpcionViewModel
                {
                    Productos = comercio.Productos.w
                }
                response.Comercios.Add
            } */

            throw new NotImplementedException();

        }


        

    }
}
