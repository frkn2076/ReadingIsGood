using Account.DataAccess;
using Account.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Account.Repository.Implementation
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly AccountDBContext _context;

        public AuthenticationRepository(AccountDBContext context) => _context = context;

        private async Task<Authentication> GetAuthenticationAsync(string refreshToken) => await _context.Authentications.AsNoTracking()
            .FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);

        private async Task<Authentication> InsertAsync(Authentication authentication) => (await _context.Authentications.AddAsync(authentication)).Entity;

        private async Task<int> SaveChangesAsync(CancellationToken cancellationToken) => await _context.SaveChangesAsync(cancellationToken);

        #region Explicit Interface Definitions
        Task<Authentication> IAuthenticationRepository.GetAuthenticationAsync(string refreshToken) => GetAuthenticationAsync(refreshToken);
        Task<Authentication> IAuthenticationRepository.InsertAsync(Authentication authentication) => InsertAsync(authentication);
        Task<int> IAuthenticationRepository.SaveChangesAsync(CancellationToken cancellationToken) => SaveChangesAsync(cancellationToken);
        #endregion
    }
}
