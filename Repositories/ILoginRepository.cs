
namespace WebApi3.Repositories
{
    public interface ILoginRepository
    {
        bool Login(string userName, string password);
    }
}
