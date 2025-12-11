using Microsoft.AspNetCore.Mvc;
using TriviaApp.API.Services.Question;
using TriviaApp.API.Services.Question.Models;

namespace TriviaApp.API.Controllers.Question;

[ApiController]
[Route("api/[controller]")]
public class QuestionController(IQuestionService questionService) : TriviaControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(GetQuestionResponseModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetQuestion()
    {
        var result = await questionService.GetQuestion(RemoteIpAddress);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CheckQuestionResponseModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> CheckQuestion([FromBody] CheckQuestionRequestModel requestModel)
    {
        var result = await questionService.CheckQuestion(RemoteIpAddress, requestModel);
        return Ok(result);
    }
}
