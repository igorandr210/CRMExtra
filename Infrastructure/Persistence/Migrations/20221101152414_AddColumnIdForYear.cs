using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class AddColumnIdForYear : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "idforyear",
                table: "profiledata",
                type: "text",
                nullable: true);
            migrationBuilder.Sql("create or replace function id_for_year() returns trigger language plpgsql as $$ declare yearvalue integer:= date_part('year', new.created); seqnextvalue integer; seqname text := 'seq_' || yearvalue; begin IF seqname IS NOT NULL THEN execute 'CREATE SEQUENCE IF NOT EXISTS ' || seqname; execute('SELECT nextval($1)') into seqnextvalue using seqname::regclass; new.idforyear = seqnextvalue|| '-' ||yearvalue; END IF; return new; end; $$;");
            migrationBuilder.Sql("create or replace trigger tr_profiledata_beforeinsert before insert on public.profiledata for each row execute procedure id_for_year();");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP TRIGGER tr_profiledata_beforeinsert on profiledata");
            migrationBuilder.Sql("DROP FUNCTION id_for_year();");
            migrationBuilder.DropColumn(
                name: "idforyear",
                table: "profiledata");
           
        }
    }
}
