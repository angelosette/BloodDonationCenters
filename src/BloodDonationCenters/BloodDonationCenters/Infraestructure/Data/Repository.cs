using BloodDonationCenters.Infraestructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BloodDonationCenters.Infraestructure.Data;

public class Repository<T>: IRepository<T> where T : class
{
    private readonly AppDbContext _context;
    private readonly DbSet<T> _dbSet;
    
    public Repository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task Add(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(T entity)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }    

    public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? filter = null)
    {
        IQueryable<T> query = _dbSet;
        if (filter != null)
            query = query.Where(filter);

        return await query.ToListAsync();
    }

    public async Task<T> GetById(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task Update(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }
}

