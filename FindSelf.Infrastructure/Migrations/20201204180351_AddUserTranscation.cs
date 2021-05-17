using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FindSelf.Infrastructure.Migrations
{
    public partial class AddUserTranscation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserTranscation",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PayerId = table.Column<Guid>(nullable: false),
                    Uid = table.Column<Guid>(nullable: false),
                    RequestId = table.Column<Guid>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    Dircition = table.Column<byte>(nullable: false),
                    CreateTime = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTranscation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTranscation_User_Uid",
                        column: x => x.Uid,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserTranscation_Uid",
                table: "UserTranscation",
                column: "Uid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserTranscation");
        }
    }
}
