using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FA.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUnusedFieldFromPostCategoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostCategory_User_CreatorId",
                table: "PostCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_PostCategory_User_UpdatorId",
                table: "PostCategory");

            migrationBuilder.DropIndex(
                name: "IX_PostCategory_CreatorId",
                table: "PostCategory");

            migrationBuilder.DropIndex(
                name: "IX_PostCategory_UpdatorId",
                table: "PostCategory");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "PostCategory");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "PostCategory");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PostCategory");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "PostCategory");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "PostCategory");

            migrationBuilder.DropColumn(
                name: "UpdatorId",
                table: "PostCategory");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "PostCategory",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "PostCategory",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "PostCategory",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "PostCategory",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "PostCategory",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatorId",
                table: "PostCategory",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_PostCategory_CreatorId",
                table: "PostCategory",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_PostCategory_UpdatorId",
                table: "PostCategory",
                column: "UpdatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostCategory_User_CreatorId",
                table: "PostCategory",
                column: "CreatorId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PostCategory_User_UpdatorId",
                table: "PostCategory",
                column: "UpdatorId",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
