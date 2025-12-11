using TriviaApp.API.Services.Question.Models;

namespace TriviaApp.API.Services.Question;

public interface IQuestionService
{
    Task<GetQuestionResponseModel> GetQuestion(string remoteIpAddress);
    Task<CheckQuestionResponseModel> CheckQuestion(string remoteIpAddress, CheckQuestionRequestModel requestModel);
}
