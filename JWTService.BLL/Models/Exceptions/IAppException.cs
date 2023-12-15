namespace JWTService.BLL.Models.Exceptions
{
    public interface IAppException
    {
        int StatusCode { get; }
        IEnumerable<Error> Errors { get; }
    }
}
