using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace c19_38_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class editDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActividadFisica",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "DescripcionObjetivos",
                columns: table => new
                {
                    IdDescripcion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    Motivacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MayorObstaculo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LugarEntrenamiento = table.Column<int>(type: "int", nullable: false),
                    EquiposEnCasa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Objetivo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreferenciaHora = table.Column<int>(type: "int", nullable: false),
                    Peso = table.Column<float>(type: "real", nullable: false),
                    Altura = table.Column<float>(type: "real", nullable: false),
                    ActividadFisica = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DescripcionObjetivos", x => x.IdDescripcion);
                    table.ForeignKey(
                        name: "FK_DescripcionObjetivos_AspNetUsers_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DescripcionObjetivos_IdUsuario",
                table: "DescripcionObjetivos",
                column: "IdUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DescripcionObjetivos");

            migrationBuilder.AddColumn<int>(
                name: "ActividadFisica",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
