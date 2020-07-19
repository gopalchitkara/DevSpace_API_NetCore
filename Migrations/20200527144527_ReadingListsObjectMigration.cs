using Microsoft.EntityFrameworkCore.Migrations;

namespace DevSpace_API.Migrations
{
    public partial class ReadingListsObjectMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReadingLists",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReadingLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReadingListArticles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticleId = table.Column<long>(nullable: false),
                    ReadingListId = table.Column<long>(nullable: false)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReadingListArticles");

            migrationBuilder.DropTable(
                name: "ReadingLists");
        }
    }
}
