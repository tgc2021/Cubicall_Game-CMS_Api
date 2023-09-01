using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class TblCubesGridrowcolLog
    {
        public int IdLog { get; set; }
        public int? IdUser { get; set; }
        public int KeyNo { get; set; }
        public int QuestionId { get; set; }
        public int? RowNo { get; set; }
        public int? ColumnNo { get; set; }
        public string Direction { get; set; }
        public int? IdOrganization { get; set; }
        public DateTime UpdatedDateTime { get; set; }
    }
}
