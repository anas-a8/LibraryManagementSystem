using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabaseSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.AddColumn<int>(
                name: "TimesBorrowed",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Author", "CopiesAvailable", "ISBN", "TimesBorrowed", "Title" },
                values: new object[] { "John Doe", 10, "1234567890", 20, "C# Programming" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Author", "CopiesAvailable", "ISBN", "TimesBorrowed", "Title" },
                values: new object[] { "Jane Smith", 8, "0987654321", 25, "ASP.NET Core Guide" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Author", "CopiesAvailable", "ISBN", "TimesBorrowed", "Title" },
                values: new object[] { "Michael Brown", 5, "1122334455", 18, "Entity Framework Core" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimesBorrowed",
                table: "Books");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Author", "CopiesAvailable", "ISBN", "Title" },
                values: new object[] { "مؤلف 1", 5, "123456", "كتاب 1" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Author", "CopiesAvailable", "ISBN", "Title" },
                values: new object[] { "مؤلف 2", 3, "789012", "كتاب 2" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Author", "CopiesAvailable", "ISBN", "Title" },
                values: new object[] { "مؤلف 3", 2, "345678", "كتاب 3" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "CopiesAvailable", "ISBN", "Title" },
                values: new object[,]
                {
                    { 4, "مؤلف 1", 4, "901234", "كتاب 4" },
                    { 5, "مؤلف 2", 1, "567890", "كتاب 5" }
                });
        }
    }
}
