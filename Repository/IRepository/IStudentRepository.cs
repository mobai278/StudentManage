using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Data;
using Repository.DbEntity;
using Repository.Model;

namespace Repository.IRepository
{
    public interface IStudentRepository : IDependencyRepository, IAppBaseService<Student>
    {
        /// <summary>
        /// 学生列表（分页）
        /// </summary>
        /// <param name="userName">用户名 or 真实姓名</param>
        /// <param name="phone">手机号</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        Task<PageData<StudentModel>> GetPageDataAsync(string userName, string phone, int pageIndex, int pageSize);
        Task<IEnumerable<string>> getStudent(int studentId);

        Task<IEnumerable<string>> getMajor(int majorId);

        Task<IEnumerable<string>> getCourse(int majorId);
    }
}
