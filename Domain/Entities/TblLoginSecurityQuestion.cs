using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class TblLoginSecurityQuestion
    {
        public int IdSecurityQuestion { get; set; }
        public string SecurityQuestion { get; set; }
        public string IsActive { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public int? IdCmsUser { get; set; }
    }
}
