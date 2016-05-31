using System;
using Cik.Services.CategoryService.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Cik.Services.CategoryService.Migrations
{
    [DbContext(typeof (CategoryDbContext))]
    internal class CategoryDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rc2-20901");

            modelBuilder.Entity("Cik.Services.CategoryService.Model.Category", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<string>("CreatedBy");

                b.Property<DateTime>("CreatedDate");

                b.Property<string>("ModifiedBy");

                b.Property<DateTime?>("ModifiedDate");

                b.Property<string>("Name");

                b.HasKey("Id");

                b.ToTable("Categories");
            });
        }
    }
}