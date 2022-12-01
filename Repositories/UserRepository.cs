using Dot.Net.WebApi.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi3.Domain;
using WebApi3.Domain.DTO;

namespace WebApi3.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly LocalDbContext _context;
        public UserRepository(LocalDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method that GETs the list of Users from the database,
        /// then converts it into a list of UserDTO (showing less properties)
        /// then returning it (to the Service)
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserDTO>> GetAllUsers()
        {
            var users = await _context.Users
                .Select(x => UserToDTO(x))
                .ToListAsync();

            return users;
        }

        /// <summary>
        /// Method that GETs a specific User by its Id from the database,
        /// then converts it into a list of UserDTO (showing less properties)
        /// then returning it (to the Service)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<UserDTO> GetSingleUser(int id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == id);
            if (user is null)
            {
                return null;
            }
            return UserToDTO(user);

        }

        /// <summary>
        /// Method that GETs a specific User by its UserName from the database,
        /// then converts it into a list of UserDTO (showing less properties)
        /// then returning it (to the Service)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<UserDTO> GetSingleUserByUserName(string userName)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
            if (user is null)
            {
                return null;
            }
            return UserToDTO(user);

        }

        /// <summary>
        /// Method that CREATEs a User and save it to the database
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        public async Task<List<UserDTO>> CreateUser(UserDTO userDTO)
        {
            // Create new User 
            var user = new User
            {
                Id = userDTO.Id,
                UserName = userDTO.UserName,
                Password = userDTO.Password,
                FullName = userDTO.FullName
            };

            // Save new User in the database
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // return the entire list of Users with the newly added User
            var userAfterAddition = GetAllUsers();
            return await userAfterAddition;
        }

        /// <summary>
        /// Method that UPDATEs a User with a given Id in the database
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        public async Task<UserDTO> UpdateUser(int id, UserDTO userDTO)
        {
            var userToUpdate = _context.Users.Find(id);
            if (userToUpdate is null)
            {
                return null;
            }

            userToUpdate.UserName = userDTO.UserName;
            userToUpdate.Password = userDTO.Password;
            userToUpdate.FullName = userDTO.FullName;

            await _context.SaveChangesAsync();

            // Return the updated User
            var result = UserToDTO(userToUpdate);
            return result;
        }

        /// <summary>
        /// Method to DELETE a User with a given Id from the database
        /// </summary>
        /// <param name="id"></param>
        public async Task<List<UserDTO>> DeleteUser(int id)
        {
            User UserToDelete = _context.Users.Find(id);
            if (UserToDelete is null)
            {
                return null;
            }
            _context.Users.Remove(UserToDelete);
            _context.SaveChanges();

            // Return the entire list of Users without the newly removed User
            var UsersAfterDeletion = GetAllUsers();
            return await UsersAfterDeletion;
        }

        /// <summary>
        /// Method that converts a User into a UserDTO
        /// </summary>
        /// <param name="trade"></param>
        /// <returns></returns>
        private static UserDTO UserToDTO(User user) =>
        new UserDTO
        {
            Id = user.Id,
            UserName = user.UserName,
            Password = user.Password,
            FullName = user.FullName,
        };  
    }
}