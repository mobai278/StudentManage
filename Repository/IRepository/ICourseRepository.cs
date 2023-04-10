using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Data;
using Repository.DbEntity;
using Repository.Model;

namespace Repository.IRepository
{
    /// <summary>
    /// 课程管理
    /// </summary>
    public interface ICourseRepository : IDependencyRepository, IAppBaseService<Course>
    {

        /// <summary>
        /// 课程列表（分页）
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        Task<PageData<CourseModel>> GetPageDataAsync(string name, int pageIndex, int pageSize);

        /// <summary>
        /// 通过学生id获取学生选取的所有课程
        /// </summary>
        /// <param name="studentId">学生id</param>
        Task<IEnumerable<Course>> GetCoursesByStudentId(int studentId);
    }
}
