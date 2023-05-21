using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class TypeOfLossManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "loss_intakeforms_fk",
                table: "intakeforms");

            migrationBuilder.DropIndex(
                name: "IX_intakeforms_typeofloss",
                table: "intakeforms");

            migrationBuilder.DropColumn(
                name: "typeofloss",
                table: "intakeforms");

            migrationBuilder.CreateTable(
                name: "intakeformstypeofloss",
                columns: table => new
                {
                    intakeformid = table.Column<Guid>(type: "uuid", nullable: false),
                    typeoflossid = table.Column<Guid>(type: "uuid", nullable: false),
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    createdby = table.Column<Guid>(type: "uuid", nullable: false),
                    lastmodified = table.Column<DateTime>(type: "timestamp", nullable: true),
                    lastmodifiedby = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_intakeformstypeofloss", x => new { x.intakeformid, x.typeoflossid });
                    table.ForeignKey(
                        name: "FK_intakeformstypeofloss_intakeforms_intakeformid",
                        column: x => x.intakeformid,
                        principalTable: "intakeforms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_intakeformstypeofloss_type_of_loss_typeoflossid",
                        column: x => x.typeoflossid,
                        principalTable: "type_of_loss",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_intakeformstypeofloss_typeoflossid",
                table: "intakeformstypeofloss",
                column: "typeoflossid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "intakeformstypeofloss");

            migrationBuilder.AddColumn<Guid>(
                name: "typeofloss",
                table: "intakeforms",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_intakeforms_typeofloss",
                table: "intakeforms",
                column: "typeofloss");

            migrationBuilder.AddForeignKey(
                name: "loss_intakeforms_fk",
                table: "intakeforms",
                column: "typeofloss",
                principalTable: "type_of_loss",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
