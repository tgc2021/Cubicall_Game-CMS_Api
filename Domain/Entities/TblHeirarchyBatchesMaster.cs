using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class TblHeirarchyBatchesMaster
    {
        public int IdBatch { get; set; }
        public int IdOrgHierarchy { get; set; }
        public string BatchName { get; set; }
        public string IsActive { get; set; }
        public int? IdOrganization { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public int? IdCmsUser { get; set; }
    }
}
