﻿using System.Net;

namespace Application.CustomResponse;
public class Response<T> : IResponse
{
    public T? Data { get; set; }
    public string? Message { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    public bool IsSuccess { get; set; }
}

