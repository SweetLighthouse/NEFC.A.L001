using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FA.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixSomeFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rate",
                table: "Post");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Category",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Blog",
                type: "nvarchar(2014)",
                maxLength: 2014,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Blog");

            migrationBuilder.AddColumn<double>(
                name: "Rate",
                table: "Post",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
