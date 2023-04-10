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
using Repository.Model;

namespace Repository.Repository
{
    public class TeachInfoRepository : AppBaseService<TeachInfo>, ITeachInfoRepository
    {
        public TeachInfoRepository(IConfiguration config)
        {
            Config = config;
        }

        /// <summary>
        /// 授课信息列表（分页）
        /// </summary>
        /// <param name="teacherName">教师姓名</param>
        /// <param name="courseName">课程名称</param>
        /// <param name="pageIndex">开始页数</param>
        /// <param name="pageSize">每页条数</param>
        public async Task<PageData<TeachInfoModel>> GetPageDataAsync(string teacherName, string courseName, int pageIndex, int pageSize)
        {
            var sql = $@"
                    SELECT  ti.* ,
                            m.Name AS MajorName,
                            t.SerialNumber AS TeacherNo ,
                            t.RealName AS TeacherName,
		                    cou.SerialNumber AS CourseNo ,
                            cou.Name AS CourseName
                    FROM    dbo.T_TeachInfo ti
                            LEFT JOIN dbo.T_Major m ON m.Id = ti.MajorId
                            LEFT JOIN dbo.T_Teacher t ON ti.TeacherId = t.Id
                            LEFT JOIN dbo.T_Course cou ON ti.CourseId = cou.Id 
                    WHERE 1=1";
            if (!string.IsNullOrEmpty(teacherName))
            {
                sql += $" AND t.RealName LIKE @teacherName ";
            }

            if (!string.IsNullOrEmpty(courseName))
            {
                sql += $" AND cou.Name LIKE @courseName ";
            }

            return await this.PagedAsync<TeachInfoModel>(sql, 
                new
                {
                    teacherName = "%" + teacherName + "%", 
                    courseName = "%" + courseName + "%"
                },
                null, 
                pageIndex, pageSize);
        }

    }
}
