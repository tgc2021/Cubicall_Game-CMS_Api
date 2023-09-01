using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class TblCmsRoleFunction
    {
        public int IdFunction { get; set; }
        public string FunctionName { get; set; }
        public string Description { get; set; }
        public int? IdOrganization { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public string IsActive { get; set; }
    }
}
