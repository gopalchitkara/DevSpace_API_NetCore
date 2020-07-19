using Microsoft.EntityFrameworkCore.Migrations;

namespace DevSpace_API.Migrations
{
    public partial class ReadingListsMigrationMods : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReadingListArticles");

            migrationBuilder.AddColumn<long>(
                name: "ArticleId",
                table: "ReadingLists",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArticleId",
                table: "ReadingLists");

            migrationBuilder.CreateTable(
                name: "ReadingListArticles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticleId = table.Column<long>(type: "bigint", nullable: false),
                    ReadingListId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReadingListArticles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReadingListArticles_ReadingLists_ReadingListId",
                        column: x => x.ReadingListId,
                        principalTable: "ReadingLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReadingListArticles_ReadingListId",
                table: "ReadingListArticles",
                column: "ReadingListId");
        }
    }
}
