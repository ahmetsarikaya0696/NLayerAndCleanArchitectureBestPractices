using System.Net;

namespace Application;

/// <summary>A service operation returns exactly one success or failure case.</summary>
public abstract record ServiceResult<T>
{
    private ServiceResult() { }

    public sealed record Succeeded(
        T Data,
        HttpStatusCode StatusCode = HttpStatusCode.OK,
        string? UrlAsCreated = null) : ServiceResult<T>;

    public sealed record Failed(
        IReadOnlyList<string> ErrorMessages,
        HttpStatusCode StatusCode = HttpStatusCode.BadRequest) : ServiceResult<T>;

    public static ServiceResult<T> Success(T data, HttpStatusCode statusCode = HttpStatusCode.OK)
        => new Succeeded(data, statusCode);

    public static ServiceResult<T> SuccessAsCreated(T data, string url)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(url);
        return new Succeeded(data, HttpStatusCode.Created, url);
    }

    public static ServiceResult<T> Fail(
        List<string> errorMessages,
        HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        ArgumentNullException.ThrowIfNull(errorMessages);
        if (errorMessages.Count == 0)
            throw new ArgumentException("At least one error message is required.", nameof(errorMessages));

        return new Failed(errorMessages.ToArray(), statusCode);
    }

    public static ServiceResult<T> Fail(
        string errorMessage,
        HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(errorMessage);
        return new Failed([errorMessage], statusCode);
    }
}

public abstract record ServiceResult
{
    private ServiceResult() { }

    public sealed record Succeeded(
        HttpStatusCode StatusCode = HttpStatusCode.OK) : ServiceResult;

    public sealed record Failed(
        IReadOnlyList<string> ErrorMessages,
        HttpStatusCode StatusCode = HttpStatusCode.BadRequest) : ServiceResult;

    public static ServiceResult Success(HttpStatusCode statusCode = HttpStatusCode.OK)
        => new Succeeded(statusCode);

    public static ServiceResult Fail(
        List<string> errorMessages,
        HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        ArgumentNullException.ThrowIfNull(errorMessages);
        if (errorMessages.Count == 0)
            throw new ArgumentException("At least one error message is required.", nameof(errorMessages));

        return new Failed(errorMessages.ToArray(), statusCode);
    }

    public static ServiceResult Fail(
        string errorMessage,
        HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(errorMessage);
        return new Failed([errorMessage], statusCode);
    }
}
