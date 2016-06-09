using System;
using Cik.Services.Magazine.MagazineService.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Cik.Services.Magazine.MagazineService.Migrations
{
    [DbContext(typeof (MagazineDbContext))]
    internal class MagazineDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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