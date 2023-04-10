using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace StudentManage.Areas.StudentManage.Models
{
    /// <summary>
    /// 返回参数
    /// </summary>
    public class ResponseMessage
    {
        public ResponseMessage()
        {
            Code = 0;
            Msg = false;
            Message = string.Empty;
            Count = 0;
            Data = null;
        }

        /// <summary>
        /// 返回 code 标志
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public bool Msg { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public long Count { get; set; }

        /// <summary>
        /// 具体数据
        /// </summary>
        public object Data { get; set; }
    }

}
