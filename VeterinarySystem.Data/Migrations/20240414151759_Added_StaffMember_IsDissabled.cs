using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VeterinarySystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class Added_StaffMember_IsDissabled : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDisabled",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bcb4f420-ch4d-g1ga-ab26-c069c6f364e",
                columns: new[] { "ConcurrencyStamp", "IsDisabled", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c851a8ea-620c-4868-b7c2-e9743acc4916", false, "AQAAAAIAAYagAAAAEPKv5NsoX8ctz15CD9JJKqcjldnTPN7CunhKcjVFQcS7LjY0nF7lfB42iGZXJ9oK9g==", "f88f0e9e-9e22-4c73-be0a-bfd9cf97e402" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d49db8c9-73e5-4e24-8cb3-80b9ee257ba3",
                columns: new[] { "ConcurrencyStamp", "IsDisabled", "PasswordHash", "SecurityStamp" },
                values: new object[] { "884e5359-93d5-4886-81dc-93d474e5abf2", false, "AQAAAAIAAYagAAAAEDFrT+ZD0P14e9aETbZz+kLiVusELwadzuE/Cjq8CRXalRx7yFsg859Mrp23w4zRZQ==", "02fdd1be-aa48-44bf-93de-42d9892a7664" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDisabled",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bcb4f420-ch4d-g1ga-ab26-c069c6f364e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7409b29b-f8e6-40aa-994b-7324c9ac9492", "AQAAAAIAAYagAAAAEKdeTL02Cb+h7Oiq88oD0YjHoopfz4G3SA+BXFJpXBdevSANC9Ej9trrf8xiwv/p2A==", "4df7e862-ab8f-471f-9088-e36e1e5ebb03" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d49db8c9-73e5-4e24-8cb3-80b9ee257ba3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b30266ca-b961-410c-a906-cc89c9d1f4cb", "AQAAAAIAAYagAAAAEAVU/mA/SWtxlI7UDM+yood1eNnW9xoldVaUlHhmwYKgYLnOzMzbQditk2Eg7SUbvg==", "9e25d956-a9d6-4b37-a723-e7b0e5e4708a" });
        }
    }
}
