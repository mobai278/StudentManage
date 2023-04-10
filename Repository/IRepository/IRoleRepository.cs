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
    /// 角色管理
    /// </summary>
    public interface IRoleRepository : IDependencyRepository, IAppBaseService<Role>
    {

        /// <summary>
        /// 角色列表（分页）
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        Task<PageData<Role>> GetRolePageDataAsync(string name, int pageIndex, int pageSize);

        /// <summary>
        /// 通过userId获取角色列表
        /// </summary>
        Task<IEnumerable<MultiSelectInfo>> GetRoleListByUserIdAsync(int userId);

    }
}
