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
    public class RelationRepository : AppBaseService<Relation>, IRelationRepository
    {
        public RelationRepository(IConfiguration config)
        {
            Config = config;
        }

        /// <summary>
        /// 通过第一个关联表id获取关联
        /// </summary>
        /// <param name="key">多对多关系标识</param>
        /// <param name="firstId">关联第一个表id</param>
        /// <returns></returns>
        public async Task<List<Relation>> GetRelationsByFirstId(string key, int firstId)
        {
            var sql = $@"SELECT * FROM dbo.T_Relation WHERE [Key] = @key AND FirstKeyId = @firstId";
            var result = await this.FindAllAsync<Relation>(sql, new {key, firstId});
            return result.ToList();
        }

    }
}
