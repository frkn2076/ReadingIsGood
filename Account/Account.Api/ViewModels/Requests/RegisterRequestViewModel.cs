namespace Account.Api.ViewModels.Requests
{
    public record RegisterRequestViewModel
    {
        public string UserName { get; init; }
        public string Name { get; init; }
        public string Password { get; init; }
    }
}
