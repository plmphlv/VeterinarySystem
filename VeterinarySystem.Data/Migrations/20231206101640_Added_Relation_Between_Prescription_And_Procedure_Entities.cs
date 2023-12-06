using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VeterinarySystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class Added_Relation_Between_Prescription_And_Procedure_Entities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_StaffMembers_StaffMemberId",
                table: "Prescriptions");

            migrationBuilder.DropIndex(
                name: "IX_Prescriptions_StaffMemberId",
                table: "Prescriptions");

            migrationBuilder.RenameColumn(
                name: "StaffMemberId",
                table: "Prescriptions",
                newName: "ProcedureId");

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
                principalColumn: "ProcedureId",
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

            migrationBuilder.RenameColumn(
                name: "ProcedureId",
                table: "Prescriptions",
                newName: "StaffMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_StaffMemberId",
                table: "Prescriptions",
                column: "StaffMemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_StaffMembers_StaffMemberId",
                table: "Prescriptions",
                column: "StaffMemberId",
                principalTable: "StaffMembers",
                principalColumn: "StaffMemberId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
