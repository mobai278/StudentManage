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
    /// 角色管理
    /// </summary>
    public class RoleRepository : AppBaseService<Role>, IRoleRepository
    {
        public RoleRepository(IConfiguration config)
        {
            Config = config;
        }

        /// <summary>
        /// 角色列表（分页）
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">条数</param>
        public async Task<PageData<Role>> GetRolePageDataAsync(string name, int pageIndex, int pageSize)
        {
            var sql = $@"SELECT * FROM dbo.T_Role WHERE 1=1";
            if (!string.IsNullOrEmpty(name))
            {
                sql += $" AND Name LIKE @name ";
            }

            return await this.PagedAsync<Role>(sql, new { name = "%" + name + "%" },
                null, 1, 10);
        }

        /// <summary>
        /// 通过userId获取角色列表
        /// </summary>
        public async Task<IEnumerable<MultiSelectInfo>> GetRoleListByUserIdAsync(int userId)
        {
            var sql = $@"
                    SELECT  r.Name ,
                            r.Id AS Value ,
                            CASE WHEN ( SELECT  re.Id
                                        FROM    dbo.T_Relation re
                                        WHERE   re.[Key] = 'UserRole'
                                                AND re.SecondKeyId = r.Id
                                                AND re.FirstKeyId = @userId
                                      ) > 0 THEN 'true'
                                 ELSE 'false'
                            END AS Selected
                    FROM    dbo.T_Role r";
            return await this.FindAllAsync<MultiSelectInfo>(sql, new { userId });
        }
      
    }
}
