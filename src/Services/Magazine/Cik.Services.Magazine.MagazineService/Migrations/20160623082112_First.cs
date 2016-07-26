using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cik.Services.Magazine.MagazineService.Migrations
{
    public partial class First : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable("Categories", table => new
            {
                Id = table.Column<Guid>(nullable: false),
                AggregateStatus = table.Column<int>(nullable: false),
                CreatedBy = table.Column<string>(nullable: true),
                CreatedDate = table.Column<DateTime>(nullable: false),
                ModifiedBy = table.Column<string>(nullable: true),
                ModifiedDate = table.Column<DateTime>(nullable: false),
                Name = table.Column<string>(nullable: true)
            },
                constraints: table => { table.PrimaryKey("PK_Categories", x => x.Id); });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Categories");
        }
    }
}