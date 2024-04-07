using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VeterinarySystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class renamedAnimalId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AnimalOwnerId",
                table: "AnimalOwners",
                newName: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bcb4f420-ch4d-g1ga-ab26-c069c6f364e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dfa52b2f-ae33-4adc-8805-f4a6175f4eef", "AQAAAAIAAYagAAAAEONAchbJPZ2gvc4Pr00ncBDE8pF4WhCg3tXe08IeZMYDSN1X7cwSMN4KjmbEkrjwww==", "fc54b700-640e-4d65-9b36-c76ad59ea2de" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d49db8c9-73e5-4e24-8cb3-80b9ee257ba3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "54710c28-1c96-4a5d-bc26-0672d516fe7d", "AQAAAAIAAYagAAAAEMYCw9sO+pUBWDQgo5Ax7pYI7BHgBVUMJPORzTluDyJgmyM5HLF91jPgmx63M00tSA==", "1f884f99-37ce-4b4e-83bf-3ccbef3ee6f5" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "AnimalOwners",
                newName: "AnimalOwnerId");

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
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "041e7ac3-5924-495b-a2eb-a1a60bfbf2be", "AQAAAAIAAYagAAAAENH4fYhp6n5vEXENJDxCsKU6T5NSdAXhVHpsDleJgwlN3TKZr72g1gFq4QGVQmcLaA==", "dac92430-66ea-454d-83eb-c67f4d2f359b" });
        }
    }
}
