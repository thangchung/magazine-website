using Cik.CoreLibs.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cik.Services.Magazine.MagazineService.Features.Category.TypeMappings
{
    public class CategoryConfiguration : EntityMappingConfiguration<Entity.Category>
    {
        public override void Map(EntityTypeBuilder<Entity.Category> b)
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