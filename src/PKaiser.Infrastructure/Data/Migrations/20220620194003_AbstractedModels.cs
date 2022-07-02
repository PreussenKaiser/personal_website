using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PKaiser.Infrastructure.Data.Migrations;

public partial class AbstractedModels : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "ImageUrl",
            table: "Blogs");

        migrationBuilder.RenameColumn(
            name: "Description",
            table: "Projects",
            newName: "Details");

        migrationBuilder.AddColumn<string>(
            name: "Content",
            table: "Projects",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "");

        migrationBuilder.AlterColumn<string>(
            name: "Title",
            table: "Blogs",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "nvarchar(max)",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "Content",
            table: "Blogs",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "nvarchar(max)",
            oldNullable: true);

        migrationBuilder.AddColumn<string>(
            name: "Details",
            table: "Blogs",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Content",
            table: "Projects");

        migrationBuilder.DropColumn(
            name: "Details",
            table: "Blogs");

        migrationBuilder.RenameColumn(
            name: "Details",
            table: "Projects",
            newName: "Description");

        migrationBuilder.AlterColumn<string>(
            name: "Title",
            table: "Blogs",
            type: "nvarchar(max)",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "nvarchar(max)");

        migrationBuilder.AlterColumn<string>(
            name: "Content",
            table: "Blogs",
            type: "nvarchar(max)",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "nvarchar(max)");

        migrationBuilder.AddColumn<string>(
            name: "ImageUrl",
            table: "Blogs",
            type: "nvarchar(max)",
            nullable: true);
    }
}
