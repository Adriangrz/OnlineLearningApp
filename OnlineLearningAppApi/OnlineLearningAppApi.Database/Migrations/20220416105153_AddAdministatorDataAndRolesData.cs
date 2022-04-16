using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineLearningAppApi.Database.Migrations
{
    public partial class AddAdministatorDataAndRolesData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
