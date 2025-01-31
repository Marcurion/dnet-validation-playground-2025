using System;
using Domain.Interfaces;

namespace Infrastructure.Repositories
{
    internal sealed class MyRepository : IRepository<object>
{
    public void SaveAsync()
    {
        throw new NotImplementedException();
    }
}
}
