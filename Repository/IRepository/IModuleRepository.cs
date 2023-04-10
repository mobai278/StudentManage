using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Repository.DbEntity;

namespace Repository.IRepository
{
    public interface IModuleRepository : IDependencyRepository, IAppBaseService<Module>
    {
        /// <summary>
        /// 获取教师模块
        /// </summary>
        Task<List<Module>> GetTeacherModules(List<int> roleIds);

        /// <summary>
        /// 获取学生模块
        /// </summary>
        Task<List<Module>> GetStudentModules(List<int> roleIds);

        /// <summary>
        /// 获取管理员模块
        /// </summary>
        /// <param name="roleIds">角色ids</param>
        Task<List<Module>> GetModules(List<int> roleIds);
    }
}
