using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Data;
using Repository.DbEntity;
using Repository.Model;

namespace Repository.IRepository
{
    public interface ITeachInfoRepository : IDependencyRepository, IAppBaseService<TeachInfo>
    {
        /// <summary>
        /// 授课信息列表（分页）
        /// </summary>
        /// <param name="teacherName">教师姓名</param>
        /// <param name="courseName">课程名称</param>
        /// <param name="pageIndex">开始页数</param>
        /// <param name="pageSize">每页条数</param>
        Task<PageData<TeachInfoModel>> GetPageDataAsync(string teacherName, string courseName, int pageIndex, int pageSize);
    }
}
