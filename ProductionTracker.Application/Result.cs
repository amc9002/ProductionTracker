namespace ProductionTracker.Application
{
    public enum ResultStatus
    {
        Success,
        NotFound,
        Conflict,
        Invalid
    }

    public class Result
    {
        public ResultStatus Status { get; }
        public string? Message { get; }
        public object? Data { get; }

        private Result(ResultStatus status, string? message, object? data)
        {
            Status = status;
            Message = message;
            Data = data;
        }

        public static Result Success(object? data = null)
            => new(ResultStatus.Success, null, data);

        public static Result Invalid(string message)
            => new(ResultStatus.Invalid, message, null);

        public static Result NotFound(string message)
            => new(ResultStatus.NotFound, message, null);

        public static Result Conflict(string message, object? data = null)
            => new(ResultStatus.Conflict, message, data);
    }

}
