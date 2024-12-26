﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 
 

namespace MyBlog.JWT.Utility.ApiResult
{
    public static   class ApiResultHelper
    {
        //成功时，返回的数据
        public static  ApiResult Success(dynamic data)//返回 一个 类:ApiResult
        {
            return new ApiResult
            {
                Code = 200,
                Data = data,
                Msg ="操作成功",
                Total=0
            };
        }
        // Success 方法 重载
        public static ApiResult Success(dynamic data,  int  total)//返回 一个 类:ApiResult  [ RefAsync<int>约等于int
        {
            return new ApiResult
            {
                Code = 200,
                Data = data,
                Msg = "操作成功",
                Total = total
            };
        }
        //失败时，返回的数据
        public static ApiResult Error(string msg)//返回 一个 类:ApiResult
        {
            return new ApiResult
            {
                Code = 500,
                Data = null,
                Msg = msg,
                Total = 0
            };
        }
    }
}