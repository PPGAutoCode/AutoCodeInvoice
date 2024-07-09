namespace ProjectName.ControllersExceptions
{
    public class BusinessException : Exception
    {
        public string Type { get; }
        public int Status { get; }
        public string Title { get; }
        public string Detail { get; }

        public BusinessException(string type, int status, string title, string detail)
            : base(detail)
        {
            Type = type;
            Status = status;
            Title = title;
            Detail = detail;
        }
    }

    public class TechnicalException : Exception
    {
        public string Type { get; }
        public int Status { get; }
        public string Title { get; }
        public string Detail { get; }

        public TechnicalException(string type, int status, string title, string detail)
            : base(detail)
        {
            Type = type;
            Status = status;
            Title = title;
            Detail = detail;
        }
    }
}
