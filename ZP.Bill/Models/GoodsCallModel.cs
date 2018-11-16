using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZP.Bill.Models
{
    /// <summary>
    /// 商品单据召集
    /// </summary>
    public class GoodsCallModel
    {
        public good Goods { get; set; }
        /// <summary>
        /// 已召集
        /// </summary>
        public int CallNumber { get; set; }
    }
}