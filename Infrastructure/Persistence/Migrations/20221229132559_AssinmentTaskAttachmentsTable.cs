using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class AssinmentTaskAttachmentsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "assingmenttaskattachments",
                columns: table => new
                {
                    assingmenttaskid = table.Column<Guid>(type: "uuid", nullable: false),
                    documentid = table.Column<Guid>(type: "uuid", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    createdby = table.Column<Guid>(type: "uuid", nullable: false),
                    lastmodified = table.Column<DateTime>(type: "timestamp", nullable: true),
                    lastmodifiedby = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assingmenttaskattachments", x => new { x.documentid, x.assingmenttaskid });
                    table.ForeignKey(
                        name: "FK_assingmenttaskattachments_assignmenttasks_assingmenttaskid",
                        column: x => x.assingmenttaskid,
                        principalTable: "assignmenttasks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_assingmenttaskattachments_documents_documentid",
                        column: x => x.documentid,
                        principalTable: "documents",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_assingmenttaskattachments_assingmenttaskid",
                table: "assingmenttaskattachments",
                column: "assingmenttaskid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "assingmenttaskattachments");
        }
    }
}
