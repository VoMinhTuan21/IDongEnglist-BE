using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IDonEnglist.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "Permissions",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "ParentId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "ParentId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "ParentId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "ParentId",
                value: 5);

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Code", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Description", "Name", "ParentId", "UpdatedBy", "UpdatedDate" },
                values: new object[] { 5, "category", null, null, null, null, null, "Category", null, null, null });

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_ParentId",
                table: "Permissions",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_Permissions_ParentId",
                table: "Permissions",
                column: "ParentId",
                principalTable: "Permissions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_Permissions_ParentId",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_ParentId",
                table: "Permissions");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Permissions");
        }
    }
}
