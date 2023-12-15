

using System.Net;
using JWTService.BLL.Models.Exceptions;

namespace JWTService.BLL.Models
{
    public class ResponseEntity<TResult> : ResponseEntity
    {
        public ResponseEntity(HttpStatusCode httpStatusCode, TResult result) : base(httpStatusCode)
        {
            Result = result;
        }

        public TResult? Result { get; set; }
    }

    public class ResponseEntity
    {
        public ResponseEntity(HttpStatusCode httpStatusCode)
        {
            StatusCode = (int)httpStatusCode;
        }

        public ResponseEntity(HttpStatusCode httpStatusCode, IEnumerable<Error> errors)
        {
            StatusCode = (int)httpStatusCode;
            Errors = errors;
        }

        public ResponseEntity(int statusCode, IEnumerable<Error>? errors)
        {
            StatusCode = statusCode;
            Errors = errors;
        }

        public static ResponseEntity CreateWithOneMessage(IAppException exception)
        {
            return new ResponseEntity(exception.StatusCode, exception.Errors);
        }

        public int StatusCode { get; set; }

        public IEnumerable<Error>? Errors { get; set; }
    }
}
