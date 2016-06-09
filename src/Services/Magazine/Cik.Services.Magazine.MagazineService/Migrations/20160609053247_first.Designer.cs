using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Cik.Services.Magazine.MagazineService.Repository;

namespace Cik.Services.Magazine.MagazineService.Migrations
{
    [DbContext(typeof(MagazineDbContext))]
    [Migration("20160609053247_first")]
    partial class first
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rc2-20901");

            modelBuilder.Entity("Cik.Services.Magazine.MagazineService.Model.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<byte[]>("Version");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });
        }
    }
}
