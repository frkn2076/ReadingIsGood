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
            TypeAdapterConfig<RegisterRequestViewModel, RegisterRequestDTO>.NewConfig();

            TypeAdapterConfig<RegisterRequestDTO, Registration>.NewConfig()
                .Map(dest => dest.Password, src => Helper.SHA512(src.Password));
        }
    }
}
