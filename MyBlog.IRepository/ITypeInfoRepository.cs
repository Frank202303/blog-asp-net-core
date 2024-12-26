using MyBlog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.IRepository
{
    // 接口 继承 父接口
    public interface ITypeInfoRepository : IBaseRepository<TypeInfo>
    {
    }
}
