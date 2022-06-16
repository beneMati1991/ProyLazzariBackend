using System;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DataAccess
{
    public partial class ApiDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<Comercio> Comercio { get; set; }
        public DbSet<Consumidor> Consumidor { get; set; }
        public DbSet<ListaCompra> ListaCompras { get; set; }
        public DbSet<Producto> Producto { get; set; }
        public DbSet<Rol> Rol { get; set; }
        public DbSet<UnidadDeMedida> UnidadDeMedida { get; set; }
        public DbSet<Usuario> Usuario { get; set; }

        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SeedRoles(modelBuilder);
            SeedUsuarios(modelBuilder);
            SeedComercios(modelBuilder);
            SeedConsumidores(modelBuilder);
            SeedUnidadesDeMedida(modelBuilder);
            SeedProductos(modelBuilder);
            SeedDomicilios(modelBuilder);
        }

        private void SeedComercios(ModelBuilder modelBuilder)
        {
            for (int i = 1; i <= 3; i++)
            {
                modelBuilder.Entity<Comercio>().HasData(new Comercio()
                {
                    Id = i,
                    Nombre = "Comercio Test" + " " + i.ToString(),
                    RazonSocial = "Razon Social Test" + " " + i.ToString(),
                    Cuit = "20-00000000-"+i.ToString(),
                    Email = "comerciotest"+i.ToString()+"@test"+i.ToString()+".com",
                    NombreContacto = "Contacto Test"+i.ToString(),
                    Telefono = "03-03-456"+i.ToString(),
                    UsuarioId = i+3,
                    DomicilioId = i,
                    Activo = true
                });
            }
        }

        private void SeedConsumidores(ModelBuilder modelBuilder)
        {
            for (int i = 1; i <= 2; i++)
            {
                modelBuilder.Entity<Consumidor>().HasData(new Consumidor()
                {
                    Id = i,
                    Nombre = "Consumidor Nombre Test" + " " + i.ToString(),
                    Apellido = "Consumidor Apellido Test" + " " + i.ToString(),
                    Telefono = "03-03-456" + i.ToString(),
                    Email = "consumidortest" + i.ToString() + "@test" + i.ToString() + ".com",
                    UsuarioId = i+1,
                    Activo = true
                });
            }
        }

        private void SeedProductos(ModelBuilder modelBuilder)
        {
            for (int i = 1; i <= 10; i++)
            {
                var comercioId = 1;

                modelBuilder.Entity<Producto>().HasData(new Producto()
                {
                    Id = i,
                    Nombre = "Producto Test" + " " + i.ToString(),
                    Marca = "Marca Test" + " " + i.ToString(),
                    Precio = (decimal)i * i,
                    Detalle = "Detalle Test" + " " + i.ToString(),
                    Imagen = "Imagen Test" + " " + i.ToString(),
                    UnidadMedidaId = i,
                    ComercioId = comercioId,
                    Activo = true
                });
                if (i < 4)
                {
                    comercioId++;
                }
                else if (i > 4 && i < 10)
                {
                    comercioId--;
                }
                else if (comercioId < 1)
                {
                    comercioId = 1;
                }
            }
        }

        private void SeedRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rol>().HasData(new Rol()
            {
                Id = 1,
                Nombre = "Administrador",
                Descripción = "Administrador de la aplicación",
                Activo = true
            },
            new Rol()
            {
                Id = 2,
                Nombre = "Consumidor",
                Descripción = "Usuario de consumidor en la aplicación",
                Activo = true
            },
            new Rol() 
            {
                Id = 3,
                Nombre = "Comerciante",
                Descripción = "Usuario de comercio en la aplicación",
                Activo = true
            }
            );
        }

        private void SeedUnidadesDeMedida(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UnidadDeMedida>().HasData(new UnidadDeMedida()
            {
                Id = 1,
                Unidad = "Kilogramo",
                Abreviatura = "Kg",
                Activo = true
            },
            new UnidadDeMedida()
            {
                Id = 2,
                Unidad = "Gramo",
                Abreviatura = "Gr",
                Activo = true
            },
            new UnidadDeMedida()
            {
                Id = 3,
                Unidad = "Litro",
                Abreviatura = "L",
                Activo = true
            },
            new UnidadDeMedida()
            {
                Id = 4,
                Unidad = "Mililitro",
                Abreviatura = "Ml",
                Activo = true
            },
            new UnidadDeMedida()
            {
                Id = 5,
                Unidad = "Paquete",
                Abreviatura = "Paq",
                Activo = true
            },
            new UnidadDeMedida()
            {
                Id = 6,
                Unidad = "Pack 3",
                Abreviatura = "Pkg3",
                Activo = true
            },
            new UnidadDeMedida()
            {
                Id = 7,
                Unidad = "Pack 6",
                Abreviatura = "Pkg6",
                Activo = true
            },
            new UnidadDeMedida()
            {
                Id = 8,
                Unidad = "Pack 8",
                Abreviatura = "Pkg8",
                Activo = true
            },
            new UnidadDeMedida()
            {
                Id = 9,
                Unidad = "Pack 12",
                Abreviatura = "Pkg12",
                Activo = true
            },
            new UnidadDeMedida()
            {
                Id = 10,
                Unidad = "Unidad",
                Abreviatura = "U",
                Activo = true
            });
        }

        private void SeedUsuarios(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().HasData(new Usuario()
            {
                Id = 1,
                Nombre = "admin",
                Email = "consumidor@test.com",
                Password = "admin",
                RolId = 1,
                Activo = true
            },
            new Usuario()
            {
                Id = 2,
                Nombre = "consumidortest1",
                Email = "consumidor@test.com",
                Password = "consumidor1234",
                RolId = 2,
                Activo = true
            },
            new Usuario()
            {
                Id = 3,
                Nombre = "consumidortest2",
                Email = "consumidor@test.com",
                Password = "consumidor1234",
                RolId = 2,
                Activo = true
            },
            new Usuario()
            {
                Id = 4,
                Nombre = "comerciotest1",
                Email = "comercio@test.com",
                Password = "comercio1234",
                RolId = 3,
                Activo = true
            },
            new Usuario()
            {
                Id = 5,
                Nombre = "comerciotest2",
                Email = "comercio@test.com",
                Password = "comercio1234",
                RolId = 3,
                Activo = true
            },
            new Usuario()
            {
                Id = 6,
                Nombre = "comerciotest3",
                Email = "comercio@test.com",
                Password = "comercio1234",
                RolId = 3,
                Activo = true
            });
        }

        private void SeedDomicilios(ModelBuilder modelBuilder)
        {
            for (int i = 1; i <= 3; i++)
            {
                modelBuilder.Entity<Domicilio>().HasData(new Domicilio()
                {
                    Id = i,
                    Calle = "Calle test " + i.ToString(),
                    Altura = i + 10,
                    Localidad = "Localidad test " + i.ToString(),
                    Municipio = "Municipio test " + i.ToString(),
                    Provincia = "Provincia Test " + i.ToString(),
                    Latitud = -34.707660166611646,
                    Longitud = -58.266075191726536,
                    Activo = true
                });
            }
        }


    }
}
