namespace Cik.MagazineWeb.Model.User
{
    public interface IUserRepository
    {
        User GetUserByUserName(string userName);

        bool ValidateUser(string userName, string password);

        int CreateUser(string userName, string displayName, string password, string email, int role, string createdBy);

        // UserInfo GetUserInfoByUserName(string userName);
    }
}