namespace Cik.MagazineWeb.Repository.User
{
    using System;

    using Cik.MagazineWeb.Data;
    using Cik.MagazineWeb.Framework.Encyption;
    using Cik.MagazineWeb.Model.User;

    public class UserRepository : GenericRepository, IUserRepository
    {
        private readonly IEncrypting _encryptor;

        public UserRepository(MainDbContext context, IEncrypting encryptor)
            : base(context)
        {
            this._encryptor = encryptor;
        }

        public User GetUserByUserName(string userName)
        {
            return this.FindOne<User>(x => x.UserName.Equals(userName, StringComparison.InvariantCulture));
        }

        public bool ValidateUser(string userName, string password)
        {
            var user = this.GetUserByUserName(userName);

            if (user == null)
                return false;

            return user.UserName.Equals(userName, StringComparison.InvariantCulture)
                   && user.Password.Equals(password, StringComparison.InvariantCulture);
        }

        public int CreateUser(string userName, string displayName, string password, string email, int role, string createdBy)
        {
            var hashPassword = this._encryptor.Encode(password);

            return this.Save(UserFactory.Create(userName, displayName, hashPassword, email, role, createdBy)).Id;
        }

        //public UserInfo GetUserInfoByUserName(string userName)
        //{
        //    return this.GetUserByUserName(userName).MapTo<UserInfo>();
        //}
    }
}