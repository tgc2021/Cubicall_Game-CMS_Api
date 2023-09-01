using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class TblCubesPertilesQuestionDetails
    {
        public int PerTileQuestionId { get; set; }
        public int QuestionId { get; set; }
        public int? CubesFacesGameId { get; set; }
        public int? PerTileId { get; set; }
        public string Question { get; set; }
        public int Complexity { get; set; }
        public int? RowNo { get; set; }
        public int? ColumnNo { get; set; }
        public string Direction { get; set; }
        public string QuestionClue { get; set; }
        public int QuestionSet { get; set; }
        public int IsApproved { get; set; }
        public string FeedBack { get; set; }
        public int IsDraft { get; set; }
        public int? IdOrganization { get; set; }
        public string IsActive { get; set; }
        public DateTime UpdatedDateTime { get; set; }
    }
}
