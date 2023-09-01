using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Cubicall_Models
{
    public class IsameCompletedModel
    {
        public int CubesFacesId { get; set; }
        public string FacesName { get; set; }
        public int IsCompleted { get; set; }
        public string Color { get; set; }

    }
    public class BadgeCountModel
    {
        public int BadgeId { get; set; }
        public string BadgeName { get; set; }
        public int BadgeCount { get; set; }
        public string ImagePath { get; set; }

    }
    public class GamePointModel
    {
        public int CubesFacesId { get; set; }
        public string Color { get; set; }
        public int? TotalPoints { get; set; }
        public string Aht { get; set; }
        public int? FcrPercentage { get; set; }
        public int? Quality { get; set; }
        public int? ServiceLevel { get; set; }
        public int? AttemptsPlayed { get; set; }
        public List<DetaiGamePointModel> DetailGamePoint { get; set; }
    }
    public class DetaiGamePointModel
    {
        public int CubesFacesId { get; set; }
        public int? TotalPoints { get; set; }
        public string Aht { get; set; }
        public int? FcrPercentage { get; set; }
        public int? Quality { get; set; }
        public int? ServiceLevel { get; set; }
        public int? AttemptsPlayed { get; set; }
        public int BadgeId { get; set; }
    }
    public class DashboardData
    {
        public List<BadgeCountModel> BadgeCounList { get; set; }
        public List<IsameCompletedModel> IsameCompletedList { get; set; }
        public List<GamePointModel> GamePointList { get; set; }
    }

}
