namespace Account.Api.ViewModels.Requests
{
    public record AccountRequestViewModel
    {
        public string UserName { get; init; }
        public string Name { get; init; }
        public string Password { get; init; }
    }
}
