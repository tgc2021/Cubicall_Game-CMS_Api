using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class TblCubesGameDetailsUserLog
    {
        public int IdLog { get; set; }
        public int? IdGame { get; set; }
        public int? CubesFacesGameId { get; set; }
        public int QuestionId { get; set; }
        public int? PerTileId { get; set; }
        public int? PerTileQuestionId { get; set; }
        public int? PerTileAnswerId { get; set; }
        public int? IsRightAns { get; set; }
        public string UserInput { get; set; }
        public int GamePoints { get; set; }
        public string TimetakenToComplete { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
        public int? IdUser { get; set; }
        public int? AttemptNo { get; set; }
        public int UserPlayCount { get; set; }
        public int IsCompleted { get; set; }
        public string CallsWaiting { get; set; }
        public string WaitTime { get; set; }
        public string Aht { get; set; }
        public int? Quality { get; set; }
        public int? FcrPercentage { get; set; }
        public int? ServiceLevel { get; set; }
        public int StreakPoints { get; set; }
    }
}
