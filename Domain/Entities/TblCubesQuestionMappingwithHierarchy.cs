using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class TblCubesQuestionMappingwithHierarchy
    {
        public int MapId { get; set; }
        public int QuestionId { get; set; }
        public int IdOrgHierarchy { get; set; }
        public string IsActive { get; set; }
        public DateTime UpdatedDateTime { get; set; }
    }
}
