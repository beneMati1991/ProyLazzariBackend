// <auto-generated />
using System;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LazzariAppProject.Migrations
{
    [DbContext(typeof(ApiDbContext))]
    [Migration("20220522001624_ModificoTipoDatoLonLat")]
    partial class ModificoTipoDatoLonLat
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.15")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Entities.Comercio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool?>("Activo")
                        .IsRequired()
                        .HasColumnType("bit");

                    b.Property<string>("Cuit")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.Property<int>("DomicilioId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(320)
                        .HasColumnType("nvarchar(320)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("NombreContacto")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("RazonSocial")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DomicilioId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Comercio");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Activo = true,
                            Cuit = "20-00000000-1",
                            DomicilioId = 1,
                            Email = "comerciotest1@test1.com",
                            Nombre = "Comercio Test 1",
                            NombreContacto = "Contacto Test1",
                            RazonSocial = "Razon Social Test 1",
                            Telefono = "03-03-4561",
                            UsuarioId = 4
                        },
                        new
                        {
                            Id = 2,
                            Activo = true,
                            Cuit = "20-00000000-2",
                            DomicilioId = 2,
                            Email = "comerciotest2@test2.com",
                            Nombre = "Comercio Test 2",
                            NombreContacto = "Contacto Test2",
                            RazonSocial = "Razon Social Test 2",
                            Telefono = "03-03-4562",
                            UsuarioId = 5
                        },
                        new
                        {
                            Id = 3,
                            Activo = true,
                            Cuit = "20-00000000-3",
                            DomicilioId = 3,
                            Email = "comerciotest3@test3.com",
                            Nombre = "Comercio Test 3",
                            NombreContacto = "Contacto Test3",
                            RazonSocial = "Razon Social Test 3",
                            Telefono = "03-03-4563",
                            UsuarioId = 6
                        });
                });

            modelBuilder.Entity("Entities.Consumidor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool?>("Activo")
                        .IsRequired()
                        .HasColumnType("bit");

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(320)
                        .HasColumnType("nvarchar(320)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Telefono")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Consumidor");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Activo = true,
                            Apellido = "Consumidor Apellido Test 1",
                            Email = "consumidortest1@test1.com",
                            Nombre = "Consumidor Nombre Test 1",
                            Telefono = "03-03-4561",
                            UsuarioId = 2
                        },
                        new
                        {
                            Id = 2,
                            Activo = true,
                            Apellido = "Consumidor Apellido Test 2",
                            Email = "consumidortest2@test2.com",
                            Nombre = "Consumidor Nombre Test 2",
                            Telefono = "03-03-4562",
                            UsuarioId = 3
                        });
                });

            modelBuilder.Entity("Entities.Domicilio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool?>("Activo")
                        .IsRequired()
                        .HasColumnType("bit");

                    b.Property<long>("Altura")
                        .HasColumnType("bigint");

                    b.Property<string>("Calle")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<double>("Latitud")
                        .HasMaxLength(255)
                        .HasColumnType("float");

                    b.Property<string>("Localidad")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<double>("Longitud")
                        .HasMaxLength(255)
                        .HasColumnType("float");

                    b.Property<string>("Municipio")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Provincia")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Domicilio");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Activo = true,
                            Altura = 11L,
                            Calle = "Calle test 1",
                            Latitud = -34.707660166611646,
                            Localidad = "Localidad test 1",
                            Longitud = -58.266075191726536,
                            Municipio = "Municipio test 1",
                            Provincia = "Provincia Test 1"
                        },
                        new
                        {
                            Id = 2,
                            Activo = true,
                            Altura = 12L,
                            Calle = "Calle test 2",
                            Latitud = -34.707660166611646,
                            Localidad = "Localidad test 2",
                            Longitud = -58.266075191726536,
                            Municipio = "Municipio test 2",
                            Provincia = "Provincia Test 2"
                        },
                        new
                        {
                            Id = 3,
                            Activo = true,
                            Altura = 13L,
                            Calle = "Calle test 3",
                            Latitud = -34.707660166611646,
                            Localidad = "Localidad test 3",
                            Longitud = -58.266075191726536,
                            Municipio = "Municipio test 3",
                            Provincia = "Provincia Test 3"
                        });
                });

            modelBuilder.Entity("Entities.Producto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool?>("Activo")
                        .IsRequired()
                        .HasColumnType("bit");

                    b.Property<int?>("Cantidad")
                        .HasColumnType("int");

                    b.Property<int>("ComercioId")
                        .HasColumnType("int");

                    b.Property<string>("Detalle")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Imagen")
                        .HasMaxLength(320)
                        .HasColumnType("nvarchar(320)");

                    b.Property<string>("Marca")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<decimal>("Precio")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("UnidadMedidaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ComercioId");

                    b.HasIndex("UnidadMedidaId");

                    b.ToTable("Producto");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Activo = true,
                            ComercioId = 1,
                            Detalle = "Detalle Test 1",
                            Imagen = "Imagen Test 1",
                            Marca = "Marca Test 1",
                            Nombre = "Producto Test 1",
                            Precio = 1m,
                            UnidadMedidaId = 1
                        },
                        new
                        {
                            Id = 2,
                            Activo = true,
                            ComercioId = 1,
                            Detalle = "Detalle Test 2",
                            Imagen = "Imagen Test 2",
                            Marca = "Marca Test 2",
                            Nombre = "Producto Test 2",
                            Precio = 4m,
                            UnidadMedidaId = 2
                        },
                        new
                        {
                            Id = 3,
                            Activo = true,
                            ComercioId = 1,
                            Detalle = "Detalle Test 3",
                            Imagen = "Imagen Test 3",
                            Marca = "Marca Test 3",
                            Nombre = "Producto Test 3",
                            Precio = 9m,
                            UnidadMedidaId = 3
                        },
                        new
                        {
                            Id = 4,
                            Activo = true,
                            ComercioId = 1,
                            Detalle = "Detalle Test 4",
                            Imagen = "Imagen Test 4",
                            Marca = "Marca Test 4",
                            Nombre = "Producto Test 4",
                            Precio = 16m,
                            UnidadMedidaId = 4
                        },
                        new
                        {
                            Id = 5,
                            Activo = true,
                            ComercioId = 1,
                            Detalle = "Detalle Test 5",
                            Imagen = "Imagen Test 5",
                            Marca = "Marca Test 5",
                            Nombre = "Producto Test 5",
                            Precio = 25m,
                            UnidadMedidaId = 5
                        },
                        new
                        {
                            Id = 6,
                            Activo = true,
                            ComercioId = 1,
                            Detalle = "Detalle Test 6",
                            Imagen = "Imagen Test 6",
                            Marca = "Marca Test 6",
                            Nombre = "Producto Test 6",
                            Precio = 36m,
                            UnidadMedidaId = 6
                        },
                        new
                        {
                            Id = 7,
                            Activo = true,
                            ComercioId = 1,
                            Detalle = "Detalle Test 7",
                            Imagen = "Imagen Test 7",
                            Marca = "Marca Test 7",
                            Nombre = "Producto Test 7",
                            Precio = 49m,
                            UnidadMedidaId = 7
                        },
                        new
                        {
                            Id = 8,
                            Activo = true,
                            ComercioId = 1,
                            Detalle = "Detalle Test 8",
                            Imagen = "Imagen Test 8",
                            Marca = "Marca Test 8",
                            Nombre = "Producto Test 8",
                            Precio = 64m,
                            UnidadMedidaId = 8
                        },
                        new
                        {
                            Id = 9,
                            Activo = true,
                            ComercioId = 1,
                            Detalle = "Detalle Test 9",
                            Imagen = "Imagen Test 9",
                            Marca = "Marca Test 9",
                            Nombre = "Producto Test 9",
                            Precio = 81m,
                            UnidadMedidaId = 9
                        },
                        new
                        {
                            Id = 10,
                            Activo = true,
                            ComercioId = 1,
                            Detalle = "Detalle Test 10",
                            Imagen = "Imagen Test 10",
                            Marca = "Marca Test 10",
                            Nombre = "Producto Test 10",
                            Precio = 100m,
                            UnidadMedidaId = 10
                        });
                });

            modelBuilder.Entity("Entities.Rol", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool?>("Activo")
                        .IsRequired()
                        .HasColumnType("bit");

                    b.Property<string>("Descripción")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Rol");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Activo = true,
                            Descripción = "Administrador de la aplicación",
                            Nombre = "Administrador"
                        },
                        new
                        {
                            Id = 2,
                            Activo = true,
                            Descripción = "Usuario de consumidor en la aplicación",
                            Nombre = "Consumidor"
                        },
                        new
                        {
                            Id = 3,
                            Activo = true,
                            Descripción = "Usuario de comercio en la aplicación",
                            Nombre = "Comerciante"
                        });
                });

            modelBuilder.Entity("Entities.UnidadDeMedida", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Abreviatura")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)");

                    b.Property<bool?>("Activo")
                        .IsRequired()
                        .HasColumnType("bit");

                    b.Property<string>("Unidad")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("UnidadDeMedida");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Abreviatura = "Kg",
                            Activo = true,
                            Unidad = "Kilogramo"
                        },
                        new
                        {
                            Id = 2,
                            Abreviatura = "Gr",
                            Activo = true,
                            Unidad = "Gramo"
                        },
                        new
                        {
                            Id = 3,
                            Abreviatura = "L",
                            Activo = true,
                            Unidad = "Litro"
                        },
                        new
                        {
                            Id = 4,
                            Abreviatura = "Ml",
                            Activo = true,
                            Unidad = "Mililitro"
                        },
                        new
                        {
                            Id = 5,
                            Abreviatura = "Paq",
                            Activo = true,
                            Unidad = "Paquete"
                        },
                        new
                        {
                            Id = 6,
                            Abreviatura = "Pkg3",
                            Activo = true,
                            Unidad = "Pack 3"
                        },
                        new
                        {
                            Id = 7,
                            Abreviatura = "Pkg6",
                            Activo = true,
                            Unidad = "Pack 6"
                        },
                        new
                        {
                            Id = 8,
                            Abreviatura = "Pkg8",
                            Activo = true,
                            Unidad = "Pack 8"
                        },
                        new
                        {
                            Id = 9,
                            Abreviatura = "Pkg12",
                            Activo = true,
                            Unidad = "Pack 12"
                        },
                        new
                        {
                            Id = 10,
                            Abreviatura = "U",
                            Activo = true,
                            Unidad = "Unidad"
                        });
                });

            modelBuilder.Entity("Entities.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool?>("Activo")
                        .IsRequired()
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(320)
                        .HasColumnType("nvarchar(320)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("RolId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Usuario");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Activo = true,
                            Email = "consumidor@test.com",
                            Nombre = "admin",
                            Password = "admin",
                            RolId = 1
                        },
                        new
                        {
                            Id = 2,
                            Activo = true,
                            Email = "consumidor@test.com",
                            Nombre = "consumidortest1",
                            Password = "consumidor1234",
                            RolId = 2
                        },
                        new
                        {
                            Id = 3,
                            Activo = true,
                            Email = "consumidor@test.com",
                            Nombre = "consumidortest2",
                            Password = "consumidor1234",
                            RolId = 2
                        },
                        new
                        {
                            Id = 4,
                            Activo = true,
                            Email = "comercio@test.com",
                            Nombre = "comerciotest1",
                            Password = "comercio1234",
                            RolId = 3
                        },
                        new
                        {
                            Id = 5,
                            Activo = true,
                            Email = "comercio@test.com",
                            Nombre = "comerciotest2",
                            Password = "comercio1234",
                            RolId = 3
                        },
                        new
                        {
                            Id = 6,
                            Activo = true,
                            Email = "comercio@test.com",
                            Nombre = "comerciotest3",
                            Password = "comercio1234",
                            RolId = 3
                        });
                });

            modelBuilder.Entity("Entities.Comercio", b =>
                {
                    b.HasOne("Entities.Domicilio", "Domicilio")
                        .WithMany()
                        .HasForeignKey("DomicilioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Usuario", "Usuario")
                        .WithMany("Comercios")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Domicilio");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Entities.Consumidor", b =>
                {
                    b.HasOne("Entities.Usuario", "Usuario")
                        .WithMany("Consumidores")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Entities.Producto", b =>
                {
                    b.HasOne("Entities.Comercio", "Comercio")
                        .WithMany("Productos")
                        .HasForeignKey("ComercioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.UnidadDeMedida", "UnidadMedida")
                        .WithMany("Productos")
                        .HasForeignKey("UnidadMedidaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Comercio");

                    b.Navigation("UnidadMedida");
                });

            modelBuilder.Entity("Entities.Comercio", b =>
                {
                    b.Navigation("Productos");
                });

            modelBuilder.Entity("Entities.UnidadDeMedida", b =>
                {
                    b.Navigation("Productos");
                });

            modelBuilder.Entity("Entities.Usuario", b =>
                {
                    b.Navigation("Comercios");

                    b.Navigation("Consumidores");
                });
#pragma warning restore 612, 618
        }
    }
}
