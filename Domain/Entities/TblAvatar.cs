using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class TblAvatar
    {
        public int IdAvatar { get; set; }
        public string Url { get; set; }
        public int? IdOrganization { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public int? IdCmsUser { get; set; }
        public string AvatarVoice { get; set; }
        public string IsActive { get; set; }
    }
}
