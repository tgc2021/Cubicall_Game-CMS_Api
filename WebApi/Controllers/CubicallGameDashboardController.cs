using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using Acclimate_Models;
using CrosswordCreator.Crossword;
using Cubicall_Models;
using DataAccess.EFCore.Data;
using Domain.Entities;
using Domain.Interfaces;
using m2ostnextservice.Models;
using Microsoft.AspNetCore.Mvc;
using TGC_Game.Web;

namespace WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/CubicallGameDashboardController")]

    public class CubicallGameDashboardController : ControllerBase
    {
        private readonly db_cubicall_game_devContext DbContext;
        AESAlgorithm ency = new AESAlgorithm();
        private readonly IUnitOfWork Uow;
        public CubicallGameDashboardController(IUnitOfWork unitOfWork, db_cubicall_game_devContext context)
        {
            Uow = unitOfWork;
            DbContext = context;
        }


        [Route("~/api/GetDashboard")]
        [HttpGet]
        public IActionResult GetDashboard(int UID,int OrgId)
        {
            try
            {
                List<BadgeCountModel> badgemdlList = new List<BadgeCountModel>();
                DashboardData UserDashboardData = new DashboardData();
                List<IsameCompletedModel> IsameCompletedList = new List<IsameCompletedModel>();
                List<GamePointModel> GamePointList = new List<GamePointModel>();
                var tbldata = DbContext.TblCubesFacesMaster.Where(x => x.IsActive == "A").ToList();
                List<TblCubesFaceBadgeMaster> badgeList = DbContext.TblCubesFaceBadgeMaster.Where(x => x.IsActive == "A" && x.IdOrganization==OrgId).ToList();
                foreach (var item in tbldata)
                {
                    TblCubesGameMasterUserLog masterlog = DbContext.TblCubesGameMasterUserLog.Where(m => m.IdUser == UID && m.CubesFacesGameId == item.CubesFacesId).OrderByDescending(y => y.UserPlayCount).Take(1).FirstOrDefault();

                    List<DetaiGamePointModel> masterlogList = (from l in DbContext.TblCubesGameMasterUserLog
                                                          where l.IdUser == UID
                                                          && l.CubesFacesGameId == item.CubesFacesId
                                                          select new DetaiGamePointModel
                                                          {
                                                              CubesFacesId = l.CubesFacesGameId ?? 0,
                                                              TotalPoints = l.TotalPoints,
                                                              FcrPercentage = l.FcrPercentage,
                                                              Aht = l.Aht,
                                                              Quality = l.Quality,
                                                              ServiceLevel = l.ServiceLevel,
                                                              AttemptsPlayed = l.UserPlayCount,
                                                              BadgeId=l.IdMasterBadge
                                                          }).ToList();


                    if (masterlog!=null && masterlogList.Count>0)
                    {
                        IsameCompletedList.Add(new IsameCompletedModel()
                        {
                            CubesFacesId = item.CubesFacesId,
                            FacesName = item.FacesName,
                            Color = item.Color,
                            IsCompleted =masterlog.IsCompleted
                       
                    });
                        GamePointList.Add(new GamePointModel()
                        {
                            CubesFacesId = item.CubesFacesId,
                            Color = item.Color,
                            TotalPoints=masterlog.TotalPoints,
                            Aht=masterlog.Aht,
                            FcrPercentage=masterlog.FcrPercentage,
                            Quality=masterlog.Quality,
                            ServiceLevel=masterlog.ServiceLevel,
                            AttemptsPlayed=masterlog.UserPlayCount,
                            DetailGamePoint=masterlogList
                        });
                        foreach (var mitem in masterlogList)
                        {
                           var badge= badgeList.Where(m => m.BadgeId == mitem.BadgeId).FirstOrDefault();
                            badgemdlList.Add(new BadgeCountModel()
                            {
                                BadgeId = mitem.BadgeId,
                                BadgeName=badge.BadgeName,
                                ImagePath=badge.BadgeImgUrl
                            });
                        }
                    }
                }
                badgeList = badgeList.DistinctBy(d => d.BadgeName).ToList();
                List<BadgeCountModel> countbadge = new List<BadgeCountModel>();
                foreach (var item in badgeList)
                {
                    int Bcount = badgemdlList.Where(p => p.BadgeName==item.BadgeName).Count();
                    countbadge.Add(new BadgeCountModel() { 
                        
                        BadgeName=item.BadgeName,
                        BadgeCount= Bcount,
                        ImagePath=item.BadgeImgUrl
                       
                    });
                    
                }
                UserDashboardData.BadgeCounList = countbadge;
                UserDashboardData.IsameCompletedList = IsameCompletedList;
                UserDashboardData.GamePointList = GamePointList.OrderByDescending(s=>s.Aht).ThenByDescending(f=>f.FcrPercentage).ThenByDescending(r=>r.ServiceLevel).ThenByDescending(q=>q.Quality).ToList();
                return Ok(UserDashboardData);

            }
            catch (Exception ex)
            {

                return Conflict("Error in Code");
            }
        }

        [Route("~/api/GetLeaderBoard")]
        [HttpGet]
        public IActionResult GetLeaderBoard(int OrgID = 1, int CubesFacesId = 0)
        {
            try
            {
                List<LeaderBoardModel> LeaderboardDATA = new List<LeaderBoardModel>();
                if (CubesFacesId > 0)
                {
                    var tbldata = (from l in DbContext.TblCubesGameMasterUserLog
                                   join u in DbContext.TblUsers on l.IdUser equals u.UserId
                                   where l.IdOrganization == OrgID
                                   && l.CubesFacesGameId == CubesFacesId
                                   select new
                                   {
                                       l.IdUser,
                                       l.TotalPoints,
                                       u.FirstName,
                                       u.LastName,
                                       u.ProfilePicture,
                                       l.CubesFacesGameId,
                                       l.BonusPoints
                                   }).GroupBy(x => new { x.IdUser, x.FirstName, x.LastName, x.ProfilePicture }).Select(g => new
                                   {
                                       UserId = g.Key.IdUser,
                                       TotalPoint = g.Sum(s => s.TotalPoints) + g.Sum(s => s.BonusPoints),
                                       Name = g.Key.FirstName + " " + g.Key.LastName,
                                       Profile_Img = g.Key.ProfilePicture
                                   }).ToList();

                    LeaderboardDATA = tbldata.OrderByDescending(x => x.TotalPoint)
                                           .Select((grp, i) => new LeaderBoardModel
                                           {
                                               UserId = grp.UserId,
                                               Name = grp.Name,
                                               Profile_Img = grp.Profile_Img,
                                               Rank = i + 1,
                                               TotalPoit = grp.TotalPoint,

                                           }).Where(x => x.TotalPoit >= 1).ToList();


                }
                else
                {
                    var tbldata = (from l in DbContext.TblCubesGameMasterUserLog
                                   join u in DbContext.TblUsers on l.IdUser equals u.UserId
                                   where l.IdOrganization == OrgID
                                   select new
                                   {
                                       l.IdUser,
                                       l.TotalPoints,
                                       u.FirstName,
                                       u.LastName,
                                       u.ProfilePicture,
                                       l.CubesFacesGameId,
                                       l.BonusPoints
                                   }).GroupBy(x => new { x.IdUser, x.FirstName, x.LastName, x.ProfilePicture }).Select(g => new
                                   {
                                       UserId = g.Key.IdUser,
                                       TotalPoint = g.Sum(s => s.TotalPoints) + g.Sum(s => s.BonusPoints),
                                       Name = g.Key.FirstName + " " + g.Key.LastName,
                                       Profile_Img = g.Key.ProfilePicture
                                   }).ToList();

                    LeaderboardDATA = tbldata.OrderByDescending(x => x.TotalPoint)
                                           .Select((grp, i) => new LeaderBoardModel
                                           {
                                               UserId = grp.UserId,
                                               Name = grp.Name,
                                               Profile_Img = grp.Profile_Img,
                                               Rank = i + 1,
                                               TotalPoit = grp.TotalPoint,
                                           }).Where(x => x.TotalPoit >= 1).ToList();


                }


                //string Data = JsonSerializer.Serialize(tbldata);
                //string Result = ency.getEncryptedString(Data);
                return Ok(LeaderboardDATA);
            }
            catch (System.Exception ex)
            {

                return Conflict("Error in Code");
            }
        }

    }
}







