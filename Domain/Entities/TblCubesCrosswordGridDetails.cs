using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class TblCubesCrosswordGridDetails
    {
        public int IdGrid { get; set; }
        public string Grid { get; set; }
        public int KeyNo { get; set; }
        public int? IdOrganization { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public int? IdCmsUser { get; set; }
        public string IsActive { get; set; }
    }
}
