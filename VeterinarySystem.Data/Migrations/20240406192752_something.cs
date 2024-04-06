using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VeterinarySystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class something : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AnimalTypeName",
                table: "AnimalTypes",
                newName: "Name");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bcb4f420-ch4d-g1ga-ab26-c069c6f364e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bb3413cf-6f46-40b5-9115-d07b86e203a7", "AQAAAAIAAYagAAAAEKpLdyQYA+p+UTtvXcTFJPzywgl8Bt1tLlRcnSyq8ZdK7yIs8kZGz3j3SI5+bHF5ww==", "f254fd7f-3c5c-4d01-baad-21f7ab80e4ba" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d49db8c9-73e5-4e24-8cb3-80b9ee257ba3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1969ab27-0811-49cc-a684-a6824dc1c64b", "AQAAAAIAAYagAAAAED47dIuRcPsqKIkPBxMPq+qdrrnOB27e4yorW6NuFJ3TDVsw5y3FuCqXHM2mXze/xw==", "625c9614-00fb-45e8-9c2e-a4563938fc6d" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "AnimalTypes",
                newName: "AnimalTypeName");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bcb4f420-ch4d-g1ga-ab26-c069c6f364e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "42da4153-2239-4834-9f7f-d39bb9715a89", "AQAAAAIAAYagAAAAEPw4fw1zItf7uQ/YMvF89cGQa1BGoVslhEEAYtm0zoeZvauCD2C5yYWrWelWM8/Abg==", "3598f86b-b77a-417c-827e-64cd2f24d34c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d49db8c9-73e5-4e24-8cb3-80b9ee257ba3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d627c2a6-98aa-4ee0-b908-438ae80c7d8a", "AQAAAAIAAYagAAAAEJpwCo4OAppugUAdD0z1jIS5pX96+o5AIgv9eUry6H909InH/GzswtFV7lB7uhlQHA==", "03ddd882-975a-442e-8d75-3af4a6507bc4" });
        }
    }
}
