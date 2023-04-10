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
    /// 课程管理
    /// </summary>
    public class CourseRepository : AppBaseService<Course>, ICourseRepository
    {
        public CourseRepository(IConfiguration config)
        {
            Config = config;
        }

        /// <summary>
        /// 角色列表（分页）
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">条数</param>
        public async Task<PageData<CourseModel>> GetPageDataAsync(string name, int pageIndex, int pageSize)
        {
            var sql = $@"SELECT c.*,m.Name AS MajorName FROM dbo.T_Course c LEFT JOIN dbo.T_Major m ON c.MajorId = m.Id WHERE 1=1";
            if (!string.IsNullOrEmpty(name))
            {
                sql += $" AND c.Name LIKE @name ";
            }

            return await this.PagedAsync<CourseModel>(sql, new { name = "%" + name + "%" },
                null, 1, 10);
        }

        /// <summary>
        /// 通过学生id获取学生选取的所有课程
        /// </summary>
        /// <param name="studentId">学生id</param>
        public async Task<IEnumerable<Course>> GetCoursesByStudentId(int studentId)
        {
            var sql = $@"
                SELECT  c.*
                FROM    dbo.T_Score s
                        LEFT JOIN dbo.T_TeachInfo t ON s.TeachInfoId = t.Id
                        LEFT JOIN dbo.T_Course c ON t.CourseId = c.Id
                WHERE   s.StudentId = @studentId";
            return await this.FindAllAsync<Course>(sql, new { studentId });
        }

    }
}
