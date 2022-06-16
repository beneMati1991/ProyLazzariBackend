using Microsoft.EntityFrameworkCore.Migrations;

namespace LazzariAppProject.Migrations
{
    public partial class ModificoTipoDatoLonLat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Longitud",
                table: "Domicilio",
                type: "float",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<double>(
                name: "Latitud",
                table: "Domicilio",
                type: "float",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.UpdateData(
                table: "Domicilio",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Latitud", "Longitud" },
                values: new object[] { -34.707660166611646, -58.266075191726536 });

            migrationBuilder.UpdateData(
                table: "Domicilio",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Latitud", "Longitud" },
                values: new object[] { -34.707660166611646, -58.266075191726536 });

            migrationBuilder.UpdateData(
                table: "Domicilio",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Latitud", "Longitud" },
                values: new object[] { -34.707660166611646, -58.266075191726536 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Longitud",
                table: "Domicilio",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Latitud",
                table: "Domicilio",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float",
                oldMaxLength: 255);

            migrationBuilder.UpdateData(
                table: "Domicilio",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Latitud", "Longitud" },
                values: new object[] { "-34.707660166611646", "-58.266075191726536" });

            migrationBuilder.UpdateData(
                table: "Domicilio",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Latitud", "Longitud" },
                values: new object[] { "-34.707660166611646", "-58.266075191726536" });

            migrationBuilder.UpdateData(
                table: "Domicilio",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Latitud", "Longitud" },
                values: new object[] { "-34.707660166611646", "-58.266075191726536" });
        }
    }
}
