using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AbysterTestProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class SnapshotWithUserRoleRelationUpdate6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "initialized",
                table: "Users",
                type: "Boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "initialized",
                table: "Users");
        }
    }
}
