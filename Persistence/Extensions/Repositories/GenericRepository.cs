using Application.Interfaces.Repositories;
using Domain.Commons;
using Microsoft.EntityFrameworkCore;
using Persistence.DataContexts;

namespace Persistence.Extensions.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseAuditableEntity
{
    private readonly ApplicationDbContext _applicationDbContext;

    public GenericRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<T> DeleteAsync(int id)
    {
        var exist = await _applicationDbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);

        if (exist == null)
        {
            throw new Exception($"{typeof(T).Name} not found");
        }

        exist.IsDeleted = true;
        exist.UpdateDate = DateTime.Now;

        _applicationDbContext.Set<T>().Update(exist);

        return exist;
    }

    public async Task<List<T>> GetAll()
    {
        return await _applicationDbContext.Set<T>().Where(x => !x.IsDeleted).ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        var exist = await _applicationDbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id&&!x.IsDeleted);

        return exist;
    }

    public async Task<T> PostAsync(T entity)
    {
        entity.CreateDate = DateTime.Now;
        entity.IsDeleted = false;
        entity.IsActive = true;

        await _applicationDbContext.Set<T>().AddAsync(entity);

        return entity;
    }

    public async Task<T> PutAsync(int id, T entity)
    {
        var exist = await _applicationDbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);

        if (exist == null)
        {
            throw new Exception($"{typeof(T).Name} not found");
        }

        entity.UpdateDate = DateTime.Now;

        _applicationDbContext.Set<T>().Update(entity);

        return entity;
    }
}
