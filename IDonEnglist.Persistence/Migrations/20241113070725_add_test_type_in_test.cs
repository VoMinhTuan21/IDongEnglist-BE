using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IDonEnglist.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class add_test_type_in_test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tests_TestTypes_TestTypeId",
                table: "Tests");

            migrationBuilder.AlterColumn<int>(
                name: "TestTypeId",
                table: "Tests",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_TestTypes_TestTypeId",
                table: "Tests",
                column: "TestTypeId",
                principalTable: "TestTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tests_TestTypes_TestTypeId",
                table: "Tests");

            migrationBuilder.AlterColumn<int>(
                name: "TestTypeId",
                table: "Tests",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_TestTypes_TestTypeId",
                table: "Tests",
                column: "TestTypeId",
                principalTable: "TestTypes",
                principalColumn: "Id");
        }
    }
}
