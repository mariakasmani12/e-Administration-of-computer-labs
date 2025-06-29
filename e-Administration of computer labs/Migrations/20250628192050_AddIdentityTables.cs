using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e_Administration_of_computer_labs.Migrations
{
    /// <inheritdoc />
    public partial class AddIdentityTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "Complaints",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_UserId1",
                table: "Complaints",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Complaints_Users_UserId1",
                table: "Complaints",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Complaints_Users_UserId1",
                table: "Complaints");

            migrationBuilder.DropIndex(
                name: "IX_Complaints_UserId1",
                table: "Complaints");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Complaints");
        }
    }
}
