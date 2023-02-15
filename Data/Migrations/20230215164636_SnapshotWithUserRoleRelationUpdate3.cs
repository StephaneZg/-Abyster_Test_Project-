using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AbysterTestProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class SnapshotWithUserRoleRelationUpdate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleUser");

            migrationBuilder.AddColumn<int>(
                name: "rolesId",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_rolesId",
                table: "Users",
                column: "rolesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_rolesId",
                table: "Users",
                column: "rolesId",
                principalTable: "Roles",
                principalColumn: "role_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_rolesId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_rolesId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "rolesId",
                table: "Users");

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
                        name: "FK_RoleUser_Roles_rolesId",
                        column: x => x.rolesId,
                        principalTable: "Roles",
                        principalColumn: "role_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleUser_Users_usersId",
                        column: x => x.usersId,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoleUser_usersId",
                table: "RoleUser",
                column: "usersId");
        }
    }
}
