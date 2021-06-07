using Account.DataAccess.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Account.Repository
{
    public interface IRegisterRepository
    {
        Task<Registration> GetUserAsync(Registration registration);
        Task<bool> IsUserNameExistAsync(string userName);
        Task<Registration> InsertAsync(Registration registration);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
