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

        public async Task<IEnumerable<ProductoDtoGetAllResponse>> GetAllProductos(string nombre)
        {
            var response = new List<ProductoDtoGetAllResponse>();

            var productos = await _repository.GetWhere<Producto>(p =>
                 p.Nombre.ToLower().Contains(nombre.ToLower()) ||
                 p.Marca.ToLower().Contains(nombre.ToLower()) ||
                 p.Detalle.ToLower().Contains(nombre.ToLower()));

            foreach (var producto in productos)
            {
                var unidadMedida = await _repository.GetById<UnidadDeMedida>(producto.UnidadMedidaId);

                var productoResponse = _mapper.Map<ProductoDtoGetAllResponse>(producto);

                productoResponse.UnidadDeMedida = unidadMedida?.Abreviatura;

                response.Add(productoResponse);
            }

            return response.GroupBy(x => new { x.Nombre, x.Marca }).Select(x => x.First());
        }


        public IEnumerable<ProductoDtoGetAllResponse> GetAllProductosPaginado(string nombre, IPaginationFilter filter)
        {
            var response = new List<ProductoDtoGetAllResponse>();

            var productos = _repository.GetQuery<Producto>(p =>
               p.Nombre.ToLower().Contains(nombre.ToLower()) ||
               p.Marca.ToLower().Contains(nombre.ToLower()) ||
               p.Detalle.ToLower().Contains(nombre.ToLower()));



            /*
                       if (!string.IsNullOrEmpty(nombre))
                       {
                           productos = _repository.GetQuery<Producto>(p =>
                           p.Nombre.ToLower().Contains(nombre.ToLower()) ||
                           p.Marca.ToLower().Contains(nombre.ToLower()) ||
                           p.Detalle.ToLower().Contains(nombre.ToLower())
                           && p.Activo.Equals(true), p => p.UnidadMedida);
                       }
                      if(filter is not null)
                       {
                           productos = _repository.GetQueryPaginado<Producto>(p =>
                           p.Nombre.ToLower().Contains(nombre.ToLower()) ||
                           p.Marca.ToLower().Contains(nombre.ToLower()) ||
                           p.Detalle.ToLower().Contains(nombre.ToLower())
                           && p.Activo.Equals(true),
                           filter, p => p.UnidadMedida);
                       }*/

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
        /// Método para calcular la distancia de un comercio desde la ubicación del usuario
        /// </summary>
        /// <param name="comercio"></param>
        /// <param name="ubicacionUsuario"></param>
        /// <returns></returns>
        private double CalcularDistanciaComercio(Domicilio domicilioComercio, Ubicacion ubicacionUsuario)
        {
            var ubicacionComercio = new Ubicacion
            {
                lat = domicilioComercio.Latitud,
                lon = domicilioComercio.Longitud
            };

            return Calcular.DistanciaEntreDosPuntosEnKm(ubicacionUsuario, ubicacionComercio);
        }

        /// <summary>
        /// Método que recibe una lista de Id de productos y devuelve un listado de productos
        /// </summary>
        /// <param name="productos"></param>
        /// <returns></returns>
        private async Task<IEnumerable<Producto>> BuscarProductos(List<int> productosId)
        {
            var productosResponse = new List<Producto>();

            foreach (var id in productosId)
            {
                var producto = await _repository.GetById<Producto>(id);

                if (producto is not null)
                {
                    var resultadoBusquedaProducto = await _repository.GetWhere<Producto>(x =>
                    x.Nombre.ToLower().Contains(producto.Nombre.ToLower()));

                    resultadoBusquedaProducto = resultadoBusquedaProducto.Where(x => x.Marca.ToLower()
                    .Contains(producto.Marca.ToLower()));

                    foreach (var resultado in resultadoBusquedaProducto)
                    {
                        var unidadMedida = await _repository.GetById<UnidadDeMedida>(resultado.UnidadMedidaId);
                        resultado.UnidadMedida = unidadMedida;

                        productosResponse.Add(resultado);
                    }
                }

            }

            return productosResponse;
        }

        /// <summary>
        /// Método que devuelve los comercios dentro de la distancia máxima establecida
        /// </summary>
        /// <param name="ubicacionUsuario"></param>
        /// <param name="distanciaMaxima"></param>
        /// <returns></returns>
        private async Task<IEnumerable<Comercio>> FiltrarComercioPorDistancia(Ubicacion ubicacionUsuario, double distanciaMaxima)
        {
            var comercios = await _repository.GetAll<Comercio>();

            var comerciosResponse = new List<Comercio>();

            foreach (var comercio in comercios)
            {
                var domicilioComercio = await _repository.GetById<Domicilio>(comercio.DomicilioId);

                var distanciaComercio = CalcularDistanciaComercio(domicilioComercio, ubicacionUsuario);

                if (distanciaComercio <= distanciaMaxima)
                {
                    comerciosResponse.Add(comercio);
                }
            }
            return comerciosResponse;
        }

        /// <summary>
        /// Servicio que devuelve las mejores opciones de carrito ordenado de menor a mayor según distancia máxima de búsqueda
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ComercioMejorOpcionViewModel>> GetMejoresOpciones(MejoresOpcionesProductosRequest request, string orderby)
        {

            var response = new List<ComercioMejorOpcionViewModel>();

            var productos = await BuscarProductos(request.ProductosId);

            var comercios = await FiltrarComercioPorDistancia(request.UbicacionUsuario, request.DistanciaMaxima);

            foreach (var comercio in comercios)
            {
                var productosComercio = productos.Where(p => p.ComercioId.Equals(comercio.Id))
                    .Select(p => new ProductoViewModel
                    {
                        Id = p.Id,
                        Nombre = p.Nombre,
                        Presentacion = p.UnidadMedida.Abreviatura,
                        Detalle = p.Detalle,
                        Precio = Convert.ToDouble(p.Precio)
                    }).ToList();

                if (productosComercio.Count > request.ProductosId.Count)
                {
                    productosComercio = productosComercio.GroupBy(x => x.Nombre).Select(x => x.First()).ToList();
                }

                var precioTotal = productosComercio.Select(x => x.Precio).Sum();

                var domicilioComercio = await _repository.GetById<Domicilio>(comercio.DomicilioId);

                var distancia = CalcularDistanciaComercio(domicilioComercio, request.UbicacionUsuario);

                var comercioResponse = new ComercioMejorOpcionViewModel
                {
                    Productos = productosComercio.ToList(),
                    Nombre = comercio.Nombre,
                    Distancia = distancia,
                    Calle = comercio.Domicilio.Calle,
                    Altura = comercio.Domicilio.Altura,
                    PrecioTotal = Convert.ToDouble(precioTotal),
                    CarritoCompleto = true
                };

                if (productosComercio.Count < request.ProductosId.Count)
                {
                    comercioResponse.CarritoCompleto = false;
                }

                if (comercioResponse.Productos.Any())
                {
                    response.Add(comercioResponse);
                }

            }

            if (orderby != null)
            {
                switch (orderby)
                {
                    case "precio":
                        response = response.OrderBy(x => x.PrecioTotal).ToList();
                        break;
                    case "distancia":
                        response = response.OrderBy(x => x.Distancia).ToList();
                        break;
                    case "carrito":
                        response = response.Where(x => x.CarritoCompleto).ToList();
                        break;
                }
            }

            if(orderby is null)
            {
                response = response.OrderBy(x => x.PrecioTotal).ToList();
            }

            return response;
        }




    }
}
