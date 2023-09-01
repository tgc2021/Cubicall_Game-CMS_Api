using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class TblGameMaster
    {
        public int Id { get; set; }
        public int IdGame { get; set; }
        public string GameName { get; set; }
        public string IsActive { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public string GameIntro { get; set; }
        public string GameIntroVideo { get; set; }
        public int IdOrganization { get; set; }
    }
}
