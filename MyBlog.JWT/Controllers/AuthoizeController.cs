// 添加 MyBlog.JWT 项目是 为了
//    添加一个服务器，用于JWT 获取 token码
// 然后添加一个控制器： 授权 控制器 AuthoizeController
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyBlog.IService;
using MyBlog.JWT.Utility._MD5;
using MyBlog.JWT.Utility.ApiResult;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;



namespace MyBlog.JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthoizeController : ControllerBase
    {
        private readonly IWriterInfoService _iWriterInfoService;
        // 依赖注入
        public   AuthoizeController(IWriterInfoService iWriterInfoService)
        {
            this._iWriterInfoService = iWriterInfoService;
        }

        // 登录 方法
        [HttpPost("Login")]
        public async Task<ApiResult> Login(string username, string userpwd)
        {
            string pwd = MD5Helper.MD5Encrypt32(userpwd);//加密
            //数据 校验

            var writer = await _iWriterInfoService.FindAsync(c => c.UserName == username && c.UserPwd == pwd);
            if (writer != null)
            {
                //登录 成功
                //  开始 copy 代码
                var claims = new Claim[]
                {
                    // 3个键值对   key：value
                    // 不能 放 敏感 信息
                    new Claim(ClaimTypes.Name,writer.Name), //相当于 身份证
                    new Claim("Id",writer.Id.ToString()),// 在这里添加Id
                     new Claim("UserName",writer.UserName)
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SDMC-CJAS1-SAD-DFSFA-SADHJVF-VF"));// 密钥 最少16位
                //issuer 代表 颁发 Token的 web 应用程序，audience 是Token的受理者
                // 6060 在 launchSettings.json里设置， 谁颁发的token
                // 5000 在 MyBlog.WebApi的 launchSettings.json里设置 
                var token = new JwtSecurityToken(
                    issuer:"http://localhost:6060",
                    audience: "http://localhost:5000",
                    claims: claims,
                    notBefore:DateTime.Now,
                    expires: DateTime.Now.AddHours(1),// 1小时 后 过期
                    signingCredentials: new SigningCredentials(key,SecurityAlgorithms.HmacSha256)// key后 是逗号， 把密钥 用上
                    );
                var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                //  结束 copy 代码

                // 以后前端访问数据时要带上这个 Token
                return ApiResultHelper.Success(jwtToken);// 返回 Token 值
            }
            else
            {
                return ApiResultHelper.Error("账号或者密码错误");
            }
        }
    }
}
