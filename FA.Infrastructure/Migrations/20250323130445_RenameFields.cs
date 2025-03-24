using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FA.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "FK_Comment_User_CreatedById",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_User_UpdatedById",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Post_User_CreatedById",
                table: "Post");

            migrationBuilder.DropForeignKey(
                name: "FK_Post_User_UpdatedById",
                table: "Post");

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

            migrationBuilder.RenameColumn(
                name: "UpdatedById",
                table: "User",
                newName: "UpdatorId");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "User",
                newName: "CreatorId");

            migrationBuilder.RenameIndex(
                name: "IX_User_UpdatedById",
                table: "User",
                newName: "IX_User_UpdatorId");

            migrationBuilder.RenameIndex(
                name: "IX_User_CreatedById",
                table: "User",
                newName: "IX_User_CreatorId");

            migrationBuilder.RenameColumn(
                name: "UpdatedById",
                table: "Tag",
                newName: "UpdatorId");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Tag",
                newName: "CreatorId");

            migrationBuilder.RenameIndex(
                name: "IX_Tag_UpdatedById",
                table: "Tag",
                newName: "IX_Tag_UpdatorId");

            migrationBuilder.RenameIndex(
                name: "IX_Tag_CreatedById",
                table: "Tag",
                newName: "IX_Tag_CreatorId");

            migrationBuilder.RenameColumn(
                name: "UpdatedById",
                table: "Post",
                newName: "UpdatorId");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Post",
                newName: "CreatorId");

            migrationBuilder.RenameIndex(
                name: "IX_Post_UpdatedById",
                table: "Post",
                newName: "IX_Post_UpdatorId");

            migrationBuilder.RenameIndex(
                name: "IX_Post_CreatedById",
                table: "Post",
                newName: "IX_Post_CreatorId");

            migrationBuilder.RenameColumn(
                name: "UpdatedById",
                table: "Comment",
                newName: "UpdatorId");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Comment",
                newName: "CreatorId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_UpdatedById",
                table: "Comment",
                newName: "IX_Comment_UpdatorId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_CreatedById",
                table: "Comment",
                newName: "IX_Comment_CreatorId");

            migrationBuilder.RenameColumn(
                name: "UpdatedById",
                table: "Category",
                newName: "UpdatorId");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Category",
                newName: "CreatorId");

            migrationBuilder.RenameIndex(
                name: "IX_Category_UpdatedById",
                table: "Category",
                newName: "IX_Category_UpdatorId");

            migrationBuilder.RenameIndex(
                name: "IX_Category_CreatedById",
                table: "Category",
                newName: "IX_Category_CreatorId");

            migrationBuilder.RenameColumn(
                name: "UpdatedById",
                table: "Blog",
                newName: "UpdatorId");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Blog",
                newName: "CreatorId");

            migrationBuilder.RenameIndex(
                name: "IX_Blog_UpdatedById",
                table: "Blog",
                newName: "IX_Blog_UpdatorId");

            migrationBuilder.RenameIndex(
                name: "IX_Blog_CreatedById",
                table: "Blog",
                newName: "IX_Blog_CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blog_User_CreatorId",
                table: "Blog",
                column: "CreatorId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Blog_User_UpdatorId",
                table: "Blog",
                column: "UpdatorId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_User_CreatorId",
                table: "Category",
                column: "CreatorId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_User_UpdatorId",
                table: "Category",
                column: "UpdatorId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_User_CreatorId",
                table: "Comment",
                column: "CreatorId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_User_UpdatorId",
                table: "Comment",
                column: "UpdatorId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_User_CreatorId",
                table: "Post",
                column: "CreatorId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_User_UpdatorId",
                table: "Post",
                column: "UpdatorId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tag_User_CreatorId",
                table: "Tag",
                column: "CreatorId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tag_User_UpdatorId",
                table: "Tag",
                column: "UpdatorId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_User_User_CreatorId",
                table: "User",
                column: "CreatorId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_User_User_UpdatorId",
                table: "User",
                column: "UpdatorId",
                principalTable: "User",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blog_User_CreatorId",
                table: "Blog");

            migrationBuilder.DropForeignKey(
                name: "FK_Blog_User_UpdatorId",
                table: "Blog");

            migrationBuilder.DropForeignKey(
                name: "FK_Category_User_CreatorId",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Category_User_UpdatorId",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_User_CreatorId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_User_UpdatorId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Post_User_CreatorId",
                table: "Post");

            migrationBuilder.DropForeignKey(
                name: "FK_Post_User_UpdatorId",
                table: "Post");

            migrationBuilder.DropForeignKey(
                name: "FK_Tag_User_CreatorId",
                table: "Tag");

            migrationBuilder.DropForeignKey(
                name: "FK_Tag_User_UpdatorId",
                table: "Tag");

            migrationBuilder.DropForeignKey(
                name: "FK_User_User_CreatorId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_User_UpdatorId",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "UpdatorId",
                table: "User",
                newName: "UpdatedById");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "User",
                newName: "CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_User_UpdatorId",
                table: "User",
                newName: "IX_User_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_User_CreatorId",
                table: "User",
                newName: "IX_User_CreatedById");

            migrationBuilder.RenameColumn(
                name: "UpdatorId",
                table: "Tag",
                newName: "UpdatedById");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "Tag",
                newName: "CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Tag_UpdatorId",
                table: "Tag",
                newName: "IX_Tag_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Tag_CreatorId",
                table: "Tag",
                newName: "IX_Tag_CreatedById");

            migrationBuilder.RenameColumn(
                name: "UpdatorId",
                table: "Post",
                newName: "UpdatedById");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "Post",
                newName: "CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Post_UpdatorId",
                table: "Post",
                newName: "IX_Post_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Post_CreatorId",
                table: "Post",
                newName: "IX_Post_CreatedById");

            migrationBuilder.RenameColumn(
                name: "UpdatorId",
                table: "Comment",
                newName: "UpdatedById");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "Comment",
                newName: "CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_UpdatorId",
                table: "Comment",
                newName: "IX_Comment_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_CreatorId",
                table: "Comment",
                newName: "IX_Comment_CreatedById");

            migrationBuilder.RenameColumn(
                name: "UpdatorId",
                table: "Category",
                newName: "UpdatedById");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "Category",
                newName: "CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Category_UpdatorId",
                table: "Category",
                newName: "IX_Category_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Category_CreatorId",
                table: "Category",
                newName: "IX_Category_CreatedById");

            migrationBuilder.RenameColumn(
                name: "UpdatorId",
                table: "Blog",
                newName: "UpdatedById");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "Blog",
                newName: "CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Blog_UpdatorId",
                table: "Blog",
                newName: "IX_Blog_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Blog_CreatorId",
                table: "Blog",
                newName: "IX_Blog_CreatedById");

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
    }
}
