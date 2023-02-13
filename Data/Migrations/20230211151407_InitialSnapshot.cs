using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AbysterTestProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialSnapshot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    categoryid = table.Column<int>(name: "category_id", type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    libelle = table.Column<string>(type: "TEXT", nullable: false),
                    createdAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    createdBy = table.Column<string>(type: "TEXT", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updatedBy = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.categoryid);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    roleid = table.Column<int>(name: "role_id", type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    libelle = table.Column<string>(type: "TEXT", nullable: false),
                    createdAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    createdBy = table.Column<string>(type: "TEXT", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updatedBy = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.roleid);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    userid = table.Column<int>(name: "user_id", type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    firstName = table.Column<string>(type: "TEXT", nullable: false),
                    lastName = table.Column<string>(type: "TEXT", nullable: false),
                    emailAddress = table.Column<string>(type: "TEXT", nullable: false),
                    password = table.Column<string>(type: "TEXT", nullable: false),
                    isActive = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true),
                    token = table.Column<string>(type: "TEXT", nullable: true),
                    refreshToken = table.Column<string>(type: "TEXT", nullable: true),
                    refreshTokenExpireTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    createdAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    createdBy = table.Column<string>(type: "TEXT", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updatedBy = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.userid);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    accountid = table.Column<int>(name: "account_id", type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    montant = table.Column<long>(type: "INTEGER", nullable: false),
                    categoryid = table.Column<int>(name: "category_id", type: "INTEGER", nullable: false),
                    userid = table.Column<int>(name: "user_id", type: "INTEGER", nullable: false),
                    createdAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    createdBy = table.Column<string>(type: "TEXT", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updatedBy = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.accountid);
                    table.ForeignKey(
                        name: "FK_Accounts_Categories_category_id",
                        column: x => x.categoryid,
                        principalTable: "Categories",
                        principalColumn: "category_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Accounts_Users_user_id",
                        column: x => x.userid,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleUser",
                columns: table => new
                {
                    rolesId = table.Column<int>(type: "INTEGER", nullable: false),
                    usersId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleUser", x => new { x.rolesId, x.usersId });
                    table.ForeignKey(
                        name: "FK_RoleUser_Role_rolesId",
                        column: x => x.rolesId,
                        principalTable: "Role",
                        principalColumn: "role_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleUser_Users_usersId",
                        column: x => x.usersId,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountJournals",
                columns: table => new
                {
                    accountjournalid = table.Column<int>(name: "account_journal_id", type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    amount = table.Column<double>(type: "REAL", nullable: false),
                    balanceBefore = table.Column<double>(type: "REAL", nullable: false),
                    balanceAfter = table.Column<double>(type: "REAL", nullable: false),
                    accountid = table.Column<int>(name: "account_id", type: "INTEGER", nullable: false),
                    createdAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    createdBy = table.Column<string>(type: "TEXT", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updatedBy = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountJournals", x => x.accountjournalid);
                    table.ForeignKey(
                        name: "FK_AccountJournals_Accounts_account_id",
                        column: x => x.accountid,
                        principalTable: "Accounts",
                        principalColumn: "account_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountJournals_account_id",
                table: "AccountJournals",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_category_id",
                table: "Accounts",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_user_id",
                table: "Accounts",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_RoleUser_usersId",
                table: "RoleUser",
                column: "usersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountJournals");

            migrationBuilder.DropTable(
                name: "RoleUser");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
