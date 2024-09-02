using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelApp.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SeyahatId",
                table: "CustomerServiceTasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerServiceTasks_SeyahatId",
                table: "CustomerServiceTasks",
                column: "SeyahatId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerServiceTasks_Seyahats_SeyahatId",
                table: "CustomerServiceTasks",
                column: "SeyahatId",
                principalTable: "Seyahats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerServiceTasks_Seyahats_SeyahatId",
                table: "CustomerServiceTasks");

            migrationBuilder.DropIndex(
                name: "IX_CustomerServiceTasks_SeyahatId",
                table: "CustomerServiceTasks");

            migrationBuilder.DropColumn(
                name: "SeyahatId",
                table: "CustomerServiceTasks");
        }
    }
}
