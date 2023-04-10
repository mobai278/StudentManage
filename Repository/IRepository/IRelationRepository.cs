using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Repository.DbEntity;

namespace Repository.IRepository
{
    public interface IRelationRepository : IDependencyRepository, IAppBaseService<Relation>
    {

        /// <summary>
        /// 通过第一个关联表id获取关联
        /// </summary>
        /// <param name="key">多对多关系标识</param>
        /// <param name="firstId">关联第一个表id</param>
        /// <returns></returns>
        Task<List<Relation>> GetRelationsByFirstId(string key, int firstId);

    }
}
