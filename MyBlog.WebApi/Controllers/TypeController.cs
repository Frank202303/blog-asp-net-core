using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBlog.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyBlog.WebApi.Utility.ApiResult;
using MyBlog.Model;
//using Microsoft.AspNetCore.Authorization;// 1引入  2  [Authorize]

namespace MyBlog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TypeController : ControllerBase
    {
        private readonly ITypeInfoService _iTypeInfoService;
        // 注入 服务service
       public TypeController(ITypeInfoService iTypeInfoService)
        {
            this._iTypeInfoService = iTypeInfoService;
        }
        /// <summary>
        /// GET方法 category
        /// </summary>
        /// <returns></returns>
        [HttpGet("Categories")]
        public async Task<ApiResult> Types()
        {
            var types = await _iTypeInfoService.QueryAsync();
            if(types.Count==0) 
                return ApiResultHelper.Error("没有更多的类型");
            return ApiResultHelper.Success(types);
        }
        /// <summary>
        /// 插入。create  
        /// </summary>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<ApiResult> Create(string name)
        {
            #region 数据验证
            if(String.IsNullOrWhiteSpace(name)) 
                return ApiResultHelper.Error("类型名不能为空");
            #endregion
            TypeInfo type = new TypeInfo
            {
                Name = name
            };
            bool b = await _iTypeInfoService.CreateAsync(type);
            if (!b) 
                return ApiResultHelper.Error(msg: "类型添加失败");
            
            return ApiResultHelper.Success(b);
        }
        /// <summary>
        ///  修改  HttpPut：update 
        /// </summary>
        /// <returns></returns>
        [HttpPut("Edit")]
        public async Task<ApiResult> Edit(int id, string name)
        {

            var type = await _iTypeInfoService.FindAsync(id);
            if (type == null) 
                return ApiResultHelper.Error(msg: "没有找到该文章类型");

            type.Name = name;
            bool b = await _iTypeInfoService.EditAsync(type);
            if (!b) 
                return ApiResultHelper.Error(msg: "修改类型失败");

            return ApiResultHelper.Success(type);
        }

        /// <summary>
        ///  删除  Delete
        /// </summary>
        /// <returns></returns>
        [HttpDelete("Delete")]
        public async Task<ApiResult> Delete(int id)
        {
            // 先查找
            bool b = await _iTypeInfoService.DeleteAsync(id);
            if (!b) 
                return ApiResultHelper.Error(msg: "删除文章类型失败");

            return ApiResultHelper.Success(b);
        }
    }
}
