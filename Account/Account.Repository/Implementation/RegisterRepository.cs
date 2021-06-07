using Account.DataAccess;
using Account.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Account.Repository.Implementation
{
    public class RegisterRepository : IRegisterRepository
    {
        private readonly AccountDBContext _context;

        public RegisterRepository(AccountDBContext context) => _context = context;

        private async Task<Registration> GetUserAsync(Registration registration) => await _context.Registrations.AsNoTracking()
            .FirstOrDefaultAsync(x => x.UserName == registration.UserName && x.Password == registration.Password);

        private async Task<bool> IsUserNameExistAsync(string userName) => await _context.Registrations.AsNoTracking().AnyAsync(x => x.UserName == userName);

        private async Task<Registration> InsertAsync(Registration registration) => (await _context.Registrations.AddAsync(registration)).Entity;

        private async Task<int> SaveChangesAsync(CancellationToken cancellationToken) => await _context.SaveChangesAsync(cancellationToken);

        #region Explicit Interface Definitions
        Task<Registration> IRegisterRepository.GetUserAsync(Registration registration) => GetUserAsync(registration);
        Task<bool> IRegisterRepository.IsUserNameExistAsync(string userName) => IsUserNameExistAsync(userName);
        Task<Registration> IRegisterRepository.InsertAsync(Registration registration) => InsertAsync(registration);
        Task<int> IRegisterRepository.SaveChangesAsync(CancellationToken cancellationToken) => SaveChangesAsync(cancellationToken);
        #endregion
    }
}
