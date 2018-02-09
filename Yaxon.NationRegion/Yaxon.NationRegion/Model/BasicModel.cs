using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yaxon.NationRegion.Model
{
    public class BasicModel
    {
        /// <summary>
        /// 父节点
        /// </summary>
        public string ParentNode { get; set; }
        /// <summary>
        /// 当前节点
        /// </summary>
        public string Node { get; set; }
        /// <summary>
        /// 链接地址(请求网站用)
        /// </summary>
        public string Href { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 级别(省市区镇村)
        /// </summary>
        public int Level { get; set; }
    }
}
