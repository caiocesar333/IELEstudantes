using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IEL.Migrations
{
    /// <inheritdoc />
    public partial class EstadoIdenum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estudantes_Estado_EstadoId",
                table: "Estudantes");

            migrationBuilder.DropTable(
                name: "Estado");

            migrationBuilder.DropTable(
                name: "Regiao");

            migrationBuilder.DropIndex(
                name: "IX_Estudantes_EstadoId",
                table: "Estudantes");

            migrationBuilder.AddColumn<int>(
                name: "Estado",
                table: "Estudantes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Estudantes");

            migrationBuilder.CreateTable(
                name: "Regiao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sigla = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regiao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Estado",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegiaoId = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sigla = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Estado_Regiao_RegiaoId",
                        column: x => x.RegiaoId,
                        principalTable: "Regiao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Estudantes_EstadoId",
                table: "Estudantes",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Estado_RegiaoId",
                table: "Estado",
                column: "RegiaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Estudantes_Estado_EstadoId",
                table: "Estudantes",
                column: "EstadoId",
                principalTable: "Estado",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
