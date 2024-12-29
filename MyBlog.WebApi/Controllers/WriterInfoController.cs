using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBlog.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyBlog.WebApi.Utility.ApiResult;
// 手动 引入
using MyBlog.WebApi.Utility._MD5;
using MyBlog.Model;
//using Microsoft.AspNetCore.Authorization;// 1引入  2  [Authorize]
using AutoMapper;
using MyBlog.Model.DTO;

namespace MyBlog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class WriterInfoController  : ControllerBase
    {
        private readonly IWriterInfoService _iWriterInfoService;
        // 依赖注入 Service
        public WriterInfoController(IWriterInfoService iWriterInfoService)
        {
            this._iWriterInfoService = iWriterInfoService;
        }
        /// <summary>
        /// GET方法
        /// </summary>
        /// <returns></returns> 
        [HttpGet("Types")]
        public async Task<ApiResult> Types()
        {
            var types = await _iWriterInfoService.QueryAsync();
            if(types.Count==0) return ApiResultHelper.Error("没有更多的类型");
            return ApiResultHelper.Success(types);
        }


        /// <summary>
        /// 插入  Create
        /// </summary>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<ApiResult> Create(string name, string username, string userpwd)
        {
            // 数据验证
            //如果合格
            WriterInfo writer = new WriterInfo
            { 
                 Name=name,
                 UserName=username,
                 // MD5 加密
                 UserPwd=MD5Helper.MD5Encrypt32(userpwd)  
            };
            //判断 数据库中是否已经存在 要添加的账号
            var oldWriter =await  _iWriterInfoService.FindAsync(c => c.UserName == username);
            if(oldWriter!=null) 
                return ApiResultHelper.Error(msg: "账号已经存在");

            bool b = await _iWriterInfoService.CreateAsync(writer);
            if (!b) 
                return ApiResultHelper.Error(msg: "添加作者失败");

            return ApiResultHelper.Success(writer);
        }

        /// <summary>
        ///  删除  
        /// </summary>
        /// <returns></returns>
        //[httpdelete("delete")]
        // public async task<apiresult> delete(int id)
        //{

        //   bool b = await _iwriterinfoservice.deleteasync(id);
        //   if (!b) return apiresulthelper.error(msg: "删除失败");
        //   return apiresulthelper.success(b);
        //}


        /// <summary>
        ///  修改 名字  
        /// </summary>
        /// <returns></returns>
        [HttpPut("Edit")]
        public async Task<ApiResult> Edit(  string name)// 需要 JWT实现后，才能 测试
        {

            int id = Convert.ToInt32(this.User.FindFirst("Id").Value);// JWT必须有id， 此处 才能 得到Id
            var writer = await _iWriterInfoService.FindAsync(id);
            writer.Name = name;
            // 再写回 数据库 
            bool b = await _iWriterInfoService.EditAsync(writer);
           
            if (!b) 
                return ApiResultHelper.Error("用户名修改失败");
            return ApiResultHelper.Success("用户名修改成功");
        }
        //[AllowAnonymous]
        [HttpGet("FindWriter")]
        public async Task<ApiResult> FindWriter([FromServices]IMapper iMapper ,int id)// 调用方法 时注入 进来
        {
            var writer = await _iWriterInfoService.FindAsync(id);
            var writerDTO = iMapper.Map<WriterDTO>(writer);//p  < 想要映射的DTO >  (放 原数据 )  相当于 过滤
            return ApiResultHelper.Success(writer); //   不yong AutoMapper时  //使用 AutoMapper时 
        }
    }
}
