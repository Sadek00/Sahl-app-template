using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text.Json;

namespace AppTemplate.Service.Helper
{
    public class ApiCallService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public ApiCallService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<T> CallApiWithSecretKeyAsync<T>(string apiUrl)
        {
            using (HttpClient client = _httpClientFactory.CreateClient())
            {
                // Retrieve the secret key from configuration
                string secretKey = _configuration["ApiSettings:LOGCRMSecretKey"];

                // Add the secret key as a custom header (replace "X-Secret-Key" with the appropriate header name)
                client.DefaultRequestHeaders.Add("CRMLOG-Secret-Key", secretKey);

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                return await HandleApiResponse<T>(response);
            }
        }
        public async Task<T> CallApiWithSecretKeyAsync<T>(string apiUrl, string requestBody)
        {
            using (HttpClient client = _httpClientFactory.CreateClient())
            {
                // Retrieve the secret key from configuration
                string secretKey = _configuration["ApiSettings:LOGCRMSecretKey"];

                // Add the secret key as a custom header
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("CRMLOG-Secret-Key", secretKey);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Serialize the request body to JSON
                //string jsonContent = JsonConvert.SerializeObject(requestBody);
                HttpContent httpContent = new StringContent(requestBody, System.Text.Encoding.UTF8, "application/json");

                // Send the request with the JSON body
                HttpResponseMessage response = await client.PostAsync(apiUrl, httpContent);
                var apiResponse = await HandleApiResponse<T>(response);
                return apiResponse;
            }
        }

        public async Task<T> UpdateApiWithSecretKeyAsync<T>(string apiUrl, string requestBody)
        {
            using (HttpClient client = _httpClientFactory.CreateClient())
            {
                // Retrieve the secret key from configuration
                string secretKey = _configuration["ApiSettings:LOGCRMSecretKey"];

                // Add the secret key as a custom header
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("CRMLOG-Secret-Key", secretKey);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Serialize the request body to JSON
                //string jsonContent = JsonConvert.SerializeObject(requestBody);
                HttpContent httpContent = new StringContent(requestBody, System.Text.Encoding.UTF8, "application/json");

                // Send the request with the JSON body
                HttpResponseMessage response = await client.PutAsync(apiUrl, httpContent);
                var apiResponse = await HandleApiResponse<T>(response);
                return apiResponse;
            }
        }

        private async Task<T> HandleApiResponse<T>(HttpResponseMessage response)
        {
            //response.EnsureSuccessStatusCode();
            string responseData = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(responseData);
            return result;
        }
    }
}
