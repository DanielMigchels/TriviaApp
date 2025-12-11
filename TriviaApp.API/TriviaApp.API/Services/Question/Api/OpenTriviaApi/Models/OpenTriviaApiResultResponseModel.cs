using System.Text.Json.Serialization;

namespace TriviaApp.API.Services.Question.Api.OpenTriviaApi.Models;

public class OpenTriviaApiResultResponseModel
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("difficulty")]
    public string Difficulty { get; set; } = string.Empty;

    [JsonPropertyName("category")]
    public string Category { get; set; } = string.Empty;

    [JsonPropertyName("question")]
    public string Question { get; set; } = string.Empty;

    [JsonPropertyName("correct_answer")]
    public string CorrectAnswer { get; set; } = string.Empty;

    [JsonPropertyName("incorrect_answers")]
    public List<string> IncorrectAnswers { get; set; } = [];
}