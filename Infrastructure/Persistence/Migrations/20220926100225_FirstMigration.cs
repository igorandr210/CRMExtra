using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Infrastructure.Persistence.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "insurance_companies",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    createdby = table.Column<Guid>(type: "uuid", nullable: false),
                    lastmodified = table.Column<DateTime>(type: "timestamp", nullable: true),
                    lastmodifiedby = table.Column<Guid>(type: "uuid", nullable: true),
                    value = table.Column<string>(type: "character varying", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_insurance_companies", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "states",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    createdby = table.Column<Guid>(type: "uuid", nullable: false),
                    lastmodified = table.Column<DateTime>(type: "timestamp", nullable: true),
                    lastmodifiedby = table.Column<Guid>(type: "uuid", nullable: true),
                    value = table.Column<string>(type: "character varying", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_states", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "storms",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    createdby = table.Column<Guid>(type: "uuid", nullable: false),
                    lastmodified = table.Column<DateTime>(type: "timestamp", nullable: true),
                    lastmodifiedby = table.Column<Guid>(type: "uuid", nullable: true),
                    value = table.Column<string>(type: "character varying", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_storms", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "type_of_loss",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    createdby = table.Column<Guid>(type: "uuid", nullable: false),
                    lastmodified = table.Column<DateTime>(type: "timestamp", nullable: true),
                    lastmodifiedby = table.Column<Guid>(type: "uuid", nullable: true),
                    value = table.Column<string>(type: "character varying", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_type_of_loss", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "type_of_occupation",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    createdby = table.Column<Guid>(type: "uuid", nullable: false),
                    lastmodified = table.Column<DateTime>(type: "timestamp", nullable: true),
                    lastmodifiedby = table.Column<Guid>(type: "uuid", nullable: true),
                    value = table.Column<string>(type: "character varying", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_type_of_occupation", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "profiledata",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    email = table.Column<string>(type: "character varying", nullable: false),
                    insuredname = table.Column<string>(type: "text", nullable: true),
                    addressline1 = table.Column<string>(type: "text", nullable: true),
                    phone = table.Column<string>(type: "text", nullable: true),
                    state = table.Column<Guid>(type: "uuid", nullable: true),
                    city = table.Column<string>(type: "text", nullable: true),
                    zip = table.Column<string>(type: "text", nullable: true),
                    created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    createdby = table.Column<Guid>(type: "uuid", nullable: false),
                    lastmodified = table.Column<DateTime>(type: "timestamp", nullable: true),
                    lastmodifiedby = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_profiledata", x => x.id);
                    table.ForeignKey(
                        name: "profiledata_fk",
                        column: x => x.state,
                        principalTable: "states",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "intakeforms",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    userid = table.Column<Guid>(type: "uuid", nullable: false),
                    dateofloss = table.Column<DateTime[]>(type: "timestamp[]", nullable: true),
                    insurancecompany = table.Column<Guid>(type: "uuid", nullable: true),
                    policynumber = table.Column<string>(type: "character varying", nullable: false),
                    effactivepolicystartdate = table.Column<DateTime>(type: "timestamp", nullable: true),
                    effactivepolicyenddate = table.Column<DateTime>(type: "timestamp", nullable: true),
                    dateoflosswithin = table.Column<bool>(type: "boolean", nullable: true),
                    propertyowner = table.Column<bool>(type: "boolean", nullable: true),
                    ispropertyoccupied = table.Column<bool>(type: "boolean", nullable: true),
                    typeofoccupation = table.Column<Guid>(type: "uuid", nullable: true),
                    typeofloss = table.Column<Guid>(type: "uuid", nullable: true),
                    selfoccupation = table.Column<Guid>(type: "uuid", nullable: true),
                    primaryresidence = table.Column<bool>(type: "boolean", nullable: true),
                    mailaddress = table.Column<string>(type: "character varying", nullable: true),
                    stormname = table.Column<Guid>(type: "uuid", nullable: true),
                    propertyaccess = table.Column<string>(type: "character varying", nullable: true),
                    exteriordamage = table.Column<bool>(type: "boolean", nullable: false),
                    interiordamage = table.Column<bool>(type: "boolean", nullable: false),
                    contentsdamage = table.Column<bool>(type: "boolean", nullable: false),
                    roofreplacedate = table.Column<DateTime>(type: "timestamp", nullable: true),
                    roofdamage = table.Column<bool>(type: "boolean", nullable: false),
                    emergencyservice = table.Column<bool>(type: "boolean", nullable: false),
                    isdraft = table.Column<bool>(type: "boolean", nullable: false),
                    isfilled = table.Column<bool>(type: "boolean", nullable: false),
                    isconfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    createdby = table.Column<Guid>(type: "uuid", nullable: false),
                    lastmodified = table.Column<DateTime>(type: "timestamp", nullable: true),
                    lastmodifiedby = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_intakeforms", x => x.id);
                    table.ForeignKey(
                        name: "company_intakeforms_fk",
                        column: x => x.insurancecompany,
                        principalTable: "insurance_companies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "loss_intakeforms_fk",
                        column: x => x.typeofloss,
                        principalTable: "type_of_loss",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "occupation_intakeforms_fk",
                        column: x => x.typeofoccupation,
                        principalTable: "type_of_occupation",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "oprofiledata_intakeforms_fkey",
                        column: x => x.userid,
                        principalTable: "profiledata",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "storms_intakeforms_fk",
                        column: x => x.stormname,
                        principalTable: "storms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "claims",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    intakeformid = table.Column<Guid>(type: "uuid", nullable: false),
                    claimfilled = table.Column<bool>(type: "boolean", nullable: false),
                    claimnumber = table.Column<string>(type: "character varying", nullable: true),
                    claiminfo = table.Column<string>(type: "character varying", nullable: true),
                    claimfilleddate = table.Column<DateTime>(type: "timestamp", nullable: true),
                    claimchecks = table.Column<bool>(type: "boolean", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    createdby = table.Column<Guid>(type: "uuid", nullable: false),
                    lastmodified = table.Column<DateTime>(type: "timestamp", nullable: true),
                    lastmodifiedby = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_claims", x => x.id);
                    table.ForeignKey(
                        name: "claims_intakeform_fkey",
                        column: x => x.intakeformid,
                        principalTable: "intakeforms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "documents",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    intakeformid = table.Column<Guid>(type: "uuid", nullable: false),
                    claimamount = table.Column<long>(type: "bigint", nullable: true),
                    claimdate = table.Column<DateTime>(type: "timestamp", nullable: true),
                    key = table.Column<string>(type: "text", nullable: true),
                    filename = table.Column<string>(type: "text", nullable: true),
                    documenttype = table.Column<int>(type: "int", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    createdby = table.Column<Guid>(type: "uuid", nullable: false),
                    lastmodified = table.Column<DateTime>(type: "timestamp", nullable: true),
                    lastmodifiedby = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_documents", x => x.id);
                    table.ForeignKey(
                        name: "documents_intakeform_fkey",
                        column: x => x.intakeformid,
                        principalTable: "intakeforms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "insuranceagencies",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    intakeformid = table.Column<Guid>(type: "uuid", nullable: false),
                    insuranceagency = table.Column<string>(type: "character varying", nullable: true),
                    nameofagent = table.Column<string>(type: "character varying", nullable: true),
                    agentphone = table.Column<string>(type: "character varying", nullable: true),
                    agentemail = table.Column<string>(type: "character varying", nullable: true),
                    created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    createdby = table.Column<Guid>(type: "uuid", nullable: false),
                    lastmodified = table.Column<DateTime>(type: "timestamp", nullable: true),
                    lastmodifiedby = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_insuranceagencies", x => x.id);
                    table.ForeignKey(
                        name: "insuranceagencies_intakeform_fkey",
                        column: x => x.intakeformid,
                        principalTable: "intakeforms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "mortgages",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    intakeformid = table.Column<Guid>(type: "uuid", nullable: false),
                    mortgage = table.Column<bool>(type: "boolean", nullable: false),
                    companyname = table.Column<string>(type: "character varying", nullable: true),
                    loannumber = table.Column<string>(type: "character varying", nullable: true),
                    refferedby = table.Column<string>(type: "character varying", nullable: true),
                    contractorname = table.Column<string>(type: "character varying", nullable: true),
                    contractoremail = table.Column<string>(type: "character varying", nullable: true),
                    created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    createdby = table.Column<Guid>(type: "uuid", nullable: false),
                    lastmodified = table.Column<DateTime>(type: "timestamp", nullable: true),
                    lastmodifiedby = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mortgages", x => x.id);
                    table.ForeignKey(
                        name: "mortgages_intakeform_fkey",
                        column: x => x.intakeformid,
                        principalTable: "intakeforms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "spouses",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    intakeformid = table.Column<Guid>(type: "uuid", nullable: false),
                    isspouse = table.Column<bool>(type: "boolean", nullable: false),
                    name = table.Column<string>(type: "character varying", nullable: true),
                    email = table.Column<string>(type: "character varying", nullable: true),
                    phone = table.Column<string>(type: "character varying", nullable: true),
                    created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    createdby = table.Column<Guid>(type: "uuid", nullable: false),
                    lastmodified = table.Column<DateTime>(type: "timestamp", nullable: true),
                    lastmodifiedby = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_spouses", x => x.id);
                    table.ForeignKey(
                        name: "spouses_intakeform_fkey",
                        column: x => x.intakeformid,
                        principalTable: "intakeforms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_claims_intakeformid",
                table: "claims",
                column: "intakeformid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_documents_intakeformid",
                table: "documents",
                column: "intakeformid");

            migrationBuilder.CreateIndex(
                name: "IX_insuranceagencies_intakeformid",
                table: "insuranceagencies",
                column: "intakeformid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_intakeforms_insurancecompany",
                table: "intakeforms",
                column: "insurancecompany");

            migrationBuilder.CreateIndex(
                name: "IX_intakeforms_stormname",
                table: "intakeforms",
                column: "stormname");

            migrationBuilder.CreateIndex(
                name: "IX_intakeforms_typeofloss",
                table: "intakeforms",
                column: "typeofloss");

            migrationBuilder.CreateIndex(
                name: "IX_intakeforms_typeofoccupation",
                table: "intakeforms",
                column: "typeofoccupation");

            migrationBuilder.CreateIndex(
                name: "IX_intakeforms_userid",
                table: "intakeforms",
                column: "userid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_mortgages_intakeformid",
                table: "mortgages",
                column: "intakeformid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_profiledata_state",
                table: "profiledata",
                column: "state");

            migrationBuilder.CreateIndex(
                name: "IX_spouses_intakeformid",
                table: "spouses",
                column: "intakeformid",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "claims");

            migrationBuilder.DropTable(
                name: "documents");

            migrationBuilder.DropTable(
                name: "insuranceagencies");

            migrationBuilder.DropTable(
                name: "mortgages");

            migrationBuilder.DropTable(
                name: "spouses");

            migrationBuilder.DropTable(
                name: "intakeforms");

            migrationBuilder.DropTable(
                name: "insurance_companies");

            migrationBuilder.DropTable(
                name: "type_of_loss");

            migrationBuilder.DropTable(
                name: "type_of_occupation");

            migrationBuilder.DropTable(
                name: "profiledata");

            migrationBuilder.DropTable(
                name: "storms");

            migrationBuilder.DropTable(
                name: "states");
        }
    }
}
