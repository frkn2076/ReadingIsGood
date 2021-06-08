using Account.Business.DTOs;
using Account.DataAccess.Entities;
using Account.Infra.Exceptions;
using Account.Repository;
using Mapster;
using Shared.Error;
using System.Threading.Tasks;

namespace Account.Business.Implementations
{
    public class BusinessManager : IBusinessManager
    {
        private readonly IRegisterRepository _registerRepository;
        public BusinessManager(IRegisterRepository registerRepository) => _registerRepository = registerRepository;

        private async Task<AccountResponseDTO> LoginAsync(AccountRequestDTO model)
        {
            var registrationEntity = model.Adapt<Registration>();

            var user = await _registerRepository.GetUserAsync(registrationEntity);
            if (user is null)
                throw new AccountNotFoundException();

            var response = user.Adapt<AccountResponseDTO>();

            return response;
        }

        private async Task<AccountResponseDTO> RegisterAsync(AccountRequestDTO model)
        {
            var isExist = await _registerRepository.IsUserNameExistAsync(model.UserName);
            if (isExist)
                throw new AccountAlreadyExistsException();

            var registerEntity = model.Adapt<Registration>();

            var user = await _registerRepository.InsertAsync(registerEntity);

            await _registerRepository.SaveChangesAsync();

            if (user is null)
                throw new SomethingWentWrongDuringDatabaseOperationException();

            var response = user.Adapt<AccountResponseDTO>();

            return response;
        }

        #region Explicit Interface Definitions
        Task<AccountResponseDTO> IBusinessManager.LoginAsync(AccountRequestDTO model) => LoginAsync(model);
        Task<AccountResponseDTO> IBusinessManager.RegisterAsync(AccountRequestDTO model) => RegisterAsync(model);
        #endregion
    }
}
