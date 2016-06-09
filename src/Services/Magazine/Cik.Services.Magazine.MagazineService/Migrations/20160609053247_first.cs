using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cik.Services.Magazine.MagazineService.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable("Categories", table => new
            {
                Id = table.Column<Guid>(nullable: false),
                Name = table.Column<string>(nullable: true),
                Version = table.Column<byte[]>(nullable: true)
            },
                constraints: table => { table.PrimaryKey("PK_Categories", x => x.Id); });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Categories");
        }
    }
}