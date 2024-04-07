using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VeterinarySystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedAnimalTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AnimalTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Cat" },
                    { 2, "Dog" },
                    { 3, "Bird" },
                    { 5, "Livestock" },
                    { 6, "Transportation Animal" },
                    { 7, "Other mammal" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bcb4f420-ch4d-g1ga-ab26-c069c6f364e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "11634d50-766d-4005-9920-efdbfdf62f64", "AQAAAAIAAYagAAAAEKFsqBLdeIs4ITQ0rPawetpClDDQRVIxKBeBSLg9R9A+oWPuThcIjU4m8GEJmNrtMw==", "72416b63-ee82-41bd-a122-d27ef648ff89" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d49db8c9-73e5-4e24-8cb3-80b9ee257ba3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4576d978-7ab3-4caa-b0a2-38a7d2da3a59", "AQAAAAIAAYagAAAAEETj7SFlNDPEKEz7gOu/EQlhs1ljv9m9gbByPNgaCEct8EHlh8IKEC3PIrO3WQRoMA==", "5ab5c42a-ab85-4501-8fe2-a830991a333e" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AnimalTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AnimalTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AnimalTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AnimalTypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AnimalTypes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AnimalTypes",
                keyColumn: "Id",
                keyValue: 7);

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
    }
}
