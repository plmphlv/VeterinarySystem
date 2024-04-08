using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VeterinarySystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class Appointment_IsUpcoming_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsUpcoming",
                table: "Appointments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bcb4f420-ch4d-g1ga-ab26-c069c6f364e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "aa1d5997-407f-4266-9a38-d43fa3cca4b0", "AQAAAAIAAYagAAAAEDx7E1cMETR6CpoRFz7jqWI8R/aawxzr8gE6ZaA0bIGtAxOly6beIO16ZYePt8/Vtw==", "251f2944-4b64-4232-9280-7fd789edef64" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d49db8c9-73e5-4e24-8cb3-80b9ee257ba3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dd03d098-eae3-48d4-ba90-f8bf5eeeb9c1", "AQAAAAIAAYagAAAAECgtRZ/nl89v6Zq8Vyz03SHULWz9ivtNNNHTuIoMrzjxuPyz4FKcQQHeulOEOqSfpg==", "e43d3395-d89e-4bb0-afaf-2394ea3629dd" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsUpcoming",
                table: "Appointments");

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
    }
}
