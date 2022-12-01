using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineLearningAppApi.Infrastructure.Persistence.Migrations
{
    public partial class AddGradeColumnToUserQuiz : Migration
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

            migrationBuilder.AddColumn<int>(
                name: "Grade",
                table: "UserQuizzes",
                type: "int",
                nullable: true);

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
                values: new object[] { "20d75387-d5bf-4333-b50a-6259f9a7964b", "ca98ebda-e2b3-4121-acc5-fae0cc4fe021", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "44bd355a-4a95-4134-bd76-8990ea56676e", "36bbf953-ab23-4f7e-ba7b-d12242540f81", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "ImagePath", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "SiteRules", "TwoFactorEnabled", "UserName" },
                values: new object[] { "a01bb744-1946-48bf-8d81-600d7fcad4ad", 0, "99b8f54f-af69-45de-ae6e-a0618c281b2d", "admin@test.pl", true, "Antek", null, "Kowalski", true, null, "ADMIN@TEST.PL", "ADMIN@TEST.PL", "AQAAAAEAACcQAAAAEKjqApHP7OFtJTLJfHhAt0vwAoL4UUD9JCp3VKCn9OkO9zdcZHxrZFJiHUcb8Bruvg==", null, false, "ee0d5f29-4d4c-4509-9aa2-e4dad39254ce", true, false, "admin@test.pl" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "20d75387-d5bf-4333-b50a-6259f9a7964b", "a01bb744-1946-48bf-8d81-600d7fcad4ad" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "44bd355a-4a95-4134-bd76-8990ea56676e", "a01bb744-1946-48bf-8d81-600d7fcad4ad" });

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
                keyValues: new object[] { "20d75387-d5bf-4333-b50a-6259f9a7964b", "a01bb744-1946-48bf-8d81-600d7fcad4ad" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "44bd355a-4a95-4134-bd76-8990ea56676e", "a01bb744-1946-48bf-8d81-600d7fcad4ad" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "20d75387-d5bf-4333-b50a-6259f9a7964b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "44bd355a-4a95-4134-bd76-8990ea56676e");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a01bb744-1946-48bf-8d81-600d7fcad4ad");

            migrationBuilder.DropColumn(
                name: "Grade",
                table: "UserQuizzes");

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
