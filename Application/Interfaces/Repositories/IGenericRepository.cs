using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Repositories;

public interface IGenericRepository<T> where T : class 
{
    Task<T> PostAsync(T entity);
    Task<T> PutAsync(int id,T entity);
    Task<T> DeleteAsync(int id);
    Task<T> GetByIdAsync(int id);
    Task<List<T>> GetAll();
}
