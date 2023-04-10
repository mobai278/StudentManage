using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Repository.DbEntity
{
    [Table("T_Module")]
    public class Module: BaseModel
    {

        /// <summary>
        /// 模块名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 模块url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 模块图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 模块父级id
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 模块描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 类型 0：后台用户 1：教师  2：学生
        /// </summary>
        public int Type { get; set; }
    }
}
