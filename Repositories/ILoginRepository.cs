
using WebApi3.Domain;

namespace WebApi3.Repositories
{
    public interface ILoginRepository
    {
        //bool Login(string userName, string password);
        User Login(string userName, string password);
    }
}
