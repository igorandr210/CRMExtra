using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Infrastructure.Persistence.Migrations
{
    public partial class AddedIsSubmitedColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "issubmited",
                table: "profiledata",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "issubmited",
                table: "profiledata");
        }
    }
}
