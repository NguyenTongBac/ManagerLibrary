using System.Linq;
using Controllers.IRepositories;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Models.Tables;

namespace Models.Repositories;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly ManagerLibraryDbContext _dbContext;
    private DbSet<T> entities;

    public Repository(ManagerLibraryDbContext dbContext)
    {
        _dbContext = dbContext;
        entities = _dbContext.Set<T>();
    }

    public virtual async Task<bool> Create(T entity)
    {
        await entities.AddAsync(entity);
        
        return true;
    }

    public virtual async Task<bool> Delete(T entity)
    {
        entities.Remove(entity);

        return true;
    }

    public virtual async Task<List<T>> GetList()
    {
        return await entities.ToListAsync();
    }

    public virtual async Task<T> GetById(Guid id)
    {
        return await entities.FirstOrDefaultAsync(x => x.Id == id);
    }

    public virtual IQueryable<T> GetQueryable()
    {
        return entities.AsQueryable();
    }

    public virtual async Task<bool> Update(T entity)
    {
        entities.Update(entity);

        return true;
    }
}