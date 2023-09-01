using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class TblUserSecurityQuestionLog
    {
        public int IdQuestionLog { get; set; }
        public int? IdUser { get; set; }
        public int? IdSecurityQuestion { get; set; }
        public string SecurityQuestionAns { get; set; }
        public int? IdOrganization { get; set; }
        public DateTime UpdatedDateTime { get; set; }
    }
}
