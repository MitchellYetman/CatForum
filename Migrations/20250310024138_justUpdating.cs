using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatForum.Migrations
{
    /// <inheritdoc />
    public partial class justUpdating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Discussion_DiscussionId",
                table: "Comment");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Discussion_DiscussionId",
                table: "Comment",
                column: "DiscussionId",
                principalTable: "Discussion",
                principalColumn: "DiscussionId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Discussion_DiscussionId",
                table: "Comment");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Discussion_DiscussionId",
                table: "Comment",
                column: "DiscussionId",
                principalTable: "Discussion",
                principalColumn: "DiscussionId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
