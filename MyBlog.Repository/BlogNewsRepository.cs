﻿using MyBlog.IRepository;
using MyBlog.Model;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;//创建任务并异步运行它们
using System.Linq;
using SqlSugar;
using System.Linq.Expressions;

namespace MyBlog.Repository
{
    public class BlogNewsRepository : BaseRepository<BlogNews>, IBlogNewsRepository
    {
        // The subclass must first inherit the parent class, and then implement the sub-interface;
        //  2. 所以重写这个 virtual 方法： 实现导航 查询
        public async override Task<List<BlogNews>> QueryAsync()
        {
            // 这里的导航 查询，相当于 连接查询  
            //  .Mapper(c =>c.TypeInfo, c => c.TypeId, c => c.TypeInfo.Id)    **错误 TypeInfo,原因： TypeInfo忘记继承BaseId了
            return await  base.Context.Queryable<BlogNews>()
                .Mapper(c => c.TypeInfo, c => c.TypeId, c => c.TypeInfo.Id)
                .Mapper(c => c.WriterInfo, c => c.WriterId, c => c.WriterInfo.Id)
                .ToListAsync();
        }
        // 自定义 查询的： 使用场景，只返回当前用户自己的blog
        public async override Task<List<BlogNews>> QueryAsync(Expression<Func<BlogNews, bool>> func)
        {
            //  .Mapper(c => c.TypeInfo, c => c.TypeId, c => c.TypeInfo.Id)    **错误 TypeInfo
            return await base.Context.Queryable<BlogNews>()
              .Where(func)

              .Mapper(c => c.TypeInfo, c => c.TypeId, c => c.TypeInfo.Id)
              .Mapper(c => c.WriterInfo, c => c.WriterId, c => c.WriterInfo.Id)
              .ToListAsync();
        }
    }
}
