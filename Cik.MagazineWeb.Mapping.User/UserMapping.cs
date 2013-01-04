namespace Cik.MagazineWeb.Mapping.User
{
    using Cik.MagazineWeb.Model.User;

    public class UserMapping : EntityMappingBase<User>
    {
        public UserMapping()
        {
            this.Property(x => x.UserName);
            this.Property(x => x.DisplayName);
            this.Property(x => x.Password);
            this.Property(x => x.Email);

            this.ToTable("User");
        }
    }
}