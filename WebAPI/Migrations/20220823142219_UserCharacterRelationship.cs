using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    public partial class UserCharacterRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Characaters",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Characaters_UserId",
                table: "Characaters",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characaters_Users_UserId",
                table: "Characaters",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characaters_Users_UserId",
                table: "Characaters");

            migrationBuilder.DropIndex(
                name: "IX_Characaters_UserId",
                table: "Characaters");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Characaters");
        }
    }
}
