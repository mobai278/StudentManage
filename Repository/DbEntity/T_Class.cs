using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Repository.DbEntity
{
    /// <summary>
    /// 班级
    /// </summary>
    [Table("T_Class")]
    public class Class : BaseModel
    {
        public readonly string RealName;

        /// <summary>
        /// 班级编号
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        /// 专业id
        /// </summary>
        public int MajorId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 班级描述
        /// </summary>
        public string Description { get; set; }

    }
}
