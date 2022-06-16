using AutoMapper;
using Core.Business;
using Core.Business.Interfaces;
using Core.Mapper;
using Core.Models.DTOs;
using Entities;
using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    [TestClass]
    public class ProductoBusinessTest
    {
        private readonly Mock<IRepository> mockRepository = new Mock<IRepository>();
        private readonly MapperConfiguration mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApiMappings());
        });
        private readonly Mock<IValidator<Producto>> mockValidator = new Mock<IValidator<Producto>>();

        public List<Producto> productos = new List<Producto>()
            {
                new Producto
                {
                    Id = 1,
                    Nombre = "Producto 1",
                    Marca = "Marca 1",
                    UnidadMedidaId = 2,
                    Detalle = "Detalle prod 1",
                    Precio = (decimal)10.50,
                    Imagen = "Imagen 1",
                 //   SucursalId = 3,
                    Activo = true,
                    UnidadMedida = new UnidadDeMedida()
                },
                new Producto
                {
                    Id = 2,
                    Nombre = "Producto 2",
                    Marca = "Marca 2",
                    UnidadMedidaId = 1,
                    Detalle = "Detalle prod 2",
                    Precio = (decimal)22.50,
                    Imagen = "Imagen 2",
                  //  SucursalId = 8,
                    Activo = true,
                    UnidadMedida = new UnidadDeMedida()
                },
                new Producto
                {
                    Id = 3,
                    Nombre = "Producto 3",
                    Marca = "Marca 3",
                    UnidadMedidaId = 3,
                    Detalle = "Detalle prod 3",
                    Precio = (decimal)25.50,
                    Imagen = "Imagen 3",
                  //  SucursalId = 8,
                    Activo = true,
                    UnidadMedida = new UnidadDeMedida()
                }
            };

        [TestMethod]
        public void TestGetAllProductos_Empty()
        {
            Mock<IRepository> mockRepo = new Mock<IRepository>();
            var mapper = mockMapper.CreateMapper();

            var _business = new ProductoBusiness(mockRepo.Object,
                mapper, mockValidator.Object);

            var allProducts = _business.GetAllProductos();

            Assert.AreEqual(0,allProducts.Result.Count());
        }

        [TestMethod]
        public void TestGetAllProductos() 
        {
            mockRepository.Setup(m => m.GetQuery<Producto>(p => p.UnidadMedida)).Returns(productos.AsQueryable());
            var mapper = mockMapper.CreateMapper();

            var _business = new ProductoBusiness(mockRepository.Object,
                mapper, mockValidator.Object);

            var allProducts = _business.GetAllProductos();

            Assert.IsNotNull(allProducts.Result);
            Assert.AreEqual(productos.Count, allProducts.Result.Count());
        }

        [TestMethod]
        public void TestGetProductoPorId_Existe()
        {
            mockRepository.Setup(m => m.GetQuery<Producto>(p => p.UnidadMedida)).Returns(productos.AsQueryable());

            var mapper = mockMapper.CreateMapper();

            var _business = new ProductoBusiness(mockRepository.Object,
                mapper, mockValidator.Object);

            var producto = _business.GetProductoPorId(1);

            Assert.IsNotNull(producto.Result);
            Assert.AreEqual(1, producto.Result.Id);
        }

        [TestMethod]
        public void TestGetProductoPorId_NoExiste()
        {
            mockRepository.Setup(m => m.GetQuery<Producto>(p => p.UnidadMedida)).Returns(productos.AsQueryable());

            var mapper = mockMapper.CreateMapper();

            var _business = new ProductoBusiness(mockRepository.Object,
                mapper, mockValidator.Object);

            var producto = _business.GetProductoPorId(12);

            Assert.IsNull(producto.Result);
        }

        /*
        [TestMethod]
        public void TestGetProductoPorNombre_ExisteUno()
        {
            var testData = "Producto 1";
            mockRepository.Setup(m => m.GetQuery<Producto>(p => p.UnidadMedida)).Returns(productos.AsQueryable());
            var mapper = mockMapper.CreateMapper();

            var _business = new ProductoBusiness(mockRepository.Object,
                mapper, mockValidator.Object);
            var productosList = _business.GetProductosPorNombre(testData);

            Assert.IsNotNull(productosList.Result);
            Assert.AreEqual(1, productosList.Result.productos.Count);
            Assert.AreEqual(testData, productosList.Result.productos[0].Nombre);
        }
        */
        /*
        [TestMethod]
        public void TestGetProductoPorNombre_ExisteMuchos()
        {
            var testData = "Prod";
            mockRepository.Setup(m => m.GetQuery<Producto>(p => p.UnidadMedida)).Returns(productos.AsQueryable());
            var mapper = mockMapper.CreateMapper();

            var _business = new ProductoBusiness(mockRepository.Object,
                mapper, mockValidator.Object);
            var productosList = _business.GetProductosPorNombre(testData);

            Assert.IsNotNull(productosList.Result);
            Assert.IsTrue(productosList.Result.productos.Count > 1);
            foreach (var producto in productosList.Result.productos)
            {
                Assert.IsTrue(producto.Nombre.Contains(testData));
            }
        
        } */
    }
}
