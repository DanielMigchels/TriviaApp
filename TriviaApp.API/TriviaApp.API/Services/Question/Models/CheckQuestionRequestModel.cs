namespace TriviaApp.API.Services.Question.Models;

public class CheckQuestionRequestModel
{
    public required Guid Id { get; set; }
    public required string Answer { get; set; }
}
