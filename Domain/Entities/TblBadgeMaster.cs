using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class TblBadgeMaster
    {
        public int IdMasterBadge { get; set; }
        public string BadgeName { get; set; }
        public string BadgeImgUrl { get; set; }
        public int? IdOrganization { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public string IsActive { get; set; }
    }
}
