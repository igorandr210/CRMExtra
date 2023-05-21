using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class EnvelopeIntakeFormFixConn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "intakeforms_envelope_fkey",
                table: "intakeforms");

            migrationBuilder.DropIndex(
                name: "IX_intakeforms_envelopeid",
                table: "intakeforms");

            migrationBuilder.RenameColumn(
                name: "envelopeid",
                table: "intakeforms",
                newName: "EnvelopeId");

            migrationBuilder.RenameColumn(
                name: "IntakeFormId",
                table: "envelopes",
                newName: "intakeformid");

            migrationBuilder.CreateIndex(
                name: "IX_envelopes_intakeformid",
                table: "envelopes",
                column: "intakeformid",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "intakeforms_envelope_fkey",
                table: "envelopes",
                column: "intakeformid",
                principalTable: "intakeforms",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "intakeforms_envelope_fkey",
                table: "envelopes");

            migrationBuilder.DropIndex(
                name: "IX_envelopes_intakeformid",
                table: "envelopes");

            migrationBuilder.RenameColumn(
                name: "EnvelopeId",
                table: "intakeforms",
                newName: "envelopeid");

            migrationBuilder.RenameColumn(
                name: "intakeformid",
                table: "envelopes",
                newName: "IntakeFormId");

            migrationBuilder.CreateIndex(
                name: "IX_intakeforms_envelopeid",
                table: "intakeforms",
                column: "envelopeid",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "intakeforms_envelope_fkey",
                table: "intakeforms",
                column: "envelopeid",
                principalTable: "envelopes",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
