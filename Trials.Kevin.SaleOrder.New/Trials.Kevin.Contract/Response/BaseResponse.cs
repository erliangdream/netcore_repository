using System;
using System.Collections.Generic;
using System.Text;

namespace Trials.Kevin.Contract.Response
{
    /// <summary>
    /// 基础响应
    /// </summary>
    public class BaseResponse
    {
        public bool IsSuccess { get; set; }
        public int Code { get; set; }

        public string Message { get; set; }

        public BaseResponse()
        {
            IsSuccess = true;
            Code = 200;
            Message = "Success";
        }
    }

    /// <summary>
    /// Data响应
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseResponse<T>: BaseResponse
    {
        public T Data { get; set; }
    }
}
