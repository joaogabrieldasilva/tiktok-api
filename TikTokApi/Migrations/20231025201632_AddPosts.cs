using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TikTokApi.Migrations
{
    /// <inheritdoc />
    public partial class AddPosts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentAnswer_Comments_CommentId",
                table: "CommentAnswer");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_CommentAnswer_CommentAnswerId",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommentAnswer",
                table: "CommentAnswer");

            migrationBuilder.RenameTable(
                name: "CommentAnswer",
                newName: "CommentsAnswers");

            migrationBuilder.RenameIndex(
                name: "IX_CommentAnswer_CommentId",
                table: "CommentsAnswers",
                newName: "IX_CommentsAnswers_CommentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommentsAnswers",
                table: "CommentsAnswers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_CommentsAnswers_CommentAnswerId",
                table: "Comments",
                column: "CommentAnswerId",
                principalTable: "CommentsAnswers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentsAnswers_Comments_CommentId",
                table: "CommentsAnswers",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_CommentsAnswers_CommentAnswerId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentsAnswers_Comments_CommentId",
                table: "CommentsAnswers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommentsAnswers",
                table: "CommentsAnswers");

            migrationBuilder.RenameTable(
                name: "CommentsAnswers",
                newName: "CommentAnswer");

            migrationBuilder.RenameIndex(
                name: "IX_CommentsAnswers_CommentId",
                table: "CommentAnswer",
                newName: "IX_CommentAnswer_CommentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommentAnswer",
                table: "CommentAnswer",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentAnswer_Comments_CommentId",
                table: "CommentAnswer",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_CommentAnswer_CommentAnswerId",
                table: "Comments",
                column: "CommentAnswerId",
                principalTable: "CommentAnswer",
                principalColumn: "Id");
        }
    }
}
