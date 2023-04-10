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
    /// 班级
    /// </summary>
    public interface IClassRepository : IDependencyRepository, IAppBaseService<Class>
    {

        /// <summary>
        /// 班级列表（分页）
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        Task<PageData<ClassModel>> GetPageDataAsync(string name, int pageIndex, int pageSize);

    }
}
