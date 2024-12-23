using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.JWT.Utility.ApiResult
{
    public class ApiResult
    {
        public int Code { get; set; }// 200   404   500
        public string Msg { get; set; }
        public int Total { get; set; }
        public dynamic Data { get; set; }
    }
}
