
namespace TriviaApp.API.Services.Question.Models;

public class GetQuestionResponseModel
{
    public bool Success { get; set; }
    public Guid? Id { get; set; }
    public QuestionType? Type { get; set; }
    public string? Question { get; set; }
    public List<string>? Answers { get; set; }
    public string? Difficulty { get; set; }
    public string? Category { get; set; }
}
