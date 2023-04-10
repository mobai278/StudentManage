using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Repository.DbEntity
{
    [Table("T_Score")]
    public class Score : BaseModel
    {
        /// <summary>
        /// 选课信息id
        /// </summary>
        public int SelectionInfoId { get; set; }

        /// <summary>
        /// 成绩
        /// </summary>
        public decimal Scores { get; set; }
    }
}
