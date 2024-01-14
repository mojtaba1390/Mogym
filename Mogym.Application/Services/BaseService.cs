using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Interfaces;
using Mogym.Common;
using Newtonsoft.Json;

namespace Mogym.Application.Services
{
    public class BaseService:IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public BaseService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<ResultDTO?> SendAsync(RequestDTO requestDto)
        {
            try
            {
                HttpClient client = _httpClientFactory.CreateClient("MogymApi");
                HttpRequestMessage message = new();
                message.Headers.Add("Accept","application/json");
                message.RequestUri = new Uri(requestDto.Url);
                if (requestDto.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8,
                        "application/json");
                }

                HttpResponseMessage apiResponse = null;

                switch (requestDto.ApiType)
                {
                    case EnumApiType.Get:
                        message.Method = HttpMethod.Get;
                        break;
                    case EnumApiType.Post:
                        message.Method = HttpMethod.Post;
                        break;
                }


                apiResponse = await client.SendAsync(message);
                switch (apiResponse.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return new() {IsSuccess = false, Message = "Not Found"};
                    case HttpStatusCode.Forbidden:
                        return new() {IsSuccess = false, Message = "Access Denied"};
                    case HttpStatusCode.Unauthorized:
                        return new() {IsSuccess = false, Message = "Unauthorized" };
                    case HttpStatusCode.InternalServerError:
                        return new() {IsSuccess = false, Message = "Internal Server Error" };
                    default:
                        var apiContent = await apiResponse.Content.ReadAsStringAsync();
                        var apiResponseDto = JsonConvert.DeserializeObject<ResultDTO>(apiContent);
                        return apiResponseDto;
                }
            }
            catch (Exception e)
            {
                var dto = new ResultDTO()
                {
                    IsSuccess = false,
                    Message = e.Message
                };
                return dto;
            }

        }
    }
}
