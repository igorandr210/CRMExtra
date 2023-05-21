using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class IntakeFormTimespanToDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "roofreplacedate",
                table: "intakeforms",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "effactivepolicystartdate",
                table: "intakeforms",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "effactivepolicyenddate",
                table: "intakeforms",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime[]>(
                name: "dateofloss",
                table: "intakeforms",
                type: "date[]",
                nullable: true,
                oldClrType: typeof(DateTime[]),
                oldType: "timestamp[]",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "claimfilleddate",
                table: "claims",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "roofreplacedate",
                table: "intakeforms",
                type: "timestamp",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "effactivepolicystartdate",
                table: "intakeforms",
                type: "timestamp",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "effactivepolicyenddate",
                table: "intakeforms",
                type: "timestamp",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime[]>(
                name: "dateofloss",
                table: "intakeforms",
                type: "timestamp[]",
                nullable: true,
                oldClrType: typeof(DateTime[]),
                oldType: "date[]",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "claimfilleddate",
                table: "claims",
                type: "timestamp",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);
        }
    }
}
