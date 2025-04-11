using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clubcore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPhoneNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name_MobileNr",
                table: "Persons",
                newName: "Details_MobileNr");

            migrationBuilder.RenameColumn(
                name: "Name_LastName",
                table: "Persons",
                newName: "Details_LastName");

            migrationBuilder.RenameColumn(
                name: "Name_FirstName",
                table: "Persons",
                newName: "Details_FirstName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Details_MobileNr",
                table: "Persons",
                newName: "Name_MobileNr");

            migrationBuilder.RenameColumn(
                name: "Details_LastName",
                table: "Persons",
                newName: "Name_LastName");

            migrationBuilder.RenameColumn(
                name: "Details_FirstName",
                table: "Persons",
                newName: "Name_FirstName");
        }
    }
}
