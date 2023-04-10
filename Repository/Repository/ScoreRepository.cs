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
    public class ScoreRepository : AppBaseService<StudentScore>, IScoreRepository
    {
        public ScoreRepository(IConfiguration config)
        {
            Config = config;
        }

        public async Task<PageData<ScoreRepository>> AddAsync(StudentScore score)
        {
            var sql = $@"SELECT * FROM dbo.T_Teacher WHERE 1=1";
            long pageIndex = 1;
            long pageSize = 10;
            return await this.PagedAsync<ScoreRepository>(sql,null,null, pageIndex, 10);
        }

        public async Task<PageData<StudentScore>> GetPageDataAsync(string SerialNumber, string studentName,string courseName, int pageIndex, int pageSize)
        {
            var sql = $@"SELECT * FROM dbo.T_StudentScore WHERE 1=1";
            if (!string.IsNullOrEmpty(SerialNumber) && SerialNumber!= "LOGOUT")
            {
                sql += $" AND ( studentId ="+SerialNumber+"  ) ";
            }
            if (!string.IsNullOrEmpty(studentName))
            {
                sql += $" AND ( studentName LIKE @studentName  ) ";
            }
            if (!string.IsNullOrEmpty(courseName)) {

                sql += $"AND (courseName LIKE @courseName)";

            }


            return await this.PagedAsync<StudentScore>(sql, new { studentName = "%" + studentName + "%", courseName = "%" + courseName + "%" }, null, pageIndex, pageSize);
        }

        public Task UpdateAsync(StudentScore model)
        {
            throw new NotImplementedException();
        }

     
        


    }
}
