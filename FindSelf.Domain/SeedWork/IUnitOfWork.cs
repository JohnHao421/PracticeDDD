using System.Threading;
using System.Threading.Tasks;

namespace FindSelf.Domain.SeedWork
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}