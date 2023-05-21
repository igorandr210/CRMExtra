using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class ChangedTaskTypeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TaskTypeId",
                table: "assignmenttasks",
                newName: "tasktypeid");

            migrationBuilder.RenameIndex(
                name: "IX_assignmenttasks_TaskTypeId",
                table: "assignmenttasks",
                newName: "IX_assignmenttasks_tasktypeid");

            migrationBuilder.AddColumn<string>(
                name: "customersenvelopelink",
                table: "envelopes",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "customersenvelopelink",
                table: "envelopes");

            migrationBuilder.RenameColumn(
                name: "tasktypeid",
                table: "assignmenttasks",
                newName: "TaskTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_assignmenttasks_tasktypeid",
                table: "assignmenttasks",
                newName: "IX_assignmenttasks_TaskTypeId");
        }
    }
}
