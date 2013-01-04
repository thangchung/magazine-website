namespace Cik.MagazineWeb.Mapping
{
    using System.Data.Entity.ModelConfiguration;

    using Cik.MagazineWeb.Model;

    public abstract class EntityMappingBase<T> : EntityTypeConfiguration<T> where T : Entity
    {
        protected EntityMappingBase()
        {
            this.HasKey(x => x.Id);

            this.Property(x => x.CreatedDate).IsRequired();
            this.Property(x => x.CreatedBy).IsRequired();
            this.Property(x => x.ModifiedDate).IsOptional();
        }
    }
}