namespace Projeto.Domain.ViewModels
{
    public class Response
    {
        public Response(int statusCode, object content, string message)
        {
            StatusCode = statusCode;
            Message = message;
            Content = content;
        }

        public Response(int statusCode, object content)
        {
            StatusCode = statusCode;
            Content = content;
        }
        public Response(string message, object content)
        {
            Message = message;
            Content = content;
        }

        public Response(int statusCode, string message)
        {
            Message = message;
            StatusCode = statusCode;
        }
        public Response(int statusCode)
        {
            StatusCode = statusCode;
        }

        public Response(string message)
        {
            Message = message;
        }
        public Response(object content)
        {
            Content = content;
        }

        public Response() { }

        public int StatusCode { get; set; }
        public string Message { get; set; }
        public object Content { get; set; }
    }
}
