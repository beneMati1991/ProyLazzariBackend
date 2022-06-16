using Microsoft.EntityFrameworkCore.Migrations;

namespace LazzariAppProject.Migrations
{
    public partial class creacionDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rol",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Descripción = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rol", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnidadDeMedida",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Unidad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Abreviatura = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnidadDeMedida", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(320)", maxLength: 320, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RolId = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comercio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    RazonSocial = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Cuit = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(320)", maxLength: 320, nullable: false),
                    NombreContacto = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TelefonoContacto = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comercio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comercio_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Consumidor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(320)", maxLength: 320, nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consumidor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Consumidor_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sucursal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Coordenadas = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(320)", maxLength: 320, nullable: true),
                    ComercioId = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sucursal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sucursal_Comercio_ComercioId",
                        column: x => x.ComercioId,
                        principalTable: "Comercio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Producto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", maxLength: 255, nullable: false),
                    Detalle = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Cantidad = table.Column<int>(type: "int", maxLength: 255, nullable: true),
                    Imagen = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    UnidadMedidaId = table.Column<int>(type: "int", nullable: false),
                    SucursalId = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Producto_Sucursal_SucursalId",
                        column: x => x.SucursalId,
                        principalTable: "Sucursal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Producto_UnidadDeMedida_UnidadMedidaId",
                        column: x => x.UnidadMedidaId,
                        principalTable: "UnidadDeMedida",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Rol",
                columns: new[] { "Id", "Activo", "Descripción", "Nombre" },
                values: new object[,]
                {
                    { 1, true, "Administrador de la aplicación", "Administrador" },
                    { 2, true, "Usuario de consumidor en la aplicación", "Consumidor" },
                    { 3, true, "Usuario de comercio en la aplicación", "Comerciante" }
                });

            migrationBuilder.InsertData(
                table: "UnidadDeMedida",
                columns: new[] { "Id", "Abreviatura", "Activo", "Unidad" },
                values: new object[,]
                {
                    { 10, "U", true, "Unidad" },
                    { 9, "Pkg12", true, "Pack 12" },
                    { 8, "Pkg8", true, "Pack 8" },
                    { 6, "Pkg3", true, "Pack 3" },
                    { 7, "Pkg6", true, "Pack 6" },
                    { 4, "Ml", true, "Mililitro" },
                    { 3, "L", true, "Litro" },
                    { 2, "Gr", true, "Gramo" },
                    { 1, "Kg", true, "Kilogramo" },
                    { 5, "Paq", true, "Paquete" }
                });

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "Id", "Activo", "Email", "Nombre", "Password", "RolId" },
                values: new object[,]
                {
                    { 5, true, "comercio@test.com", "comerciotest2", "comercio1234", 3 },
                    { 1, true, "consumidor@test.com", "admin", "admin", 1 },
                    { 2, true, "consumidor@test.com", "consumidortest1", "consumidor1234", 2 },
                    { 3, true, "consumidor@test.com", "consumidortest2", "consumidor1234", 2 },
                    { 4, true, "comercio@test.com", "comerciotest1", "comercio1234", 3 },
                    { 6, true, "comercio@test.com", "comerciotest3", "comercio1234", 3 }
                });

            migrationBuilder.InsertData(
                table: "Comercio",
                columns: new[] { "Id", "Activo", "Cuit", "Email", "Nombre", "NombreContacto", "RazonSocial", "TelefonoContacto", "UsuarioId" },
                values: new object[,]
                {
                    { 1, true, "20-00000000-1", "comerciotest1@test1.com", "Comercio Test 1", "Contacto Test1", "Razon Social Test 1", "03-03-4561", 4 },
                    { 2, true, "20-00000000-2", "comerciotest2@test2.com", "Comercio Test 2", "Contacto Test2", "Razon Social Test 2", "03-03-4562", 5 },
                    { 3, true, "20-00000000-3", "comerciotest3@test3.com", "Comercio Test 3", "Contacto Test3", "Razon Social Test 3", "03-03-4563", 6 }
                });

            migrationBuilder.InsertData(
                table: "Consumidor",
                columns: new[] { "Id", "Activo", "Apellido", "Email", "Nombre", "Telefono", "UsuarioId" },
                values: new object[,]
                {
                    { 1, true, "Consumidor Apellido Test 1", "consumidortest1@test1.com", "Consumidor Nombre Test 1", "03-03-4561", 2 },
                    { 2, true, "Consumidor Apellido Test 2", "consumidortest2@test2.com", "Consumidor Nombre Test 2", "03-03-4562", 3 }
                });

            migrationBuilder.InsertData(
                table: "Sucursal",
                columns: new[] { "Id", "Activo", "ComercioId", "Coordenadas", "Direccion", "Email", "Nombre", "Telefono" },
                values: new object[,]
                {
                    { 1, true, 1, "-34.796832422771345, -58.16006347521772", "Dirección de Sucursal Test 1", "sucursal@test.com", "Sucursal Test 1", "03-03-456" },
                    { 2, true, 1, "-34.7905727308453, -58.150182401641565", "Dirección de Sucursal Test 2", "sucursal@test.com", "Sucursal Test 2", "03-03-456" },
                    { 3, true, 2, "-34.79095806184642, -58.152203987417174", "Dirección de Sucursal Test 3", "sucursal@test.com", "Sucursal Test 3", "03-03-456" },
                    { 4, true, 3, "-34.787202832961185, -58.164380241732474", "Dirección de Sucursal Test 4", "sucursal@test.com", "Sucursal Test 4", "03-03-456" }
                });

            migrationBuilder.InsertData(
                table: "Producto",
                columns: new[] { "Id", "Activo", "Cantidad", "Detalle", "Imagen", "Marca", "Nombre", "Precio", "SucursalId", "UnidadMedidaId" },
                values: new object[,]
                {
                    { 1, true, null, "Detalle Test 1", "Imagen Test 1", "Marca Test 1", "Producto Test 1", 1m, 1, 1 },
                    { 2, true, null, "Detalle Test 2", "Imagen Test 2", "Marca Test 2", "Producto Test 2", 4m, 2, 2 },
                    { 7, true, null, "Detalle Test 7", "Imagen Test 7", "Marca Test 7", "Producto Test 7", 49m, 2, 7 },
                    { 8, true, null, "Detalle Test 8", "Imagen Test 8", "Marca Test 8", "Producto Test 8", 64m, 2, 8 },
                    { 9, true, null, "Detalle Test 9", "Imagen Test 9", "Marca Test 9", "Producto Test 9", 81m, 2, 9 },
                    { 10, true, null, "Detalle Test 10", "Imagen Test 10", "Marca Test 10", "Producto Test 10", 100m, 2, 10 },
                    { 3, true, null, "Detalle Test 3", "Imagen Test 3", "Marca Test 3", "Producto Test 3", 9m, 3, 3 },
                    { 6, true, null, "Detalle Test 6", "Imagen Test 6", "Marca Test 6", "Producto Test 6", 36m, 3, 6 },
                    { 4, true, null, "Detalle Test 4", "Imagen Test 4", "Marca Test 4", "Producto Test 4", 16m, 4, 4 },
                    { 5, true, null, "Detalle Test 5", "Imagen Test 5", "Marca Test 5", "Producto Test 5", 25m, 4, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comercio_UsuarioId",
                table: "Comercio",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Consumidor_UsuarioId",
                table: "Consumidor",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_SucursalId",
                table: "Producto",
                column: "SucursalId");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_UnidadMedidaId",
                table: "Producto",
                column: "UnidadMedidaId");

            migrationBuilder.CreateIndex(
                name: "IX_Sucursal_ComercioId",
                table: "Sucursal",
                column: "ComercioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Consumidor");

            migrationBuilder.DropTable(
                name: "Producto");

            migrationBuilder.DropTable(
                name: "Rol");

            migrationBuilder.DropTable(
                name: "Sucursal");

            migrationBuilder.DropTable(
                name: "UnidadDeMedida");

            migrationBuilder.DropTable(
                name: "Comercio");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
