using System.Net.Http.Headers;
using FilmFinderTMDB.Source.Data.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FilmFinderTMDB.Source.Services
{
    public class WebApiClient
    {
        private string _bearerToken = string.Empty;

        public WebApiClient(string bearerToken)
        {
            _bearerToken = bearerToken;
        }

        public async Task<string> GetData(string requestUri)
        {
            string serviceResult = string.Empty;
            try
            {
                using (var httpClient = new HttpClient())
                {
                    SetupHttpClient(httpClient);

                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUri);
                    string userAgent = GetUserAgent();
                    request.Headers.Add("User-Agent", userAgent);
                    HttpResponseMessage response = await httpClient.SendAsync(request);
                    serviceResult = await ProcessResponse(response);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return serviceResult;
        }

        private string GetUserAgent()
        {
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                return "Mozilla/5.0 (Linux; Android 10; Mobile) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/126.0.0.0 Mobile Safari/537.36";
            }
            else if (DeviceInfo.Platform == DevicePlatform.iOS)
            {
                return "Mozilla/5.0 (iPhone; CPU iPhone OS 14_0 like Mac OS X) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/126.0.0.0 Mobile Safari/537.36";
            }
            else
            {
                return "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/126.0.0.0 Safari/537.36";
            }
        }

        private void SetupHttpClient(HttpClient httpClient)
        {
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _bearerToken);
        }

        private async Task<string> ProcessResponse(HttpResponseMessage response)
        {
            string serviceResult = string.Empty;
            string statusCode = response.StatusCode.ToString();
            if (response.IsSuccessStatusCode)
            {
                serviceResult = await response.Content.ReadAsStringAsync();
            }
            else
            {
                var responseBody = JsonConvert.DeserializeObject<ServerResponse>(await response.Content.ReadAsStringAsync());
                serviceResult = responseBody.ErrorMessage;
            }
            return serviceResult;
        }

        private async Task ProcessResponseStatus(HttpResponseMessage response, BaseResponseModel responseStatus)
        {
            if (response.IsSuccessStatusCode)
            {
                responseStatus.IsSuccess = true;
                responseStatus.Message = response.ReasonPhrase.ToString();
            }
            else
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var errorResponse = JObject.Parse(responseContent);
                if (errorResponse["errors"] != null)
                {
                    var serverResponse = JsonConvert.DeserializeObject<ServerResponse>(await response.Content.ReadAsStringAsync());
                    responseStatus.IsSuccess = false;
                    responseStatus.Message = serverResponse.ErrorMessage;
                }
                else
                {
                    var errorResult = JsonConvert.DeserializeObject<BaseResponseModel>(await response.Content.ReadAsStringAsync());
                    responseStatus.IsSuccess = false;
                    responseStatus.Message = errorResult.Message;
                }
            }
        }
    }

}

