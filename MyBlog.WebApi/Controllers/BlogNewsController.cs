using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBlog.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyBlog.WebApi.Utility.ApiResult;
using MyBlog.Model;
using Microsoft.AspNetCore.Authorization;// 1引入  2  [Authorize]

namespace MyBlog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //2 使用
    [Authorize]
    public class BlogNewsController : ControllerBase
    {
        private readonly IBlogNewsService _iBlogNewsService;
        public BlogNewsController(IBlogNewsService iBlogNewsService)// 注入 服务
        {
           this._iBlogNewsService = iBlogNewsService;
        }

        /// <summary>
        /// 获取/读取 文章
        /// </summary>
        /// <returns></returns>
        [HttpGet(template: "BlogNews")]// get 方法
        public async Task<ActionResult<ApiResult>> GetBlogNews()//
        {
            int id = Convert.ToInt32(this.User.FindFirst("Id").Value);
            var data = await _iBlogNewsService.QueryAsync(c=> c.WriterId==id);
            if (data == null) return ApiResultHelper.Error(msg: "没有更多的文章");
            return ApiResultHelper.Success(data);
        }

        /// <summary>
        /// 添加 文章
        /// </summary>
        /// <returns></returns>
        [HttpPost(template:"Create")]//  post 方法
        public async Task<ActionResult<ApiResult>> Create( string title ,string content, int typeid)//
        {
            // 此处 应该 放数据 验证模块
            // 如果 数据 合格
            BlogNews blogNews = new BlogNews
            {
                BrowseCount = 0,
                Content = content,
                LikeCount=0,
                Time=DateTime.Now,
                Title=title,
                TypeId=typeid,
                WriterId= Convert.ToInt32(this.User.FindFirst("Id").Value)
            };
            bool b = await _iBlogNewsService.CreateAsync(blogNews);
            if (!b) return ApiResultHelper.Error(msg: "文章添加失败，服务器发生错误");
            return ApiResultHelper.Success(blogNews);
        }
        /// <summary>
        /// 删除 文章
        /// </summary>
        /// <returns></returns>
        [HttpDelete(template: "Delete")]//  post 方法
        public async Task<ActionResult<ApiResult>> Delete( int  id)//
        {
            // 此处 应该 放数据 验证模块
            // 如果 数据 合格
            
            bool b = await _iBlogNewsService.DeleteAsync(id);
            if (!b) return ApiResultHelper.Error(msg: "文章删除失败，服务器发生错误");
            return ApiResultHelper.Success(b);
        }


        /// <summary>
        /// 修改 文章
        /// </summary>
        /// <returns></returns>
        [HttpPut(template: "Edit")]//  post 方法
        public async Task<ActionResult<ApiResult>> Edit(int id,string title, string content, int typeid)//
        {
            // 此处 应该 放数据 验证模块
            // 如果 数据 合格
            //第一步 找到文章
            var blogNews = await _iBlogNewsService.FindAsync(id);
            if(blogNews==null) return ApiResultHelper.Error(msg: "没有找到文章");
            //第2步 修改文章
            blogNews.Title= title;
            blogNews.Content =content;
            blogNews.TypeId = typeid;

            //第3步 写入数据库文章
            bool b = await _iBlogNewsService.EditAsync(blogNews);
            if (!b) return ApiResultHelper.Error(msg: "文章修改失败，服务器发生错误");
            return ApiResultHelper.Success(blogNews);
        }
    }
}
