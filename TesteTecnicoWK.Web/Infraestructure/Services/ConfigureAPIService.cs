using System.Net.Http.Headers;
using TesteTecnicoWK.Web.Infraestructure.Interfaces;

namespace TesteTecnicoWK.Web.Infraestructure.Services
{
    public class ConfigureAPIService : IConfigureAPIService
    {
        private readonly IConfiguration _configuration;
        public ConfigureAPIService(IConfiguration configuration)
        {
            _configuration = configuration;   
        }

        public HttpClient Configure()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_configuration.GetValue<string>("ApiUrl"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;  
        }
    }
}
