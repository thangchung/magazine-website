using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cik.Services.CategoryService.Migrations
{
    public partial class MyFirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable("Categories", table => new
            {
                Id = table.Column<Guid>(nullable: false),
                CreatedBy = table.Column<string>(nullable: true),
                CreatedDate = table.Column<DateTime>(nullable: false),
                ModifiedBy = table.Column<string>(nullable: true),
                ModifiedDate = table.Column<DateTime>(nullable: true),
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