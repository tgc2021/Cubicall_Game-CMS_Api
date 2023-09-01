using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class TblRewardsRedeemMaster
    {
        public int IdRewards { get; set; }
        public string AccountId { get; set; }
        public string EmailAddress { get; set; }
        public string PartnerCode { get; set; }
        public string ProviderCode { get; set; }
        public string RedeemType { get; set; }
        public int? TotalPoints { get; set; }
        public string TransactionId { get; set; }
        public string UserName { get; set; }
        public int? IdUser { get; set; }
        public int? IdOrg { get; set; }
        public int? IdGame { get; set; }
    }
}
