using TriviaApp.API.Services.Question.Api;
using TriviaApp.API.Services.Question.Models;

namespace TriviaApp.API.Services.Question;

public class QuestionService(ILogger<QuestionService> logger, IQuestionApiService questionApiService) : IQuestionService
{
    public async Task<GetQuestionResponseModel> GetQuestion()
    {
        var question = await questionApiService.GetQuestion();

        if (question is null)
        {
            logger.LogError("Failed to retrieve question from API service.");
            return new GetQuestionResponseModel { Success = false };
        }

        var answers = question.IncorrectAnswers.Concat(new[] { question.CorrectAnswer }).OrderBy(_ => Guid.NewGuid()).ToList();

        return new GetQuestionResponseModel()
        {
            Success = true,
            Answers = answers,
            Category = question.Category,
            Difficulty = question.Difficulty,
            Question = question.Question,
            Type = question.Type,
        };
    }

    public async Task<CheckQuestionResponseModel> CheckQuestion(CheckQuestionRequestModel requestModel)
    {
        await Task.Delay(1);

        return new CheckQuestionResponseModel()
        {

        };
    }
}
