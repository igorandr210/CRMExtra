using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class ChangedJobCronSettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "claimfilled",
                table: "jobcronsettings",
                newName: "cronsettingstring");

            migrationBuilder.AddColumn<bool>(
                name: "isactive",
                table: "jobcronsettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isactive",
                table: "jobcronsettings");

            migrationBuilder.RenameColumn(
                name: "cronsettingstring",
                table: "jobcronsettings",
                newName: "claimfilled");
        }
    }
}
