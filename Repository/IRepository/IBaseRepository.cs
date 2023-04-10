using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Data;
using Repository.DbEntity;
using Repository.Model;

namespace Repository.IRepository
{
    /// <summary>
    /// 基础数据
    /// </summary>
    public interface IBaseRepository : IDependencyRepository, IAppBaseService<User>
    {

    }
}
