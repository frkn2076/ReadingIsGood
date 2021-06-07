using Mapster;
using Product.Api.ViewModels.Requests;
using Product.Business.DTOs;
using Product.DataAccess.Entities;

namespace Product.Api.Mapper
{
    public class MyMapper
    {
        public static void MapsterInit()
        {
            TypeAdapterConfig<ProductRequestViewModel, ProductRequestDTO>.NewConfig();

            TypeAdapterConfig<Production, ProductResponseDTO>.NewConfig();

        }
    }
}
