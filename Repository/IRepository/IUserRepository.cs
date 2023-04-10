﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Data;
using Repository.DbEntity;

namespace Repository.IRepository
{
    public interface IUserRepository : IDependencyRepository, IAppBaseService<User>
    {

        /// <summary>
        /// 用户列表（分页）
        /// </summary>
        /// <param name="userName">用户名 or 真实姓名</param>
        /// <param name="phone">手机号</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        Task<PageData<User>> GetPageDataAsync(string userName, string phone, int pageIndex, int pageSize);

    }
}
