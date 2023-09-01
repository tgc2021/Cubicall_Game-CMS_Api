using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class TblCubesFacesMaster
    {
        public int CubesFacesId { get; set; }
        public string FacesName { get; set; }
        public string Color { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public string IsActive { get; set; }
    }
}
