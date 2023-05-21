using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class SelfOccupation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "selfoccupation",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    createdby = table.Column<Guid>(type: "uuid", nullable: false),
                    lastmodified = table.Column<DateTime>(type: "timestamp", nullable: true),
                    lastmodifiedby = table.Column<Guid>(type: "uuid", nullable: true),
                    value = table.Column<string>(type: "character varying", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_selfoccupation", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_intakeforms_selfoccupation",
                table: "intakeforms",
                column: "selfoccupation");

            migrationBuilder.AddForeignKey(
                name: "selfoccupation_intakeforms_fk",
                table: "intakeforms",
                column: "selfoccupation",
                principalTable: "selfoccupation",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "selfoccupation_intakeforms_fk",
                table: "intakeforms");

            migrationBuilder.DropTable(
                name: "selfoccupation");

            migrationBuilder.DropIndex(
                name: "IX_intakeforms_selfoccupation",
                table: "intakeforms");
        }
    }
}
