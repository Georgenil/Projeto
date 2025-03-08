using System.Net;
using System.Text;

namespace Projeto.Infra.Utils.ExtensionMethod
{
    public class Response<TEntity>
    {
        public Dictionary<string, string[]> Dictionary = new Dictionary<string, string[]>();

        public TEntity Entity
        {
            get;
            set;
        }
        private string _messageDetail { get; set; }
        private string _message { get; set; }
        public string Message
        {
            get
            {
                return _message;
            }

            set
            {
                _message = value;
                if (value != null)
                {
                    Status = Status == HttpStatusCode.OK ? HttpStatusCode.BadRequest : Status;
                    if (Messages == null)
                    {
                        var msgs = new List<string>() { _message };

                        if (!string.IsNullOrEmpty(_messageDetail))
                        {
                            msgs.Add(_messageDetail);
                        }

                        Messages = msgs.ToArray();
                    }
                    else
                    {
                        var msgs = Messages.ToList();

                        msgs.Add(_message);

                        Messages = msgs.ToArray();
                    }
                }
            }
        }
        public string MessageDetail
        {
            get
            {
                return _messageDetail;
            }

            set
            {
                _messageDetail = value;
                if (value != null)
                {
                    Status = HttpStatusCode.BadRequest;
                    if (Messages == null) Messages = new string[] { _message, _messageDetail };
                    Code = "Bad";
                }
            }
        }

        public string Version { get; set; }
        public HttpStatusCode Status { get; set; }
        public string Code { get; set; }
        public string[] Messages { get; set; }
        public KeyValuePair<string, string>[] DetailedMessages { get; set; }
        private Exception exception { get; set; }
        public static Response<TEntity> Create() { return new Response<TEntity>(); }


        public Response()
        {
            Status = HttpStatusCode.OK;
            Code = "Clear";

        }

        public Response(HttpStatusCode status, string message)
        {
            Status = status;
            Message = message;
        }
        public Response(HttpStatusCode status, string message, TEntity entity)
        {
            Status = status;
            Message = message;
            Entity = entity;
        }

        public virtual void Copy<OtherEntity>(Response<OtherEntity> other)
        {
            this.Code = other.Code;
            this.Message = other.Message;
            this.Messages = other.Messages;
            this.Status = other.Status;

            if (this.Messages != null && this.Messages.Any())
            {
                var message = new StringBuilder();

                foreach (var item in this.Messages)
                {
                    message.AppendLine(item);
                }

                Error = new Error()
                {
                    Code = (int)other.Status,
                    Message = message.ToString()
                };
            }
        }
        public void Exception(Exception exception)
        {
            this.exception = exception;
            this.Code = string.Concat("Exception");
            this.Messages = new string[] { exception.Message, exception.StackTrace };
            this.Status = HttpStatusCode.InternalServerError;
            if (exception.InnerException != null)
            {
                this.Messages = new string[] { "<h3>", exception.Source, "</h3>", exception.Message, exception.StackTrace, "<hr /><h3>", exception.InnerException.Source, "</h3>", exception.InnerException.Message, exception.InnerException.StackTrace };
            }

            Error = new Error()
            {
                Code = (int)HttpStatusCode.InternalServerError,
                Message = string.Concat("Message: ", exception.Message, "Stack Trace: ", exception.StackTrace)
            };
        }

        public Exception GetException()
        {
            return this.exception;
        }

        public Error Error { get; set; }
    }

    public class Error
    {
        public int Code { get; set; }
        public string Message { get; set; }
    }

    public class Response<TEntity, OtherEntity>
    {
        public OtherEntity Other
        {
            get;
            set;
        }

        public TEntity Entity
        {
            get;
            set;
        }
        public string Version { get; set; }
        public HttpStatusCode Status { get; set; }
        public string Code { get; set; }
        public string[] Messages { get; set; }
        private Exception exception { get; set; }
        public static Response<TEntity, OtherEntity> Create() { return new Response<TEntity, OtherEntity>(); }


        public Response()
        {
            Status = HttpStatusCode.OK;
            Code = "Clear";

        }

        public void Copy(Response<OtherEntity> other)
        {
            this.Code = other.Code;
            this.Messages = other.Messages;
            this.Status = other.Status;
        }
        public void Exception(Exception exception)
        {
            this.exception = exception;
            this.Code = string.Concat("Exception");
            this.Messages = new string[] { exception.Message, exception.StackTrace };
            this.Status = HttpStatusCode.InternalServerError;
            if (exception.InnerException != null)
            {
                this.Messages = new string[] { "<h3>", exception.Source, "</h3>", exception.Message, exception.StackTrace, "<hr /><h3>", exception.InnerException.Source, "</h3>", exception.InnerException.Message, exception.InnerException.StackTrace };
            }

        }

        public Exception GetException()
        {
            return this.exception;
        }


    }
}
