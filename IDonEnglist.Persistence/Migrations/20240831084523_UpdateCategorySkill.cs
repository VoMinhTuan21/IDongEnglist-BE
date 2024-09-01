using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IDonEnglist.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCategorySkill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategorySkills_TestTypes_TestTypeId",
                table: "CategorySkills");

            migrationBuilder.DropIndex(
                name: "IX_CategorySkills_TestTypeId",
                table: "CategorySkills");

            migrationBuilder.DropColumn(
                name: "TestTypeId",
                table: "CategorySkills");

            migrationBuilder.AddColumn<int>(
                name: "CategorySkillId",
                table: "TestTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TestTypes_CategorySkillId",
                table: "TestTypes",
                column: "CategorySkillId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TestTypes_CategorySkills_CategorySkillId",
                table: "TestTypes",
                column: "CategorySkillId",
                principalTable: "CategorySkills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestTypes_CategorySkills_CategorySkillId",
                table: "TestTypes");

            migrationBuilder.DropIndex(
                name: "IX_TestTypes_CategorySkillId",
                table: "TestTypes");

            migrationBuilder.DropColumn(
                name: "CategorySkillId",
                table: "TestTypes");

            migrationBuilder.AddColumn<int>(
                name: "TestTypeId",
                table: "CategorySkills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CategorySkills_TestTypeId",
                table: "CategorySkills",
                column: "TestTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategorySkills_TestTypes_TestTypeId",
                table: "CategorySkills",
                column: "TestTypeId",
                principalTable: "TestTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
