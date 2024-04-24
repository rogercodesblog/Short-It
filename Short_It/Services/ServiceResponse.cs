namespace Short_It.Services
{
    public class ServiceResponse<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Error { get; set; }
        public List<string> ErrorMessagees { get; set; } = null;
    }
}
