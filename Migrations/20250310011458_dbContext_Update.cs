using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatForum.Migrations
{
    /// <inheritdoc />
    public partial class dbContext_Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discussion_AspNetUsers_ApplicationUserId",
                table: "Discussion");

            migrationBuilder.AddForeignKey(
                name: "FK_Discussion_AspNetUsers_ApplicationUserId",
                table: "Discussion",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discussion_AspNetUsers_ApplicationUserId",
                table: "Discussion");

            migrationBuilder.AddForeignKey(
                name: "FK_Discussion_AspNetUsers_ApplicationUserId",
                table: "Discussion",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
