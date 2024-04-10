using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VeterinarySystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class Prescription_Now_Has_StaffMember : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StaffMemberId",
                table: "Prescriptions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_StaffMemberId",
                table: "Prescriptions",
                column: "StaffMemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_AspNetUsers_StaffMemberId",
                table: "Prescriptions",
                column: "StaffMemberId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_AspNetUsers_StaffMemberId",
                table: "Prescriptions");

            migrationBuilder.DropIndex(
                name: "IX_Prescriptions_StaffMemberId",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "StaffMemberId",
                table: "Prescriptions");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bcb4f420-ch4d-g1ga-ab26-c069c6f364e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ea49f47b-4082-4fc7-8d40-0624c838a239", "AQAAAAIAAYagAAAAEMi4kgfi/uOC2UzuA6bqUDEWMs2TsCxfE3kWuPoeK3WD9+yLGaaMUTZV0xXDU5XKKA==", "9660be0b-b97d-4cb6-b040-14f0ba5a2e72" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d49db8c9-73e5-4e24-8cb3-80b9ee257ba3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "535611c9-859f-470e-a7a8-8a4232fbf216", "AQAAAAIAAYagAAAAEOrsV914ufkmX6mkaNv00cmge9/m6H5YWi8sc26bqHfwdU6EuKmxr73/Zp3ydsji/A==", "fcc231aa-352e-4da4-9300-c1b1061841f2" });
        }
    }
}
