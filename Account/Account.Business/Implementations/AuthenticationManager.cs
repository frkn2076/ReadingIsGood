using Account.Business.DTOs;
using Account.DataAccess.Entities;
using Account.Repository;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shared.Error;
using Shared.Extensions.Config;
using System;
using System.Collections.Immutable;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Account.Business.Implementations
{
    public class AuthenticationManager : IAuthenticationManager
    {
        private static readonly ImmutableList<string> _excludedBuiltinClaims = ImmutableList.Create("exp", "nbf");
        private readonly JWTSettings _jwtSettings;
        private readonly IAuthenticationRepository _authenticationRepository;

        private readonly byte[] _signingKey;

        public AuthenticationManager(IAuthenticationRepository authenticationRepository, JWTSettings jwtSettings)
        {
            _authenticationRepository = authenticationRepository;
            _jwtSettings = jwtSettings;
            _signingKey = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);
        }

        private async Task<TokenResponseDTO> GenerateToken(params (string, string)[] claims)
        {
            var now = DateTime.UtcNow;

            var accessTokenExpiresIn = (int)TimeSpan.FromMinutes(_jwtSettings.AccessTokenExpiresIn).TotalSeconds;
            var refreshTokenExpiresIn = (int)TimeSpan.FromMinutes(_jwtSettings.RefreshTokenExpiresIn).TotalSeconds;

            var userClaims = claims.Select(claim => new Claim(claim.Item1, claim.Item2)).ToArray();

            var accessToken = new JwtSecurityToken(
                claims: userClaims,
                notBefore: now,
                expires: now.Add(TimeSpan.FromMinutes(_jwtSettings.AccessTokenExpiresIn)),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(_signingKey), SecurityAlgorithms.HmacSha256)
            );

            var handler = new JwtSecurityTokenHandler();

            var encodedAccessToken = handler.WriteToken(accessToken);

            var refreshToken = Guid.NewGuid().ToString();

            var authenticationEntity = new Authentication()
            {
                AccessToken = encodedAccessToken,
                RefreshToken = refreshToken,
                RefreshTokenSeconds = refreshTokenExpiresIn
            };

            var authentication = await _authenticationRepository.InsertAsync(authenticationEntity);

            if(authentication is null)
                throw new SomethingWentWrongDuringDatabaseOperationException();

            await _authenticationRepository.SaveChangesAsync();

            var responseJson = new TokenResponseDTO
            {
                AccessToken = encodedAccessToken,
                AccessTokenExpiresIn = (int)TimeSpan.FromMinutes(_jwtSettings.AccessTokenExpiresIn).TotalSeconds,
                RefreshToken = refreshToken,
                RefreshTokenExpiresIn = (int)TimeSpan.FromMinutes(_jwtSettings.RefreshTokenExpiresIn).TotalSeconds
            };

            return responseJson;
        }

        private async Task<TokenResponseDTO> RefreshToken(string refreshToken)
        {
            var authenticationEntity = await _authenticationRepository.GetAuthenticationAsync(refreshToken);

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(authenticationEntity.AccessToken);
            var userClaims = token.Claims.Where(x => !_excludedBuiltinClaims.Contains(x.Type)).Select(x => (x.Type, x.Value)).ToArray();

            var tokenResponse = await GenerateToken(userClaims);

            return tokenResponse;
        }


        Task<TokenResponseDTO> IAuthenticationManager.GenerateToken(params (string, string)[] claims) => GenerateToken(claims);

        Task<TokenResponseDTO> IAuthenticationManager.RefreshToken(string refreshToken) => RefreshToken(refreshToken);
    }
}
