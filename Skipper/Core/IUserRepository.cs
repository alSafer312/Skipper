namespace Skipper.Core
{
    public interface IUserRepository:IGenericRepository<User>
    {
        //Task<IEnumerable<User?>> GetAllMentors();
        Task<User> GetByEmailAsync(string email);
        User GetByEmail(string email);
        Task<bool> Any(string email);
        Task<User> GetByVerificationToken(string token);
    }
}
