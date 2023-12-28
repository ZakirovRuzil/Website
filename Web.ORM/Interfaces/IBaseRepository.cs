namespace Web.ORM.Interfaces;

public interface IBaseRepository<T> where T : class
{
    bool Add(T entity);
    bool Update(T entity);
    bool Delete(int id);
    IEnumerable<T> Select(T entity);
    T SelectById(int id);
}