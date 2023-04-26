namespace Controllers.IRepositories;

public interface IRepository<T>
{
    Task<List<T>> GetList();

    IQueryable<T> GetQueryable();

    Task<T> GetById(Guid id);

    Task<bool> Update(T entity);

    Task<bool> Create(T entity);

    Task<bool> Delete(T entity);
}