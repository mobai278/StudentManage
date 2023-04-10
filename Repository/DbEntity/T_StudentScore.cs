using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Repository.DbEntity
{

    [Table("T_StudentScore")]
    public class StudentScore : BaseModel
    {
        /**
         *  学号
         */
        public int studentId { get; set; }


        public string studentName { get; set; }

        public int majorId { get; set; }

        public string majorName { get; set; }

        public int courseId { get; set; }

        public string courseName { get; set; }

        public int stuTime { get; set; }

        public int stuGrade { get; set; }

        public decimal stuScore { get;set; }

    }
}
