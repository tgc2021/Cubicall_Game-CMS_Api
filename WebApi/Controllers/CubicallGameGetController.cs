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
using Domain.Models;
using m2ostnextservice.Models;
using Microsoft.AspNetCore.Mvc;
using TGC_Game.Web;

namespace WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/CubicallGameGetController")]

    public class CubicallGameGetController : ControllerBase
    {
        private readonly db_cubicall_game_devContext DbContext;
        AESAlgorithm ency = new AESAlgorithm();
        private readonly IUnitOfWork Uow;
        public CubicallGameGetController(IUnitOfWork unitOfWork, db_cubicall_game_devContext context)
        {
            Uow = unitOfWork;
            DbContext = context;
        }


        [Route("~/api/GetGameWellComeMsg")]
        [HttpGet]
        public IActionResult GetGameWellComeMsg(int OrgID, int IsFirstTimeLogin)
        {
            try
            {
                var tbldata = (from l in DbContext.TblGameWellcomeMsg
                               where l.IdOrganization == OrgID && l.IsActive == "A"
                               && l.IsFirstTimeLogin == IsFirstTimeLogin
                               select l).ToList();
                if (tbldata != null)
                    return Ok(tbldata);
                else
                    return BadRequest("No data avaible");
            }
            catch (System.Exception ex)
            {

                return Conflict("Error in Code");
            }
        }
        [Route("~/api/GetUserCubesFacesStatus")]
        [HttpGet]
        public IActionResult GetUserCubesFacesStatus(int UID, int CubesFacesGameId)
        {
            try
            {
                int maxUserPalyCount = DbContext.TblCubesGameMasterUserLog
                    .Where(x => x.IdUser == UID && x.CubesFacesGameId == CubesFacesGameId)
                    .OrderByDescending(y => y.UserPlayCount).Select(v => v.UserPlayCount).Take(1).FirstOrDefault();
                if (maxUserPalyCount > 0)
                {
                    TblCubesGameMasterUserLog Face1 = DbContext.TblCubesGameMasterUserLog.Where(s => s.UserPlayCount == maxUserPalyCount).FirstOrDefault();
                    if (Face1 != null)
                        return Ok(Face1.IsCompleted);
                    else
                        return Ok("User not play this game yet");
                }
                else
                    return Ok("User not play this game yet");
            }
            catch (System.Exception ex)
            {

                return Conflict("Error in Code");
            }
        }

        [Route("~/api/GetUserAllCubesFacesStatus")]
        [HttpGet]
        public IActionResult GetUserAllCubesFacesStatus(int UID)
        {
            try
            {
                List<AllGameStatusModel> data = new List<AllGameStatusModel>();
                List<AllGameStatusModel> dataList = new List<AllGameStatusModel>();
                TblCubesGameMasterUserLog Face1 = DbContext.TblCubesGameMasterUserLog
                    .Where(x => x.IdUser == UID && x.CubesFacesGameId == 1)
                    .OrderByDescending(y => y.UserPlayCount).Take(1).FirstOrDefault();

                TblCubesGameMasterUserLog Face2 = DbContext.TblCubesGameMasterUserLog
                    .Where(x => x.IdUser == UID && x.CubesFacesGameId == 2)
                    .OrderByDescending(y => y.UserPlayCount).Take(1).FirstOrDefault();

                TblCubesGameMasterUserLog Face3 = DbContext.TblCubesGameMasterUserLog
                    .Where(x => x.IdUser == UID && x.CubesFacesGameId == 3)
                    .OrderByDescending(y => y.UserPlayCount).Take(1).FirstOrDefault();

                TblCubesGameMasterUserLog Face4 = DbContext.TblCubesGameMasterUserLog
                    .Where(x => x.IdUser == UID && x.CubesFacesGameId == 4)
                    .OrderByDescending(y => y.UserPlayCount).Take(1).FirstOrDefault();

                TblCubesGameMasterUserLog Face5 = DbContext.TblCubesGameMasterUserLog
                    .Where(x => x.IdUser == UID && x.CubesFacesGameId == 5)
                    .OrderByDescending(y => y.UserPlayCount).Take(1).FirstOrDefault();

                TblCubesGameMasterUserLog Face6 = DbContext.TblCubesGameMasterUserLog
                    .Where(x => x.IdUser == UID && x.CubesFacesGameId == 6)
                    .OrderByDescending(y => y.UserPlayCount).Take(1).FirstOrDefault();

                if (Face1 != null)
                {
                    data.Add(new AllGameStatusModel()
                    {
                        CubesFacesGameId = Face1.CubesFacesGameId ?? 0,
                        PerTileId = Face1.PerTileId ?? 0,
                        IsCompleted = Face1.IsCompleted
                    });
                }
                else if (Face2 != null)
                {
                    data.Add(new AllGameStatusModel()
                    {
                        CubesFacesGameId = Face2.CubesFacesGameId ?? 0,
                        PerTileId = Face2.PerTileId ?? 0,
                        IsCompleted = Face2.IsCompleted
                    });
                }
                else if (Face3 != null)
                {
                    data.Add(new AllGameStatusModel()
                    {
                        CubesFacesGameId = Face3.CubesFacesGameId ?? 0,
                        PerTileId = Face3.PerTileId ?? 0,
                        IsCompleted = Face3.IsCompleted
                    });
                }
                else if (Face4 != null)
                {
                    data.Add(new AllGameStatusModel()
                    {
                        CubesFacesGameId = Face4.CubesFacesGameId ?? 0,
                        PerTileId = Face4.PerTileId ?? 0,
                        IsCompleted = Face4.IsCompleted
                    });
                }
                else if (Face5 != null)
                {
                    data.Add(new AllGameStatusModel()
                    {
                        CubesFacesGameId = Face5.CubesFacesGameId ?? 0,
                        PerTileId = Face5.PerTileId ?? 0,
                        IsCompleted = Face5.IsCompleted
                    });
                }
                else if (Face6 != null)
                {
                    data.Add(new AllGameStatusModel()
                    {
                        CubesFacesGameId = Face6.CubesFacesGameId ?? 0,
                        PerTileId = Face6.PerTileId ?? 0,
                        IsCompleted = Face6.IsCompleted
                    });
                }
                else
                {
                    return Ok("User not play this game yet");
                }
                if (data != null)
                {
                    foreach (var item in data)
                    {
                        dataList.Add(new AllGameStatusModel()
                        {
                            CubesFacesGameId = item.CubesFacesGameId,
                            PerTileId = item.PerTileId,
                            IsCompleted = item.IsCompleted
                        });
                    }
                    return Ok(dataList);
                }
                return Ok("User not play this game yet");
            }
            catch (System.Exception ex)
            {

                return Conflict("Error in Code");
            }
        }
        [Route("~/api/GetOrgHierarchy")]
        [HttpGet]
        public IActionResult GetOrgHierarchy(int OrgId, int ParentIdOrgHierarchy = 0)
        {
            try
            {
                List<TblOrganizationHierarchy> Data = new List<TblOrganizationHierarchy>();

                Data = DbContext.TblOrganizationHierarchy.Where(x => x.IdOrganization == OrgId && x.IsActive == "A" && x.ParentIdOrgHierarchy == ParentIdOrgHierarchy).ToList();

                if (Data.Count > 0)
                    return Ok(Data);
                else
                    return Ok("No data available");
            }
            catch (Exception)
            {

                return Conflict("Error in Code");
            }
        }

        [Route("~/api/GetLastLevelOrgHierarchy")]
        [HttpGet]
        public IActionResult GetLastLevelOrgHierarchy(int OrgId)
        {
            try
            {
                List<int> ParentIdOrgHierarchylist = DbContext.TblOrganizationHierarchy
                   .Where(x => x.IdOrganization == OrgId && x.IsActive == "A").Select(v => v.ParentIdOrgHierarchy).ToList();

                List<TblOrganizationHierarchy> Data = new List<TblOrganizationHierarchy>();
                if (ParentIdOrgHierarchylist != null)
                {
                    Data = DbContext.TblOrganizationHierarchy.Where(x => !ParentIdOrgHierarchylist.Contains(x.IdOrgHierarchy) && x.IdOrganization == OrgId && x.IsActive == "A").ToList();

                }
                if (Data.Count > 0)
                    return Ok(Data);
                else
                    return Ok("No data available");
            }
            catch (Exception)
            {

                return Conflict("Error in Code");
            }
        }

        [Route("~/api/GetLevelOrgHierarchy")]
        [HttpGet]
        public IActionResult GetLevelOrgHierarchy(int OrgId, int ParentIdOrgHierarchy)
        {
            try
            {
                List<TblOrganizationHierarchy> Data = new List<TblOrganizationHierarchy>();

                Data = DbContext.TblOrganizationHierarchy.Where(x => x.IdOrganization == OrgId && x.IsActive == "A" && x.IdOrgHierarchy == ParentIdOrgHierarchy).ToList();

                if (Data.Count > 0)
                    return Ok(Data);
                else
                    return Ok("No data available");
            }
            catch (Exception)
            {

                return Conflict("Error in Code");
            }
        }
        [Route("~/api/GetAllPoint")]
        [HttpGet]
        public IActionResult GetAllPoint(int UID)
        {
            try
            {
                int game_Points = (from l in DbContext.TblCubesGameMasterUserLog
                                   where l.IdUser == UID
                                   select new
                                   {
                                       l.IdUser,
                                       l.TotalPoints,
                                       l.BonusPoints
                                   }).GroupBy(x => new { x.IdUser }).Select(g => new
                                   {
                                       GamePoints = g.Sum(s => s.TotalPoints) + g.Sum(y => y.BonusPoints) ?? 0

                                   }).Select(x => x.GamePoints).FirstOrDefault();

                int redmeePoint = get_redmeePoint(UID);
                return Ok(game_Points - redmeePoint);
            }
            catch (Exception ex)
            {

                return Conflict("Error in Code");
            }
        }

        [Route("~/api/GetGameAttemptOver")]
        [HttpGet]
        public IActionResult GetGameAttemptOver(int UID, int OrgId, int CubesFacesId = 1)
        {
            try
            {
                int UserPlayedAttempt = DbContext.TblCubesGameMasterUserLog.Where(x => x.CubesFacesGameId == CubesFacesId && x.IdUser == UID).OrderByDescending(p => p.UserPlayCount).Select(y => y.UserPlayCount).FirstOrDefault();
                int? GameAttemptOver = DbContext.TblCubesFacesGameDetails.Where(x => x.CubesFacesId == CubesFacesId && x.IdOrganization == OrgId).Select(y => y.GameAttemptNo).FirstOrDefault();
                if (UserPlayedAttempt > 0 & GameAttemptOver > 0)
                {
                    if (UserPlayedAttempt == GameAttemptOver)
                    return Ok(3);
                    else
                    return Ok(1);
                }

                return Ok(0);
            }
            catch (Exception)
            {

                return Conflict("Error in Code");
            }
        }

        [Route("~/api/GetRewards")]
        [HttpGet]
        public IActionResult GetRewards(int? UID, int OrgId)
        {
            try
            {
                UserRewardAllData UserRewardsAllData = new UserRewardAllData();
                List<RewardsBadgeMaster> Data = new List<RewardsBadgeMaster>();
                List<TblCubesFaceBadgeMaster> BadgeMaster = DbContext.TblCubesFaceBadgeMaster.Where(x => x.IsActive == "A" && x.IdOrganization == OrgId).DistinctBy(f=>f.CubesFacesGameId).ToList();
                if (BadgeMaster != null)
                {

                    foreach (var item in BadgeMaster)
                    {
                        int UserGetBadge = 0;
                        TblCubesGameMasterUserLog LastRecords = DbContext.TblCubesGameMasterUserLog
                    .Where(x => x.IdUser == UID && x.CubesFacesGameId == item.CubesFacesGameId)
                    .OrderByDescending(y => y.UserPlayCount).Take(1).FirstOrDefault();


                        if (LastRecords != null)
                        {
                            if (LastRecords.IdMasterBadge == item.IdMasterBadge)
                                UserGetBadge = 1;
                        }

                        Data.Add(new RewardsBadgeMaster()
                        {
                            CubesFacesGameId = item.CubesFacesGameId,
                            IdMasterBadge = item.IdMasterBadge,
                            BadgeName = item.BadgeName,
                            BadgePoints = item.BadgePoints,
                            BadgeImgUrl = item.BadgeImgUrl,
                            IsUserGet = UserGetBadge

                        });

                    }
                }
                int game_Points = (from l in DbContext.TblCubesGameMasterUserLog
                                   where l.IdUser == UID
                                   select new
                                   {
                                       l.IdUser,
                                       l.TotalPoints,
                                       l.BonusPoints
                                   }).GroupBy(x => new { x.IdUser }).Select(g => new
                                   {
                                       GamePoints = g.Sum(s => s.TotalPoints) + g.Sum(y => y.BonusPoints) ?? 0

                                   }).Select(x => x.GamePoints).FirstOrDefault();

                int Mission_Points = (from l in DbContext.TblGameMissionsUserLog
                                      where l.IdUser == UID
                                      select new
                                      {
                                          l.IdUser,
                                          l.MissionPoints
                                      }).GroupBy(x => new { x.IdUser }).Select(g => new
                                      {
                                          MissionPoints = g.Sum(s => s.MissionPoints) ?? 0

                                      }).Select(x => x.MissionPoints).FirstOrDefault();

                int Badgepoint = Data.Where(u => u.IsUserGet == 1).Sum(p => p.BadgePoints);

                UserRewardsAllData.RewardsBadgeMaster = Data;
                UserRewardsAllData.Badge_Points = Badgepoint;
                UserRewardsAllData.Game_Points = game_Points;
                UserRewardsAllData.Mission_Points = Mission_Points;
                int redmeePoint = get_redmeePoint(UID);
                UserRewardsAllData.Total_Points = (Badgepoint + game_Points + Mission_Points) - redmeePoint;
                return Ok(UserRewardsAllData);
            }
            catch (Exception ex)
            {

                return Conflict("Error in Code");
            }
        }

        [Route("~/api/GetMissionRules")]
        [HttpGet]
        public IActionResult GetMissionRules()
        {
            try
            {

                List<TblGameMissionsRulesMaster> GameMissionsRulesMaster = DbContext.TblGameMissionsRulesMaster.Where(x => x.IsActive == "A" ).ToList();

                return Ok(GameMissionsRulesMaster);
            }
            catch (Exception ex)
            {

                return Conflict("Error in Code");
            }
        }
        [Route("~/api/GetBatchMasterdata")]
        [HttpGet]
        public IActionResult GetBatchMasterdata(int OrgId, int IdOrgHierarchy = 0)
        {
            try
            {
                List<TblHeirarchyBatchesMaster> Data = new List<TblHeirarchyBatchesMaster>();

                Data = DbContext.TblHeirarchyBatchesMaster.Where(x => x.IdOrganization == OrgId && x.IsActive == "A" && x.IdOrgHierarchy == IdOrgHierarchy).ToList();

                if (Data.Count > 0)
                    return Ok(Data);
                else
                    return Ok("No data available");
            }
            catch (Exception)
            {

                return Conflict("Error in Code");
            }
        }

        [Route("~/api/GetUserBatchId")]
        [HttpGet]
        public IActionResult GetUserBatchId(int UID)
        {
            try
            {
                int IdBatch = DbContext.TblUsersMappingwithBatchid.Where(x => x.UserId == UID && x.IsActive == "A").Select(y => y.IdBatch).FirstOrDefault();

                return Ok(IdBatch);

            }
            catch (Exception)
            {

                return Conflict("Error in Code");
            }
        }
        [Route("~/api/GetCubeFaceAccessForBatch")]
        [HttpGet]
        public IActionResult GetCubeFaceAccessForBatch(int IdBatch)
        {
            try
            {

                List<TblCubefaceBatchMaster> data = DbContext.TblCubefaceBatchMaster.Where(x => x.IdBatch == IdBatch && x.IsActive == "A").ToList();

                return Ok(data);

            }
            catch (Exception)
            {

                return Conflict("Error in Code");
            }
        }

        [Route("~/api/GetCubeFaceColorCode")]
        [HttpGet]
        public IActionResult GetCubeFaceColorCode()
        {
            try
            {

                List<TblCubesFacesMaster> data = DbContext.TblCubesFacesMaster.Where(x=>x.IsActive == "A").ToList();

                return Ok(data);

            }
            catch (Exception)
            {

                return Conflict("Error in Code");
            }
        }
        public int get_redmeePoint(int? UID)
        {
            var data = (from l in DbContext.TblRewardsRedeemMaster
                        where l.IdUser == UID
                        select new
                        {
                            l.IdUser,
                            l.TotalPoints
                        }).GroupBy(x => new { x.IdUser }).Select(g => new
                        {
                            score = g.Sum(s => s.TotalPoints) ?? 0
                        }).FirstOrDefault();

            if (data != null)
                return data.score;
            else
                return 0;
        }

        public string GetHierarchyPath(List<TblOrganizationHierarchy> data, int id)
        {
            var item = data.FirstOrDefault(x => x.IdOrgHierarchy == id);
            if (item == null) return string.Empty;
            if (item.ParentIdOrgHierarchy == 0) return item.HierarchyName;

            return item.HierarchyName + "," + GetHierarchyPath(data, item.ParentIdOrgHierarchy);
        }
        [Route("~/api/GetBottomToTopHeirarchyName")]
        [HttpGet]
        public IActionResult GetBottomToTopHeirarchyName(int OrgId)
        {
            try
            {
                List<int> ParentIdOrgHierarchylist = DbContext.TblOrganizationHierarchy
                                 .Where(x => x.IdOrganization == OrgId && x.IsActive == "A").Select(v => v.ParentIdOrgHierarchy).ToList();
                List<TblOrganizationHierarchy> DataResult = new List<TblOrganizationHierarchy>();
                List<TblOrganizationHierarchy> Data = new List<TblOrganizationHierarchy>();
                if (ParentIdOrgHierarchylist != null)
                {
                    Data = DbContext.TblOrganizationHierarchy.Where(x => !ParentIdOrgHierarchylist.Contains(x.IdOrgHierarchy) && x.IdOrganization == OrgId && x.IsActive == "A").ToList();

                }
                if (Data != null)
                {
                    foreach (var items in Data)
                    {
                        List<TblOrganizationHierarchy> list = DbContext.TblOrganizationHierarchy.Where(x => x.IdOrganization == OrgId && x.IsActive == "A").ToList();

                        var item = list.FirstOrDefault(x => x.IdOrgHierarchy == items.IdOrgHierarchy); // assuming we're starting from id 3
                        if (item != null)
                        {
                            var path = GetHierarchyPath(list, item.ParentIdOrgHierarchy);
                            var result = $"{item.HierarchyName}({path.TrimEnd(',')})";
                            //Console.WriteLine(result); // Outputs: BATCH 1(NHT,OGt)
                            DataResult.Add(new TblOrganizationHierarchy()
                            {
                                IdOrgHierarchy = item.IdOrgHierarchy,
                                HierarchyName = result,
                                ParentIdOrgHierarchy = item.ParentIdOrgHierarchy,
                               

                            });
                           
                        }
                    }
                }
                else
                {
                    return BadRequest("No data");
                }
                return Ok(DataResult);

                //List<TblOrganizationHierarchy> list = DbContext.TblOrganizationHierarchy.Where(x => x.IdOrganization == OrgId && x.IsActive == "A").ToList();

                //var item = list.FirstOrDefault(x => x.IdOrgHierarchy == IdOrgHierarchy); // assuming we're starting from id 3
                //if (item != null)
                //{
                //    var path = GetHierarchyPath(list, item.ParentIdOrgHierarchy);
                //    var result = $"{item.HierarchyName}({path.TrimEnd(',')})";
                //    Console.WriteLine(result); // Outputs: BATCH 1(NHT,OGt)
                //    return Ok(result);
                //}
                //else
                //{
                //    return BadRequest("No data");
                //}
            }
            catch (Exception)
            {

                return Conflict("Error in Code");
            }


        }
    }
}







