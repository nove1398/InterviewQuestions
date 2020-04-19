using Microsoft.EntityFrameworkCore.Migrations;

namespace Interview.Api.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agents",
                columns: table => new
                {
                    AgentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 15, nullable: false),
                    ContactNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agents", x => x.AgentId);
                });

            migrationBuilder.InsertData(
                table: "Agents",
                columns: new[] { "AgentId", "ContactNumber", "Name" },
                values: new object[,]
                {
                    { 1, 123453231, "john doe1" },
                    { 27, 123453231, "john doe27" },
                    { 28, 123453231, "john doe28" },
                    { 29, 123453231, "john doe29" },
                    { 30, 123453231, "john doe30" },
                    { 31, 123453231, "john doe31" },
                    { 32, 123453231, "john doe32" },
                    { 33, 123453231, "john doe33" },
                    { 34, 123453231, "john doe34" },
                    { 35, 123453231, "john doe35" },
                    { 36, 123453231, "john doe36" },
                    { 37, 123453231, "john doe37" },
                    { 38, 123453231, "john doe38" },
                    { 39, 123453231, "john doe39" },
                    { 40, 123453231, "john doe40" },
                    { 41, 123453231, "john doe41" },
                    { 42, 123453231, "john doe42" },
                    { 43, 123453231, "john doe43" },
                    { 44, 123453231, "john doe44" },
                    { 45, 123453231, "john doe45" },
                    { 46, 123453231, "john doe46" },
                    { 47, 123453231, "john doe47" },
                    { 26, 123453231, "john doe26" },
                    { 48, 123453231, "john doe48" },
                    { 25, 123453231, "john doe25" },
                    { 23, 123453231, "john doe23" },
                    { 2, 123453231, "john doe2" },
                    { 3, 123453231, "john doe3" },
                    { 4, 123453231, "john doe4" },
                    { 5, 123453231, "john doe5" },
                    { 6, 123453231, "john doe6" },
                    { 7, 123453231, "john doe7" },
                    { 8, 123453231, "john doe8" },
                    { 9, 123453231, "john doe9" },
                    { 10, 123453231, "john doe10" },
                    { 11, 123453231, "john doe11" },
                    { 12, 123453231, "john doe12" },
                    { 13, 123453231, "john doe13" },
                    { 14, 123453231, "john doe14" },
                    { 15, 123453231, "john doe15" },
                    { 16, 123453231, "john doe16" },
                    { 17, 123453231, "john doe17" },
                    { 18, 123453231, "john doe18" },
                    { 19, 123453231, "john doe19" },
                    { 20, 123453231, "john doe20" },
                    { 21, 123453231, "john doe21" },
                    { 22, 123453231, "john doe22" },
                    { 24, 123453231, "john doe24" },
                    { 49, 123453231, "john doe49" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Agents");
        }
    }
}
