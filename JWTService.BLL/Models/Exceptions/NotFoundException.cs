

namespace JWTService.BLL.Models.Exceptions
{
    public class NotFoundException : BaseException
    {
        public override int StatusCode => 404;
        public static NotFoundException Default<TEntity>()
        {
            return new NotFoundException($"{typeof(TEntity)} not found");
        }

        public NotFoundException(string error) : base(error)
        {
        }

        public NotFoundException(IEnumerable<string> error) : base(error)
        {
        }
    }
}
