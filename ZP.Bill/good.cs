//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ZP.Bill
{
    using System;
    using System.Collections.Generic;
    
    public partial class good
    {
        public long ID { get; set; }
        public long SellerID { get; set; }
        public string GoodsName { get; set; }
        public string GoodsPictureUrls { get; set; }
        public int Price { get; set; }
        public int DiscountPrice { get; set; }
        public int PersonNum { get; set; }
        public string Keywrod { get; set; }
        public System.DateTime EndDate { get; set; }
        public int PayDays { get; set; }
        public System.DateTime CreateDate { get; set; }
    }
}
