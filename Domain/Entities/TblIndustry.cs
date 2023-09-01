using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class TblIndustry
    {
        public int IdIndustry { get; set; }
        public string Industryname { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime UpdatedDateTime { get; set; }
    }
}
