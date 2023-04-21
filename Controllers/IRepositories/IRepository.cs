namespace Controllers.IRepositories;

public interface IRepository<T>
{
    List<T> GetList();

    IQueryable<T> GetQueryable();

    T GetById(Guid id);

    void Update(T entity);

    void Create(T entity);

    void Delete(T entity);
}