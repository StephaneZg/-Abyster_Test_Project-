using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AbysterTestProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class SnapshotWithValidBoolType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "isActive",
                table: "Users",
                type: "Boolean",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "INTEGER",
                oldDefaultValue: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "isActive",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "Boolean",
                oldDefaultValue: true);
        }
    }
}
