using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Repository.DbEntity;

namespace Repository.IRepository
{
    public interface IAuthRepository : IDependencyRepository, IAppBaseService<User>
    {
        /// <summary>
        /// 登录接口
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="role">角色 0：管理员，1：教师，2：学生</param>
        /// <returns>返回userId</returns>
        Task<int> Login(string userName,string password,int role);

    }
}
