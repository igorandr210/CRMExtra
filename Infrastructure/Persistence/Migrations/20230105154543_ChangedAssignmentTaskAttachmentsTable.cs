using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class ChangedAssignmentTaskAttachmentsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_assingmenttaskattachments_documents_documentid",
                table: "assingmenttaskattachments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_assingmenttaskattachments",
                table: "assingmenttaskattachments");

            migrationBuilder.RenameColumn(
                name: "documentid",
                table: "assingmenttaskattachments",
                newName: "id");

            migrationBuilder.AddColumn<string>(
                name: "filename",
                table: "assingmenttaskattachments",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "key",
                table: "assingmenttaskattachments",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_assingmenttaskattachments",
                table: "assingmenttaskattachments",
                column: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_assingmenttaskattachments",
                table: "assingmenttaskattachments");

            migrationBuilder.DropColumn(
                name: "filename",
                table: "assingmenttaskattachments");

            migrationBuilder.DropColumn(
                name: "key",
                table: "assingmenttaskattachments");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "assingmenttaskattachments",
                newName: "documentid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_assingmenttaskattachments",
                table: "assingmenttaskattachments",
                columns: new[] { "documentid", "assingmenttaskid" });

            migrationBuilder.AddForeignKey(
                name: "FK_assingmenttaskattachments_documents_documentid",
                table: "assingmenttaskattachments",
                column: "documentid",
                principalTable: "documents",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
