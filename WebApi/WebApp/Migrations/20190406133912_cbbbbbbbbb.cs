using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class cbbbbbbbbb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cups_Users_ownerId",
                table: "Cups");

            migrationBuilder.DropForeignKey(
                name: "FK_Friendships_Users_friend1Id",
                table: "Friendships");

            migrationBuilder.DropForeignKey(
                name: "FK_Friendships_Users_friend2Id",
                table: "Friendships");

            migrationBuilder.DropIndex(
                name: "IX_Friendships_friend1Id",
                table: "Friendships");

            migrationBuilder.DropIndex(
                name: "IX_Friendships_friend2Id",
                table: "Friendships");

            migrationBuilder.DropIndex(
                name: "IX_Cups_ownerId",
                table: "Cups");

            migrationBuilder.RenameColumn(
                name: "friend2Id",
                table: "Friendships",
                newName: "friend2");

            migrationBuilder.RenameColumn(
                name: "friend1Id",
                table: "Friendships",
                newName: "friend1");

            migrationBuilder.RenameColumn(
                name: "ownerId",
                table: "Cups",
                newName: "owner");

            migrationBuilder.AlterColumn<string>(
                name: "friend2",
                table: "Friendships",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "friend1",
                table: "Friendships",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "owner",
                table: "Cups",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "friend2",
                table: "Friendships",
                newName: "friend2Id");

            migrationBuilder.RenameColumn(
                name: "friend1",
                table: "Friendships",
                newName: "friend1Id");

            migrationBuilder.RenameColumn(
                name: "owner",
                table: "Cups",
                newName: "ownerId");

            migrationBuilder.AlterColumn<string>(
                name: "friend2Id",
                table: "Friendships",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "friend1Id",
                table: "Friendships",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ownerId",
                table: "Cups",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_friend1Id",
                table: "Friendships",
                column: "friend1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_friend2Id",
                table: "Friendships",
                column: "friend2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Cups_ownerId",
                table: "Cups",
                column: "ownerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cups_Users_ownerId",
                table: "Cups",
                column: "ownerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Friendships_Users_friend1Id",
                table: "Friendships",
                column: "friend1Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Friendships_Users_friend2Id",
                table: "Friendships",
                column: "friend2Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
