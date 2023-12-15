using static System.Runtime.InteropServices.JavaScript.JSType;

namespace JWTService.BLL.Models.Exceptions
{
    public abstract class BaseException : Exception, IAppException
    {
        public virtual int StatusCode { get; }

        public IEnumerable<Error> Errors { get; }

        protected BaseException(string error)
        {
            Errors = new[] { new Error(error) };
        }

        protected BaseException(IEnumerable<string> error)
        {
            Errors = new List<Error>(error.Select(e => new Error(e)));
        }
    }
}
