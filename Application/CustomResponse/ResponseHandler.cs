﻿using System.Net;

namespace Application.CustomResponse;

public class ResponseHandler
{
    public static Response<T> Created<T>(string message = null!)
    {
        return new Response<T>()
        {
            StatusCode = HttpStatusCode.Created,
            Message = message ?? "Created Successfully",
            IsSuccess = true,
        };
    }
    public static Response<T> Deleted<T>(string message = null!)
    {
        return new Response<T>()
        {
            StatusCode = HttpStatusCode.NoContent,
            Message = message ?? "Deleted Successfully",
            IsSuccess = true,
        };
    }
    public static Response<T> NotFound<T>(string message = null!)
    {
        return new Response<T>()
        {
            StatusCode = HttpStatusCode.NotFound,
            Message = message ?? "Entity is not founed",
            IsSuccess = false,
        };
    }
    public static Response<T> Success<T>(T data, string message = null!)
    {
        return new Response<T>()
        {
            Data = data,
            StatusCode = HttpStatusCode.OK,
            Message = message ?? "Success",
            IsSuccess = true,
        };
    }
    public static Response<T> NoContent<T>(string message = null!)
    {
        return new Response<T>()
        {
            StatusCode = HttpStatusCode.NoContent,
            Message = message ?? "Success process",
            IsSuccess = true,
        };
    }
    public static Response<T> BadRequest<T>(string message = null!)
    {
        return new Response<T>()
        {
            StatusCode = HttpStatusCode.BadRequest,
            Message = message ?? "Bad Request",
            IsSuccess = false,
        };
    }
    public static Response<T> UnAuthorize<T>(string message = null!)
    {
        return new Response<T>()
        {
            StatusCode = HttpStatusCode.Unauthorized,
            Message = message ?? "Unauthorized",
            IsSuccess = false,
        };
    }
    public static Response<T> InternalServerError<T>(string message = null!)
    {
        return new Response<T>()
        {
            StatusCode = HttpStatusCode.InternalServerError,
            Message = message ?? "An error happens while saving the data",
            IsSuccess = false,
        };
    }
    public static Response<T> UnprocessableEntity<T>(T entity = null!) where T : class
    {
        return new Response<T>()
        {
            Data = entity,
            StatusCode = HttpStatusCode.UnprocessableEntity,
            Message = "UnprocessableEntity",
            IsSuccess = false,
        };
    }
}

