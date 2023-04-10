using System;
using System.Collections.Generic;
using System.Text;
using Repository.DbEntity;

namespace Repository.Model
{
    public class StudentModel : Student
    {
       

        /// <summary>
        /// 专业名称
        /// </summary>
        public string EnterTime => EnterDate;

        public string name => RealName;
    }
}
