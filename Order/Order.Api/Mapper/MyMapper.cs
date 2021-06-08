using Mapster;
using Order.Api.ViewModels.Requests;
using Order.Business.DTOs;

namespace Order.Api.Mapper
{
    public class MyMapper
    {
        public static void MapsterInit()
        {
            TypeAdapterConfig<ProductOrderRequestViewModel, ProductOrderRequestDTO>.NewConfig();


        }
    }
}
