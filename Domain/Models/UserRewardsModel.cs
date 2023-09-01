using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class APILogicBaniyaRequestModel
    {
        public string Data { get; set; }
        public string Hash { get; set; }
    }
    public class UserRewardAllData
    {
        public List<RewardsBadgeMaster> RewardsBadgeMaster { get; set; }
        public int Total_Points { get; set; }
        public int Game_Points { get; set; }
        public int Mission_Points { get; set; }
        public int Badge_Points { get; set; }
    }
    public partial class RewardsBadgeMaster
    {
        public int? CubesFacesGameId { get; set; }
        public int? IdMasterBadge { get; set; }
        public string BadgeName { get; set; }
        public int BadgePoints { get; set; }
        public string BadgeImgUrl { get; set; }
        public int IsUserGet { get; set; }
    }
}
