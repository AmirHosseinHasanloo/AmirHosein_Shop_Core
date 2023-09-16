using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    public interface IUserRepository
    {
        void AddUser(Users user);
        bool IsExistUserByEmail(string email);
        Users GetUserForLogin(string email,string password);

    }
}
