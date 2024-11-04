using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IDonEnglist.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PublicIdInMedia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PublicId",
                table: "Medias",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "Medias");
        }
    }
}
