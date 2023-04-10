using System;
using System.Collections.Generic;
using System.Text;
using Repository.DbEntity;

namespace Repository.Model
{
    public class CourseModel : Course
    {
        /// <summary>
        /// 专业名称
        /// </summary>
        public string MajorName { get; set; }
    }
}
