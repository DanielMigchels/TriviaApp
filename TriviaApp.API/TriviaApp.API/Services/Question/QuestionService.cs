using Microsoft.Extensions.Caching.Distributed;
using TriviaApp.API.Services.Question.Api;
using TriviaApp.API.Services.Question.Models;

namespace TriviaApp.API.Services.Question;

public class QuestionService(ILogger<QuestionService> logger, IQuestionApiService questionApiService, IDistributedCache distributedCache) : IQuestionService
{
    public async Task<GetQuestionResponseModel> GetQuestion(string remoteIpAddress)
    {
        var question = await questionApiService.GetQuestion();

        if (question is null)
        {
            logger.LogError("Failed to retrieve question from API service.");
            return new GetQuestionResponseModel { Success = false };
        }

        logger.LogInformation("Correct answer for question {question} is {answer}", question.Question, question.CorrectAnswer);
        var questionId = Guid.NewGuid();
        await distributedCache.SetStringAsync($"{remoteIpAddress}{questionId}", question.CorrectAnswer, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
        });

        var answers = question.IncorrectAnswers
            .Concat(new[] { question.CorrectAnswer })
            .ToList();

        if (question.Type == QuestionType.MultipleChoice)
        {
            // Fisher–Yates shuffle
            var rng = Random.Shared;
            for (int i = answers.Count - 1; i > 0; i--)
            {
                int j = rng.Next(i + 1);
                (answers[i], answers[j]) = (answers[j], answers[i]);
            }
        }

        answers.Reverse();

        return new GetQuestionResponseModel()
        {
            Id = questionId,
            Success = true,
            Answers = answers,
            Category = question.Category,
            Difficulty = question.Difficulty,
            Question = question.Question,
            Type = question.Type,
        };
    }

    public async Task<CheckQuestionResponseModel> CheckQuestion(string remoteIpAddress, CheckQuestionRequestModel requestModel)
    {
        var cachedAnswer = await distributedCache.GetStringAsync($"{remoteIpAddress}{requestModel.Id}");

        if (cachedAnswer == null)
        {
            logger.LogWarning("No cached answer found for question ID {QuestionId} and IP {RemoteIpAddress}", requestModel.Id, remoteIpAddress);
            return new CheckQuestionResponseModel
            {
                Success = false,
            };
        }

        await distributedCache.RemoveAsync($"{remoteIpAddress}{requestModel.Id}");
        logger.LogInformation("Cached answer removed for question ID {QuestionId} and IP {RemoteIpAddress}", requestModel.Id, remoteIpAddress);

        return new CheckQuestionResponseModel()
        {
            Success = true,
            WasAnswerCorrect = string.Equals(cachedAnswer, requestModel.Answer, StringComparison.OrdinalIgnoreCase),
            CorrectAnswer = cachedAnswer,
        };
    }
}
