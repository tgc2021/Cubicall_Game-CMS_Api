using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Cubicall_Models
{
    public class LeaderBoardModel
    {
        public int Rank { get; set; }
        public int? UserId { get; set; }
        public int ?TotalPoit { get; set; }
        public int? CubesFacesId { get; set; }
        public string Name { get; set; }
        public string Profile_Img { get; set; }

    }

}
