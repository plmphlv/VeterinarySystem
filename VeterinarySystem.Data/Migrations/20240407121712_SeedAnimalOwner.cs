using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VeterinarySystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedAnimalOwner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AnimalOwners",
                columns: new[] { "AnimalOwnerId", "Address", "FirstName", "LastName", "PhoneNumber" },
                values: new object[] { 1, null, "Plamen", "Pehlivanov", "0123456789" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bcb4f420-ch4d-g1ga-ab26-c069c6f364e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ecf76f98-5acd-449e-a76a-d7023dba8042", "AQAAAAIAAYagAAAAEJCXb6XF6OW+P4ft7qD78Cvj9M+FUzURcwsSa3kBco7JgJtZWi4BeWRsvw3RCQrHCA==", "9b035549-10bf-4aee-856a-fe5a8a597487" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d49db8c9-73e5-4e24-8cb3-80b9ee257ba3",
                columns: new[] { "ConcurrencyStamp", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "041e7ac3-5924-495b-a2eb-a1a60bfbf2be", "Steliyana", "Tifonova", "AQAAAAIAAYagAAAAENH4fYhp6n5vEXENJDxCsKU6T5NSdAXhVHpsDleJgwlN3TKZr72g1gFq4QGVQmcLaA==", "dac92430-66ea-454d-83eb-c67f4d2f359b" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AnimalOwners",
                keyColumn: "AnimalOwnerId",
                keyValue: 1);

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
                columns: new[] { "ConcurrencyStamp", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4576d978-7ab3-4caa-b0a2-38a7d2da3a59", "Стелияна", "Трифонова", "AQAAAAIAAYagAAAAEETj7SFlNDPEKEz7gOu/EQlhs1ljv9m9gbByPNgaCEct8EHlh8IKEC3PIrO3WQRoMA==", "5ab5c42a-ab85-4501-8fe2-a830991a333e" });
        }
    }
}
