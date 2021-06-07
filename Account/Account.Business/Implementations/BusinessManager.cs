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

        private async Task<int> LoginAsync(RegisterRequestDTO model)
        {
            var registrationEntity = model.Adapt<Registration>();

            var user = await _registerRepository.GetUserAsync(registrationEntity);
            if (user is null)
                throw new AccountNotFoundException();
            
            return user.Id;
        }

        private async Task<int> RegisterAsync(RegisterRequestDTO model)
        {
            var isExist = await _registerRepository.IsUserNameExistAsync(model.UserName);
            if (isExist)
                throw new AccountAlreadyExistsException();

            var registerEntity = model.Adapt<Registration>();

            var user = await _registerRepository.InsertAsync(registerEntity);

            await _registerRepository.SaveChangesAsync();

            if (user is null)
                throw new SomethingWentWrongDuringDatabaseOperationException();

            return user.Id;
        }

        #region Explicit Interface Definitions
        Task<int> IBusinessManager.LoginAsync(RegisterRequestDTO model) => LoginAsync(model);
        Task<int> IBusinessManager.RegisterAsync(RegisterRequestDTO model) => RegisterAsync(model);
        #endregion
    }
}
