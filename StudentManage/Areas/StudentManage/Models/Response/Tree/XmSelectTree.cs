﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManage.Areas.StudentManage.Models.Response.Tree
{
    public class XmSelectTree
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        public bool Disabled { get; set; } = false;

        public bool Selected { get; set; } = false;

        /// <summary>
        /// 子集
        /// </summary>
        public List<XmSelectTree> Children { get; set; } = new List<XmSelectTree>();
        
    }
}
