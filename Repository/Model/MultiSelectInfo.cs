using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Model
{
    /// <summary>
    /// 多选下拉框model
    /// </summary>
    public class MultiSelectInfo
    {
        /// <summary>
        /// 显示名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool Selected { get; set; } = false;

        /// <summary>
        /// 是否不允许选择
        /// </summary>
        public bool Disabled { get; set; } = false;
    }
}
