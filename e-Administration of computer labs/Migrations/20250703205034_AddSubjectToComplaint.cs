using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e_Administration_of_computer_labs.Migrations
{
    /// <inheritdoc />
    public partial class AddSubjectToComplaint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "Complaints",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Subject",
                table: "Complaints");
        }
    }
}
