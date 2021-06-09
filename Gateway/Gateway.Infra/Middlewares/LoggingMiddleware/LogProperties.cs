using System.ComponentModel;

namespace Gateway.Infra.Middlewares.LoggingMiddleware
{
    public class LogProperties
    {
        [DefaultValue(true)]
        public bool HasRequestPath { get; set; }
        [DefaultValue(false)]
        public bool HasRemoteIpAddress { get; set; }
        [DefaultValue(false)]
        public bool HasRequestHeaders { get; set; }
        [DefaultValue(false)]
        public bool HasRequestBody { get; set; }
        [DefaultValue(false)]
        public bool HasResponseStatusCode { get; set; }
        [DefaultValue(false)]
        public bool HasResponseBody { get; set; }
        [DefaultValue(false)]
        public bool HasCreatedAt { get; set; }
    }
}
