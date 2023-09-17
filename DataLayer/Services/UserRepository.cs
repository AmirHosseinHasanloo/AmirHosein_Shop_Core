using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;

namespace DataLayer
{
    public class UserRepository : IUserRepository
    {
        EshopContext _context;

        public UserRepository(EshopContext context)
        {
            _context = context;
        }

        public void AddUser(Users user)
        {
            _context.Add(user);
            _context.SaveChanges();
        }

        public bool IsExistUserByEmail(string email)
        {
            return _context.Users.Any(u => u.Email.ToLower().Trim() == email.ToLower().Trim());
        }

        public Users GetUserForLogin(string email, string password)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
        }

    }
}
