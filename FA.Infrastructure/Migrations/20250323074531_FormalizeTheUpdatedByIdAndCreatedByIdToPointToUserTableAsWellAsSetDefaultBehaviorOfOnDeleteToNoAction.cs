using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FA.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FormalizeTheUpdatedByIdAndCreatedByIdToPointToUserTableAsWellAsSetDefaultBehaviorOfOnDeleteToNoAction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Post_PostId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Post_Blog_BlogId",
                table: "Post");

            migrationBuilder.DropForeignKey(
                name: "FK_Post_Category_CategoryId",
                table: "Post");

            migrationBuilder.DropForeignKey(
                name: "FK_PostTag_Post_PostId",
                table: "PostTag");

            migrationBuilder.DropForeignKey(
                name: "FK_PostTag_Tag_TagId",
                table: "PostTag");

            migrationBuilder.CreateIndex(
                name: "IX_User_CreatedById",
                table: "User",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_User_UpdatedById",
                table: "User",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_CreatedById",
                table: "Tag",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_UpdatedById",
                table: "Tag",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Post_CreatedById",
                table: "Post",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Post_UpdatedById",
                table: "Post",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_CreatedById",
                table: "Comment",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_UpdatedById",
                table: "Comment",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Category_CreatedById",
                table: "Category",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Category_UpdatedById",
                table: "Category",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Blog_CreatedById",
                table: "Blog",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Blog_UpdatedById",
                table: "Blog",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Blog_User_CreatedById",
                table: "Blog",
                column: "CreatedById",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Blog_User_UpdatedById",
                table: "Blog",
                column: "UpdatedById",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_User_CreatedById",
                table: "Category",
                column: "CreatedById",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_User_UpdatedById",
                table: "Category",
                column: "UpdatedById",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Post_PostId",
                table: "Comment",
                column: "PostId",
                principalTable: "Post",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_User_CreatedById",
                table: "Comment",
                column: "CreatedById",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_User_UpdatedById",
                table: "Comment",
                column: "UpdatedById",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_Blog_BlogId",
                table: "Post",
                column: "BlogId",
                principalTable: "Blog",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_Category_CategoryId",
                table: "Post",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_User_CreatedById",
                table: "Post",
                column: "CreatedById",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_User_UpdatedById",
                table: "Post",
                column: "UpdatedById",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PostTag_Post_PostId",
                table: "PostTag",
                column: "PostId",
                principalTable: "Post",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PostTag_Tag_TagId",
                table: "PostTag",
                column: "TagId",
                principalTable: "Tag",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tag_User_CreatedById",
                table: "Tag",
                column: "CreatedById",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tag_User_UpdatedById",
                table: "Tag",
                column: "UpdatedById",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_User_User_CreatedById",
                table: "User",
                column: "CreatedById",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_User_User_UpdatedById",
                table: "User",
                column: "UpdatedById",
                principalTable: "User",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blog_User_CreatedById",
                table: "Blog");

            migrationBuilder.DropForeignKey(
                name: "FK_Blog_User_UpdatedById",
                table: "Blog");

            migrationBuilder.DropForeignKey(
                name: "FK_Category_User_CreatedById",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Category_User_UpdatedById",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Post_PostId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_User_CreatedById",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_User_UpdatedById",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Post_Blog_BlogId",
                table: "Post");

            migrationBuilder.DropForeignKey(
                name: "FK_Post_Category_CategoryId",
                table: "Post");

            migrationBuilder.DropForeignKey(
                name: "FK_Post_User_CreatedById",
                table: "Post");

            migrationBuilder.DropForeignKey(
                name: "FK_Post_User_UpdatedById",
                table: "Post");

            migrationBuilder.DropForeignKey(
                name: "FK_PostTag_Post_PostId",
                table: "PostTag");

            migrationBuilder.DropForeignKey(
                name: "FK_PostTag_Tag_TagId",
                table: "PostTag");

            migrationBuilder.DropForeignKey(
                name: "FK_Tag_User_CreatedById",
                table: "Tag");

            migrationBuilder.DropForeignKey(
                name: "FK_Tag_User_UpdatedById",
                table: "Tag");

            migrationBuilder.DropForeignKey(
                name: "FK_User_User_CreatedById",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_User_UpdatedById",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_CreatedById",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_UpdatedById",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Tag_CreatedById",
                table: "Tag");

            migrationBuilder.DropIndex(
                name: "IX_Tag_UpdatedById",
                table: "Tag");

            migrationBuilder.DropIndex(
                name: "IX_Post_CreatedById",
                table: "Post");

            migrationBuilder.DropIndex(
                name: "IX_Post_UpdatedById",
                table: "Post");

            migrationBuilder.DropIndex(
                name: "IX_Comment_CreatedById",
                table: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_Comment_UpdatedById",
                table: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_Category_CreatedById",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Category_UpdatedById",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Blog_CreatedById",
                table: "Blog");

            migrationBuilder.DropIndex(
                name: "IX_Blog_UpdatedById",
                table: "Blog");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Post_PostId",
                table: "Comment",
                column: "PostId",
                principalTable: "Post",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Post_Blog_BlogId",
                table: "Post",
                column: "BlogId",
                principalTable: "Blog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Post_Category_CategoryId",
                table: "Post",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostTag_Post_PostId",
                table: "PostTag",
                column: "PostId",
                principalTable: "Post",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostTag_Tag_TagId",
                table: "PostTag",
                column: "TagId",
                principalTable: "Tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
