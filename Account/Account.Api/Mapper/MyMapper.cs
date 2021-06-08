using Account.Api.ViewModels.Requests;
using Account.Business.DTOs;
using Account.DataAccess.Entities;
using Mapster;
using Shared.Helper;

namespace Account.Api.Mapper
{
    public class MyMapper
    {
        public static void MapsterInit()
        {
            TypeAdapterConfig<RegisterRequestViewModel, AccountRequestDTO>.NewConfig();

            TypeAdapterConfig<AccountRequestDTO, Registration>.NewConfig()
                .Map(dest => dest.Password, src => Helper.SHA512(src.Password));

            TypeAdapterConfig<Registration, AccountResponseDTO>.NewConfig()
                .Map(dest => dest.UserId, src => src.Id)
                .Map(dest => dest.CustomerName, src => src.Name);

        }
    }
}
