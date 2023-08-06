namespace ThoughtzLand.Core.Services
{
    public class OperationResult
    {
        public OperationResult(bool success, string message) 
        {
            Success = success;
            Message = message;
        }
        public bool Success { get; }
        public string Message { get; }
    }
    public class OperationResult<TContent>
    {
        public OperationResult(bool success, string message, TContent content) 
        {
            Success = success;
            Message = message;
            Content = content;
        }
        public bool Success { get; }
        public string Message { get; }
        public TContent Content { get; }
    }
}
