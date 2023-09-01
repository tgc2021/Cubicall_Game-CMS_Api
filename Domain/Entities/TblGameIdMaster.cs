using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class TblGameIdMaster
    {
        public int IdGame { get; set; }
        public string GameName { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public string IsActive { get; set; }
    }
}
