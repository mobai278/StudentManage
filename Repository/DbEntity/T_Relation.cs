using System;
using System.Collections.Generic;
using System.Text;
using Dapper.Contrib.Extensions;

namespace Repository.DbEntity
{
    /// <summary>
    /// 多对多关系映射表
    /// </summary>
    [Table("T_Relation")]
    public class Relation : BaseModel
    {
        /// <summary>
        /// 多对多关系标识 例如：“UserRole”,"RoleModule" 等
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 关联第一个表id 例如 key为“UserRole” 此字段则为User表的id
        /// </summary>
        public int FirstKeyId { get; set; }

        /// <summary>
        /// 关联的第二个表id 例如 key为“UserRole” 此字段则为Role表的id
        /// </summary>
        public int SecondKeyId { get; set; }
    }
}
