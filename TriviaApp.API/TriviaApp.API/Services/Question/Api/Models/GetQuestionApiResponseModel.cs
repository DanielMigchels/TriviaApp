using TriviaApp.API.Services.Question.Models;

namespace TriviaApp.API.Services.Question.Api.Models;

public class GetQuestionApiResponseModel
{
    public QuestionType Type { get; set; }
    public string Question { get; set; } = string.Empty;
    public string CorrectAnswer { get; set; } = string.Empty;
    public List<string> IncorrectAnswers { get; set; } = [];
    public string? Difficulty { get; set; }
    public string? Category { get; set; }
}
