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
    public class StudentRepository : AppBaseService<Student>, IStudentRepository
    {
        public StudentRepository(IConfiguration config)
        {
            Config = config;
        }

        public async Task<IEnumerable<string>> getCourse(int courseId)
        {
            var sql = $@"select Name from dbo.T_Course where Id =" + courseId;
            return await this.FindAllAsync<string>(sql, null);
        }

        public async Task<IEnumerable<string>> getMajor(int majorId)
        {
            var sql = $@"select Name from dbo.T_Major where Id =" + majorId;
            return await this.FindAllAsync<string>(sql, null);
        }

        /// <summary>
        /// 学生列表（分页）`
        /// </summary>
        /// <param name="userName">用户名 or 真实姓名</param>
        /// <param name="phone">手机号</param>
        /// <param name="pageIndex">开始页数</param>
        /// <param name="pageSize">每页条数</param>
        public async Task<PageData<StudentModel>> GetPageDataAsync(string userName, string phone, int pageIndex, int pageSize)
        {
            var sql = $@"SELECT * FROM dbo.T_Student WHERE 1=1";
            if (!string.IsNullOrEmpty(userName))
            {
                sql += $" AND ( UserName LIKE @userName OR RealName LIKE @userName ) ";
            }

            if (!string.IsNullOrEmpty(phone))
            {
                sql += $" AND Phone LIKE @phone ";
            }

            return await this.PagedAsync<StudentModel>(sql, new { userName = "%" + userName + "%", phone = "%" + phone + "%" },
                null, pageIndex, pageSize);
        }

        public Task<StudentModel> GetStudent(int studentId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<string>> getStudent(int studentId)
        {
          var sql = $@"select RealName from dbo.T_student where SerialNumber =" +studentId;
          return await this.FindAllAsync<string>(sql, null);
        }



    }
}
