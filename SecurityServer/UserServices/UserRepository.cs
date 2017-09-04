using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VacationSolution.Web.Entities;

namespace SecurityServer.UserServices
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _User = new List<User>
        {
            new User
            {
                  Email="okosodovictor@yahoo.com",
                   Password="Hello Me"
            },
            new User
            {
                  Email="okosodovictor@outlook.com",
                   Password="Hello Me"
            }
        };

        public  bool ValidateCredential(string email, string password)
        {
            var user = FindByEmail(email);
            if (user != null)
            {
                return user.Password.Equals(password);
            }
            return false;
        }

        public User FindByEmail(string email)
        {
            return _User.FirstOrDefault(x => x.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        public User FindBySubjectId(string v)
        {
            throw new NotImplementedException();
        }
    } 
}
