using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Model
{
    public class CurrentUser
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 角色 0：管理员 1：教师 2：学生
        /// </summary>
        public int Role { get; set; }

        /// <summary>
        /// 管理员角色ids
        /// </summary>
        public List<int> RoleIds { get; set; } = new List<int>();
    }
}
