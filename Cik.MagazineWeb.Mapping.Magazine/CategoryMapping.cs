namespace Cik.MagazineWeb.Mapping.Magazine
{
    using Cik.MagazineWeb.Model.Magazine;

    public class CategoryMapping : EntityMappingBase<Category>
    {
        public CategoryMapping()
        {
            this.Property(x => x.Name).IsRequired();

            this.HasMany(x => x.Items).WithRequired(y => y.Category);

            this.ToTable("Category");
        }
    }
}