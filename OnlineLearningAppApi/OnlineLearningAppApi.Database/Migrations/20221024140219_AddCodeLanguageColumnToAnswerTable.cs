using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineLearningAppApi.Database.Migrations
{
    public partial class AddCodeLanguageColumnToAnswerTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "CodeLanguage",
                table: "Answers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "75c4f453-e975-4e3b-a43c-d62d643732c1", "fd50d506-db8e-4936-83ef-9f6bcf34957f", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "967875b7-0c76-4f41-ace5-c38ac09ecf46", "81019d75-b6b9-4235-9f55-4ff9be41c610", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "ImagePath", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "SiteRules", "TwoFactorEnabled", "UserName" },
                values: new object[] { "425ef0eb-304d-42bf-a081-a28aa335e25b", 0, "9fc13107-13b0-48f8-bab4-a5a2e9109647", "admin@test.pl", true, "Antek", null, "Kowalski", true, null, "ADMIN@TEST.PL", "ADMIN@TEST.PL", "AQAAAAEAACcQAAAAEFe1pzj18AZe1aaMZBTR5gcJfdldqIkRZYCE7bUxaQRo/0ia7AcDT0o+0JlcAuu8ow==", null, false, "f9edcc0c-c7eb-4dab-a9c4-6b0008f91313", true, false, "admin@test.pl" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "75c4f453-e975-4e3b-a43c-d62d643732c1", "425ef0eb-304d-42bf-a081-a28aa335e25b" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "967875b7-0c76-4f41-ace5-c38ac09ecf46", "425ef0eb-304d-42bf-a081-a28aa335e25b" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "75c4f453-e975-4e3b-a43c-d62d643732c1", "425ef0eb-304d-42bf-a081-a28aa335e25b" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "967875b7-0c76-4f41-ace5-c38ac09ecf46", "425ef0eb-304d-42bf-a081-a28aa335e25b" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "75c4f453-e975-4e3b-a43c-d62d643732c1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "967875b7-0c76-4f41-ace5-c38ac09ecf46");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "425ef0eb-304d-42bf-a081-a28aa335e25b");

            migrationBuilder.DropColumn(
                name: "CodeLanguage",
                table: "Answers");

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
    }
}
