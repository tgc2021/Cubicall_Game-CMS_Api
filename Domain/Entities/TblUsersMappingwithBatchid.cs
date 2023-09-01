using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class TblUsersMappingwithBatchid
    {
        public int MapBatchId { get; set; }
        public int UserId { get; set; }
        public int IdBatch { get; set; }
        public string IsActive { get; set; }
        public DateTime UpdatedDateTime { get; set; }
    }
}
