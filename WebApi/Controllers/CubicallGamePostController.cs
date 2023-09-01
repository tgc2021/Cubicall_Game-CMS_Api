using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Acclimate_Models;
using DataAccess.EFCore.Data;
using Domain.Entities;
using Domain.Interfaces;
using m2ostnextservice.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/CubicallGamePostController")]

    public class CubicallGamePostController : ControllerBase
    {
        private readonly db_cubicall_game_devContext DbContext;
        AESAlgorithm ency = new AESAlgorithm();
        private readonly IUnitOfWork Uow;
        public CubicallGamePostController(IUnitOfWork unitOfWork, db_cubicall_game_devContext context)
        {
            Uow = unitOfWork;
            DbContext = context;
        }
        [Route("~/api/PostCubeFaceComplteUserMasterLog")]
        [HttpPost]
        public IActionResult PostCubeFaceComplteUserMasterLog([FromBody] APIRequestModel ApiRequestdata)
        {
            try
            {
                TblCubesGameMasterUserLog ReqData = new TblCubesGameMasterUserLog();
                //string Requestdata = ency.getDecryptedString(ApiRequestdata.Data);
                ReqData = JsonSerializer.Deserialize<TblCubesGameMasterUserLog>(ApiRequestdata.Data);

                ReqData.UpdatedDateTime = DateTime.UtcNow;
                DbContext.TblCubesGameMasterUserLog.Add(ReqData);
                DbContext.SaveChanges();
                return Ok("Save data Successfully");

            }
            catch (Exception ex)
            {
                return Conflict("Error in Code"); ;
            }

        }

        [Route("~/api/PostCubeFacePerTileDetailsUserLog")]
        [HttpPost]
        public IActionResult PostCubeFacePerTileDetailsUserLog([FromBody] APIRequestModel ApiRequestdata)
        {
            try
            {
                List<TblCubesGameDetailsUserLog> ReqData = new List<TblCubesGameDetailsUserLog>();
                //string Requestdata = ency.getDecryptedString(ApiRequestdata.Data);
                ReqData = JsonSerializer.Deserialize<List<TblCubesGameDetailsUserLog>>(ApiRequestdata.Data);

                foreach (var item in ReqData)
                {
                    TblCubesGameDetailsUserLog tbldata = new TblCubesGameDetailsUserLog();
                    tbldata.IdGame = item.IdGame;
                    tbldata.CubesFacesGameId = item.CubesFacesGameId;
                    tbldata.PerTileId = item.PerTileId;
                    tbldata.PerTileQuestionId = item.PerTileQuestionId;
                    tbldata.QuestionId = item.QuestionId;
                    tbldata.PerTileAnswerId = item.PerTileAnswerId;
                    tbldata.IsRightAns = item.IsRightAns;
                    tbldata.UserInput = item.UserInput;
                    tbldata.GamePoints = item.GamePoints;
                    tbldata.TimetakenToComplete = item.TimetakenToComplete;
                    tbldata.UpdatedDateTime = item.UpdatedDateTime;
                    tbldata.IdUser = item.IdUser;
                    tbldata.AttemptNo = item.AttemptNo;
                    tbldata.UserPlayCount = item.UserPlayCount;
                    tbldata.IsCompleted = item.IsCompleted;
                    tbldata.CallsWaiting = item.CallsWaiting;
                    tbldata.WaitTime = item.WaitTime;
                    tbldata.Aht = item.Aht;
                    tbldata.Quality = item.Quality;
                    tbldata.FcrPercentage = item.FcrPercentage;
                    tbldata.ServiceLevel = item.ServiceLevel;
                    tbldata.StreakPoints = item.StreakPoints;
                    DbContext.TblCubesGameDetailsUserLog.Add(tbldata);
                    DbContext.SaveChanges();
                }

                return Ok("Save data Successfully");

            }
            catch (Exception ex)
            {
                return Conflict("Error in Code"); ;
            }

        }

        [Route("~/api/PostIsFirstTimeLogin")]
        [HttpPost]
        public IActionResult PostIsFirstTimeLogin([FromBody] APIRequestModel ApiRequestdata)
        {
            try
            {
                FirstTimeLoginModel ReqData = new FirstTimeLoginModel();
             //   string Requestdata = ency.getDecryptedString(ApiRequestdata.Data);
                ReqData = JsonSerializer.Deserialize<FirstTimeLoginModel>(ApiRequestdata.Data);
                TblUsers objData = (from c in DbContext.TblUsers
                                    where c.UserId == ReqData.UID
                                    select c).FirstOrDefault();
                if (objData != null)
                {
                    objData.IsFirstTimeLogin = 2;
                    DbContext.SaveChanges();
                }
                return Ok("Save data Successfully");

            }
            catch (Exception ex)
            {
                return Conflict("Error in Code"); ;
            }

        }

        [Route("~/api/PostUserHierarchy")]
        [HttpPost]
        public IActionResult PostUserHierarchy([FromBody] APIRequestModel ApiRequestdata)
        {
            try
            {
                TblUsers userdata = new TblUsers();
                ModelUsers userModel = new ModelUsers();
                TblUsersMappingwithHierarchy ReqData = new TblUsersMappingwithHierarchy();
                //string Requestdata = ency.getDecryptedString(ApiRequestdata.Data);
                ReqData = JsonSerializer.Deserialize<TblUsersMappingwithHierarchy>(ApiRequestdata.Data);
                TblUsersMappingwithHierarchy objData = (from c in DbContext.TblUsersMappingwithHierarchy
                                                        where c.UserId == ReqData.UserId && c.IsActive == "A"
                                                        select c).FirstOrDefault();
                if (objData != null)
                {
                    objData.UserId = ReqData.UserId;
                    objData.IdOrgHierarchy = ReqData.IdOrgHierarchy;
                    objData.IsActive = "D";
                    objData.UpdatedDateTime = DateTime.UtcNow;
                    DbContext.SaveChanges();


                }
                ReqData.UpdatedDateTime = DateTime.UtcNow;
                ReqData.IsActive = "A";
                DbContext.TblUsersMappingwithHierarchy.Add(ReqData);
                DbContext.SaveChanges();

                userdata = Uow.Login.GetUserBYUserId(ReqData.UserId);
                userModel.UserId = userdata.UserId;
                userModel.IdOrganization = userdata.IdOrganization;
                userModel.TrainerIdUser = userdata.TrainerIdUser;
                userModel.IdBranch = userdata.IdBranch;
                userModel.IdDepartment = userdata.IdDepartment;
                userModel.IdRole = userdata.IdRole;
                userModel.IdReportingManager = userdata.IdReportingManager;
                userModel.LoginUserId = userdata.LoginUserId;
                userModel.Password = userdata.Password;
                userModel.FirstName = userdata.FirstName;
                userModel.MiddleName = userdata.MiddleName;
                userModel.LastName = userdata.LastName;
                userModel.BirthDate = userdata.BirthDate;
                userModel.PhoneNo = userdata.PhoneNo;
                userModel.PermanentStreetAddress1 = userdata.PermanentStreetAddress1;
                userModel.PermanentStreetAddress2 = userdata.PermanentStreetAddress2;
                userModel.PermanentCity = userdata.PermanentCity;
                userModel.PermanentState = userdata.PermanentState;
                userModel.PermanentPincode = userdata.PermanentPincode;
                userModel.CurrentStreetAddress1 = userdata.CurrentStreetAddress1;
                userModel.CurrentStreetAddress2 = userdata.CurrentStreetAddress2;
                userModel.CurrentCity = userdata.CurrentCity;
                userModel.CurrentState = userdata.CurrentState;
                userModel.CurrentPincode = userdata.CurrentPincode;
                userModel.AadharNumber = userdata.AadharNumber;
                userModel.AadharCardImage = userdata.AadharCardImage;
                userModel.PanNumber = userdata.PanNumber;
                userModel.PanCardImage = userdata.PanCardImage;
                userModel.ProfilePicture = userdata.ProfilePicture;
                userModel.EmplyeeId = userdata.EmplyeeId;
                userModel.EmployeeDesignation = userdata.EmployeeDesignation;
                userModel.IsFirstTimeLogin = userdata.IsFirstTimeLogin;
                userModel.Otp = userdata.Otp;
                userModel.LoginType = userdata.LoginType;
                userModel.CountryCode = userdata.CountryCode;
                userModel.Status = userdata.Status;
                userModel.UpdatedDateTime = userdata.UpdatedDateTime;
                userModel.Email = userdata.Email;
                TblUsersMappingwithHierarchy OrgHier = Uow.Login.GetbyHierarchy_Id(userdata.UserId);
                if (OrgHier != null)
                    userModel.IdOrgHierarchy = OrgHier.IdOrgHierarchy;
                return Ok(userModel);

            }
            catch (Exception ex)
            {
                return Conflict("Error in Code"); ;
            }

        }
        [Route("~/api/PostMissionCompletedUserLog")]
        [HttpPost]
        public IActionResult PostMissionCompletedUserLog([FromBody] APIRequestModel ApiRequestdata)
        {
            try
            {
                TblGameMissionsUserLog ReqData = new TblGameMissionsUserLog();
                ReqData = JsonSerializer.Deserialize<TblGameMissionsUserLog>(ApiRequestdata.Data);

                ReqData.UpdatedDateTime = DateTime.UtcNow;
                DbContext.TblGameMissionsUserLog.Add(ReqData);
                DbContext.SaveChanges();
                return Ok("Save data Successfully");

            }
            catch (Exception ex)
            {
                return Conflict("Error in Code"); ;
            }

        }
        [Route("~/api/PostUserMappingWithBatch")]
        [HttpPost]
        public IActionResult PostUserMappingWithBatch([FromBody] APIRequestModel ApiRequestdata)
        {
            try
            {
                TblUsersMappingwithBatchid ReqData = new TblUsersMappingwithBatchid();
                ReqData = JsonSerializer.Deserialize<TblUsersMappingwithBatchid>(ApiRequestdata.Data);

                if(ReqData.UserId>0)
                {
                    TblUsersMappingwithBatchid objData = (from c in DbContext.TblUsersMappingwithBatchid
                                        where c.UserId == ReqData.UserId
                                        && c.IsActive=="A"
                                        select c).FirstOrDefault();
                    if (objData != null)
                    {
                        objData.IsActive = "D";
                        objData.UpdatedDateTime = DateTime.UtcNow;
                        DbContext.SaveChanges();
                        return Ok("Success");
                    }
                }
                ReqData.IsActive = "A";
                ReqData.UpdatedDateTime = DateTime.UtcNow;
                DbContext.TblUsersMappingwithBatchid.Add(ReqData);
                DbContext.SaveChanges();
                return Ok("Save data Successfully");

            }
            catch (Exception ex)
            {
                return Conflict("Error in Code"); ;
            }

        }

    }
}