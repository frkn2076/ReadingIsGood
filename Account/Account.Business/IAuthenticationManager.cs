using Account.Business.DTOs;
using System.Threading.Tasks;

namespace Account.Business
{
    public interface IAuthenticationManager
    {
        Task<TokenResponseDTO> GenerateToken(params (string, string)[] claims);
        Task<TokenResponseDTO> RefreshToken(string refreshToken);
    }
}
