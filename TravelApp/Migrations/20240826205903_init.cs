using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelApp.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerServices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Seyahats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Header = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    MiniDescription = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ImagePathSlider = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ShowMainPage = table.Column<bool>(type: "bit", nullable: false),
                    ShowInSlider = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seyahats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seyahats_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerServiceTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerServiceId = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    IsComplete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerServiceTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerServiceTasks_CustomerServices_CustomerServiceId",
                        column: x => x.CustomerServiceId,
                        principalTable: "CustomerServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerServiceTasks_CustomerServiceId",
                table: "CustomerServiceTasks",
                column: "CustomerServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Seyahats_CategoryId",
                table: "Seyahats",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerServiceTasks");

            migrationBuilder.DropTable(
                name: "Seyahats");

            migrationBuilder.DropTable(
                name: "CustomerServices");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
