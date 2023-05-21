using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class EditEnvelopes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created",
                table: "envelopes");

            migrationBuilder.DropColumn(
                name: "createdby",
                table: "envelopes");

            migrationBuilder.DropColumn(
                name: "lastmodified",
                table: "envelopes");

            migrationBuilder.DropColumn(
                name: "lastmodifiedby",
                table: "envelopes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "created",
                table: "envelopes",
                type: "timestamp",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "createdby",
                table: "envelopes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "lastmodified",
                table: "envelopes",
                type: "timestamp",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "lastmodifiedby",
                table: "envelopes",
                type: "uuid",
                nullable: true);
        }
    }
}
