namespace PlaceRentalApp.Application.Models;

public class ResultViewModel
{
    public string Message { get; set; }
    public bool IsSuccess { get; set; }

    public ResultViewModel(string message = "", bool isSuccess = true)
    {
        Message = message;
        IsSuccess = isSuccess;
    }

    public static ResultViewModel Success() => new ResultViewModel();

    public static ResultViewModel Error(string message) => new ResultViewModel(message, false);
}

public class ResultViewModel<T> : ResultViewModel
{
    public T? Data { get; set; }
    public ResultViewModel(T? data, string message = "", bool isSuccess = true)
        : base(message, isSuccess)
    {
        Data = data;
    }
    public static ResultViewModel<T> Success(T data) => new (data);
    public static ResultViewModel<T> Error(string message) => new (default, message, false);
}
