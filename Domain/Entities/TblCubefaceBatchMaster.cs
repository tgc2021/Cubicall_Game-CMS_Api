using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class TblCubefaceBatchMaster
    {
        public int CubefaceBatchId { get; set; }
        public int? CubesFacesId { get; set; }
        public int IdBatch { get; set; }
        public string IsActive { get; set; }
        public DateTime UpdatedDateTime { get; set; }
    }
}
