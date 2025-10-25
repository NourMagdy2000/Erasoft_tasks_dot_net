using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace To_do_app.Migrations
{
    /// <inheritdoc />
    public partial class updatetaskmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Time",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "file",
                table: "Tasks",
                newName: "File");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Tasks",
                newName: "DeadLine");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "File",
                table: "Tasks",
                newName: "file");

            migrationBuilder.RenameColumn(
                name: "DeadLine",
                table: "Tasks",
                newName: "Date");

            migrationBuilder.AddColumn<string>(
                name: "Time",
                table: "Tasks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
