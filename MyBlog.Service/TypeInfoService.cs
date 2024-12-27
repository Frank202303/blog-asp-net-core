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
    public class TypeInfoService : BaseService<TypeInfo>, ITypeInfoService
    {
        private readonly ITypeInfoRepository _iTypeInfoRepository; //子类 独有
        public TypeInfoService(ITypeInfoRepository iTypeInfoRepository)
        {
            // 从子类的构造函数 传入 到 父类
            base._iBaseRepository = iTypeInfoRepository;
            _iTypeInfoRepository = iTypeInfoRepository;
        }
    }
}
