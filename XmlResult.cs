namespace RestaurantWebAPI;

public class XmlResult<T> : IResult
{
    private static readonly XmlSerializer XmlSerializer = new(typeof(T));
    private readonly T _result;
    public XmlResult(T result) => _result = result;

    public Task ExecuteAsync(HttpContext httpContext)
    {
        using MemoryStream memoryStream = new MemoryStream();
        XmlSerializer.Serialize(memoryStream, _result);
        httpContext.Response.ContentType = "application/xml";
        memoryStream.Position = 0;
        return memoryStream.CopyToAsync(httpContext.Response.Body);
    }
}

internal static class XmlResultExtensions
{
    public static IResult Xml<T>(this IResultExtensions _, T result) =>
        new XmlResult<T>(result);
}