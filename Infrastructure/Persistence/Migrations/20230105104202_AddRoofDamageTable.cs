using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class AddRoofDamageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "roofdamage",
                table: "intakeforms",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "roofdamage",
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
                    table.PrimaryKey("PK_roofdamage", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_intakeforms_roofdamage",
                table: "intakeforms",
                column: "roofdamage",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "roofdamage_intakeforms_fk",
                table: "intakeforms",
                column: "roofdamage",
                principalTable: "roofdamage",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "roofdamage_intakeforms_fk",
                table: "intakeforms");

            migrationBuilder.DropTable(
                name: "roofdamage");

            migrationBuilder.DropIndex(
                name: "IX_intakeforms_roofdamage",
                table: "intakeforms");

            migrationBuilder.DropColumn(
                name: "roofdamage",
                table: "intakeforms");
        }
    }
}
