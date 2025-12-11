using TriviaApp.API.Services.Question.Api.Models;
using TriviaApp.API.Services.Question.Models;

namespace TriviaApp.API.Services.Question.Api;

public interface IQuestionApiService
{
    Task<GetQuestionApiResponseModel?> GetQuestion();
}
