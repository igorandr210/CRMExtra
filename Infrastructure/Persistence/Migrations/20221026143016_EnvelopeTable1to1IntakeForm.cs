using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class EnvelopeTable1to1IntakeForm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "envelopeid",
                table: "intakeforms",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "envelopes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    IntakeFormId = table.Column<Guid>(type: "uuid", nullable: false),
                    iscustomersign = table.Column<bool>(type: "boolean", nullable: false),
                    isadminsign = table.Column<bool>(type: "boolean", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    createdby = table.Column<Guid>(type: "uuid", nullable: false),
                    lastmodified = table.Column<DateTime>(type: "timestamp", nullable: true),
                    lastmodifiedby = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_envelopes", x => x.id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "intakeforms_envelope_fkey",
                table: "intakeforms");

            migrationBuilder.DropTable(
                name: "envelopes");

            migrationBuilder.DropIndex(
                name: "IX_intakeforms_envelopeid",
                table: "intakeforms");

            migrationBuilder.DropColumn(
                name: "envelopeid",
                table: "intakeforms");
        }
    }
}
