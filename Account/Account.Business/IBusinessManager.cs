using Account.Business.DTOs;
using System.Threading.Tasks;

namespace Account.Business
{
    public interface IBusinessManager
    {
        /// <summary>
        /// User logins the system. 
        /// </summary>
        /// <returns>User's Unique Id</returns>
        Task<int> LoginAsync(RegisterRequestDTO model);

        /// <summary>
        /// User registers the system. 
        /// </summary>
        /// <returns>User's Unique Id</returns>
        Task<int> RegisterAsync(RegisterRequestDTO model);
    }
}
