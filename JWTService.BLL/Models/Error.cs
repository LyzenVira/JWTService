

namespace JWTService.BLL.Models
{
    public class Error
    {
        public Error(string message)
        {
            Message = message;
        }

        public Error(string property, string message)
        {
            Property = property;
            Message = message;
        }
        public string Message { get; private set; }
        public string? Property { get; set; }
    }
}
