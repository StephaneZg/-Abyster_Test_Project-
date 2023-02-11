
using System.Linq.Expressions;
using Abyster_Test_Project.SharedKernel;

namespace Abyster_Test_Project.Contract;

public interface IRepositoryBase<TEntity>  where TEntity : Common
{
    IQueryable<TEntity> FindAll(bool trackChanges);
    IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression,bool trackChanges);
    void Create(TEntity entity);
    Task CreateRange(IEnumerable<TEntity> entity);  
    void Update(TEntity entity);
    void Delete(TEntity entity);
    void DeleteRange(IEnumerable<TEntity> entity);
    Task<bool> Exists(int id);
}