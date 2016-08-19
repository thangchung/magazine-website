using Cik.CoreLibs.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cik.Services.Magazine.MagazineService.Model.Entity
{
    public class CategoryConfiguration : EntityMappingConfiguration<Category>
    {
        public override void Map(EntityTypeBuilder<Category> b)
        {
            b.ToTable("Categories", "magazine").HasKey(p => p.Id);
            b.Property(p => p.Name).HasMaxLength(20).IsRequired();
            b.Property(p => p.AggregateStatus).IsRequired();
            b.Property(p => p.CreatedBy).HasMaxLength(10).IsRequired();
            b.Property(p => p.CreatedDate).IsRequired();
            b.Property(p => p.ModifiedBy).HasMaxLength(10);
        }
    }
}