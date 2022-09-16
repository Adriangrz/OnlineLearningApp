using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineLearningAppApi.Database.Migrations
{
    public partial class AddTeamImageAndUserImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "9cb6265d-f347-4e10-84bc-9d48bc421122", "3488f163-a7a2-4d0c-9c3d-d82a709072d3" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "d1ee9e82-aed8-490d-9728-f3143c5a0634", "3488f163-a7a2-4d0c-9c3d-d82a709072d3" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9cb6265d-f347-4e10-84bc-9d48bc421122");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d1ee9e82-aed8-490d-9728-f3143c5a0634");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3488f163-a7a2-4d0c-9c3d-d82a709072d3");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Teams");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Teams",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TeamsImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamsImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamsImages_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserImages_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5323e08c-9d65-420c-8dc9-74157d6ba5bf", "aa523d39-eb85-401e-a3d9-1402b0c4cfa2", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c2844634-bd48-4093-abbf-22cd08d19635", "a6afa29b-ebca-4ed8-bbfa-3e78071a4bb6", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "ImagePath", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "SiteRules", "TwoFactorEnabled", "UserName" },
                values: new object[] { "dbe8547f-9f93-4c57-a396-b7d6cdc55069", 0, "40b49be8-14de-45c5-b4d7-46aaa577ba3a", "admin@test.pl", true, "Antek", null, "Kowalski", true, null, "ADMIN@TEST.PL", "ADMIN@TEST.PL", "AQAAAAEAACcQAAAAEIFpLTPTyZdQ7g8ZW7aUjKKgTfYYZQpZlaQscCHs+8L85sAhSg+L4vQ245B/e4yneA==", null, false, "1df0e673-26d7-4ff5-83fc-6cb02e6881a0", true, false, "admin@test.pl" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "5323e08c-9d65-420c-8dc9-74157d6ba5bf", "dbe8547f-9f93-4c57-a396-b7d6cdc55069" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "c2844634-bd48-4093-abbf-22cd08d19635", "dbe8547f-9f93-4c57-a396-b7d6cdc55069" });

            migrationBuilder.CreateIndex(
                name: "IX_TeamsImages_TeamId",
                table: "TeamsImages",
                column: "TeamId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserImages_UserId",
                table: "UserImages",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeamsImages");

            migrationBuilder.DropTable(
                name: "UserImages");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "5323e08c-9d65-420c-8dc9-74157d6ba5bf", "dbe8547f-9f93-4c57-a396-b7d6cdc55069" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "c2844634-bd48-4093-abbf-22cd08d19635", "dbe8547f-9f93-4c57-a396-b7d6cdc55069" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5323e08c-9d65-420c-8dc9-74157d6ba5bf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c2844634-bd48-4093-abbf-22cd08d19635");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dbe8547f-9f93-4c57-a396-b7d6cdc55069");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Teams",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9cb6265d-f347-4e10-84bc-9d48bc421122", "8f4c68c7-e906-431e-9ff1-8b5a9243be7a", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d1ee9e82-aed8-490d-9728-f3143c5a0634", "d1ecb90c-128f-491e-bdec-dd4aaf34b7a5", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "SiteRules", "TwoFactorEnabled", "UserName" },
                values: new object[] { "3488f163-a7a2-4d0c-9c3d-d82a709072d3", 0, "194d7de2-f7d0-4622-964a-de30ece670af", "admin@test.pl", true, "Antek", "Kowalski", true, null, "ADMIN@TEST.PL", "ADMIN@TEST.PL", "AQAAAAEAACcQAAAAEMS5n+kxnhA0oLGRzyxIW84YIuBX0ATRS/cqYMj9lHn9N0zrYM3hsslVVLuAgv5KEw==", null, false, "2e773c27-4b7e-4248-bc88-a5fef0fa7f33", true, false, "admin@test.pl" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "9cb6265d-f347-4e10-84bc-9d48bc421122", "3488f163-a7a2-4d0c-9c3d-d82a709072d3" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "d1ee9e82-aed8-490d-9728-f3143c5a0634", "3488f163-a7a2-4d0c-9c3d-d82a709072d3" });
        }
    }
}
