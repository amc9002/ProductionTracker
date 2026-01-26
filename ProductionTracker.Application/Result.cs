namespace ProductionTracker.Application
{
    public enum ResultStatus
    {
        Success,
        NotFound,
        Conflict,
        Invalid
    }

    public class Result(ResultStatus status, string? message, object? data)
    {
        public ResultStatus Status { get; } = status;
        public string? Message { get; } = message;
        public object? Data { get; } = data;
    }
}
