﻿using MyBlog.IRepository;
using MyBlog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Repository
{
    // the subclass must first inherit the parent class, and then implement the sub-interface;
    public class WriterInfoRepository:BaseRepository<WriterInfo>, IWriterInfoRepository
    {
    }
}
