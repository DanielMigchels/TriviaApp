using AutoFixture;
using AutoFixture.AutoMoq;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using System.Text;
using TriviaApp.API.Services.Question;
using TriviaApp.API.Services.Question.Api;
using TriviaApp.API.Services.Question.Api.Models;
using TriviaApp.API.Services.Question.Models;
using Xunit;

namespace TriviaApp.Tests.Question;

public class QuestionServiceTests
{
    private readonly IFixture _fixture;

    public QuestionServiceTests()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
    }

    [Fact]
    public async Task GetQuestionMultipleChoice()
    {
        var questionApiService = new Mock<IQuestionApiService>();
        questionApiService.Setup(x => x.GetQuestion()).ReturnsAsync(new GetQuestionApiResponseModel()
        {
            Question = "What is 2+2",
            Category = "Math",
            Difficulty = "easy",
            Type = QuestionType.MultipleChoice,
            CorrectAnswer = "4",
            IncorrectAnswers = ["1", "3", "5"]
        });
        _fixture.Inject(questionApiService);

        var distributedCache = new Mock<IDistributedCache>();
        _fixture.Inject(distributedCache);

        var questionService = _fixture.Create<QuestionService>();
        var result = await questionService.GetQuestion("10.40.20.1");
        
        distributedCache.Verify(x => x.SetAsync(It.Is<string>(k => k == $"10.40.20.1{result.Id}"), It.Is<byte[]>(v => Encoding.UTF8.GetString(v) == "4"), It.IsAny<DistributedCacheEntryOptions>(), It.IsAny<CancellationToken>()), Times.Exactly(1));
        Assert.NotNull(result);        
    }

    [Fact]
    public async Task CheckQuestionCorrectAnswer()
    {
        var distributedCache = new Mock<IDistributedCache>();
        distributedCache.Setup(x => x.GetAsync(It.Is<string>(k => k == "10.40.20.1216c527c-7b69-42c9-919e-9c22754fa6c4"), It.IsAny<CancellationToken>())).ReturnsAsync(Encoding.UTF8.GetBytes("4"));
        _fixture.Inject(distributedCache);

        var questionService = _fixture.Create<QuestionService>();
        var result = await questionService.CheckQuestion("10.40.20.1", new CheckQuestionRequestModel()
        {
            Id = Guid.Parse("216c527c-7b69-42c9-919e-9c22754fa6c4"),
            Answer = "4",
        });

        Assert.NotNull(result);
        Assert.True(result.WasAnswerCorrect);
    }

    [Fact]
    public async Task CheckQuestionWrongAnswer()
    {
        var distributedCache = new Mock<IDistributedCache>();
        distributedCache.Setup(x => x.GetAsync(It.Is<string>(k => k == "10.40.20.1216c527c-7b69-42c9-919e-9c22754fa6c4"), It.IsAny<CancellationToken>())).ReturnsAsync(Encoding.UTF8.GetBytes("4"));
        _fixture.Inject(distributedCache);

        var questionService = _fixture.Create<QuestionService>();
        var result = await questionService.CheckQuestion("10.40.20.1", new CheckQuestionRequestModel()
        {
            Id = Guid.Parse("216c527c-7b69-42c9-919e-9c22754fa6c4"),
            Answer = "5",
        });

        Assert.NotNull(result);
        Assert.False(result.WasAnswerCorrect);
    }
}
