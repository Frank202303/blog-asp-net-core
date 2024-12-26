using MyBlog.IRepository;
using MyBlog.IService;
using MyBlog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Service
{
    // 定义3个 类：继承于BaseService类， 实现IBlogNewsService接口
    public class BlogNewsService : BaseService<BlogNews>, IBlogNewsService
    {
        private readonly IBlogNewsRepository _iBlogNewsRepository; //子类 独有
        public BlogNewsService(IBlogNewsRepository iBlogNewsRepository)
        {
            base._iBaseRepository = iBlogNewsRepository;
            _iBlogNewsRepository = iBlogNewsRepository;
        }
    }
}
