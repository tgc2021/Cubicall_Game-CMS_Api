using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class TblGameMissionsRulesMaster
    {
        public int IdMissionsRules { get; set; }
        public int MissionNo { get; set; }
        public string Text { get; set; }
        public int? StreakNo { get; set; }
        public int? CategorieNo { get; set; }
        public int? MissionTime { get; set; }
        public int? QuestionCount { get; set; }
        public string ServiceLevelColor { get; set; }
        public int? MissionPoints { get; set; }
        public int? IdOrganization { get; set; }
        public string IsActive { get; set; }
        public DateTime UpdatedDateTime { get; set; }
    }
}
