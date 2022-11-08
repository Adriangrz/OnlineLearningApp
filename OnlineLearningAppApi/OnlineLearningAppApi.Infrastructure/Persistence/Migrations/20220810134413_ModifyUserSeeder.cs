using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineLearningAppApi.Infrastructure.Persistence.Migrations
{
    public partial class ModifyUserSeeder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3c96d978-37ba-43ae-845d-7584f37e6ad7", "45792f46-ca3a-4cc7-a4a5-f110f7837e9d" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "ad22f956-1969-4f11-a989-cca903257c68", "45792f46-ca3a-4cc7-a4a5-f110f7837e9d" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3c96d978-37ba-43ae-845d-7584f37e6ad7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad22f956-1969-4f11-a989-cca903257c68");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "45792f46-ca3a-4cc7-a4a5-f110f7837e9d");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3c96d978-37ba-43ae-845d-7584f37e6ad7", "bb77c2a5-5b15-469b-82e4-c9f4b5fa8520", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ad22f956-1969-4f11-a989-cca903257c68", "b6f2859f-ac07-4ccd-af98-f945da11da2f", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "SiteRules", "TwoFactorEnabled", "UserName" },
                values: new object[] { "45792f46-ca3a-4cc7-a4a5-f110f7837e9d", 0, "b8004a8d-5b5c-4052-ad98-aecf635fb984", "admin@test.pl", true, "Antek", "Kowalski", true, null, "ADMIN@TEST.PL", "ANTEK", "AQAAAAEAACcQAAAAEIB52UcSUF1BYWrGENeWvk9pZ4FA51c7FQg3p+OHa+8bUylUXLZo3pKmoWAsepJ9Dw==", null, false, "62457915-fd1a-4bee-89d8-67f2a8c49f38", true, false, "Antek Kowalski" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "3c96d978-37ba-43ae-845d-7584f37e6ad7", "45792f46-ca3a-4cc7-a4a5-f110f7837e9d" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "ad22f956-1969-4f11-a989-cca903257c68", "45792f46-ca3a-4cc7-a4a5-f110f7837e9d" });
        }
    }
}
