namespace CustomerChat.Application.Common;

/// <summary>
/// Discriminated union representing success or failure.
/// Avoids throwing exceptions for expected failures (validation, not found).
/// </summary>
public sealed class Result<T>
{
    private Result(T value)
    {
        Value = value;
        IsSuccess = true;
        Error = string.Empty;
    }

    private Result(string error)
    {
        Value = default;
        IsSuccess = false;
        Error = error;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public T? Value { get; }
    public string Error { get; }

    public static Result<T> Success(T value) => new(value);
    public static Result<T> Failure(string error) => new(error);

    public TResult Match<TResult>(Func<T, TResult> onSuccess, Func<string, TResult> onFailure) =>
        IsSuccess ? onSuccess(Value!) : onFailure(Error);
}

public sealed class Result
{
    private Result() { IsSuccess = true; Error = string.Empty; }
    private Result(string error) { IsSuccess = false; Error = error; }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public string Error { get; }

    public static Result Success() => new();
    public static Result Failure(string error) => new(error);
    //public Result Match<Result>(Action<Result> onSuccess, Func<string, Result> onFailure) =>
    //    IsSuccess ? onSuccess : onFailure(Error);
}
