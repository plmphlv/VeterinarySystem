using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VeterinarySystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovedMedicineAndSeededPets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Procedures_ProcedureId",
                table: "Prescriptions");

            migrationBuilder.DropTable(
                name: "PrescriptionMedicines");

            migrationBuilder.DropTable(
                name: "Medicines");

            migrationBuilder.DropTable(
                name: "MedicineCategories");

            migrationBuilder.DropIndex(
                name: "IX_Prescriptions_ProcedureId",
                table: "Prescriptions");

            migrationBuilder.RenameColumn(
                name: "ProcedureId",
                table: "Prescriptions",
                newName: "AnimalId");

            migrationBuilder.AddColumn<DateTime>(
                name: "IssueDate",
                table: "Prescriptions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Number",
                table: "Prescriptions",
                type: "nvarchar(9)",
                maxLength: 9,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "PrescriptionCounters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrentNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrescriptionCounters", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Animals",
                columns: new[] { "Id", "Age", "AnimalOwnerId", "AnimalTypeId", "Name", "Weight" },
                values: new object[,]
                {
                    { 1, 13, 1, 2, "Bobo", 6.0 },
                    { 2, 1, 1, 1, "Buba", 3.0 }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bcb4f420-ch4d-g1ga-ab26-c069c6f364e",
                columns: new[] { "ConcurrencyStamp", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ea49f47b-4082-4fc7-8d40-0624c838a239", "Giga", "Chad", "AQAAAAIAAYagAAAAEMi4kgfi/uOC2UzuA6bqUDEWMs2TsCxfE3kWuPoeK3WD9+yLGaaMUTZV0xXDU5XKKA==", "9660be0b-b97d-4cb6-b040-14f0ba5a2e72" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d49db8c9-73e5-4e24-8cb3-80b9ee257ba3",
                columns: new[] { "ConcurrencyStamp", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "535611c9-859f-470e-a7a8-8a4232fbf216", "Trifonova", "AQAAAAIAAYagAAAAEOrsV914ufkmX6mkaNv00cmge9/m6H5YWi8sc26bqHfwdU6EuKmxr73/Zp3ydsji/A==", "fcc231aa-352e-4da4-9300-c1b1061841f2" });

            migrationBuilder.InsertData(
                table: "PrescriptionCounters",
                columns: new[] { "Id", "CurrentNumber" },
                values: new object[] { 1, 0 });

            migrationBuilder.InsertData(
                table: "Procedures",
                columns: new[] { "Id", "AnimalId", "Date", "Description", "Name", "StaffMemberId" },
                values: new object[] { 1, 1, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Extraction of a broken upper-right wisdom tooth that causes infections", "Tooth Extraction", "d49db8c9-73e5-4e24-8cb3-80b9ee257ba3" });

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_AnimalId",
                table: "Prescriptions",
                column: "AnimalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Animals_AnimalId",
                table: "Prescriptions",
                column: "AnimalId",
                principalTable: "Animals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Animals_AnimalId",
                table: "Prescriptions");

            migrationBuilder.DropTable(
                name: "PrescriptionCounters");

            migrationBuilder.DropIndex(
                name: "IX_Prescriptions_AnimalId",
                table: "Prescriptions");

            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Procedures",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "IssueDate",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "Prescriptions");

            migrationBuilder.RenameColumn(
                name: "AnimalId",
                table: "Prescriptions",
                newName: "ProcedureId");

            migrationBuilder.CreateTable(
                name: "MedicineCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicineCategoryName = table.Column<string>(type: "nvarchar(62)", maxLength: 62, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicineCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Medicines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicineCategoryId = table.Column<int>(type: "int", nullable: false),
                    Barcode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(62)", maxLength: 62, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Producer = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    ProductionDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medicines_MedicineCategories_MedicineCategoryId",
                        column: x => x.MedicineCategoryId,
                        principalTable: "MedicineCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrescriptionMedicines",
                columns: table => new
                {
                    PrescriptionId = table.Column<int>(type: "int", nullable: false),
                    MedicineId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrescriptionMedicines", x => new { x.PrescriptionId, x.MedicineId });
                    table.ForeignKey(
                        name: "FK_PrescriptionMedicines_Medicines_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "Medicines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrescriptionMedicines_Prescriptions_PrescriptionId",
                        column: x => x.PrescriptionId,
                        principalTable: "Prescriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bcb4f420-ch4d-g1ga-ab26-c069c6f364e",
                columns: new[] { "ConcurrencyStamp", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bccb4893-f5fe-4ec0-b740-0386fcccb0db", "Гига", "Чад", "AQAAAAIAAYagAAAAELBonSLaIvuacL4BcCcaXcb+il9Hgn1TLiEGK6/O0DLPYfF44dAZ9lYzzCaIZ2f9kg==", "6418dfad-c9b0-48d1-befb-4cc68a22167d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d49db8c9-73e5-4e24-8cb3-80b9ee257ba3",
                columns: new[] { "ConcurrencyStamp", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "daac5167-66f3-4849-a1ec-02063214de0d", "Tifonova", "AQAAAAIAAYagAAAAEKrLRnFV2A7OvYyP3WVVh4qgYt6ynE9DbjH00J087xMUM/cWYC6P388FXJJnPzukkw==", "8ed540a6-df50-4797-8583-6190242344e3" });

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_ProcedureId",
                table: "Prescriptions",
                column: "ProcedureId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_MedicineCategoryId",
                table: "Medicines",
                column: "MedicineCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionMedicines_MedicineId",
                table: "PrescriptionMedicines",
                column: "MedicineId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Procedures_ProcedureId",
                table: "Prescriptions",
                column: "ProcedureId",
                principalTable: "Procedures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
