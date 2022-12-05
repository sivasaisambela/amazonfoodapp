using AmazonFood.Web.Models;
using AmazonFood.Web.Services.IServices;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace AmazonFood.Web.Services
{
    public class BaseService : IBaseService
    {
        public  IHttpClientFactory httpClient { get; set; }

        public ResponseDto responseModel { get; set; }

        

        public BaseService(IHttpClientFactory httpClient)
        {
            this.responseModel = new ResponseDto();
            this.httpClient = httpClient;
        }
        public async Task<T> SendAsync<T>(ApiRequest apiRequest)
        {
            try
            {
                var client = httpClient.CreateClient("AmazonFoodAPI");
                HttpRequestMessage msg = new HttpRequestMessage();
                msg.Headers.Add("Accept", "application/json");
                msg.RequestUri =new Uri(apiRequest.Url);
                client.DefaultRequestHeaders.Clear();
                if(apiRequest.Data!= null)
                {

                    msg.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data),
                        Encoding.UTF8, "application/json");

                }

                HttpResponseMessage apiResponse = null;
                switch (apiRequest.ApiType)
                {
                    case SD.ApiType.POST:
                        msg.Method = HttpMethod.Post;
                        break;

                    case SD.ApiType.PUT:
                        msg.Method = HttpMethod.Put;
                        break;

                    case SD.ApiType.DELETE:
                        msg.Method = HttpMethod.Delete;
                        break;

                    default:
                        msg.Method = HttpMethod.Get;
                        break;

                }
                apiResponse = await client.SendAsync(msg);

                var apiContent = await apiResponse.Content.ReadAsStringAsync();

                var apiResponseDto = JsonConvert.DeserializeObject<T>(apiContent);
                return apiResponseDto;
            }
            catch(Exception ex)
            {
                var dto = new ResponseDto
                {
                    DisplayMessage = "Error",
                    ErrorMessage = new List<string> { Convert.ToString(ex.Message) },
                    IsSuccess = false
                };
                var res = JsonConvert.SerializeObject(dto);
                var apiResponseDto = JsonConvert.DeserializeObject<T>(res);
                return apiResponseDto; 
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}
