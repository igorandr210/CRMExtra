using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class AddAttachedProfileFk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "assignedTo",
                table: "assignmenttasks",
                newName: "assignedtoid");

            migrationBuilder.RenameIndex(
                name: "IX_assignmenttasks_assignedTo",
                table: "assignmenttasks",
                newName: "IX_assignmenttasks_assignedtoid");

            migrationBuilder.AddColumn<Guid>(
                name: "attachedprofileid",
                table: "assignmenttasks",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_assignmenttasks_attachedprofileid",
                table: "assignmenttasks",
                column: "attachedprofileid");

            migrationBuilder.AddForeignKey(
                name: "assignmenttask_attachedprofiledata_fk",
                table: "assignmenttasks",
                column: "attachedprofileid",
                principalTable: "profiledata",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "assignmenttask_attachedprofiledata_fk",
                table: "assignmenttasks");

            migrationBuilder.DropIndex(
                name: "IX_assignmenttasks_attachedprofileid",
                table: "assignmenttasks");

            migrationBuilder.DropColumn(
                name: "attachedprofileid",
                table: "assignmenttasks");

            migrationBuilder.RenameColumn(
                name: "assignedtoid",
                table: "assignmenttasks",
                newName: "assignedTo");

            migrationBuilder.RenameIndex(
                name: "IX_assignmenttasks_assignedtoid",
                table: "assignmenttasks",
                newName: "IX_assignmenttasks_assignedTo");
        }
    }
}
