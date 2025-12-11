using TriviaApp.API.Services.Question.Models;

namespace TriviaApp.API.Services.Question;

public interface IQuestionService
{
    Task<GetQuestionResponseModel> GetQuestion();
    Task<CheckQuestionResponseModel> CheckQuestion(CheckQuestionRequestModel requestModel);
}
