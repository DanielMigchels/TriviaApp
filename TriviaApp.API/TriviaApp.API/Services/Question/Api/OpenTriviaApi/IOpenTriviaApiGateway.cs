namespace TriviaApp.API.Services.Question.Api.OpenTriviaApi;

public interface IOpenTriviaApiGateway
{
    public Task<T> Get<T>(string action = "");
    public Task<T> Post<T, T2>(T2 query, string action = "");
}
