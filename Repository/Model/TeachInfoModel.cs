using System;
using System.Collections.Generic;
using System.Text;
using Repository.DbEntity;

namespace Repository.Model
{
    public class TeachInfoModel:TeachInfo
    {
        public string MajorName { get; set; }

        /// <summary>
        /// 教师编号
        /// </summary>
        public string TeacherNo { get; set; }

        /// <summary>
        /// 教师姓名
        /// </summary>
        public string TeacherName { get; set; }

        /// <summary>
        /// 班级编号
        /// </summary>
        public string ClassNo { get; set; }

        /// <summary>
        /// 班级名称
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// 课程编号
        /// </summary>
        public string CourseNo { get; set; }

        /// <summary>
        /// 课程名称
        /// </summary>
        public string CourseName { get; set; }
    }
}
