using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SqlToMySql.Migrations
{
    /// <inheritdoc />
    public partial class nummer1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Cpbs",
                table: "Cpbs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cabgs",
                table: "Cabgs");

            migrationBuilder.RenameTable(
                name: "Cpbs",
                newName: "CPBS");

            migrationBuilder.RenameTable(
                name: "Cabgs",
                newName: "CABGS");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CPBS",
                table: "CPBS",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CABGS",
                table: "CABGS",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CPBS",
                table: "CPBS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CABGS",
                table: "CABGS");

            migrationBuilder.RenameTable(
                name: "CPBS",
                newName: "Cpbs");

            migrationBuilder.RenameTable(
                name: "CABGS",
                newName: "Cabgs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cpbs",
                table: "Cpbs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cabgs",
                table: "Cabgs",
                column: "Id");
        }
    }
}
