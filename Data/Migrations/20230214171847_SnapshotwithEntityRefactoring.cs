using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AbysterTestProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class SnapshotwithEntityRefactoring : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Categories_category_id",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleUser_Role_rolesId",
                table: "RoleUser");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_category_id",
                table: "Accounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Role",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "category_id",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "balanceAfter",
                table: "AccountJournals");

            migrationBuilder.DropColumn(
                name: "balanceBefore",
                table: "AccountJournals");

            migrationBuilder.RenameTable(
                name: "Role",
                newName: "Roles");

            migrationBuilder.RenameColumn(
                name: "montant",
                table: "Accounts",
                newName: "balance");

            migrationBuilder.AddColumn<int>(
                name: "category_id",
                table: "AccountJournals",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "user_id",
                table: "AccountJournals",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_AccountJournals_category_id",
                table: "AccountJournals",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_AccountJournals_user_id",
                table: "AccountJournals",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountJournals_Categories_category_id",
                table: "AccountJournals",
                column: "category_id",
                principalTable: "Categories",
                principalColumn: "category_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountJournals_Users_user_id",
                table: "AccountJournals",
                column: "user_id",
                principalTable: "Users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleUser_Roles_rolesId",
                table: "RoleUser",
                column: "rolesId",
                principalTable: "Roles",
                principalColumn: "role_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountJournals_Categories_category_id",
                table: "AccountJournals");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountJournals_Users_user_id",
                table: "AccountJournals");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleUser_Roles_rolesId",
                table: "RoleUser");

            migrationBuilder.DropIndex(
                name: "IX_AccountJournals_category_id",
                table: "AccountJournals");

            migrationBuilder.DropIndex(
                name: "IX_AccountJournals_user_id",
                table: "AccountJournals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "category_id",
                table: "AccountJournals");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "AccountJournals");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "Role");

            migrationBuilder.RenameColumn(
                name: "balance",
                table: "Accounts",
                newName: "montant");

            migrationBuilder.AddColumn<int>(
                name: "category_id",
                table: "Accounts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "balanceAfter",
                table: "AccountJournals",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "balanceBefore",
                table: "AccountJournals",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Role",
                table: "Role",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_category_id",
                table: "Accounts",
                column: "category_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Categories_category_id",
                table: "Accounts",
                column: "category_id",
                principalTable: "Categories",
                principalColumn: "category_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleUser_Role_rolesId",
                table: "RoleUser",
                column: "rolesId",
                principalTable: "Role",
                principalColumn: "role_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
