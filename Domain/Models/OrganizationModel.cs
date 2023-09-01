using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Acclimate_Models
{
    
    public partial class OrganizationModel
    {
        public int IdOrganization { get; set; }
        public int? IdBusinessType { get; set; }
        public string BUSINESSTYPENAME { get; set; }
        public int? IdIndustry { get; set; }
        public string IndustryName { get; set; }

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
        public string Logo_Imgbytes { get; set; }
        
    }
}
