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

    public virtual void Create(T entity)
    {
        entities.Add(entity);
        _dbContext.SaveChanges();
    }

    public virtual void Delete(T entity)
    {
        entities.Remove(entity);
        _dbContext.SaveChanges();
    }

    public virtual List<T> GetList()
    {
        return entities.ToList();
    }

    public virtual T GetById(Guid id)
    {
        return entities.FirstOrDefault(x => x.Id == id);
    }

    public virtual IQueryable<T> GetQueryable()
    {
        return entities.AsQueryable();
    }

    public virtual void Update(T entity)
    {
        entities.Update(entity);
        _dbContext.SaveChanges();
    }
}