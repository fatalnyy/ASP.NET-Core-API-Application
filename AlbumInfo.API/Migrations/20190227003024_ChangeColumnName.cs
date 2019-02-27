using Microsoft.EntityFrameworkCore.Migrations;

namespace AlbumInfo.API.Migrations
{
    public partial class ChangeColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Desctription",
                table: "Tracks",
                newName: "Description");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Tracks",
                newName: "Desctription");
        }
    }
}
