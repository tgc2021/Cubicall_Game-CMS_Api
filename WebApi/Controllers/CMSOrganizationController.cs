using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text.Json;
using Acclimate_Models;
using DataAccess.EFCore.Data;
using Domain.Entities;
using Domain.Interfaces;
using m2ostnextservice.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/UserLoginController")]

    public class CMSOrganizationController : ControllerBase
    {
        private readonly db_cubicall_game_devContext DbContext;
        AESAlgorithm ency = new AESAlgorithm();
        private readonly IUnitOfWork Uow;
        private IWebHostEnvironment _env;
        string hrefURL = Startup.GethrefURL();
        private readonly IWebHostEnvironment webHostEnvironment;
        public CMSOrganizationController(IWebHostEnvironment hostEnvironment, IUnitOfWork unitOfWork, db_cubicall_game_devContext context, IWebHostEnvironment env)
        {
            Uow = unitOfWork;
            DbContext = context;
            _env = env;
            webHostEnvironment = hostEnvironment;
        }
        [Route("~/api/CreateOrganization")]
        [HttpPost]
        public IActionResult CreateOrganization([FromBody] APIRequestModel ApiRequestdata)
        {
            try
            {

                OrganizationModel OrganizationModel = new OrganizationModel();
                TblOrganization TblOrganizationdata = new TblOrganization();
                OrganizationModel = JsonSerializer.Deserialize<OrganizationModel>(ApiRequestdata.Data);
                bool isExist = false;
                isExist = DbContext.TblOrganization.Where(x => x.OrganizationCode == OrganizationModel.OrganizationCode).Any();
                if (isExist)
                {
                    return Ok("Organization code is already exist");
                }
                else
                {
                    if (OrganizationModel.Logo_Imgbytes != null)
                    {
                        byte[] imageBytes = Convert.FromBase64String(OrganizationModel.Logo_Imgbytes);
                        var mappedPath = Path.Combine(webHostEnvironment.WebRootPath, "OrganizationLogo");

                        var fileName = OrganizationModel.OrganizationCode + "_OrganizationLogo.png";
                        using (var fileStream = new FileStream(Path.Combine(mappedPath, fileName), FileMode.Create))
                        {
                            fileStream.Write(imageBytes, 0, imageBytes.Length);
                            TblOrganizationdata.Logo = hrefURL + "OrganizationLogo/" + fileName;
                        }
                    }
                    else
                    {
                        TblOrganizationdata.Logo = OrganizationModel.Logo;
                    }
                    if (OrganizationModel.IdOrganization == 0)
                    {

                        TblOrganizationdata.OrganizationCode = OrganizationModel.OrganizationCode;
                        TblOrganizationdata.OrganizationName = OrganizationModel.OrganizationName;
                        TblOrganizationdata.Description = OrganizationModel.Description;
                        TblOrganizationdata.Logo = OrganizationModel.Logo;
                        TblOrganizationdata.Name = OrganizationModel.Name;
                        TblOrganizationdata.PhoneNo = OrganizationModel.PhoneNo;
                        TblOrganizationdata.IdIndustry = OrganizationModel.IdIndustry;
                        TblOrganizationdata.IdBusinessType = OrganizationModel.IdBusinessType;
                        TblOrganizationdata.ContactEmail = OrganizationModel.ContactEmail;
                        TblOrganizationdata.DefaultEmail = OrganizationModel.DefaultEmail;
                        TblOrganizationdata.Status = OrganizationModel.Status;
                        TblOrganizationdata.UpdatedDateTime = OrganizationModel.UpdatedDateTime;
                        TblOrganizationdata.IdCmsUser = OrganizationModel.IdCmsUser;
                        TblOrganizationdata.SenderPassword = OrganizationModel.SenderPassword;
                        TblOrganizationdata.DomainEmailId = OrganizationModel.DomainEmailId;
                        TblOrganizationdata.Status = "A";
                        TblOrganizationdata.UpdatedDateTime = DateTime.UtcNow;
                        DbContext.TblOrganization.Add(TblOrganizationdata);
                        DbContext.SaveChanges();
                        return Ok("Save data Successfully");
                    }

                    else
                    {
                        TblOrganization objData = (from c in DbContext.TblOrganization
                                                   where c.IdOrganization == OrganizationModel.IdOrganization
                                                   select c).FirstOrDefault();
                        if (objData != null)
                        {
                            objData.OrganizationCode = OrganizationModel.OrganizationCode;
                            objData.OrganizationName = OrganizationModel.OrganizationName;
                            objData.Description = OrganizationModel.Description;
                            objData.Logo = OrganizationModel.Logo;
                            objData.Name = OrganizationModel.Name;
                            objData.PhoneNo = OrganizationModel.PhoneNo;
                            objData.IdIndustry = OrganizationModel.IdIndustry;
                            objData.IdBusinessType = OrganizationModel.IdBusinessType;
                            objData.ContactEmail = OrganizationModel.ContactEmail;
                            objData.DefaultEmail = OrganizationModel.DefaultEmail;
                            objData.Status = OrganizationModel.Status;
                            objData.UpdatedDateTime = OrganizationModel.UpdatedDateTime;
                            objData.IdCmsUser = OrganizationModel.IdCmsUser;
                            objData.SenderPassword = OrganizationModel.SenderPassword;
                            objData.DomainEmailId = OrganizationModel.DomainEmailId;
                            objData.Status = "A";
                            objData.UpdatedDateTime = DateTime.UtcNow;
                            DbContext.SaveChanges();
                            return Ok("Save data Successfully");

                        }

                    }
                }

                return Ok("update data Successfully");

            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        [Route("~/api/GetOrganizationHierarchy")]
        [HttpGet]
        public IActionResult GetOrganizationHierarchy(int OrgId)
        {
            try
            {
                List<TblOrganizationHierarchy> ParentIdOrgHierarchylist = DbContext.TblOrganizationHierarchy
                   .Where(x => x.IdOrganization == OrgId && x.IsActive == "A").ToList();

                return Ok(ParentIdOrgHierarchylist);
            }
            catch (Exception)
            {

                return Conflict("Error in Code");
            }
        }

        [Route("~/api/GetOrganization")]
        [HttpGet]
        public IActionResult GetOrganization()
        {
            try
            {
                var model = (from T in this.DbContext.TblOrganization
                             join S in this.DbContext.TblIndustry on T.IdIndustry equals S.IdIndustry
                             join R in this.DbContext.TblBusinessType on T.IdBusinessType equals R.IdBusinessType

                             select new
                             {
                                 R.BusinessTypeName,
                                 S.Industryname,
                                 T.IdBusinessType,
                                 T.IdIndustry,
                                 T.IdOrganization,
                                 T.OrganizationName,
                                 T.Description,
                                 T.Logo,
                                 T.Name,
                                 T.PhoneNo,
                                 T.ContactEmail,
                                 T.DefaultEmail,
                                 T.Status,
                                 T.UpdatedDateTime,
                                 T.SenderPassword,
                                 T.DomainEmailId,

                             }).Select(X => new OrganizationModel
                             {
                                 IdOrganization = X.IdOrganization,
                                 OrganizationName = X.OrganizationName,
                                 IdIndustry = X.IdIndustry,
                                 IdBusinessType = X.IdBusinessType,
                                 Description = X.Description,
                                 Logo = X.Logo,
                                 Name = X.Name,
                                 PhoneNo = X.PhoneNo,
                                 ContactEmail = X.ContactEmail,
                                 DefaultEmail = X.DefaultEmail,
                                 Status = X.Status,
                                 UpdatedDateTime = X.UpdatedDateTime,
                                 IndustryName = X.Industryname,
                                 BUSINESSTYPENAME = X.BusinessTypeName,
                                 SenderPassword = X.SenderPassword,
                                 DomainEmailId = X.DomainEmailId,
                             }).ToList();

                return Ok(model);
            }
            catch (Exception)
            {

                return Conflict("Error in Code");
            }
        }
        [Route("~/api/CreateOrganizationHierarchy")]
        [HttpPost]
        public IActionResult CreateOrganizationHierarchy([FromBody] APIRequestModel ApiRequestdata)
        {
            try
            {

                TblOrganizationHierarchy TblOrganizationdata = new TblOrganizationHierarchy();
                TblOrganizationdata = JsonSerializer.Deserialize<TblOrganizationHierarchy>(ApiRequestdata.Data);

                if (TblOrganizationdata.IdOrgHierarchy == 0)
                {

                    TblOrganizationdata.IsActive = "A";
                    TblOrganizationdata.UpdatedDateTime = DateTime.UtcNow;
                    DbContext.TblOrganizationHierarchy.Add(TblOrganizationdata);
                    DbContext.SaveChanges();
                    return Ok("Save data Successfully");
                }

                else
                {
                    TblOrganizationHierarchy objData = (from c in DbContext.TblOrganizationHierarchy
                                                        where c.IdOrgHierarchy == TblOrganizationdata.IdOrgHierarchy
                                                        select c).FirstOrDefault();
                    if (objData != null)
                    {
                        objData.IdOrganization = TblOrganizationdata.IdOrganization;
                        objData.IdCmsUser = TblOrganizationdata.IdCmsUser;
                        objData.IdOrgHierarchy = TblOrganizationdata.IdOrgHierarchy;
                        objData.ParentIdOrgHierarchy = TblOrganizationdata.ParentIdOrgHierarchy;

                        objData.IsActive = TblOrganizationdata.IsActive;
                        objData.UpdatedDateTime = DateTime.UtcNow;
                        DbContext.SaveChanges();
                        return Ok("Save data Successfully");

                    }

                }

                return Ok("update data Successfully");

            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }
        [Route("~/api/GetIndustryList")]
        [HttpGet]
        public IActionResult GetIndustryList()
        {
            try
            {
                List<TblIndustry> IndustryList = DbContext.TblIndustry
                   .Where(x => x.Status == "A").ToList();

                return Ok(IndustryList);
            }
            catch (Exception)
            {

                return Conflict("Error in Code");
            }
        }

        [Route("~/api/GetBusinessTypeList")]
        [HttpGet]
        public IActionResult GetBusinessTypeList()
        {
            try
            {
                List<TblBusinessType> BusinessTypeList = DbContext.TblBusinessType
                   .Where(x => x.Status == "A").ToList();

                return Ok(BusinessTypeList);
            }
            catch (Exception)
            {

                return Conflict("Error in Code");
            }
        }
        [Route("~/api/GetRoles")]
        [HttpGet]
        public IActionResult GetRoles(int OrgId)
        {
            try
            {
                List<TblCmsRoles> data = new List<TblCmsRoles>();
                List<TblCmsRoles> Rolelist = DbContext.TblCmsRoles
                   .Where(x => x.IdOrganization == OrgId && x.Status == "A").ToList();
                foreach (var item in Rolelist)
                {
                    List<int> idsfunction = item.IdsFunction.Split(',').Select(int.Parse).ToList();
                    List<TblCmsRoleFunction> functionList = this.DbContext.TblCmsRoleFunction.Where(x => idsfunction.Contains(x.IdFunction) && x.IsActive == "A").ToList();
                    string valueFunction = string.Join(",", functionList.Select(x => x.FunctionName.ToString()).ToArray());
                    data.Add(new TblCmsRoles()
                    {
                        IdCmsRole = item.IdCmsRole,
                        IdOrganization = item.IdOrganization,
                        RoleName = item.RoleName,
                        IdsFunction = valueFunction,
                        Description = item.Description,
                        Status = item.Status

                    });


    }

             
                return Ok(data);
            }
            catch (Exception)
            {

                return Conflict("Error in Code");
            }
        }

        [Route("~/api/CreateCMSRole")]
        [HttpPost]
        public IActionResult CreateCMSRole([FromBody] APIRequestModel ApiRequestdata)
        {
            try
            {
                TblCmsRoles ReqData = new TblCmsRoles();
                //string Requestdata = ency.getDecryptedString(ApiRequestdata.Data);
                ReqData = JsonSerializer.Deserialize<TblCmsRoles>(ApiRequestdata.Data);
                if (ReqData.IdCmsRole == 0)
                {
                    ReqData.UpdatedDateTime = DateTime.UtcNow;
                    ReqData.Status = "A";
                    DbContext.TblCmsRoles.Add(ReqData);
                    DbContext.SaveChanges();
                    return Ok("Save data Successfully");
                }
                else
                {
                    TblCmsRoles objData = (from c in DbContext.TblCmsRoles
                                           where c.IdCmsRole == ReqData.IdCmsRole
                                           select c).FirstOrDefault();
                    if (objData != null)
                    {

                        objData.IdOrganization = ReqData.IdOrganization;
                        objData.RoleName = ReqData.RoleName;
                        objData.IdsFunction = ReqData.IdsFunction;
                        objData.Description = ReqData.Description;
                        objData.Status = ReqData.Status;
                        objData.UpdatedDateTime = DateTime.UtcNow;
                        DbContext.SaveChanges();
                        return Ok("Edit data Successfully");
                    }
                    else
                    {
                        return BadRequest("can't edid");
                    }
                }

            }
            catch (Exception ex)
            {
                return Conflict("Error in Code"); ;
            }

        }

        [Route("~/api/PostCMS_UserRoleMapping")]
        [HttpPost]
        public IActionResult PostCMS_UserRoleMapping([FromBody] APIRequestModel ApiRequestdata)
        {
            try
            {
                TblRoleCmsUserMapping ReqData = new TblRoleCmsUserMapping();
                //string Requestdata = ency.getDecryptedString(ApiRequestdata.Data);
                ReqData = JsonSerializer.Deserialize<TblRoleCmsUserMapping>(ApiRequestdata.Data);
                if (ReqData.IdCmsRole == 0)
                {
                    ReqData.UpdatedDateTime = DateTime.UtcNow;
                    ReqData.Status = "A";
                    DbContext.TblRoleCmsUserMapping.Add(ReqData);
                    DbContext.SaveChanges();
                    return Ok("Save data Successfully");
                }
                else
                {
                    TblRoleCmsUserMapping objData = (from c in DbContext.TblRoleCmsUserMapping
                                                     where c.IdCmsRole == ReqData.IdCmsRole
                                                     select c).FirstOrDefault();
                    if (objData != null)
                    {
                        objData.IdCmsUser = ReqData.IdCmsUser;
                        objData.IdCmsRole = ReqData.IdCmsRole;
                        objData.IdOrganization = ReqData.IdOrganization;
                        objData.Status = ReqData.Status;
                        objData.UpdatedDateTime = DateTime.UtcNow;
                        DbContext.SaveChanges();
                        return Ok("Save data Successfully");
                    }
                    else
                    {
                        return BadRequest("can't edid");
                    }
                }

            }
            catch (Exception ex)
            {
                return Conflict("Error in Code"); ;
            }

        }
        [Route("~/api/CMSLogin")]
        [HttpPost]
        public IActionResult CMSLogin([FromBody] APIRequestModel ApiRequestdata)
        {
            try
            {
                TblCmsUsers user = new TblCmsUsers();
                //string Requestdata = ency.getDecryptedString(ApiRequestdata.Data);
                user = JsonSerializer.Deserialize<TblCmsUsers>(ApiRequestdata.Data);

                bool isExist = false;
                isExist = DbContext.TblCmsUsers.Where(x => x.UserName.Trim().ToLower() == user.UserName.Trim().ToLower() && x.Status == "A").Any();
                if (isExist)
                {
                    if (Uow.Login.IsCMSEmailPassExist(user.UserName, user.Password) == true)
                    {
                        TblCmsUsers cmsuser = (from x in DbContext.TblCmsUsers
                                               where x.UserName == user.UserName
                                               select x).FirstOrDefault();
                        return Ok(cmsuser);

                    }
                    else
                    {
                        return BadRequest("User Id And PassWord Not Match");
                    }
                }
                else
                {
                    return BadRequest("User Id Not Exist");
                }
            }
            catch (Exception ex)
            {
                return Conflict("Error in Code");
            }

        }



        [Route("~/api/CreateUser")]
        [HttpPost]
        public IActionResult CreateUser([FromBody] APIRequestModel ApiRequestdata)
        {
            try
            {
                TblCmsUsers ReqData = new TblCmsUsers();
                //string Requestdata = ency.getDecryptedString(ApiRequestdata.Data);
                ReqData = JsonSerializer.Deserialize<TblCmsUsers>(ApiRequestdata.Data);
                if (ReqData.IdCmsRole == 0)
                {
                    ReqData.UpdatedDateTime = DateTime.UtcNow;
                    ReqData.Status = "A";
                    DbContext.TblCmsUsers.Add(ReqData);
                    DbContext.SaveChanges();
                    return Ok("Save data Successfully");
                }
                else
                {
                    TblCmsUsers objData = (from c in DbContext.TblCmsUsers
                                           where c.IdCmsRole == ReqData.IdCmsUser
                                           select c).FirstOrDefault();
                    if (objData != null)
                    {

                        objData.IdOrganization = ReqData.IdOrganization;
                        objData.UserName = ReqData.UserName;
                        objData.Name   = ReqData.Name;
                        objData.PhoneNo = ReqData.PhoneNo;
                        objData.Password = ReqData.Password;
                        objData.IdOrgHierarchy = ReqData.IdOrgHierarchy;
                        objData.IdCmsRole = ReqData.IdCmsRole;
                        objData.Status = ReqData.Status;
                        objData.UpdatedDateTime = DateTime.UtcNow;
                        DbContext.SaveChanges();
                        return Ok("Edit data Successfully");
                    }
                    else
                    {
                        return BadRequest("can't edid");
                    }
                }

            }
            catch (Exception ex)
            {
                return Conflict("Error in Code"); ;
            }

        }
        [Route("~/api/GetCMSUserDetails")]
        [HttpGet]
        public IActionResult GetCMSUserDetails(int CMSUID)
        {
            try
            {
                TblCmsUsers objData = (from c in DbContext.TblCmsUsers
                                       where c.IdCmsRole == CMSUID
                                       select c).FirstOrDefault();
                CMSUserModel Userdate = new CMSUserModel();
                if (objData!=null)
                {
                    TblCmsRoles roleData = (from c in DbContext.TblCmsRoles
                                           where c.IdCmsRole == objData.IdCmsRole
                                           select c).FirstOrDefault();
                    List<int> idsfunction = roleData.IdsFunction.Split(',').Select(int.Parse).ToList();
                    List<TblCmsRoleFunction> functionList = this.DbContext.TblCmsRoleFunction.Where(x => idsfunction.Contains(x.IdFunction) && x.IsActive == "A").ToList();
                    string valueFunction = string.Join(",", functionList.Select(x => x.FunctionName.ToString()).ToArray());
                 

                    Userdate.IdCmsUser = Userdate.IdCmsUser;
                    Userdate.UserName = Userdate.UserName;
                    Userdate.EmployeeId = Userdate.EmployeeId;
                    Userdate.Name = Userdate.Name;
                    Userdate.Email = Userdate.Email;
                    Userdate.PhoneNo = Userdate.PhoneNo;
                    Userdate.Password = Userdate.Password;
                    Userdate.Status = Userdate.Status;
                    Userdate.UpdatedDateTime = Userdate.UpdatedDateTime;
                    Userdate.IdOrgHierarchy = Userdate.IdOrgHierarchy;
                    Userdate.IdOrganization = Userdate.IdOrganization;
                    Userdate.IdCmsRole = Userdate.IdCmsRole;
                    Userdate.Functions = valueFunction;



                }

                return Ok(Userdate);
            }
            catch (Exception)
            {

                return Conflict("Error in Code");
            }
        }
    }
}