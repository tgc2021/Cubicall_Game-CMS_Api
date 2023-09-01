using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class TblCubesGameMasterUserLog
    {
        public int IdLog { get; set; }
        public int? IdUser { get; set; }
        public int? IdGame { get; set; }
        public int? CubesFacesGameId { get; set; }
        public int? TotalPoints { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
        public string TimetakenToComplete { get; set; }
        public int? IdOrganization { get; set; }
        public int UserPlayCount { get; set; }
        public int IsCompleted { get; set; }
        public int? PerTileId { get; set; }
        public string CallsWaiting { get; set; }
        public string WaitTime { get; set; }
        public string Aht { get; set; }
        public int? Quality { get; set; }
        public int? FcrPercentage { get; set; }
        public int? ServiceLevel { get; set; }
        public int BonusPoints { get; set; }
        public int IdMasterBadge { get; set; }
    }
}
