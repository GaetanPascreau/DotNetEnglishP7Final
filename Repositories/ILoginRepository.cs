using WebApi3.Domain;

namespace WebApi3.Repositories
{
    public interface ILoginRepository
    {
        User Login(string userName, string password);
    }
}
