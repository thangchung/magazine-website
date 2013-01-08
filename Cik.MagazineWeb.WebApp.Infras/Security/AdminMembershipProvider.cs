namespace Cik.MagazineWeb.WebApp.Infras.Security
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    using Cik.MagazineWeb.Framework.Encyption;
    using Cik.MagazineWeb.Framework.Encyption.Impl;
    using Cik.MagazineWeb.Mapping.User;
    using Cik.MagazineWeb.Model.User;

    using WebMatrix.WebData;

    public class CustomAdminMembershipProvider : SimpleMembershipProvider
    {
        // TODO: will do a better way
        private const string SELECT_ALL_USER_SCRIPT = "select * from [dbo].[User]private where UserName = '{0}'";

        private readonly IEncrypting _encryptor;

        private readonly SimpleSecurityContext _simpleSecurityContext;

        public CustomAdminMembershipProvider() : this(new SimpleSecurityContext())
        {
        }

        public CustomAdminMembershipProvider(SimpleSecurityContext simpleSecurityContext)
            : this(new Encryptor(), new SimpleSecurityContext("DefaultDb"))
        {
        }

        public CustomAdminMembershipProvider(IEncrypting encryptor, SimpleSecurityContext simpleSecurityContext)
        {
            this._encryptor = encryptor;
            this._simpleSecurityContext = simpleSecurityContext;
        }

        public override bool ValidateUser(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentException("Argument cannot be null or empty", "username");
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Argument cannot be null or empty", "password");
            }

            var hash = this._encryptor.Encode(password);

            var users = this._simpleSecurityContext.Users.SqlQuery(string.Format(SELECT_ALL_USER_SCRIPT, username));

            if (users == null && !users.Any())
            {
                return false;
            }

            var firstOrDefault  = users.FirstOrDefault();

            return firstOrDefault != null 
                && String.Compare(firstOrDefault.Password, hash, StringComparison.Ordinal) == 0;
        }
    }

    public class SimpleSecurityContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public SimpleSecurityContext()
            : this("DefaultDb")
        {
        }

        public SimpleSecurityContext(string connStringName) :
            base(connStringName)
        {
            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Configurations.Add(new UserMapping());
        }
    }
}