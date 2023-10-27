using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TikTokApi.Migrations
{
    /// <inheritdoc />
    public partial class ChangeCommentAnswersTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                newName: "CommentAnswers");

            migrationBuilder.RenameIndex(
                name: "IX_CommentsAnswers_CommentId",
                table: "CommentAnswers",
                newName: "IX_CommentAnswers_CommentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommentAnswers",
                table: "CommentAnswers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentAnswers_Comments_CommentId",
                table: "CommentAnswers",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_CommentAnswers_CommentAnswerId",
                table: "Comments",
                column: "CommentAnswerId",
                principalTable: "CommentAnswers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentAnswers_Comments_CommentId",
                table: "CommentAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_CommentAnswers_CommentAnswerId",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommentAnswers",
                table: "CommentAnswers");

            migrationBuilder.RenameTable(
                name: "CommentAnswers",
                newName: "CommentsAnswers");

            migrationBuilder.RenameIndex(
                name: "IX_CommentAnswers_CommentId",
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
    }
}
