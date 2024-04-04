using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VeterinarySystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class SomeEdits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Procedures_ProcedureId",
                table: "Prescriptions");

            migrationBuilder.DropIndex(
                name: "IX_Prescriptions_ProcedureId",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "AnimalOwners");

            migrationBuilder.RenameColumn(
                name: "ProcedureId",
                table: "Prescriptions",
                newName: "AnimalId");

            migrationBuilder.RenameColumn(
                name: "AnimalOwnerId",
                table: "AnimalOwners",
                newName: "Id");

            migrationBuilder.AddColumn<int>(
                name: "PrescriptionId",
                table: "Procedures",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Barcode",
                table: "Medicines",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Animals",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Age",
                table: "Animals",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Procedures_PrescriptionId",
                table: "Procedures",
                column: "PrescriptionId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Procedures_Prescriptions_PrescriptionId",
                table: "Procedures",
                column: "PrescriptionId",
                principalTable: "Prescriptions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Animals_AnimalId",
                table: "Prescriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Procedures_Prescriptions_PrescriptionId",
                table: "Procedures");

            migrationBuilder.DropIndex(
                name: "IX_Procedures_PrescriptionId",
                table: "Procedures");

            migrationBuilder.DropIndex(
                name: "IX_Prescriptions_AnimalId",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "PrescriptionId",
                table: "Procedures");

            migrationBuilder.RenameColumn(
                name: "AnimalId",
                table: "Prescriptions",
                newName: "ProcedureId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "AnimalOwners",
                newName: "AnimalOwnerId");

            migrationBuilder.AlterColumn<string>(
                name: "Barcode",
                table: "Medicines",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(13)",
                oldMaxLength: 13);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Animals",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "Age",
                table: "Animals",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AnimalOwners",
                type: "nvarchar(90)",
                maxLength: 90,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_ProcedureId",
                table: "Prescriptions",
                column: "ProcedureId",
                unique: true);

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
