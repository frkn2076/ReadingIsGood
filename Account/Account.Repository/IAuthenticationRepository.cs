using Account.DataAccess.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Account.Repository
{
    public interface IAuthenticationRepository
    {
        Task<Authentication> GetAuthenticationAsync(string refreshToken);
        Task<Authentication> InsertAsync(Authentication authentication);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
