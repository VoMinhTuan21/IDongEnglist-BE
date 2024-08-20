namespace IDonEnglist.Application.Response
{
    public class BaseCommandResponse<T> where T : class
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; }
        public List<string>? Errors { get; set; }
        public T Data { get; set; }
    }
}
