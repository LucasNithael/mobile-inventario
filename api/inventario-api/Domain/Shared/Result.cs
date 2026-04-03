namespace inventario_api.Domain.Shared
{
    public class Result
    {
        public bool Success { get; }
        public int StatusCode { get; }
        public string? Message { get; }
        public List<string> Errors { get; }

        protected Result(bool success, int statusCode, string? message, List<string>? errors)
        {
            Success = success;
            StatusCode = statusCode;
            Message = message;
            Errors = errors ?? new List<string>();
        }

        public static Result Ok(string? message = null)
            => new Result(true, 200, message, null);

        public static Result Created(string? message = null)
            => new Result(true, 201, message, null);

        public static Result NoContent(string? message = null)
            => new Result(true, 204, message, null);

        public static Result Fail(string message, int statusCode = 400)
            => new Result(false, statusCode, message, new List<string>());

        public static Result Fail(List<string> errors, string? message = null, int statusCode = 400)
            => new Result(false, statusCode, message, errors);
    }

    public class Result<T> : Result
    {
        public T? Data { get; }

        private Result(bool success, int statusCode, string? message, List<string>? errors, T? data)
            : base(success, statusCode, message, errors)
        {
            Data = data;
        }

        public static Result<T> Ok(T data, string? message = null)
            => new Result<T>(true, 200, message, null, data);

        public static Result<T> Created(T data, string? message = null)
            => new Result<T>(true, 201, message, null, data);

        public static Result<T> Fail(string message, int statusCode = 400)
            => new Result<T>(false, statusCode, message, new List<string>(), default);

        public static Result<T> Fail(List<string> errors, string? message = null, int statusCode = 400)
            => new Result<T>(false, statusCode, message, errors, default);
    }
}