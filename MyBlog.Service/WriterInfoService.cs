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
    public class WriterInfoService:BaseService<WriterInfo>, IWriterInfoService
    {
        private readonly IWriterInfoRepository _iWriterInfoRepository;
        public WriterInfoService(IWriterInfoRepository iWriterInfoRepository)
        {
            base._iBaseRepository = iWriterInfoRepository;
            _iWriterInfoRepository = iWriterInfoRepository;
        }
    }
}
