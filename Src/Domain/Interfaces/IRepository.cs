namespace Domain.Interfaces;

public interface IRepository<T> where T : class
{
    void SaveAsync();
}