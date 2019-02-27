using Microsoft.EntityFrameworkCore.Migrations;

namespace AlbumInfo.API.Migrations
{
    public partial class AlbumInfoDBAddTrackDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Desctription",
                table: "Tracks",
                maxLength: 200,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Desctription",
                table: "Tracks");
        }
    }
}
