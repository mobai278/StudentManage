using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Repository.DbEntity;
using Repository.IRepository;

namespace Repository.Repository
{
    public class AuthRepository : AppBaseService<User>, IAuthRepository
    {
        public AuthRepository(IConfiguration config)
        {
            Config = config;
        }

        /// <summary>
        /// 登录接口
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="role">角色 0：管理员，1：教师，2：学生</param>
        /// <returns>登录成功返回true，否则返回false</returns>
        public async Task<int> Login(string userName, string password, int role)
        {
            switch (role)
            {
                case 0:
                    return await this.ExecuteScalarAsync<int>(
                        $@"SELECT Id FROM dbo.T_User WHERE UserName = @userName AND Password = @password",
                        new { userName, password });
                case 1:
                    return await this.ExecuteScalarAsync<int>(
                               $@"SELECT Id FROM dbo.T_Teacher WHERE SerialNumber = @userName AND Password = @password",
                               new { userName, password });

                case 2:
                    return await this.ExecuteScalarAsync<int>(
                               $@"SELECT Id FROM dbo.T_Student WHERE SerialNumber = @userName AND Password = @password",
                               new { userName, password });
            }

            return 0;
        }

    }
}
