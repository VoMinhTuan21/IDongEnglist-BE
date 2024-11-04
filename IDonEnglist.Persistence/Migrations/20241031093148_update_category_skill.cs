using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IDonEnglist.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class update_category_skill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TestTypes_CategorySkillId",
                table: "TestTypes");

            migrationBuilder.CreateIndex(
                name: "IX_TestTypes_CategorySkillId",
                table: "TestTypes",
                column: "CategorySkillId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TestTypes_CategorySkillId",
                table: "TestTypes");

            migrationBuilder.CreateIndex(
                name: "IX_TestTypes_CategorySkillId",
                table: "TestTypes",
                column: "CategorySkillId",
                unique: true);
        }
    }
}
