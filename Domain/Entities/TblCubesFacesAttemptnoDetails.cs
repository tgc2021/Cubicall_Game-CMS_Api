using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class TblCubesFacesAttemptnoDetails
    {
        public int AttemptNoId { get; set; }
        public int AttemptNoMapId { get; set; }
        public int? CubesFacesId { get; set; }
        public int AttemptNo { get; set; }
        public string GamePoints { get; set; }
        public int? IdOrganization { get; set; }
        public string IsActive { get; set; }
        public DateTime UpdatedDateTime { get; set; }
    }
}
