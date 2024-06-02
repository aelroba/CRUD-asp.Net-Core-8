using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApiApp.Migrations
{
    /// <inheritdoc />
    public partial class CommentOneToOneRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4961d55c-e64b-489e-8715-a3d4713169f4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5c9815df-b592-4931-bbac-bba1faa31ece");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Comments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "631e669f-6b0e-4ff8-ac66-6155dce9fceb", null, "User", "USER" },
                    { "982efe5d-8dcb-421c-8ea4-43e1f64e0378", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ApplicationUserId",
                table: "Comments",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_ApplicationUserId",
                table: "Comments",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_ApplicationUserId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_ApplicationUserId",
                table: "Comments");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "631e669f-6b0e-4ff8-ac66-6155dce9fceb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "982efe5d-8dcb-421c-8ea4-43e1f64e0378");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Comments");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4961d55c-e64b-489e-8715-a3d4713169f4", null, "User", "USER" },
                    { "5c9815df-b592-4931-bbac-bba1faa31ece", null, "Admin", "ADMIN" }
                });
        }
    }
}
