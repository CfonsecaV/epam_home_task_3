using RestSharp;
using System.Threading.Tasks;

namespace EpamHomeTask.Core
{
    public class RestClientManager
    {
        private readonly RestClient _client;

        public RestClientManager(string baseUrl)
        {
            _client = new RestClient(baseUrl);
        }

        public async Task<RestResponse<T>> ExecuteAsync<T>(RestRequest request) where T : new()
        {
            var response = await _client.ExecuteAsync<T>(request);
            return response;
        }
    }
}
