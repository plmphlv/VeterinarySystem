using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VeterinarySystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class newChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_StaffType_StaffTypeId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Animals_AnimalId",
                table: "Prescriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Procedures_Prescriptions_PrescriptionId",
                table: "Procedures");

            migrationBuilder.DropTable(
                name: "StaffType");

            migrationBuilder.DropIndex(
                name: "IX_Procedures_PrescriptionId",
                table: "Procedures");

            migrationBuilder.DropIndex(
                name: "IX_Prescriptions_AnimalId",
                table: "Prescriptions");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_StaffTypeId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PrescriptionId",
                table: "Procedures");

            migrationBuilder.DropColumn(
                name: "StaffTypeId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "AnimalId",
                table: "Prescriptions",
                newName: "ProcedureId");

            migrationBuilder.RenameColumn(
                name: "OwnerLastName",
                table: "AnimalOwners",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "OwnerFirstName",
                table: "AnimalOwners",
                newName: "FirstName");

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
                name: "AppointmentDesctiption",
                table: "Appointments",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "AnimalOwners",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(13)",
                oldMaxLength: 13);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "LastName",
                table: "AnimalOwners",
                newName: "OwnerLastName");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "AnimalOwners",
                newName: "OwnerFirstName");

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

            migrationBuilder.AddColumn<int>(
                name: "StaffTypeId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "AppointmentDesctiption",
                table: "Appointments",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "AnimalOwners",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.CreateTable(
                name: "StaffType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPositionName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Procedures_PrescriptionId",
                table: "Procedures",
                column: "PrescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_AnimalId",
                table: "Prescriptions",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_StaffTypeId",
                table: "AspNetUsers",
                column: "StaffTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_StaffType_StaffTypeId",
                table: "AspNetUsers",
                column: "StaffTypeId",
                principalTable: "StaffType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
    }
}
