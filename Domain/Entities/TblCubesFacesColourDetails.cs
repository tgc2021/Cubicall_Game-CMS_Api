using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class TblCubesFacesColourDetails
    {
        public int ColourId { get; set; }
        public int? CubesFacesId { get; set; }
        public int WaitTime { get; set; }
        public string Colour { get; set; }
        public int? IdOrganization { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public string IsActive { get; set; }
    }
}
