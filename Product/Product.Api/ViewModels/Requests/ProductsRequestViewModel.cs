using System.Collections.Generic;

namespace Product.Api.ViewModels.Requests
{
    public record ProductsRequestViewModel
    {
        public List<ProductRequestViewModel> Products { get; init; }
    }
}
