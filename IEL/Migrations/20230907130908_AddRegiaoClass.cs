using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IEL.Migrations
{
    /// <inheritdoc />
    public partial class AddRegiaoClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RegiaoId",
                table: "Estado",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_Estado_RegiaoId",
                table: "Estado",
                column: "RegiaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Estado_Regiao_RegiaoId",
                table: "Estado",
                column: "RegiaoId",
                principalTable: "Regiao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estado_Regiao_RegiaoId",
                table: "Estado");

            migrationBuilder.DropTable(
                name: "Regiao");

            migrationBuilder.DropIndex(
                name: "IX_Estado_RegiaoId",
                table: "Estado");

            migrationBuilder.DropColumn(
                name: "RegiaoId",
                table: "Estado");
        }
    }
}
