using Shared.Messages;

namespace Product.Api.ViewModels.Responses
{
    public class ProductResponseViewModel : BaseResponse
    {
        public int Id { get; set; }
        public string Name { get; init; }
        public string Color { get; init; }
        public string Weight { get; init; }
        public string Volume { get; init; }
    }
}
