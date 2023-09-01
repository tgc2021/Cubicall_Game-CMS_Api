using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class TblCmsUsers
    {
        public int IdCmsUser { get; set; }
        public string UserName { get; set; }
        public string EmployeeId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public int IdOrgHierarchy { get; set; }
        public int? IdOrganization { get; set; }
        public int? IdCmsRole { get; set; }

        public virtual TblOrganization IdOrganizationNavigation { get; set; }
    }
}
