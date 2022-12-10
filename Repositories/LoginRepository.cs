using Dot.Net.WebApi.Data;
using System.Linq;
using System.Web.Helpers;
using WebApi3.Domain;

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
        public User Login(string userName, string password)
        {
            // get a UserName and Password from the user and compare them to User info from the database 
            var user = _context.Users.SingleOrDefault(u => u.UserName == userName);

            if (user == null)
            {
                return null;
            }

            var hashedPassword = user.Password;
            var IsPasswordCorrect = Crypto.VerifyHashedPassword(hashedPassword, password);

            if (!IsPasswordCorrect)
            {
                return null;
            }

            return user;
        }
    }
}
