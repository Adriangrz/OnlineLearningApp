using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineLearningAppApi.Infrastructure.Persistence.Migrations
{
    public partial class AddTeamTableWithRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "27d1f0fe-d2eb-4dd2-b5cf-9a05896c9d9b", "930dec17-2d6e-4740-bb0d-41f23c427396" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "794d7973-a1ab-407e-a9e5-c5fd89818e6d", "930dec17-2d6e-4740-bb0d-41f23c427396" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "27d1f0fe-d2eb-4dd2-b5cf-9a05896c9d9b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "794d7973-a1ab-407e-a9e5-c5fd89818e6d");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "930dec17-2d6e-4740-bb0d-41f23c427396");

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdminId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teams_AspNetUsers_AdminId",
                        column: x => x.AdminId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserTeams",
                columns: table => new
                {
                    TeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTeams", x => new { x.TeamId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserTeams_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTeams_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Teams_AdminId",
                table: "Teams",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTeams_UserId",
                table: "UserTeams",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserTeams");

            migrationBuilder.DropTable(
                name: "Teams");

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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "27d1f0fe-d2eb-4dd2-b5cf-9a05896c9d9b", "f5394ae2-02e0-4188-8697-7b964b410a8f", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "794d7973-a1ab-407e-a9e5-c5fd89818e6d", "a685ac8c-7dbb-4ce0-b40a-4eab907074ae", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "SiteRules", "TwoFactorEnabled", "UserName" },
                values: new object[] { "930dec17-2d6e-4740-bb0d-41f23c427396", 0, "fd5487a1-5975-43d1-a921-165b5e6ce9fb", "admin@test.pl", true, "Antek", "Kowalski", true, null, "ADMIN@TEST.PL", "ADMIN@TEST.PL", "AQAAAAEAACcQAAAAENkT9FZ0aM5ZD8h5rzR3zxqZkmmzUcBTyj/1OVcbzg2UU0CVt5cEauFiUYobVZrW3Q==", null, false, "f922b84e-de5e-4d4d-896f-07132231588e", true, false, "admin@test.pl" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "27d1f0fe-d2eb-4dd2-b5cf-9a05896c9d9b", "930dec17-2d6e-4740-bb0d-41f23c427396" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "794d7973-a1ab-407e-a9e5-c5fd89818e6d", "930dec17-2d6e-4740-bb0d-41f23c427396" });
        }
    }
}
