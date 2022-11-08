using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineLearningAppApi.Infrastructure.Persistence.Migrations
{
    public partial class Fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tokens_AspNetUsers_UserId",
                table: "Tokens");

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

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Tokens",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2df9f646-4660-41e3-b0f8-5e3592530e1d", "b638d077-34cc-41ec-8377-fe42bbd903ab", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ba5d75ac-7b69-4c5e-9094-0e263dcc820d", "d5c6ee55-4ced-4f79-b081-fcc9a585bbd8", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "ImagePath", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "SiteRules", "TwoFactorEnabled", "UserName" },
                values: new object[] { "98559e96-52a2-4212-ac5f-9dd12f9a4942", 0, "4ab72b92-9962-41f6-b177-3e75c66230f5", "admin@test.pl", true, "Antek", null, "Kowalski", true, null, "ADMIN@TEST.PL", "ADMIN@TEST.PL", "AQAAAAEAACcQAAAAEMkSw6/hX4MTLUIdzznTmScvF2OQ2eWtuoJ57BwPdf1km893W0wlYGemMC5lDmfqPA==", null, false, "6e595a1a-e214-453c-a4cc-211d322709e7", true, false, "admin@test.pl" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "2df9f646-4660-41e3-b0f8-5e3592530e1d", "98559e96-52a2-4212-ac5f-9dd12f9a4942" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "ba5d75ac-7b69-4c5e-9094-0e263dcc820d", "98559e96-52a2-4212-ac5f-9dd12f9a4942" });

            migrationBuilder.AddForeignKey(
                name: "FK_Tokens_AspNetUsers_UserId",
                table: "Tokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tokens_AspNetUsers_UserId",
                table: "Tokens");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2df9f646-4660-41e3-b0f8-5e3592530e1d", "98559e96-52a2-4212-ac5f-9dd12f9a4942" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "ba5d75ac-7b69-4c5e-9094-0e263dcc820d", "98559e96-52a2-4212-ac5f-9dd12f9a4942" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2df9f646-4660-41e3-b0f8-5e3592530e1d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ba5d75ac-7b69-4c5e-9094-0e263dcc820d");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "98559e96-52a2-4212-ac5f-9dd12f9a4942");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Tokens",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Tokens_AspNetUsers_UserId",
                table: "Tokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
