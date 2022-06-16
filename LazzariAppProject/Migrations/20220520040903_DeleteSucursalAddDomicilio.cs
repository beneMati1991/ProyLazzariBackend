using Microsoft.EntityFrameworkCore.Migrations;

namespace LazzariAppProject.Migrations
{
    public partial class DeleteSucursalAddDomicilio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Producto_Sucursal_SucursalId",
                table: "Producto");

            migrationBuilder.DropTable(
                name: "Sucursal");

            migrationBuilder.RenameColumn(
                name: "SucursalId",
                table: "Producto",
                newName: "ComercioId");

            migrationBuilder.RenameIndex(
                name: "IX_Producto_SucursalId",
                table: "Producto",
                newName: "IX_Producto_ComercioId");

            migrationBuilder.RenameColumn(
                name: "TelefonoContacto",
                table: "Comercio",
                newName: "Telefono");

            migrationBuilder.AlterColumn<string>(
                name: "Unidad",
                table: "UnidadDeMedida",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Abreviatura",
                table: "UnidadDeMedida",
                type: "nvarchar(6)",
                maxLength: 6,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Imagen",
                table: "Producto",
                type: "nvarchar(320)",
                maxLength: 320,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DomicilioId",
                table: "Comercio",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Domicilio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Calle = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Altura = table.Column<long>(type: "bigint", nullable: false),
                    Localidad = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Municipio = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Provincia = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Latitud = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Longitud = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Domicilio", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Domicilio",
                columns: new[] { "Id", "Activo", "Altura", "Calle", "Latitud", "Localidad", "Longitud", "Municipio", "Provincia" },
                values: new object[,]
                {
                    { 1, true, 11L, "Calle test 1", "-34.707660166611646", "Localidad test 1", "-58.266075191726536", "Municipio test 1", "Provincia Test 1" },
                    { 2, true, 12L, "Calle test 2", "-34.707660166611646", "Localidad test 2", "-58.266075191726536", "Municipio test 2", "Provincia Test 2" },
                    { 3, true, 13L, "Calle test 3", "-34.707660166611646", "Localidad test 3", "-58.266075191726536", "Municipio test 3", "Provincia Test 3" }
                });

            migrationBuilder.UpdateData(
                table: "Producto",
                keyColumn: "Id",
                keyValue: 2,
                column: "ComercioId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Producto",
                keyColumn: "Id",
                keyValue: 3,
                column: "ComercioId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Producto",
                keyColumn: "Id",
                keyValue: 4,
                column: "ComercioId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Producto",
                keyColumn: "Id",
                keyValue: 5,
                column: "ComercioId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Producto",
                keyColumn: "Id",
                keyValue: 6,
                column: "ComercioId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Producto",
                keyColumn: "Id",
                keyValue: 7,
                column: "ComercioId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Producto",
                keyColumn: "Id",
                keyValue: 8,
                column: "ComercioId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Producto",
                keyColumn: "Id",
                keyValue: 9,
                column: "ComercioId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Producto",
                keyColumn: "Id",
                keyValue: 10,
                column: "ComercioId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Comercio",
                keyColumn: "Id",
                keyValue: 1,
                column: "DomicilioId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Comercio",
                keyColumn: "Id",
                keyValue: 2,
                column: "DomicilioId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Comercio",
                keyColumn: "Id",
                keyValue: 3,
                column: "DomicilioId",
                value: 3);

            migrationBuilder.CreateIndex(
                name: "IX_Comercio_DomicilioId",
                table: "Comercio",
                column: "DomicilioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comercio_Domicilio_DomicilioId",
                table: "Comercio",
                column: "DomicilioId",
                principalTable: "Domicilio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Producto_Comercio_ComercioId",
                table: "Producto",
                column: "ComercioId",
                principalTable: "Comercio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comercio_Domicilio_DomicilioId",
                table: "Comercio");

            migrationBuilder.DropForeignKey(
                name: "FK_Producto_Comercio_ComercioId",
                table: "Producto");

            migrationBuilder.DropTable(
                name: "Domicilio");

            migrationBuilder.DropIndex(
                name: "IX_Comercio_DomicilioId",
                table: "Comercio");

            migrationBuilder.DropColumn(
                name: "DomicilioId",
                table: "Comercio");

            migrationBuilder.RenameColumn(
                name: "ComercioId",
                table: "Producto",
                newName: "SucursalId");

            migrationBuilder.RenameIndex(
                name: "IX_Producto_ComercioId",
                table: "Producto",
                newName: "IX_Producto_SucursalId");

            migrationBuilder.RenameColumn(
                name: "Telefono",
                table: "Comercio",
                newName: "TelefonoContacto");

            migrationBuilder.AlterColumn<string>(
                name: "Unidad",
                table: "UnidadDeMedida",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Abreviatura",
                table: "UnidadDeMedida",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(6)",
                oldMaxLength: 6);

            migrationBuilder.AlterColumn<string>(
                name: "Imagen",
                table: "Producto",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(320)",
                oldMaxLength: 320,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Sucursal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    ComercioId = table.Column<int>(type: "int", nullable: false),
                    Coordenadas = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(320)", maxLength: 320, nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
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

            migrationBuilder.UpdateData(
                table: "Producto",
                keyColumn: "Id",
                keyValue: 2,
                column: "SucursalId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Producto",
                keyColumn: "Id",
                keyValue: 3,
                column: "SucursalId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Producto",
                keyColumn: "Id",
                keyValue: 4,
                column: "SucursalId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Producto",
                keyColumn: "Id",
                keyValue: 5,
                column: "SucursalId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Producto",
                keyColumn: "Id",
                keyValue: 6,
                column: "SucursalId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Producto",
                keyColumn: "Id",
                keyValue: 7,
                column: "SucursalId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Producto",
                keyColumn: "Id",
                keyValue: 8,
                column: "SucursalId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Producto",
                keyColumn: "Id",
                keyValue: 9,
                column: "SucursalId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Producto",
                keyColumn: "Id",
                keyValue: 10,
                column: "SucursalId",
                value: 2);

            migrationBuilder.CreateIndex(
                name: "IX_Sucursal_ComercioId",
                table: "Sucursal",
                column: "ComercioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Producto_Sucursal_SucursalId",
                table: "Producto",
                column: "SucursalId",
                principalTable: "Sucursal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
