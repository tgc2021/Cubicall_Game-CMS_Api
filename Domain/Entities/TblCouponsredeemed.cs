using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class TblCouponsredeemed
    {
        public int IdCouponsRedeemed { get; set; }
        public string CouponId { get; set; }
        public string WebsiteName { get; set; }
        public string CouponCode { get; set; }
        public string CouponTitle { get; set; }
        public string CouponDescription { get; set; }
        public string Link { get; set; }
        public string PointsUsed { get; set; }
        public string Image { get; set; }
        public string ExpiryDate { get; set; }
        public int? IdUser { get; set; }
        public int? IdOrganization { get; set; }
        public int? IdRewards { get; set; }
        public int? CardType { get; set; }
        public string Status { get; set; }
        public DateTime UpdatedDateTime { get; set; }
    }
}
