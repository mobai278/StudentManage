using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Repository.DbEntity
{
    [Table("T_TeachInfo")]
    public class TeachInfo : BaseModel
    {
        /// <summary>
        /// 专业id
        /// </summary>
        public int MajorId { get; set; }

        /// <summary>
        /// 教师id
        /// </summary>
        public int TeacherId { get; set; }

        /// <summary>
        /// 班级id
        /// </summary>
        public int ClassId { get; set; }

        /// <summary>
        /// 课程id
        /// </summary>
        public int CourseId { get; set; }

        /// <summary>
        /// 学时
        /// </summary>
        public decimal Hours { get; set; }

        /// <summary>
        /// 学分
        /// </summary>
        public decimal Credit { get; set; }

    }
}
