using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class TblCubesPertilesAnswerDetails
    {
        public int PerTileAnswerId { get; set; }
        public int? CubesFacesGameId { get; set; }
        public int QuestionId { get; set; }
        public int? PerTileId { get; set; }
        public string Answer { get; set; }
        public int IsRightAns { get; set; }
        public int? IdOrganization { get; set; }
        public string IsActive { get; set; }
        public DateTime UpdatedDateTime { get; set; }
    }
}
