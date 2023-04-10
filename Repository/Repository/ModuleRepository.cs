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
    public class ModuleRepository : AppBaseService<Module>, IModuleRepository
    {
        public ModuleRepository(IConfiguration config)
        {
            Config = config;
        }

        /// <summary>
        /// 获取教师模块
        /// </summary>
        public async Task<List<Module>> GetTeacherModules(List<int> roleIds)
        {
            var sql = $@"
                    SELECT  *
                    FROM    dbo.T_Module
                    WHERE   Id IN ( SELECT DISTINCT
                                            SecondKeyId
                                    FROM    dbo.T_Relation
                                    WHERE   [Key] = 'RoleModule'
                                            AND FirstKeyId IN ( 2))";

            using (var conn = Connection)
            {
                await conn.OpenAsync();
                var models = await this.Connection.QueryAsync<Module>(sql);
                return models.ToList();
            }
        }

        /// <summary>
        /// 获取学生模块
        /// </summary>
        public async Task<List<Module>> GetStudentModules(List<int> roleIds)
        {
            var sql = $@"
                    SELECT  *
                    FROM    dbo.T_Module
                    WHERE   Id IN ( SELECT DISTINCT
                                            SecondKeyId
                                    FROM    dbo.T_Relation
                                    WHERE   [Key] = 'RoleModule'
                                            AND FirstKeyId IN (3) )";

            using (var conn = Connection)
            {
                await conn.OpenAsync();
                var models = await this.Connection.QueryAsync<Module>(sql);
                return models.ToList();
            }
        }

        /// <summary>
        /// 获取管理员模块
        /// </summary>
        /// <param name="roleIds">用户角色ids</param>
        public async Task<List<Module>> GetModules(List<int> roleIds)
        {
            int role = roleIds[0];
            var sql = $@"
                    SELECT  *
                    FROM    dbo.T_Module
                    WHERE   Id IN ( SELECT DISTINCT
                                            SecondKeyId
                                    FROM    dbo.T_Relation
                                    WHERE   [Key] = 'RoleModule'
                                            AND FirstKeyId IN ("+ role + ") )";

            using (var conn = Connection)
            {
                await conn.OpenAsync();
                var models = await this.Connection.QueryAsync<Module>(sql);
                return models.ToList();
            }
        }
    }
}
