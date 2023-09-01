using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class TblUsersLog
    {
        public int IdLog { get; set; }
        public int IdUser { get; set; }
        public int IdOrganization { get; set; }
        public DateTime UpdatedDateTime { get; set; }
    }
}
