using Microsoft.EntityFrameworkCore;
using ModulyBack.IAM.Domain.Model.Aggregates;
using ModulyBack.IAM.Domain.Repositories;
using ModulyBack.Shared.Infraestructure.Persistences.EFC.Configuration;
using ModulyBack.Shared.Infraestructure.Persistences.EFC.Repositories;

namespace ModulyBack.IAM.Infrastructure.Persistence.EFC.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context) { }

    public async Task<User?> FindByUsernameAsync(string username)
    {
        return await Context.Set<User>().FirstOrDefaultAsync(user => user.Username.Equals(username));
    }

    public bool ExistsByUsername(string username)
    {
        return Context.Set<User>().Any(user => user.Username.Equals(username));
    }

    public async Task<bool> ExistsByUsernameAsync(string username)
    {
        return await Context.Set<User>().AnyAsync(user => user.Username.Equals(username));
    }


    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await Context.Set<User>().ToListAsync();
    }

    public async Task<User?> GetByIdAsync(Guid userId)
    {
        return await Context.Set<User>().FindAsync(userId);
    }

  

    public async Task UpdateAsync(User user)
    {
        Context.Set<User>().Update(user);
        await Context.SaveChangesAsync();
    }
   
   




}