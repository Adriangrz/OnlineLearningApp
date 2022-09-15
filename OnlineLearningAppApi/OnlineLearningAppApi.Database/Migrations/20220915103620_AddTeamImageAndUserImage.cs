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
                name: "TeamImage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    TeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamImage_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserImage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserImage_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2103f81c-eba2-42cf-8e43-7a0462221b3e", "e754c461-3a1e-4a95-85d9-6b6dedacb787", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bb87eb81-67ac-499c-9191-22595fcc1d17", "8cfef5f4-21fc-4492-9524-4321da6f0838", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "ImagePath", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "SiteRules", "TwoFactorEnabled", "UserName" },
                values: new object[] { "60e06d9c-af33-42a3-82b1-513578255f70", 0, "d75f7fe8-3558-4ade-9b6c-fca3a966dc7c", "admin@test.pl", true, "Antek", null, "Kowalski", true, null, "ADMIN@TEST.PL", "ADMIN@TEST.PL", "AQAAAAEAACcQAAAAEKIbhAknPR2UpHAx5xXfjKNQEI2dAcVw7XooafTZgcflcXw9pLhMAyRzhqq2wpsGnw==", null, false, "1d7ab4ad-c52c-4bee-b2bc-7e103298461f", true, false, "admin@test.pl" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "2103f81c-eba2-42cf-8e43-7a0462221b3e", "60e06d9c-af33-42a3-82b1-513578255f70" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "bb87eb81-67ac-499c-9191-22595fcc1d17", "60e06d9c-af33-42a3-82b1-513578255f70" });

            migrationBuilder.CreateIndex(
                name: "IX_TeamImage_TeamId",
                table: "TeamImage",
                column: "TeamId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserImage_UserId",
                table: "UserImage",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeamImage");

            migrationBuilder.DropTable(
                name: "UserImage");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2103f81c-eba2-42cf-8e43-7a0462221b3e", "60e06d9c-af33-42a3-82b1-513578255f70" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "bb87eb81-67ac-499c-9191-22595fcc1d17", "60e06d9c-af33-42a3-82b1-513578255f70" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2103f81c-eba2-42cf-8e43-7a0462221b3e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bb87eb81-67ac-499c-9191-22595fcc1d17");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "60e06d9c-af33-42a3-82b1-513578255f70");

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
