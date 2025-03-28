using System.Linq.Expressions;

namespace BloodDonationCenters.Infraestructure.Interfaces;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? filter = null);
    Task<T> GetById(Guid id);
    Task Add(T entity);
    Task Update(T entity);
    Task Delete(T entity);    
}

