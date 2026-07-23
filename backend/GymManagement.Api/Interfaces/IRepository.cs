namespace GymManagement.Api.Interfaces;

public interface IRepository<T> where T : class
{
    Task<List<T>> GetAllAsync(CancellationToken ct = default);
    Task<T?> GetByIdAsync(string id, CancellationToken ct = default);
    Task InsertAsync(T entity, CancellationToken ct = default);
    Task<bool> ReplaceAsync(string id, T entity, CancellationToken ct = default);
    Task<bool> DeleteAsync(string id, CancellationToken ct = default);
}