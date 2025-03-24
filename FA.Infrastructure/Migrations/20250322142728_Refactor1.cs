using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FA.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Refactor1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "User",
                newName: "UpdatedById");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "User",
                newName: "CreatedById");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Tag",
                newName: "UpdatedById");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Tag",
                newName: "CreatedById");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Post",
                newName: "UpdatedById");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Post",
                newName: "CreatedById");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Comment",
                newName: "UpdatedById");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Comment",
                newName: "CreatedById");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Category",
                newName: "UpdatedById");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Category",
                newName: "CreatedById");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Blog",
                newName: "UpdatedById");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Blog",
                newName: "CreatedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedById",
                table: "User",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "User",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "UpdatedById",
                table: "Tag",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Tag",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "UpdatedById",
                table: "Post",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Post",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "UpdatedById",
                table: "Comment",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Comment",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "UpdatedById",
                table: "Category",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Category",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "UpdatedById",
                table: "Blog",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Blog",
                newName: "CreatedBy");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
