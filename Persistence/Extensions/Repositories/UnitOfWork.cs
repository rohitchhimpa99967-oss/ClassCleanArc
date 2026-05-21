using Application.Interfaces.Repositories;
using Domain.Commons;
using Persistence.DataContexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Extensions.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private Dictionary<string, object> _repo = new();

    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public IGenericRepository<T> Repository<T>() where T : BaseAuditableEntity
    {
        var type = typeof(T).Name;

        if(_repo.ContainsKey(type))
        {
            var repositori= new GenericRepository<T>(_context);

            _repo.Add(type, repositori);
        }

        return (IGenericRepository<T>)_repo[type];
    }

    public Task<int> Save(CancellationToken cancellationToken)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }

}
