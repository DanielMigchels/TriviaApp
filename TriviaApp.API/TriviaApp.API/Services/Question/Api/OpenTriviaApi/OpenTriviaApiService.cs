using System.Net;
using TriviaApp.API.Services.Question.Api.Models;
using TriviaApp.API.Services.Question.Api.OpenTriviaApi.Models;
using TriviaApp.API.Services.Question.Models;

namespace TriviaApp.API.Services.Question.Api.OpenTriviaApi;

public class OpenTriviaApiService(ILogger<OpenTriviaApiService> logger, IOpenTriviaApiGateway openTriviaApiGateway) : IQuestionApiService
{
    public async Task<GetQuestionApiResponseModel?> GetQuestion()
    {
        try
        {
            var result = await openTriviaApiGateway.Get<OpenTriviaApiResponseModel>();

            if (result.ResponseCode != 0)
            {
                throw new Exception($"HTTP request succeeded, but response code from OpenTriviaApi response body is not 0 for success, it is {result.ResponseCode}");
            }

            var question = result.Results.FirstOrDefault();
            if (question == null)
            {
                throw new Exception($"We could not find a quesiton in the response from OpenTriviaApi.");
            }

            var questionType =
                question.Type == "multiple" ? QuestionType.MultipleChoice :
                question.Type == "boolean" ? QuestionType.TrueFalse :
                default;

            return new GetQuestionApiResponseModel()
            {
                Type = questionType,
                Question = WebUtility.HtmlDecode(question.Question).Replace('\"', '\''),
                CorrectAnswer = WebUtility.HtmlDecode(question.CorrectAnswer).Replace('\"', '\''),
                IncorrectAnswers = question.IncorrectAnswers.Select(x => WebUtility.HtmlDecode(x).Replace('\"', '\'')).ToList(),
                Difficulty = question.Difficulty,
                Category = question.Category,
            };
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while retrieving question from OpenTriviaApi.");
            return null;
        }
    }
}