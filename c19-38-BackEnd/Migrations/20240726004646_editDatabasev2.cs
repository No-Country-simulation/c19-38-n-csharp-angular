using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace c19_38_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class editDatabasev2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Altura",
                table: "DescripcionObjetivos");

            migrationBuilder.DropColumn(
                name: "Peso",
                table: "DescripcionObjetivos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Altura",
                table: "DescripcionObjetivos",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Peso",
                table: "DescripcionObjetivos",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
