using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class TblCubesFacesGameDetails
    {
        public int CubesFacesGameId { get; set; }
        public int CubesFacesMapId { get; set; }
        public int? CubesFacesId { get; set; }
        public string CategoryName { get; set; }
        public int? GameAttemptNo { get; set; }
        public int PerTileTimer { get; set; }
        public int OverAllTimer { get; set; }
        public int? IdOrganization { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public string FaceGameIntro { get; set; }
        public string FaceGameIntroVedio { get; set; }
        public string IsActive { get; set; }
    }
}
