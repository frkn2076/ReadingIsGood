using Shared.Messages;

namespace Stock.Api.ViewModels.Responses
{
    public class StocksResponseViewModel : BaseResponse
    {
        public StockResponseViewModel[] Stocks { get; set; }
    }
}
