using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi3.Domain.DTO;

namespace WebApi3.Repositories
{
    public interface IUserRepository
    {
        Task<List<UserDTO>> GetAllUsers();
        Task<UserDTO> GetSingleUser(int id);
        Task<UserDTO> GetSingleUserByUserName(string userName);
        Task<List<UserDTO>> CreateUser(UserDTO userDTO);
        Task<UserDTO> UpdateUser(int id, UserDTO userDTO);
        Task<List<UserDTO>> DeleteUser(int id);
    }
}
