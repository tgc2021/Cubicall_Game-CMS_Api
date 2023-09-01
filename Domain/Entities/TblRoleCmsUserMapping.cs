using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class TblRoleCmsUserMapping
    {
        public int IdRoleCmsUserMapping { get; set; }
        public int IdCmsUser { get; set; }
        public int? IdCmsRole { get; set; }
        public int IdOrganization { get; set; }
        public string Status { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
    }
}
