using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineLearningAppApi.Database.Migrations
{
    public partial class AddRefreshToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("2cf42a2f-a3b8-427b-8034-fc25a689778f"), new Guid("395f8412-ea9d-4ee8-b517-137865d19d29") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("a68a9ae0-2f5d-4ebc-af46-1d2e10ef8bcd"), new Guid("395f8412-ea9d-4ee8-b517-137865d19d29") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2cf42a2f-a3b8-427b-8034-fc25a689778f"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a68a9ae0-2f5d-4ebc-af46-1d2e10ef8bcd"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("395f8412-ea9d-4ee8-b517-137865d19d29"));

            migrationBuilder.CreateTable(
                name: "Tokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("4fac61d6-91e8-4a32-84ba-21f1ba48ca46"), "80a26af8-78be-4c7c-895e-aa81942f6eab", "User", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("61d3accf-9084-4817-8a45-3fc6921177d7"), "dc418353-1612-4821-a878-3a2cea7fa9bb", "Admin", null });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "SiteRules", "Surname", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("8a5169b9-6dd5-4fd6-96a3-add37061c79f"), 0, "f0512a06-a3a1-43a8-a8b9-4ccdfd89c637", "admin@test.pl", true, true, null, "ADMIN@TEST.PL", "ANTEK", "AQAAAAEAACcQAAAAEPVp+Mgu1yot/bXjtAf+Kbn5Jq9r1oUIEvON+T2JFQek3lG2LzEa1yj7lW0ODJ22Sw==", null, false, "19a8013e-7ca5-42ec-a5b0-d0584eea74d4", true, "Kowalski", false, "Antek" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("4fac61d6-91e8-4a32-84ba-21f1ba48ca46"), new Guid("8a5169b9-6dd5-4fd6-96a3-add37061c79f") });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("61d3accf-9084-4817-8a45-3fc6921177d7"), new Guid("8a5169b9-6dd5-4fd6-96a3-add37061c79f") });

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_UserId",
                table: "Tokens",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tokens");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("4fac61d6-91e8-4a32-84ba-21f1ba48ca46"), new Guid("8a5169b9-6dd5-4fd6-96a3-add37061c79f") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("61d3accf-9084-4817-8a45-3fc6921177d7"), new Guid("8a5169b9-6dd5-4fd6-96a3-add37061c79f") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4fac61d6-91e8-4a32-84ba-21f1ba48ca46"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("61d3accf-9084-4817-8a45-3fc6921177d7"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8a5169b9-6dd5-4fd6-96a3-add37061c79f"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("2cf42a2f-a3b8-427b-8034-fc25a689778f"), "16fff452-1018-49da-a896-e3041c260a26", "User", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("a68a9ae0-2f5d-4ebc-af46-1d2e10ef8bcd"), "3896e971-d622-4af4-9c82-b3dd9edb7a7c", "Admin", null });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "SiteRules", "Surname", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("395f8412-ea9d-4ee8-b517-137865d19d29"), 0, "a7148b2b-da0a-4940-b71d-1c9e29cdded7", "admin@test.pl", true, true, null, "ADMIN@TEST.PL", "ANTEK", "AQAAAAEAACcQAAAAELks1/O8TF22axOKL6F/aAenBUBWFYWgAZ5//QsDwaipEY0Z62qOYAL1ZPei7JSoqw==", null, false, "62e06331-7146-4dc3-a328-6890e816d482", true, "Kowalski", false, "Antek" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("2cf42a2f-a3b8-427b-8034-fc25a689778f"), new Guid("395f8412-ea9d-4ee8-b517-137865d19d29") });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("a68a9ae0-2f5d-4ebc-af46-1d2e10ef8bcd"), new Guid("395f8412-ea9d-4ee8-b517-137865d19d29") });
        }
    }
}
