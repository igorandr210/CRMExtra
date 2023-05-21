using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class TaskTypeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "statuscode",
                table: "assignmenttasks");

            migrationBuilder.AddColumn<Guid>(
                name: "TaskTypeId",
                table: "assignmenttasks",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "tasktype",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    lastmodified = table.Column<DateTime>(type: "timestamp", nullable: true),
                    lastmodifiedby = table.Column<Guid>(type: "uuid", nullable: true),
                    value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tasktype", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_assignmenttasks_TaskTypeId",
                table: "assignmenttasks",
                column: "TaskTypeId");

            migrationBuilder.AddForeignKey(
                name: "assignmenttask_tasktype_fk",
                table: "assignmenttasks",
                column: "TaskTypeId",
                principalTable: "tasktype",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "assignmenttask_tasktype_fk",
                table: "assignmenttasks");

            migrationBuilder.DropTable(
                name: "tasktype");

            migrationBuilder.DropIndex(
                name: "IX_assignmenttasks_TaskTypeId",
                table: "assignmenttasks");

            migrationBuilder.DropColumn(
                name: "TaskTypeId",
                table: "assignmenttasks");

            migrationBuilder.AddColumn<string>(
                name: "statuscode",
                table: "assignmenttasks",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
