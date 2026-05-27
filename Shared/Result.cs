using Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared;

public class Result<T> : IResult<T>
{
    public string Message { get ; set ; }
    public bool IsSuccess { get ; set ; }
    public int Code { get ; set ; }
    public string? Token { get ; set; }
    public T? Data { get ; set ; }
    public Exception? Exception { get ; set ; }


    public static Result<T> Success(string message)
    {
        return new Result<T>
        {
            Message=message,
            IsSuccess=true,
            Code = 200
        };
    }

    public static Result<T> Success(T data, string message)
    {
        return new Result<T>
        {
            Message = message,
            IsSuccess = true,
            Code = 200,
            Data = data
        };
    }

    public static Result<T> Success(T data,string token, string message)
    {
        return new Result<T>
        {
            Message = message,
            IsSuccess = true,
            Code = 200,
            Data = data,
            Token = token
        };
    }

    public static Result<T> BadRequest(string message)
    {
        return new Result<T>
        {
            Message = message,
            IsSuccess = false,
            Code = 400
        };
    }
}
