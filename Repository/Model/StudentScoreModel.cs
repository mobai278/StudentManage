
using System;
using System.Collections.Generic;
using System.Text;
using Repository.DbEntity;

namespace Repository.Model
{
    public class StudentScoreModel
    {
        public int Id { get; set; }
        public int studentId { get; set; }

        public string studentName { get; set; }

        public int majorId { get; set; }

        public string majorName { get; set; }

        public int courseId { get; set; }

        public string courseName { get; set; }

        public int stuTime { get; set; }

        public int stuGrade { get; set; }

        public decimal stuScore { get; set; }

    }
}
