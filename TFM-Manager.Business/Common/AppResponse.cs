using System;
using System.Collections.Generic;
using System.Text;

namespace TFM_Manager.Business.Common
{
    public sealed class AppResponse<T>
    {
        public bool IsSuccess { get; init; }
        public string Message { get; init; } = string.Empty;
        public T? Data { get; init; }
        public AppError Error { get; init; }
        public AppResponse()
        {
            IsSuccess = true;
            Message = string.Empty;
            Data = default;
        }
        public static AppResponse<T> Success(bool isSuccess, string message, T? data)
        {
            return new AppResponse<T>
            {
                IsSuccess = isSuccess,
                Message = message,
                Data = data
            };
        }
        public static AppResponse<T> Failure(string message, string code)
        {
            return new AppResponse<T>
            {
                IsSuccess = false,
                Message = message,
                Error = new AppError
                {
                    Code = code,
                    Message = message
                }
            };
        }
    }
    
    
}
