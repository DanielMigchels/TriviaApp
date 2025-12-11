using TriviaApp.API.Utilities.Gateway;

namespace TriviaApp.API.Services.Question.Api.OpenTriviaApi;

public class OpenTriviaApiGateway : UnauthenticatedJsonApiGateway, IOpenTriviaApiGateway
{
    private const string BaseUrlValue = "https://opentdb.com/api.php?amount=1";

    public OpenTriviaApiGateway(HttpClient client) : base(client)
    {
    }

    protected override string BaseUrl => BaseUrlValue;
}