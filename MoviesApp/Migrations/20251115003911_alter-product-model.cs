using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoviesApp.Migrations
{
    /// <inheritdoc />
    public partial class alterproductmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "status",
                table: "Movies",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "Movies",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "img",
                table: "Movies",
                newName: "Img");

            migrationBuilder.AlterColumn<string>(
                name: "Img",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Movies",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Movies",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "Img",
                table: "Movies",
                newName: "img");

            migrationBuilder.AlterColumn<string>(
                name: "img",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
