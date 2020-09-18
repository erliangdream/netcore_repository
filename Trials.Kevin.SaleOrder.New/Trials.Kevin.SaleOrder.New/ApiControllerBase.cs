using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Trials.Kevin.Contract.Attr;
using Trials.Kevin.Contract.Response;

namespace Trials.Kevin.SaleOrder.New.Host
{
    public class ApiControllerBase : ControllerBase
    {
        /// <summary>
        /// 验证有效dto，先只做string类型的
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="dto"></param>
        /// <returns></returns>
        public BaseResponse ValidDto<TDto>(TDto dto) where TDto : class
        {
            BaseResponse response = new BaseResponse();
            foreach (var prop in dto?.GetType().GetProperties())
            {
                DtoEmptyAttribute dtoEmptyAttribute = prop.GetCustomAttribute<DtoEmptyAttribute>();
                if (dtoEmptyAttribute != null)
                {
                    object value = prop.GetValue(dto);

                    if (prop.PropertyType == typeof(string))
                    {
                        if (value == null || string.IsNullOrEmpty(value.ToString()))
                        {
                            response.IsSuccess = false;
                            response.Code = (int)HttpStatusCode.BadRequest;
                            response.Message = dtoEmptyAttribute.EmptyMessage;
                            break;
                        }
                    }
                }
            }
            return response;
        }
    }
}
