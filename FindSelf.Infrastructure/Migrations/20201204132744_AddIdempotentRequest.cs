using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FindSelf.Infrastructure.Migrations
{
    public partial class AddIdempotentRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IdempotentRequest",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CommandType = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdempotentRequest", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IdempotentRequest");
        }
    }
}
