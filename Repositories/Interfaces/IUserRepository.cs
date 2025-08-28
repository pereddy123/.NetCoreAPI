using WorkSphereAPI.Models;

namespace WorkSphereAPI.Repositories.Interfaces
{
    
        public interface IUserRepository
        {
            Task<IEnumerable<User>> GetAllAsync();
       
        Task<int> CountAsync();
        Task<User?> GetByIdAsync(int id);

            Task<User?> GetByUsernameAsync(string username);
            Task AddAsync(User user);
        Task<User> CreateAsync(User user, string password);

        void Update(User user);
            void Delete(User user);
            Task<bool> SaveChangesAsync();
        
     

    }
}
