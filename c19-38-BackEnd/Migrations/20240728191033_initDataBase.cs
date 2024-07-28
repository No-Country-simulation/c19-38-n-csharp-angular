using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace c19_38_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class initDataBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MediaUrl",
                table: "PlanesDeEntrenamiento",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Titulo",
                table: "PlanesDeEntrenamiento",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "MediaUrl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MediaUrl",
                table: "PlanesDeEntrenamiento");

            migrationBuilder.DropColumn(
                name: "Titulo",
                table: "PlanesDeEntrenamiento");

            migrationBuilder.AlterColumn<string>(
                name: "MediaUrl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
