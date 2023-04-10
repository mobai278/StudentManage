﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data
{
    /// <summary>
    /// 框架的service基类，不要直接引用Base
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseService<T> : IBaseService<T> where T : class
    {
        public IConfiguration Config;

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(Config.GetConnectionString("DefaultConnection"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TU"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<TU> ExecuteScalarAsync<TU>(string sql, object param = null)
        {
            using (var conn = Connection)
            {
                await conn.OpenAsync();
                return await conn.ExecuteScalarAsync<TU>(sql, param);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<int> ExecuteAsync(string sql, object param = null)
        {
            using (var dbConnection = Connection)
            {
                await dbConnection.OpenAsync();
                return await dbConnection.ExecuteAsync(sql, param);
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task<int> AddAsync(T t)
        {
            using (var dbConnection = Connection)
            {
                await dbConnection.OpenAsync();
                return await dbConnection.InsertAsync<T>(t);
            }
        }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task<int> AddListAsync(List<T> t)
        {
            using (var dbConnection = Connection)
            {
                await dbConnection.OpenAsync();
                return await dbConnection.InsertAsync<List<T>>(t);
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(T t)
        {
            using (var dbConnection = Connection)
            {
                await dbConnection.OpenAsync();
                return await dbConnection.UpdateAsync<T>(t);
            }
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task<bool> UpdateListAsync(List<T> t)
        {
            using (var dbConnection = Connection)
            {
                await dbConnection.OpenAsync();
                return await dbConnection.UpdateAsync<List<T>>(t);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(T t)
        {
            using (var dbConnection = Connection)
            {
                await dbConnection.OpenAsync();
                return await dbConnection.DeleteAsync(t);
            }
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task<bool> DeleteListAsync(List<T> t)
        {
            using (var dbConnection = Connection)
            {
                await dbConnection.OpenAsync();
                return await dbConnection.DeleteAsync<List<T>>(t);
            }
        }

        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> SingleAsync(int id)
        {
            using (var dbConnection = Connection)
            {
                await dbConnection.OpenAsync();
                return await dbConnection.GetAsync<T>(id);
            }
        }


        /// <summary>
        /// 根据SQL语句获取符合条件的所有对象
        /// </summary>
        public async Task<IEnumerable<TU>> FindAllAsync<TU>(string @sql, object param = null)
        {
            using (var dbConnection = Connection)
            {
                await dbConnection.OpenAsync();
                return await dbConnection.QueryAsync<TU>(sql, param);
            }
        }

        /// <summary>
        /// 获取所有表数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<IEnumerable<T>> FindAllAsync<T>() where T : class
        {
            using (var dbConnection = Connection)
            {
                await dbConnection.OpenAsync();
                return await dbConnection.GetAllAsync<T>();
            }
        }

        /// <summary>
        /// 异步分页查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="sortBy">排序（必须字段）</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PageData<T>> PagedAsync<T>(string sql, object param, string sortBy, long pageIndex, long pageSize)
        {
            await using var dbConnection = Connection;
            await dbConnection.OpenAsync();

            var pageResult = new PageData<T>();

            if (pageIndex < 1)
                pageIndex = 1;
            pageResult.PageIndex = pageIndex;
            pageResult.PageSize = pageSize;

            var queryNumSql = new StringBuilder($" SELECT COUNT(1) FROM ({sql}) AS mapTable ");
            pageResult.TotalCount = dbConnection.ExecuteScalar<long>(queryNumSql.ToString(), param);
            if (pageResult.TotalCount == 0 || pageSize == 0)
            {
                return pageResult;
            }

            var pageMinCount = (pageIndex - 1) * pageSize + 1;
            if (pageResult.TotalCount < pageMinCount)
            {
                pageIndex = (int)((pageResult.TotalCount - 1) / pageSize) + 1;
            }

            pageResult.TotalPages = pageResult.TotalCount / pageSize;
            if ((pageResult.TotalCount % pageSize) != 0)
                pageResult.TotalPages++;

            var querySql = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                querySql.AppendLine(
                    $@" SELECT  mapTable.*
                            FROM    ( {sql}
                                    ) AS mapTable
                            ORDER BY {sortBy}
                                    OFFSET {(pageIndex - 1) * pageSize} ROWS FETCH NEXT {pageSize} ROWS ONLY");
            }
            else
            {
                querySql.Append(
                    $@" SELECT  b.*
                            FROM    ( SELECT    ROW_NUMBER() OVER ( ORDER BY ( SELECT NULL ) ) rn ,
                                                a.*
                                        FROM      ( {sql}
                                                ) a
                                    ) b
                            WHERE   b.rn > {(pageIndex - 1) * pageSize}
                                    AND b.rn <= {pageSize * pageIndex} ");
            }

            pageResult.Items = dbConnection.Query<T>(querySql.ToString(), param).ToList();
            return pageResult;
        }
    }
}
