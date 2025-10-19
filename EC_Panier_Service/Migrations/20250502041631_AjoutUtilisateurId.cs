using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EC_Panier_Service.Migrations
{
    /// <inheritdoc />
    public partial class AjoutUtilisateurId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "Paniers",
                newName: "UtilisateurId");

            migrationBuilder.CreateTable(
                name: "LignesPanier",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PanierId = table.Column<int>(type: "int", nullable: false),
                    ProduitId = table.Column<int>(type: "int", nullable: false),
                    Quantite = table.Column<int>(type: "int", nullable: false),
                    Prix = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LignesPanier", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LignesPanier_Paniers_PanierId",
                        column: x => x.PanierId,
                        principalTable: "Paniers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LignesPanier_PanierId",
                table: "LignesPanier",
                column: "PanierId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LignesPanier");

            migrationBuilder.RenameColumn(
                name: "UtilisateurId",
                table: "Paniers",
                newName: "ClientId");
        }
    }
}
