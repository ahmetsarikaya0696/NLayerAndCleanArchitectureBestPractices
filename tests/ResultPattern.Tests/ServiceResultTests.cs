using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace ResultPattern.Tests;

public class ServiceResultTests
{
    [Fact]
    public void CleanSuccessContainsData()
    {
        var result = Application.ServiceResult<int>.Success(42);
        var success = Assert.IsType<Application.ServiceResult<int>.Succeeded>(result);
        Assert.Equal(42, success.Data);
        Assert.Equal(HttpStatusCode.OK, success.StatusCode);
    }

    [Fact]
    public void NLayerFailureContainsErrors()
    {
        var result = App.Services.ServiceResult<string>.Fail("Missing", HttpStatusCode.NotFound);
        var failure = Assert.IsType<App.Services.ServiceResult<string>.Failed>(result);
        Assert.Equal(HttpStatusCode.NotFound, failure.StatusCode);
        Assert.Equal("Missing", Assert.Single(failure.ErrorMessages));
    }

    [Fact]
    public void CleanCreatedResponsePreservesPayloadAndLocation()
    {
        var controller = new API.Controllers.CustomBaseController();
        var result = Application.ServiceResult<int>.SuccessAsCreated(42, "/api/products/42");
        var response = Assert.IsType<CreatedResult>(controller.CreateActionResult(result));
        Assert.Equal("/api/products/42", response.Location);
        using var json = JsonDocument.Parse(JsonSerializer.Serialize(
            response.Value, new JsonSerializerOptions(JsonSerializerDefaults.Web)));
        Assert.Equal(42, json.RootElement.GetProperty("data").GetInt32());
        Assert.Equal(JsonValueKind.Null, json.RootElement.GetProperty("errorMessages").ValueKind);
    }

    [Fact]
    public void NLayerNotFoundResponsePreservesErrors()
    {
        var controller = new App.API.Controllers.CustomBaseController();
        var result = App.Services.ServiceResult<string>.Fail("Missing", HttpStatusCode.NotFound);
        var response = Assert.IsType<ObjectResult>(controller.CreateActionResult(result));
        Assert.Equal(404, response.StatusCode);
        using var json = JsonDocument.Parse(JsonSerializer.Serialize(
            response.Value, new JsonSerializerOptions(JsonSerializerDefaults.Web)));
        Assert.Equal(JsonValueKind.Null, json.RootElement.GetProperty("data").ValueKind);
        Assert.Equal("Missing", json.RootElement.GetProperty("errorMessages")[0].GetString());
    }

    [Fact]
    public void NonGenericNoContentIs204()
    {
        var controller = new API.Controllers.CustomBaseController();
        var result = Application.ServiceResult.Success(HttpStatusCode.NoContent);
        Assert.IsType<NoContentResult>(controller.CreateActionResult(result));
    }

    [Fact]
    public void InvalidFailuresAreRejected()
    {
        Assert.Throws<ArgumentException>(() => Application.ServiceResult<string>.Fail(" "));
        Assert.Throws<ArgumentException>(() => App.Services.ServiceResult.Fail(new List<string>()));
    }
}
