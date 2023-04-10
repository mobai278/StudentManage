using System;
using System.Collections.Generic;
using System.Linq;
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
    /// 班级管理
    /// </summary>
    public class ClassRepository : AppBaseService<Class>, IClassRepository
    {
        public ClassRepository(IConfiguration config)
        {
            Config = config;
        }

        /// <summary>
        /// 角色列表（分页）
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">条数</param>
        public async Task<PageData<ClassModel>> GetPageDataAsync(string name, int pageIndex, int pageSize)
        {
            var sql = $@"SELECT c.*,m.Name AS MajorName FROM dbo.T_Class c LEFT JOIN dbo.T_Major m ON c.MajorId = m.Id WHERE 1=1";
            if (!string.IsNullOrEmpty(name))
            {
                sql += $" AND c.Name LIKE @name ";
            }

            return await this.PagedAsync<ClassModel>(sql, new { name = "%" + name + "%" },
                null, 1, 10);
        }

    }
}
