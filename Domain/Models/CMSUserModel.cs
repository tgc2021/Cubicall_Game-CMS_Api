using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Acclimate_Models
{
    
    public partial class CMSUserModel
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
        public string  Functions { get; set; }

    }
}
