using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Data;
using Repository.DbEntity;
using Repository.Model;

namespace Repository.IRepository
{
    public interface IScoreRepository : IDependencyRepository, IAppBaseService<StudentScore>
    {
        /// <summary>
        /// 成绩列表（分页）
        /// </summary>
        /// <param name="studentName">学生姓名</param>
        /// <param name="pageIndex">开始页数</param>
        /// <param name="pageSize">每页条数</param>
        Task<PageData<StudentScore>> GetPageDataAsync(string id,string studentName,string courseName, int pageIndex, int pageSize);
        


    }
}
