using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TikTokApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCommentAnswerModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_CommentAnswers_CommentAnswerId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_CommentAnswerId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "CommentAnswerId",
                table: "Comments");

            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "CommentAnswers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "CommentAnswers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_CommentAnswers_AuthorId",
                table: "CommentAnswers",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentAnswers_Users_AuthorId",
                table: "CommentAnswers",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentAnswers_Users_AuthorId",
                table: "CommentAnswers");

            migrationBuilder.DropIndex(
                name: "IX_CommentAnswers_AuthorId",
                table: "CommentAnswers");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "CommentAnswers");

            migrationBuilder.DropColumn(
                name: "Text",
                table: "CommentAnswers");

            migrationBuilder.AddColumn<int>(
                name: "CommentAnswerId",
                table: "Comments",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CommentAnswerId",
                table: "Comments",
                column: "CommentAnswerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_CommentAnswers_CommentAnswerId",
                table: "Comments",
                column: "CommentAnswerId",
                principalTable: "CommentAnswers",
                principalColumn: "Id");
        }
    }
}
