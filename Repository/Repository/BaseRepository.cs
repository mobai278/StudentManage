using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Repository.DbEntity;
using Repository.IRepository;
using Repository.Model;

namespace Repository.Repository
{
    /// <summary>
    /// 基础数据
    /// </summary>
    public class BaseRepository : AppBaseService<User>, IBaseRepository
    {
        public BaseRepository(IConfiguration config)
        {
            Config = config;
        }

    }
}
