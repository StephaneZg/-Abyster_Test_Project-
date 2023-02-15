
using System.Linq.Expressions;
using Abyster_Test_Project.Contract;
using Abyster_Test_Project.Data;
using Microsoft.EntityFrameworkCore;

namespace Abyster_Test_Project.SharedKernel;

public class RepositoryBase<TEntity> : IRepositoryBase<TEntity>
    where TEntity : Common
{


    private readonly DatabaseContext _context;

    public RepositoryBase(DatabaseContext context)
    {
        _context = context;
    }

    public void Create(TEntity entity)
    {
        _context.Set<TEntity>().Add(entity);
    }

    public async Task CreateRange(IEnumerable<TEntity> entity)
    {
        await _context.Set<TEntity>().AddRangeAsync(entity);
    }

    public void Delete(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
    }

    public void DeleteRange(IEnumerable<TEntity> entities)
    {
        _context.Set<TEntity>().RemoveRange(entities);
    }

    public async Task<bool> Exists(int id)
    {
        return await _context.Set<TEntity>()
            .AnyAsync(e => e.Id == id);
    }

    public virtual IQueryable<TEntity> FindAll(bool trackChanges)
    {
       if (trackChanges)
        {
            return _context.Set<TEntity>();
        }
        else
        {
            return _context.Set<TEntity>().AsNoTracking();
        }
    }

    public virtual IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression, bool trackChanges)
    {
        if (trackChanges)
        {
            return _context.Set<TEntity>().Where(expression);
        }
        else
        {
            return _context.Set<TEntity>().Where(expression).AsNoTracking();
        }
    }

    public void Update(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
    }
}