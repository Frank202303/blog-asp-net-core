using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MyBlog.Model;
using MyBlog.Model.DTO;

namespace MyBlog.WebApi.Utility._AutoMapper
{
    // 2. 定义一个类， 继承Profile
    public class CustomAutoMapperProfile: Profile
    {
        public CustomAutoMapperProfile()
        {
            // 把 WriterInfo类 映射成 WriterDTO类
             
            // WriterInfo类 包含密码
            // WriterDTO类： 没有密码 属性
            base.CreateMap<WriterInfo, WriterDTO>();//  配置 完毕
        }

    }
}
