using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspNetCore.FileUpload.Approaches.Migrations
{
    /// <inheritdoc />
    public partial class multipleimageproduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MultipleImageProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultipleImageProducts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MultipleImageProductPaths",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MultipleImageProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultipleImageProductPaths", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MultipleImageProductPaths_MultipleImageProducts_MultipleImageProductId",
                        column: x => x.MultipleImageProductId,
                        principalTable: "MultipleImageProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MultipleImageProductPaths_MultipleImageProductId",
                table: "MultipleImageProductPaths",
                column: "MultipleImageProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MultipleImageProductPaths");

            migrationBuilder.DropTable(
                name: "MultipleImageProducts");
        }
    }
}
