using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AppTemplate.Domain.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "departments",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    createdby = table.Column<int>(type: "integer", nullable: true),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updatedby = table.Column<int>(type: "integer", nullable: true),
                    updatedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    statusid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_departments", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "designations",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    createdby = table.Column<int>(type: "integer", nullable: true),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updatedby = table.Column<int>(type: "integer", nullable: true),
                    updatedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    statusid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_designations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "menus",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    code = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    url = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    sortid = table.Column<short>(type: "smallint", nullable: true),
                    menuicon = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ismodule = table.Column<bool>(type: "boolean", nullable: false),
                    isparent = table.Column<bool>(type: "boolean", nullable: false),
                    parentid = table.Column<int>(type: "integer", nullable: true),
                    showmenu = table.Column<bool>(type: "boolean", nullable: true),
                    isvisible = table.Column<bool>(type: "boolean", nullable: true),
                    levelat = table.Column<byte>(type: "smallint", nullable: true),
                    createdby = table.Column<int>(type: "integer", nullable: true),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updatedby = table.Column<int>(type: "integer", nullable: true),
                    updatedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    statusid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_menus", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "usertypes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    createdby = table.Column<int>(type: "integer", nullable: true),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updatedby = table.Column<int>(type: "integer", nullable: true),
                    updatedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    statusid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_usertypes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fullname = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    phonenumber = table.Column<string>(type: "text", nullable: false),
                    username = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    securitystamp = table.Column<string>(type: "text", nullable: false),
                    islocked = table.Column<bool>(type: "boolean", nullable: false),
                    logintrycount = table.Column<int>(type: "integer", nullable: false),
                    isadmin = table.Column<bool>(type: "boolean", nullable: true),
                    usertypeid = table.Column<int>(type: "integer", nullable: false),
                    departmentid = table.Column<int>(type: "integer", nullable: true),
                    designationid = table.Column<int>(type: "integer", nullable: true),
                    createdby = table.Column<int>(type: "integer", nullable: true),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updatedby = table.Column<int>(type: "integer", nullable: true),
                    updatedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    statusid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                    table.ForeignKey(
                        name: "fk_users_departments_departmentid",
                        column: x => x.departmentid,
                        principalTable: "departments",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_users_designations_designationid",
                        column: x => x.designationid,
                        principalTable: "designations",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_users_usertypes_usertypeid",
                        column: x => x.usertypeid,
                        principalTable: "usertypes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    applicationuserid = table.Column<int>(type: "integer", nullable: true),
                    createdby = table.Column<int>(type: "integer", nullable: true),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updatedby = table.Column<int>(type: "integer", nullable: true),
                    updatedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    statusid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_roles", x => x.id);
                    table.ForeignKey(
                        name: "fk_roles_users_applicationuserid",
                        column: x => x.applicationuserid,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "rolemenupermissions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    roleid = table.Column<int>(type: "integer", nullable: false),
                    menuid = table.Column<int>(type: "integer", nullable: false),
                    createdby = table.Column<int>(type: "integer", nullable: true),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updatedby = table.Column<int>(type: "integer", nullable: true),
                    updatedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    statusid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_rolemenupermissions", x => x.id);
                    table.ForeignKey(
                        name: "fk_rolemenupermissions_menus_menuid",
                        column: x => x.menuid,
                        principalTable: "menus",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_rolemenupermissions_roles_roleid",
                        column: x => x.roleid,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_rolemenupermissions_menuid",
                table: "rolemenupermissions",
                column: "menuid");

            migrationBuilder.CreateIndex(
                name: "ix_rolemenupermissions_roleid",
                table: "rolemenupermissions",
                column: "roleid");

            migrationBuilder.CreateIndex(
                name: "ix_roles_applicationuserid",
                table: "roles",
                column: "applicationuserid");

            migrationBuilder.CreateIndex(
                name: "ix_users_departmentid",
                table: "users",
                column: "departmentid");

            migrationBuilder.CreateIndex(
                name: "ix_users_designationid",
                table: "users",
                column: "designationid");

            migrationBuilder.CreateIndex(
                name: "ix_users_usertypeid",
                table: "users",
                column: "usertypeid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "rolemenupermissions");

            migrationBuilder.DropTable(
                name: "menus");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "departments");

            migrationBuilder.DropTable(
                name: "designations");

            migrationBuilder.DropTable(
                name: "usertypes");
        }
    }
}
