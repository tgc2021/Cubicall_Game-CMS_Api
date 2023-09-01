using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class TblOrganization
    {
        public TblOrganization()
        {
            TblCmsUsers = new HashSet<TblCmsUsers>();
        }

        public int IdOrganization { get; set; }
        public int? IdOrganizationType { get; set; }
        public int? IdBusinessType { get; set; }
        public int? IdIndustry { get; set; }
        public string OrganizationCode { get; set; }
        public string OrganizationName { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public string Name { get; set; }
        public string PhoneNo { get; set; }
        public string ContactEmail { get; set; }
        public string DefaultEmail { get; set; }
        public string Status { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public int? IdCmsUser { get; set; }
        public string SenderPassword { get; set; }
        public string DomainEmailId { get; set; }

        public virtual ICollection<TblCmsUsers> TblCmsUsers { get; set; }
    }
}
