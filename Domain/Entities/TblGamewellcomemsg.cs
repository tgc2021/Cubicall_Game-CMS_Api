using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class TblGameWellcomeMsg
    {
        public int IdMsg { get; set; }
        public string TitleMsg { get; set; }
        public string WellComeMsg { get; set; }
        public int IsFirstTimeLogin { get; set; }
        public int? IdOrganization { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public string IsActive { get; set; }
    }
}
