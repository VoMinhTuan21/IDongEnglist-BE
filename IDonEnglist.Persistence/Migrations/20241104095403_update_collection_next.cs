using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IDonEnglist.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class update_collection_next : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collections_Medias_ThumbnailId",
                table: "Collections");

            migrationBuilder.DropIndex(
                name: "IX_Collections_ThumbnailId",
                table: "Collections");

            migrationBuilder.AlterColumn<int>(
                name: "ThumbnailId",
                table: "Collections",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Collections_ThumbnailId",
                table: "Collections",
                column: "ThumbnailId",
                unique: true,
                filter: "[ThumbnailId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Collections_Medias_ThumbnailId",
                table: "Collections",
                column: "ThumbnailId",
                principalTable: "Medias",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collections_Medias_ThumbnailId",
                table: "Collections");

            migrationBuilder.DropIndex(
                name: "IX_Collections_ThumbnailId",
                table: "Collections");

            migrationBuilder.AlterColumn<int>(
                name: "ThumbnailId",
                table: "Collections",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Collections_ThumbnailId",
                table: "Collections",
                column: "ThumbnailId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Collections_Medias_ThumbnailId",
                table: "Collections",
                column: "ThumbnailId",
                principalTable: "Medias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
