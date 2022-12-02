using Dot.Net.WebApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi3.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly LocalDbContext _context;
        public LoginRepository(LocalDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Method that check if a User exists in the database and can access the API
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool Login(string userName, string password)
        {
            // get a UserName and Password from the user and compare them to User info from the database 
            var user = _context.Users.SingleOrDefault(u => u.UserName == userName && u.Password == password);

            if (user == null)
            {
                return false;
            }

            return true;
        }
    }
}
