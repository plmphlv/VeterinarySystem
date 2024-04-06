using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VeterinarySystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class DataSeed_Users : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LasttName",
                table: "AspNetUsers",
                newName: "LastName");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "bcb4f420-ch4d-g1ga-ab26-c069c6f364e", 0, "42da4153-2239-4834-9f7f-d39bb9715a89", "chad@admin.com", false, "Гига", "Чад", false, null, "CHAD@ADMIN.COM", "CHAD@ADMIM.COM", "AQAAAAIAAYagAAAAEPw4fw1zItf7uQ/YMvF89cGQa1BGoVslhEEAYtm0zoeZvauCD2C5yYWrWelWM8/Abg==", null, false, "3598f86b-b77a-417c-827e-64cd2f24d34c", false, "chad@admin.com" },
                    { "d49db8c9-73e5-4e24-8cb3-80b9ee257ba3", 0, "d627c2a6-98aa-4ee0-b908-438ae80c7d8a", "steli@vet.com", false, "Стелияна", "Трифонова", false, null, "STELI@VET.COM", "STELI@VET.COM", "AQAAAAIAAYagAAAAEJpwCo4OAppugUAdD0z1jIS5pX96+o5AIgv9eUry6H909InH/GzswtFV7lB7uhlQHA==", null, false, "03ddd882-975a-442e-8d75-3af4a6507bc4", false, "steli@vet.com" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bcb4f420-ch4d-g1ga-ab26-c069c6f364e");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d49db8c9-73e5-4e24-8cb3-80b9ee257ba3");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "AspNetUsers",
                newName: "LasttName");
        }
    }
}
