using System;
using System.Collections.Generic;
using System.Text;
using Repository.DbEntity;

namespace Repository.Model
{
    public class ScoreModel:TeachInfo
    {
        public string MajorName { get; set; }

        /// <summary>
        /// 学生编号
        /// </summary>
        public string StudentNo { get; set; }

        /// <summary>
        /// 学生姓名
        /// </summary>
        public string StudentName { get; set; }

        ///// <summary>
        ///// 班级编号
        ///// </summary>
        public string ClassNo { get; set; }

        ///// <summary>
        ///// 班级名称
        ///// </summary>
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
