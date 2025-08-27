using System.Net;

namespace Application.CustomResponse;

public interface IResponse
{
    string? Message { get; set; }
    HttpStatusCode StatusCode { get; set; }
    bool IsSuccess { get; set; }
}

