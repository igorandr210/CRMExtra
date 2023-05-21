using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class AssignmentTasksTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "assignmenttasks",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    statuscode = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "character varying", nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false),
                    assignedTo = table.Column<Guid>(type: "uuid", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    createdby = table.Column<Guid>(type: "uuid", nullable: false),
                    lastmodified = table.Column<DateTime>(type: "timestamp", nullable: true),
                    lastmodifiedby = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assignmenttasks", x => x.id);
                    table.ForeignKey(
                        name: "assignmenttask_assigneeprofiledata_fk",
                        column: x => x.assignedTo,
                        principalTable: "profiledata",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "assignmenttask_creatorprofiledata_fk",
                        column: x => x.createdby,
                        principalTable: "profiledata",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_assignmenttasks_assignedTo",
                table: "assignmenttasks",
                column: "assignedTo");

            migrationBuilder.CreateIndex(
                name: "IX_assignmenttasks_createdby",
                table: "assignmenttasks",
                column: "createdby");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "assignmenttasks");
        }
    }
}
