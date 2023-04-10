using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Repository.DbEntity;
using Repository.IRepository;

namespace Repository.Repository
{
    public class TeacherRepository : AppBaseService<Teacher>, ITeacherRepository
    {
        public TeacherRepository(IConfiguration config)
        {
            Config = config;
        }

        /// <summary>
        /// 教师列表（分页）
        /// </summary>
        /// <param name="userName">用户名 or 真实姓名</param>
        /// <param name="phone">手机号</param>
        /// <param name="pageIndex">开始页数</param>
        /// <param name="pageSize">每页条数</param>
        public async Task<PageData<Teacher>> GetPageDataAsync(string userName, string phone, int pageIndex, int pageSize)
        {
            var sql = $@"SELECT * FROM dbo.T_Teacher WHERE 1=1";
            if (!string.IsNullOrEmpty(userName))
            {
                sql += $" AND ( UserName LIKE @userName OR RealName LIKE @userName ) ";
            }

            if (!string.IsNullOrEmpty(phone))
            {
                sql += $" AND Phone LIKE @phone ";
            }

            return await this.PagedAsync<Teacher>(sql, new { userName = "%" + userName + "%", phone = "%" + phone + "%" },
                null, pageIndex, pageSize);
        }

    }
}
