using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VeterinarySystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class new_procedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMedical",
                table: "Procedures");

            migrationBuilder.RenameColumn(
                name: "ProcedureDescription",
                table: "Procedures",
                newName: "Description");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Procedures",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bcb4f420-ch4d-g1ga-ab26-c069c6f364e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bccb4893-f5fe-4ec0-b740-0386fcccb0db", "AQAAAAIAAYagAAAAELBonSLaIvuacL4BcCcaXcb+il9Hgn1TLiEGK6/O0DLPYfF44dAZ9lYzzCaIZ2f9kg==", "6418dfad-c9b0-48d1-befb-4cc68a22167d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d49db8c9-73e5-4e24-8cb3-80b9ee257ba3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "daac5167-66f3-4849-a1ec-02063214de0d", "AQAAAAIAAYagAAAAEKrLRnFV2A7OvYyP3WVVh4qgYt6ynE9DbjH00J087xMUM/cWYC6P388FXJJnPzukkw==", "8ed540a6-df50-4797-8583-6190242344e3" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Procedures");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Procedures",
                newName: "ProcedureDescription");

            migrationBuilder.AddColumn<bool>(
                name: "IsMedical",
                table: "Procedures",
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
    }
}
