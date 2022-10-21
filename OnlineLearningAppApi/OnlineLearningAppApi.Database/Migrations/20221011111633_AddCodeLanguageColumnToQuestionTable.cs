using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineLearningAppApi.Database.Migrations
{
    public partial class AddCodeLanguageColumnToQuestionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "f94d5729-bbb7-4b04-a90c-398c0d085106", "64736f53-ac93-43cd-9f20-9ecd33d86986" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "fc9382aa-e8b0-4ec1-b87e-c4da81d6b453", "64736f53-ac93-43cd-9f20-9ecd33d86986" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f94d5729-bbb7-4b04-a90c-398c0d085106");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fc9382aa-e8b0-4ec1-b87e-c4da81d6b453");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "64736f53-ac93-43cd-9f20-9ecd33d86986");

            migrationBuilder.AddColumn<string>(
                name: "CodeLanguage",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "964904a3-a2a4-4f95-9dea-7e399cc5dcdf", "fb870e4f-9b68-469d-8dd6-dae890d93d41", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b5961660-af20-42c6-bbfb-7a933fd97cb1", "3561e125-331e-468a-8e8c-6af754d15fd3", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "ImagePath", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "SiteRules", "TwoFactorEnabled", "UserName" },
                values: new object[] { "5949ff29-6828-43c5-9f12-3fda9a227531", 0, "a1933872-17f6-483e-afea-25472ecb0bab", "admin@test.pl", true, "Antek", null, "Kowalski", true, null, "ADMIN@TEST.PL", "ADMIN@TEST.PL", "AQAAAAEAACcQAAAAENSRw8EIeNytGMOsrGIlHadJd81L8S0VeTaSGLsozlEpSmDhnETZMgFCHsGmzLBbNQ==", null, false, "758f2a5f-373a-4d95-bc4e-297cf1f901a5", true, false, "admin@test.pl" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "964904a3-a2a4-4f95-9dea-7e399cc5dcdf", "5949ff29-6828-43c5-9f12-3fda9a227531" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "b5961660-af20-42c6-bbfb-7a933fd97cb1", "5949ff29-6828-43c5-9f12-3fda9a227531" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "964904a3-a2a4-4f95-9dea-7e399cc5dcdf", "5949ff29-6828-43c5-9f12-3fda9a227531" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "b5961660-af20-42c6-bbfb-7a933fd97cb1", "5949ff29-6828-43c5-9f12-3fda9a227531" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "964904a3-a2a4-4f95-9dea-7e399cc5dcdf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b5961660-af20-42c6-bbfb-7a933fd97cb1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5949ff29-6828-43c5-9f12-3fda9a227531");

            migrationBuilder.DropColumn(
                name: "CodeLanguage",
                table: "Questions");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f94d5729-bbb7-4b04-a90c-398c0d085106", "1336cfe2-bce3-4474-ba7a-d9e84202602e", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fc9382aa-e8b0-4ec1-b87e-c4da81d6b453", "2970ed2c-3ff2-4eda-b44c-e2b2c459a49e", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "ImagePath", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "SiteRules", "TwoFactorEnabled", "UserName" },
                values: new object[] { "64736f53-ac93-43cd-9f20-9ecd33d86986", 0, "6cbb7189-d359-419e-bae1-79b3df9e9200", "admin@test.pl", true, "Antek", null, "Kowalski", true, null, "ADMIN@TEST.PL", "ADMIN@TEST.PL", "AQAAAAEAACcQAAAAEAKxwZzUmvBBgBHF9arQZBt2GkNUoE/zvRNk57VFv1EJ4LpZ1VXatDKjYXQh/N83Lg==", null, false, "b643a351-f0f0-4882-9b70-f30f4d8507f6", true, false, "admin@test.pl" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "f94d5729-bbb7-4b04-a90c-398c0d085106", "64736f53-ac93-43cd-9f20-9ecd33d86986" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "fc9382aa-e8b0-4ec1-b87e-c4da81d6b453", "64736f53-ac93-43cd-9f20-9ecd33d86986" });
        }
    }
}
