namespace TriviaApp.API.Services.Question.Models;

public class CheckQuestionResponseModel
{
    public bool Success { get; set; }
    public bool? WasAnswerCorrect { get; set; }
    public string? CorrectAnswer { get; set; }
}
