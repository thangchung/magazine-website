namespace Cik.MagazineWeb.Model.User
{
    using System;

    public class UserFactory
    {
        public static User Create(string userName, string displayName, string password, string email, int role, string createdBy)
        {
            return new User
                {
                    UserName = userName,
                    DisplayName = displayName,
                    Password = password,
                    Email = email,
                    Role = role,
                    CreatedDate = DateTime.Now,
                    CreatedBy = createdBy
                };   
        }
    }
}