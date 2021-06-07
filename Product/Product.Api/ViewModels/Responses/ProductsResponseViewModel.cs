using Shared.Messages;
using System.Collections.Generic;

namespace Product.Api.ViewModels.Responses
{
    public class ProductsResponseViewModel : BaseResponse
    {
        public List<ProductResponseViewModel> Products { get; set; }
    }
}
