using Microsoft.EntityFrameworkCore;

namespace Skipper.Core.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(DataContext context) : base(context)
        {

        }

        public async Task<User> GetByEmailAsync(string email)
        {
            try
            {
                return await _context.Users
                                .AsNoTracking()
                                .FirstOrDefaultAsync(x => x.Email == email);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
        public User GetByEmail(string email)
        {
            try
            {
                return _context.Users
                                .AsNoTracking()
                                .FirstOrDefault(x => x.Email == email);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
        public async Task<bool> Any(string email)
        {
            try
            {
                return await _context.Users
                                .AsNoTracking()
                                .AnyAsync(x => x.Email == email);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
        public async Task<User> GetByVerificationToken(string token)
        {
            try
            {
                return await _context.Users
                                .FirstOrDefaultAsync(x => x.VeryficationToken == token);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
        public override async Task<User?> GetById(Guid id)
        {
            try
            {
                return await _context.Users
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
        /*
        public override async Task<bool> Add(User entity)
        {
            try
            {
                await _context.Users
                    .AsNoTracking()
                    .AddAsync(entity);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        */
    }
}
