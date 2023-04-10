using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Repository.DbEntity
{
    /// <summary>
    /// 课程
    /// </summary>
    [Table("T_Course")]
    public class Course : BaseModel
    {
        /// <summary>
        /// 专业id
        /// </summary>
        public int MajorId { get; set; }

        /// <summary>
        /// 课程编号
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 课程描述
        /// </summary>
        public string Description { get; set; }

    }
}
