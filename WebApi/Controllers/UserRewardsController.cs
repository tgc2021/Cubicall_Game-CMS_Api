using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Acclimate_Models;
using AesEverywhere;
using CrosswordCreator.Crossword;
using Cubicall_Models;
using DataAccess.EFCore.Data;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Models;
using m2ostnextservice.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TGC_Game.Web;

namespace WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/UserRewardsController")]

    public class UserRewardsController : ControllerBase
    {
        private readonly db_cubicall_game_devContext DbContext;
        AESAlgorithm ency = new AESAlgorithm();
        AES256 objency = new AES256();
        private readonly IUnitOfWork _unitOfWork;
        private IConfiguration Configuration;
        Crossword _board = new Crossword(18, 19);
        Random _rand = new Random();
        public UserRewardsController(IUnitOfWork unitOfWork, db_cubicall_game_devContext context, IConfiguration _configuration)
        {
            _unitOfWork = unitOfWork;
            DbContext = context;
            Configuration = _configuration;
        }
        [Route("~/api/rewardsreedmeLog")]
        [HttpPost]
        public IActionResult rewardsreedmeLog(APILogicBaniyaRequestModel ReqParam)
        {
            try
            {
                List<JToken> CouponsRedeemed = new List<JToken>();
                TblRewardsRedeemMaster data = new TblRewardsRedeemMaster();
                //List<tbl_couponsredeemed> dataCouponsRedeemed = new List<tbl_couponsredeemed>();
                string Requestdata = objency.Decrypt(ReqParam.Data, "DEMOTLB12345abcd");
                var jObj = (JObject)JsonConvert.DeserializeObject(Requestdata);
                data.AccountId = jObj["AccountID"].ToString();
                data.UserName = jObj["UserName"].ToString();
                data.EmailAddress = jObj["EmailAddress"].ToString();
                data.PartnerCode = jObj["PartnerCode"].ToString();
                data.ProviderCode = jObj["ProviderCode"].ToString();
                data.RedeemType = jObj["RedeemType"].ToString();
                data.TransactionId = jObj["TransactionID"].ToString();
                data.TotalPoints = Convert.ToInt32(jObj["TotalPoints"].ToString());
                data.IdUser = Convert.ToInt32(jObj["AccountID"].ToString());
                CouponsRedeemed = jObj["MiscellaneousData1"]["CouponsRedeemed"].ToList();
                //var userdata = Uow.GetUserBYemail(data.EmailAddress);


                //data.id_org = userdata.ID_ORGANIZATION;
                DbContext.TblRewardsRedeemMaster.Add(data);
                DbContext.SaveChanges();
                for (var i = 0; i < CouponsRedeemed.Count; i++)
                {

                    TblCouponsredeemed dataCouponsRedeemed = new TblCouponsredeemed()
                    {
                        CouponId = (string)CouponsRedeemed[i]["CouponID"],
                        WebsiteName = (string)CouponsRedeemed[i]["WebsiteName"],
                        CouponCode = (string)CouponsRedeemed[i]["CouponCode"],
                        CouponTitle = (string)CouponsRedeemed[i]["CouponTitle"],
                        CouponDescription = (string)CouponsRedeemed[i]["CouponDescription"].ToString(),
                        Link = (string)CouponsRedeemed[i]["Link"],
                        PointsUsed = (string)CouponsRedeemed[i]["PointsUsed"],
                        Image = (string)CouponsRedeemed[i]["Image"],
                        ExpiryDate = (string)CouponsRedeemed[i]["ExpiryDate"],
                        UpdatedDateTime = DateTime.UtcNow,
                        Status = "A",
                        IdUser = Convert.ToInt32(jObj["AccountID"].ToString()),
                        //id_organization = userdata.ID_ORGANIZATION,
                        IdRewards = data.IdRewards,
                        CardType = 1
                    };
                    DbContext.TblCouponsredeemed.Add(dataCouponsRedeemed);
                    DbContext.SaveChanges();
                }
                return Ok("Save data Successfully");
            }
            catch (Exception ex)
            {
                return Conflict("Error in Code"); ;
            }
        }
        [Route("~/api/GetCouponDetails")]
        [HttpGet]
        public IActionResult GetCouponDetails(int UID)
        {
            try
            {
                var DataModel = DbContext.TblCouponsredeemed.Where(x => x.IdUser == UID).ToList();
                return Ok(DataModel);
            }
            catch (System.Exception ex)
            {
                return Conflict("Error in Code");
            }
        }
    }
}