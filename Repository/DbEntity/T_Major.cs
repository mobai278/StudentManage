using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Repository.DbEntity
{
    /// <summary>
    /// 专业
    /// </summary>
    [Table("T_Major")]
    public class Major : BaseModel
    {
        /// <summary>
        /// 学院id
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

    }
}
