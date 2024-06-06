using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestInitProject.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Addnewrole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Value", "Name" },
                values: new object[] { 2, "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Value",
                keyValue: 2);
        }
    }
}
