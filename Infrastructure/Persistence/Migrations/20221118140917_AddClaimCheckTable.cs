using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class AddClaimCheckTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "claimamount",
                table: "documents");

            migrationBuilder.DropColumn(
                name: "claimdate",
                table: "documents");

            migrationBuilder.DropColumn(
                name: "dateofpostmark",
                table: "documents");

            migrationBuilder.CreateTable(
                name: "claimcheck",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    intakeformid = table.Column<Guid>(type: "uuid", nullable: false),
                    documentid = table.Column<Guid>(type: "uuid", nullable: true),
                    claimamount = table.Column<long>(type: "bigint", nullable: true),
                    claimdate = table.Column<DateTime>(type: "timestamp", nullable: true),
                    dateofpostmark = table.Column<DateTime>(type: "timestamp", nullable: true),
                    created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    createdby = table.Column<Guid>(type: "uuid", nullable: false),
                    lastmodified = table.Column<DateTime>(type: "timestamp", nullable: true),
                    lastmodifiedby = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_claimcheck", x => x.id);
                    table.ForeignKey(
                        name: "claimcheck_documents_fkey",
                        column: x => x.documentid,
                        principalTable: "documents",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "claimcheck_intakeform_fkey",
                        column: x => x.intakeformid,
                        principalTable: "intakeforms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_claimcheck_documentid",
                table: "claimcheck",
                column: "documentid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_claimcheck_intakeformid",
                table: "claimcheck",
                column: "intakeformid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "claimcheck");

            migrationBuilder.AddColumn<long>(
                name: "claimamount",
                table: "documents",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "claimdate",
                table: "documents",
                type: "timestamp",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "dateofpostmark",
                table: "documents",
                type: "timestamp",
                nullable: true);
        }
    }
}
