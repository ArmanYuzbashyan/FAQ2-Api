using Microsoft.OpenApi.Models;

namespace FAQ2_Api
{
    internal class Info : OpenApiInfo
    {
        public string Title { get; set; }
        public string Version { get; set; }
    }
}