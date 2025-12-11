using System.Text.Json.Serialization;

namespace TriviaApp.API.Services.Question.Api.OpenTriviaApi.Models;

public class OpenTriviaApiResponseModel
{
    [JsonPropertyName("response_code")]
    public int ResponseCode { get; set; }

    [JsonPropertyName("results")]
    public List<OpenTriviaApiResultResponseModel> Results { get; set; } = [];
}

