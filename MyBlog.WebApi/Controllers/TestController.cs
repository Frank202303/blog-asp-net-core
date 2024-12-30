using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;// 1引入  2  [Authorize]

namespace MyBlog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase // 添加测试 控制器
    {
        [HttpGet("NoAuthorize")]
        public string NoAuthorize()
        {
            //这个 方法 / Action： 不 做 鉴权
            return "this is NoAuthorize";
        }
        [Authorize] //这个 方法 / Action：  做 鉴权
        [HttpGet("Authorize")]
        public string Authorize()
        {
            return "this is Authorize";
        }
    }
}
