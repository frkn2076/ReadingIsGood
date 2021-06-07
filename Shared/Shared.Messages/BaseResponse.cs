namespace Shared.Messages
{
    public class BaseResponse
    {
        public static BaseResponse Success => new(null);
        public static BaseResponse Fail => new("Something went wrong");
        public static BaseResponse Create(string message) => new(message);

        private BaseResponse(string message)
        {
            IsError = !string.IsNullOrWhiteSpace(message);
            Message = message;
        }

        public BaseResponse() { }

        public bool IsError { get; set; }
        public string Message { get; set; }
        public string AccessToken { get; set; }
        public int AccessTokenExpiresIn { get; set; }
        public string RefreshToken { get; set; }
        public int RefreshTokenExpiresIn { get; set; }
    }
}
