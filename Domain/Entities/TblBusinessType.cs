using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class TblBusinessType
    {
        public int IdBusinessType { get; set; }
        public string BusinessTypeName { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime UpdatedDateTime { get; set; }
    }
}
