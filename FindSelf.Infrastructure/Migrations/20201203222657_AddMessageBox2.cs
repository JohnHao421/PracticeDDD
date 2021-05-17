using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FindSelf.Infrastructure.Migrations
{
    public partial class AddMessageBox2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MessageBox",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Uid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageBox", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageBox_User_Uid",
                        column: x => x.Uid,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SiteMessage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Content = table.Column<string>(nullable: false),
                    SendTimesteamp = table.Column<DateTimeOffset>(nullable: false),
                    SnederId = table.Column<Guid>(nullable: false),
                    ReceiverId = table.Column<Guid>(nullable: false),
                    BoxId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteMessage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SiteMessage_MessageBox_BoxId",
                        column: x => x.BoxId,
                        principalTable: "MessageBox",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MessageBox_Uid",
                table: "MessageBox",
                column: "Uid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SiteMessage_BoxId",
                table: "SiteMessage",
                column: "BoxId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SiteMessage");

            migrationBuilder.DropTable(
                name: "MessageBox");
        }
    }
}
