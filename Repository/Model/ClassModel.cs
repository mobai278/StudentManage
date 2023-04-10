using System;
using System.Collections.Generic;
using System.Text;
using Repository.DbEntity;

namespace Repository.Model
{
    public class ClassModel:Class
    {
        /// <summary>
        /// 专业名称
        /// </summary>
        public string MajorName { get; set; }
    }
}
