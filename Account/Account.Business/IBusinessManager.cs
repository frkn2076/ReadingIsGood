using Account.Business.DTOs;
using System.Threading.Tasks;

namespace Account.Business
{
    public interface IBusinessManager
    {
        /// <summary>
        /// User logins the system. 
        /// </summary>
        /// <returns>RegisterResponseDTO</returns>
        Task<AccountResponseDTO> LoginAsync(AccountRequestDTO model);

        /// <summary>
        /// User registers the system. 
        /// </summary>
        /// <returns>RegisterResponseDTO</returns>
        Task<AccountResponseDTO> RegisterAsync(AccountRequestDTO model);
    }
}
