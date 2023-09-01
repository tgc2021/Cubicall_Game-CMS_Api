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

    public class UserLoginController : ControllerBase
    {
        private readonly db_cubicall_game_devContext DbContext;
        AESAlgorithm ency = new AESAlgorithm();
        private readonly IUnitOfWork Uow;
        private IWebHostEnvironment _env;
        string hrefURL = Startup.GethrefURL();
        private readonly IWebHostEnvironment webHostEnvironment;
        public UserLoginController(IWebHostEnvironment hostEnvironment,IUnitOfWork unitOfWork, db_cubicall_game_devContext context, IWebHostEnvironment env)
        {
            Uow = unitOfWork;
            DbContext = context;
            _env = env;
            webHostEnvironment = hostEnvironment;
        }
        [Route("~/api/Login")]
        [HttpPost]
        public IActionResult Login([FromBody] APIRequestModel ApiRequestdata)
        {
            try
            {
                LoginModel user = new LoginModel();
                ModelUsers userModel = new ModelUsers();
                // string Requestdata = ency.getDecryptedString(ApiRequestdata.Data);
                user = JsonSerializer.Deserialize<LoginModel>(ApiRequestdata.Data);
                TblOrganization organization = Uow.Organization.GetById(1);
                string senderID = organization.ContactEmail;// use sender’s email id here..
                string senderPassword = organization.SenderPassword;

                if (user.LoginType == 2 || user.LoginType == 3 || user.LoginType == 4)
                {
                    if (Uow.Login.ISExist(user.UserLogin_Id))
                    {
                        TblUsers userdata = Uow.Login.GetUserBYLogin_Id(user.UserLogin_Id);
                        TblUsersLog log = new TblUsersLog();
                        log.IdUser = userdata.UserId;
                        log.UpdatedDateTime = DateTime.UtcNow;
                        log.IdOrganization = userdata.IdOrganization ?? 0;
                        Uow.LoginUserLog.Add(log);
                        Uow.Complete();
                        // string Data = JsonSerializer.Serialize(userdata);
                        // string Result = ency.getEncryptedString(Data);
                        return Ok(userdata);
                    }
                    else
                    {
                        TblUsers userdata = new TblUsers();
                        userdata.IdOrganization = 1;
                        //userdata.FirstName = user.Name;
                        userdata.LoginUserId = user.UserLogin_Id;
                        userdata.LoginType = user.LoginType;
                        userdata.IsFirstTimeLogin = 1;
                        userdata.Status = "A";
                        userdata.UpdatedDateTime = DateTime.UtcNow;
                        Uow.Login.Add(userdata);
                        Uow.Complete();
                        return Ok(userdata);
                    }

                }
                else if (user.LoginType == 5)
                {
                    if (Uow.Login.ISExist(user.UserLogin_Id))
                    {
                        TblUsers userdata = Uow.Login.GetUserBYLogin_Id(user.UserLogin_Id);
                        TblUsersLog log = new TblUsersLog();
                        log.IdUser = userdata.UserId;
                        log.UpdatedDateTime = DateTime.UtcNow;
                        log.IdOrganization = userdata.IdOrganization ?? 0;
                        Uow.LoginUserLog.Add(log);
                        Uow.Complete();
                        int flag = EmailAuthenticationforLogin(user.UserLogin_Id);
                        if (flag == 1)
                        {
                            return Ok("otp code sent to your email id");
                        }
                        else
                        {
                            return Ok("Code error");
                        }
                    }
                    else
                    { return Ok("This email is not register"); }
                }
                else if (user.LoginType == 7)
                {
                    if (Uow.Login.ISExist(user.UserLogin_Id))
                    {
                        if (Uow.Login.IsEmailPassExist(user.UserLogin_Id, user.Password) == true)
                        {
                            TblUsers userdata = Uow.Login.GetUserBYLogin_Id(user.UserLogin_Id);
                            TblUsersLog log = new TblUsersLog();
                            log.IdUser = userdata.UserId;
                            log.UpdatedDateTime = DateTime.UtcNow;
                            log.IdOrganization = userdata.IdOrganization ?? 0;
                            Uow.LoginUserLog.Add(log);
                            Uow.Complete();
                            ModelUsers data = Uow.Login.AllUserInfo(userdata.UserId);
                            return Ok(data);
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
                //else if (user.LoginType == 7)
                //{
                //    if (Uow.Login.ISExist(user.UserLogin_Id))
                //    {
                //        TblUsers userdata = Uow.Login.GetUserBYLogin_Id(user.UserLogin_Id);
                //        TblUsersLog log = new TblUsersLog();
                //        log.IdUser = userdata.UserId;
                //        log.UpdatedDateTime = DateTime.UtcNow;
                //        log.IdOrganization = userdata.IdOrganization ?? 0;
                //        Uow.LoginUserLog.Add(log);
                //        Uow.Complete();
                //        int flag = EmailAuthenticationforLogin(user.UserLogin_Id);
                //        if (flag == 1)
                //        {
                //            return Ok("otp code sent to your email id");
                //        }
                //        else
                //        {
                //            return Ok("Code error");
                //        }
                //    }
                //    else
                //    {
                //        if (OrgHier != null)
                //        {
                //            TblUsers userdata = new TblUsers();
                //            string[] emailList = user.UserLogin_Id.Split("@");
                //            TblOrganization orgdata = Uow.Login.IsEmailDomainExist(emailList[1]);
                //            if (orgdata != null)
                //            {
                //                int flag = LoginWithEmailDomain(user.UserLogin_Id, orgdata.IdOrganization, OrgHier.IdOrgHierarchy);
                //                if (flag == 1)
                //                {
                //                    return Ok("otp code sent to your email id");
                //                }
                //                else
                //                {
                //                    return Ok("Code error");
                //                }

                //            }
                //            else
                //            {
                //                return NotFound("Email Domain Not Exist");
                //            }
                //        }
                //        else
                //        {
                //            return BadRequest("Wrong Hierarchy In Request");
                //        }
                //    }
                //}
                else
                {
                    return Conflict("Login type not match");
                }
            }
            catch (Exception ex)
            {
                return Conflict("Error in Code");
            }

        }
        [Route("~/api/SignUpWithEmail")]
        [HttpPost]
        public IActionResult SignUpWithEmail([FromBody] APIRequestModel ApiRequestdata)
        {
            try
            {
                SignUpWithEmailModel user = new SignUpWithEmailModel();
                // string Requestdata = ency.getDecryptedString(ApiRequestdata.Data);
                user = JsonSerializer.Deserialize<SignUpWithEmailModel>(ApiRequestdata.Data);
                if (user.Email != null)
                {
                    TblOrganization organization = Uow.Organization.GetById(1);
                    string senderID = organization.ContactEmail;// use sender’s email id here..
                    string senderPassword = organization.SenderPassword;
                    if (Uow.Login.ISExist(user.Email))
                    {
                        return Ok("Email Already Exist");

                    }
                    else if (Uow.Login.ISExistforOTP(user.Email))
                    {
                        Random rnd = new Random();
                        int otp = rnd.Next(100000, 999999);
                        var webRoot = _env.WebRootPath; //get wwwroot Folder
                        var pathToFile = _env.WebRootPath
                                + Path.DirectorySeparatorChar.ToString()
                                + "EmailTemplate"
                                + Path.DirectorySeparatorChar.ToString()
                                + "GmailOTP.html";

                        var builder = new BodyBuilder();
                        using (StreamReader SourceReader = System.IO.File.OpenText(pathToFile))
                        {
                            builder.HtmlBody = SourceReader.ReadToEnd();
                        }
                        string htmlBody = builder.HtmlBody.Replace("#OTP#", Convert.ToString(otp))
                            //    .Replace("#LoginUserID#", Convert.ToString(LoginUser_Id))
                            //    .Replace("#Name#", Convert.ToString(UserName))
                            .Replace("#Date#", String.Format("{0:dddd, d MMMM yyyy}", DateTime.Now));

                        string[] multiemail = user.Email.Split(",");

                        SmtpClient smtp = new SmtpClient
                        {
                            Host = "smtp.gmail.com",
                            Port = 587,
                            EnableSsl = true,
                            DeliveryMethod = SmtpDeliveryMethod.Network,
                            Credentials = new System.Net.NetworkCredential(senderID, senderPassword),
                            Timeout = 30000
                        };
                        foreach (string userEmail in multiemail)
                        {
                            MailMessage message = new MailMessage(senderID, userEmail, "OTP", htmlBody);//body replaced by msg
                            message.IsBodyHtml = true;
                            smtp.Send(message);

                        }
                        TblUsers tbluser = Uow.Login.GetDeActiveUserBYemail(user.Email);
                        if (Uow.Login.SaveOTP(tbluser.UserId, Convert.ToString(otp)))
                        { return Ok("otp code sent to your email id"); }
                        else
                        { return Conflict("Error in Code"); }

                    }
                    else
                    {
                        Random rnd = new Random();
                        int otp = rnd.Next(100000, 999999);
                        var webRoot = _env.WebRootPath; //get wwwroot Folder
                        var pathToFile = _env.WebRootPath
                                + Path.DirectorySeparatorChar.ToString()
                                + "EmailTemplate"
                                + Path.DirectorySeparatorChar.ToString()
                                + "GmailOTP.html";

                        var builder = new BodyBuilder();
                        using (StreamReader SourceReader = System.IO.File.OpenText(pathToFile))
                        {
                            builder.HtmlBody = SourceReader.ReadToEnd();
                        }
                        string htmlBody = builder.HtmlBody.Replace("#OTP#", Convert.ToString(otp))
                            //    .Replace("#LoginUserID#", Convert.ToString(LoginUser_Id))
                            //    .Replace("#Name#", Convert.ToString(UserName))
                            .Replace("#Date#", String.Format("{0:dddd, d MMMM yyyy}", DateTime.Now));

                        string[] multiemail = user.Email.Split(",");

                        SmtpClient smtp = new SmtpClient
                        {
                            Host = "smtp.gmail.com",
                            Port = 587,
                            EnableSsl = true,
                            DeliveryMethod = SmtpDeliveryMethod.Network,
                            Credentials = new System.Net.NetworkCredential(senderID, senderPassword),
                            Timeout = 30000
                        };
                        foreach (string userEmail in multiemail)
                        {
                            MailMessage message = new MailMessage(senderID, userEmail, "OTP", htmlBody);//body replaced by msg
                            message.IsBodyHtml = true;
                            smtp.Send(message);

                        }
                        TblUsers userdata = new TblUsers();
                        userdata.IdOrganization = 1;
                        userdata.LoginUserId = user.Email;
                        userdata.IsFirstTimeLogin = 1;
                        userdata.LoginType = 5;
                        userdata.Email = user.Email;
                        userdata.Otp = Convert.ToString(otp);
                        userdata.Status = "D";
                        userdata.UpdatedDateTime = DateTime.UtcNow;
                        Uow.Login.Add(userdata);
                        Uow.Complete();
                        return Ok("otp code sent to your email id");
                    }


                }
                return Ok("otp code sent to your email id");
            }
            catch (Exception ex)
            {
                return Conflict("Error in Code");
            }
        }


        [Route("~/api/EmailOTPVerification")]
        [HttpPost]
        public IActionResult EmailOTPVerification([FromBody] APIRequestModel ApiRequestdata)
        {
            try
            {
                ConfirmOTPModel Modeldata = new ConfirmOTPModel();
                // string Requestdata = ency.getDecryptedString(ApiRequestdata.Data);
                Modeldata = JsonSerializer.Deserialize<ConfirmOTPModel>(ApiRequestdata.Data);

                if (Modeldata != null)
                {
                    TblUsers userdata = Uow.Login.GetUserBYLogin_Id(Modeldata.Email);
                    TblUsersMappingwithHierarchy OrgHier = Uow.Login.GetbyHierarchy_Id(userdata.UserId);
                    ModelUsers userModel = new ModelUsers();
                    if (userdata != null && OrgHier != null)
                    {

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
                        userModel.IdOrgHierarchy = OrgHier.IdOrgHierarchy;


                        if (Uow.Login.ISExistforOTP(Modeldata.Email) && Modeldata.loginorsignup == 0)
                        {
                            TblUsers user = Uow.Login.GetUserBYemailForEmailOTP(Modeldata.Email);
                            if (Uow.Login.IsEmailOTPExist(Modeldata.Email, Modeldata.Email_OTP))
                            {
                                if (Uow.Login.SetuserSatus(user.UserId, "A"))
                                {
                                    userModel.Status = "A";
                                    return Ok(userModel);
                                }
                                return Ok(userModel);
                            }
                            else
                            {
                                return BadRequest("Wrong OTP");
                            }
                        }
                        else if (Uow.Login.ISExist(Modeldata.Email) && Modeldata.loginorsignup == 1)
                        {
                            TblUsers user = Uow.Login.GetUserBYemail(Modeldata.Email);
                            if (Uow.Login.IsEmailOTPExist(Modeldata.Email, Modeldata.Email_OTP))
                            {
                                if (Uow.Login.SetuserSatus(user.UserId, "A"))
                                {
                                    userModel.Status = "A";
                                    return Ok(userModel);
                                }
                                return Ok(userModel);
                            }
                            else
                            {
                                return BadRequest("Wrong OTP");
                            }
                        }
                        else
                        {
                            return NotFound("Email not exist");

                        }
                    }
                    else
                    {
                        return BadRequest("Wrong Hierarchy In Request");
                    }
                }

                else
                {
                    return NotFound("Post data is null");
                }
            }
            catch (Exception ex)
            {
                return Conflict("Error in Code");
            }

        }
        [Route("~/api/CubiCallSSOLogin")]
        [HttpPost]
        public IActionResult CubiCallSSOLogin([FromBody] APIRequestModel ApiRequestdata)
        {
            try
            {

                SSOLoginModel user = new SSOLoginModel();
                user = JsonSerializer.Deserialize<SSOLoginModel>(ApiRequestdata.Data);
                if (Uow.Login.ISExist(user.UserLogin_Id))
                {
                    TblUsers userdata = Uow.Login.GetUserBYLogin_Id(user.UserLogin_Id);
                    TblUsersLog log = new TblUsersLog();
                    log.IdUser = userdata.UserId;
                    log.UpdatedDateTime = DateTime.UtcNow;
                    log.IdOrganization = userdata.IdOrganization ?? 0;
                    Uow.LoginUserLog.Add(log);
                    Uow.Complete();
                    ModelUsers userModel = Uow.Login.AllUserInfo(userdata.UserId);
                    return Ok(userModel);
                }
                else
                {
                    TblUsers userdata = new TblUsers();
                    userdata.IdOrganization = user.IdOrganization;
                    userdata.LoginUserId = user.UserLogin_Id;
                    userdata.LoginType = 6;//type is for sso login
                    userdata.IsFirstTimeLogin = 1;
                    userdata.Status = "A";
                    userdata.UpdatedDateTime = DateTime.UtcNow;
                    Uow.Login.Add(userdata);
                    Uow.Complete();
                    ModelUsers userModel = Uow.Login.AllUserInfo(userdata.UserId);
                    return Ok(userModel);
                }
            }
            catch (Exception)
            {

                return Conflict("Error in Code");
            }
        }


        //[Route("~/api/CubiCallSSOLogin")]
        //[HttpPost]
        //public IActionResult CubiCallSSOLogin([FromBody] APIRequestModel ApiRequestdata)
        //{
        //    try
        //    {

        //        SSOLoginModel user = new SSOLoginModel();
        //        ModelUsers userModel = new ModelUsers();
        //        user = JsonSerializer.Deserialize<SSOLoginModel>(ApiRequestdata.Data);
        //        TblOrganizationHierarchy OrgHier = Uow.Login.GetUserHierarchy_Id(user.IdOrganization, user.HierarchyName);
        //        if (Uow.Login.ISExist(user.UserLogin_Id))
        //        {
        //            TblUsers userdata = Uow.Login.GetUserBYLogin_Id(user.UserLogin_Id);
        //            TblUsersLog log = new TblUsersLog();
        //            log.IdUser = userdata.UserId;
        //            log.UpdatedDateTime = DateTime.UtcNow;
        //            log.IdOrganization = userdata.IdOrganization ?? 0;
        //            Uow.LoginUserLog.Add(log);
        //            Uow.Complete();
        //            userModel.UserId = userdata.UserId;
        //            userModel.IdOrganization = userdata.IdOrganization;
        //            userModel.TrainerIdUser = userdata.TrainerIdUser;
        //            userModel.IdBranch = userdata.IdBranch;
        //            userModel.IdDepartment = userdata.IdDepartment;
        //            userModel.IdRole = userdata.IdRole;
        //            userModel.IdReportingManager = userdata.IdReportingManager;
        //            userModel.LoginUserId = userdata.LoginUserId;
        //            userModel.Password = userdata.Password;
        //            userModel.FirstName = userdata.FirstName;
        //            userModel.MiddleName = userdata.MiddleName;
        //            userModel.LastName = userdata.LastName;
        //            userModel.BirthDate = userdata.BirthDate;
        //            userModel.PhoneNo = userdata.PhoneNo;
        //            userModel.PermanentStreetAddress1 = userdata.PermanentStreetAddress1;
        //            userModel.PermanentStreetAddress2 = userdata.PermanentStreetAddress2;
        //            userModel.PermanentCity = userdata.PermanentCity;
        //            userModel.PermanentState = userdata.PermanentState;
        //            userModel.PermanentPincode = userdata.PermanentPincode;
        //            userModel.CurrentStreetAddress1 = userdata.CurrentStreetAddress1;
        //            userModel.CurrentStreetAddress2 = userdata.CurrentStreetAddress2;
        //            userModel.CurrentCity = userdata.CurrentCity;
        //            userModel.CurrentState = userdata.CurrentState;
        //            userModel.CurrentPincode = userdata.CurrentPincode;
        //            userModel.AadharNumber = userdata.AadharNumber;
        //            userModel.AadharCardImage = userdata.AadharCardImage;
        //            userModel.PanNumber = userdata.PanNumber;
        //            userModel.PanCardImage = userdata.PanCardImage;
        //            userModel.ProfilePicture = userdata.ProfilePicture;
        //            userModel.EmplyeeId = userdata.EmplyeeId;
        //            userModel.EmployeeDesignation = userdata.EmployeeDesignation;
        //            userModel.IsFirstTimeLogin = userdata.IsFirstTimeLogin;
        //            userModel.Otp = userdata.Otp;
        //            userModel.LoginType = userdata.LoginType;
        //            userModel.CountryCode = userdata.CountryCode;
        //            userModel.Status = userdata.Status;
        //            userModel.UpdatedDateTime = userdata.UpdatedDateTime;
        //            userModel.Email = userdata.Email;
        //            userModel.IdOrgHierarchy = OrgHier.IdOrgHierarchy;

        //            return Ok(userModel);
        //        }
        //        else
        //        {
        //            if (OrgHier != null)
        //            {
        //                TblUsersMappingwithHierarchy userHierdata = new TblUsersMappingwithHierarchy();
        //                TblUsers userdata = new TblUsers();
        //                userdata.IdOrganization = user.IdOrganization;
        //                userdata.FirstName = user.Name;
        //                userdata.LoginUserId = user.UserLogin_Id;
        //                userdata.LoginType = 6;//type is for sso login
        //                userdata.IsFirstTimeLogin = 1;
        //                userdata.Status = "A";
        //                userdata.UpdatedDateTime = DateTime.UtcNow;
        //                Uow.Login.Add(userdata);
        //                Uow.Complete();

        //                userHierdata.IdOrgHierarchy = OrgHier.IdOrgHierarchy;
        //                userHierdata.UserId = userdata.UserId;
        //                userHierdata.IsActive = "A";
        //                userHierdata.UpdatedDateTime = DateTime.UtcNow;
        //                DbContext.TblUsersMappingwithHierarchy.Add(userHierdata);
        //                DbContext.SaveChanges();

        //                userModel.UserId = userdata.UserId;
        //                userModel.LoginType = userdata.LoginType;
        //                userModel.IdOrganization = userdata.IdOrganization;
        //                userModel.LoginUserId = userdata.LoginUserId;
        //                userModel.FirstName = userdata.FirstName;
        //                userModel.Email = userdata.Email;
        //                userModel.IdOrgHierarchy = OrgHier.IdOrgHierarchy;


        //                return Ok(userModel);
        //            }
        //            else
        //            {
        //                return BadRequest("Wrong Hierarchy In Request");
        //            }

        //        }
        //    }
        //    catch (Exception)
        //    {

        //        return Conflict("Error in Code");
        //    }
        //}
        public int EmailAuthenticationforLogin(string toEmail)
        {
            try
            {
                if (toEmail != null)
                {
                    TblOrganization organization = Uow.Organization.GetById(1);
                    string senderID = organization.ContactEmail;// use sender’s email id here..
                    string senderPassword = organization.SenderPassword;

                    Random rnd = new Random();
                    int otp = rnd.Next(100000, 999999);
                    var webRoot = _env.WebRootPath; //get wwwroot Folder
                    var pathToFile = _env.WebRootPath
                            + Path.DirectorySeparatorChar.ToString()
                            + "EmailTemplate"
                            + Path.DirectorySeparatorChar.ToString()
                            + "GmailOTP.html";

                    var builder = new BodyBuilder();
                    using (StreamReader SourceReader = System.IO.File.OpenText(pathToFile))
                    {
                        builder.HtmlBody = SourceReader.ReadToEnd();
                    }
                    string htmlBody = builder.HtmlBody.Replace("#OTP#", Convert.ToString(otp))
                    //    .Replace("#LoginUserID#", Convert.ToString(LoginUser_Id))
                    //    .Replace("#Name#", Convert.ToString(UserName))
                        .Replace("#Date#", String.Format("{0:dddd, d MMMM yyyy}", DateTime.Now));

                    string[] multiemail = toEmail.Split(",");

                    SmtpClient smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        Credentials = new System.Net.NetworkCredential(senderID, senderPassword),
                        Timeout = 30000
                    };
                    foreach (string userEmail in multiemail)
                    {
                        MailMessage message = new MailMessage(senderID, userEmail, "OTP", htmlBody);//body replaced by msg
                        message.IsBodyHtml = true;
                        smtp.Send(message);
                        if (Uow.Login.ISExist(userEmail))
                        {
                            TblUsers tbluser = Uow.Login.GetUserBYemail(userEmail);
                            if (Uow.Login.SaveOTP(tbluser.UserId, Convert.ToString(otp)))
                            { return 1; }
                            else
                            { return 0; }

                        }
                        else
                        {
                            return 0;
                        }
                    }

                }
                return 1; ;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        [Route("~/api/UserRegistration")]
        [HttpPost]
        public IActionResult UserRegistration(LoginModel user)
        {
            try
            {
                //UserRegistrationModel user = new UserRegistrationModel();

                if (user.UserLogin_Id != null && !Uow.Login.ISExist(user.UserLogin_Id))
                {
                    TblUsers userdata = new TblUsers();
                    string[] emailList = user.UserLogin_Id.Split("@");
                    TblOrganization orgdata = Uow.Login.IsEmailDomainExist(emailList[1]);
                    if (orgdata != null)
                    {
                        userdata.IdOrganization = orgdata.IdOrganization;
                        userdata.LoginUserId = user.UserLogin_Id;
                        userdata.IsFirstTimeLogin = 1;
                        userdata.Status = "A";
                        userdata.UpdatedDateTime = DateTime.UtcNow;
                        Uow.Login.Add(userdata);
                        Uow.Complete();
                        return Ok("User Registration Success");
                    }
                    else
                    {
                        return NotFound("Email Domain Not Exist");
                    }

                }
                else
                {
                    return BadRequest("Email already exist");
                }
            }
            catch (Exception ex)
            {
                return Conflict("Error in Code");
            }

        }

        public int LoginWithEmailDomain(string Email, int OrgID, int OHId)
        {
            try
            {

                if (Email != null)
                {
                    TblOrganization organization = Uow.Organization.GetById(1);
                    string senderID = organization.ContactEmail;// use sender’s email id here..
                    string senderPassword = organization.SenderPassword;
                    if (Uow.Login.ISExistforOTP(Email))
                    {
                        Random rnd = new Random();
                        int otp = rnd.Next(100000, 999999);
                        var webRoot = _env.WebRootPath; //get wwwroot Folder
                        var pathToFile = _env.WebRootPath
                                + Path.DirectorySeparatorChar.ToString()
                                + "EmailTemplate"
                                + Path.DirectorySeparatorChar.ToString()
                                + "GmailOTP.html";

                        var builder = new BodyBuilder();
                        using (StreamReader SourceReader = System.IO.File.OpenText(pathToFile))
                        {
                            builder.HtmlBody = SourceReader.ReadToEnd();
                        }
                        string htmlBody = builder.HtmlBody.Replace("#OTP#", Convert.ToString(otp))
                            //    .Replace("#LoginUserID#", Convert.ToString(LoginUser_Id))
                            //    .Replace("#Name#", Convert.ToString(UserName))
                            .Replace("#Date#", String.Format("{0:dddd, d MMMM yyyy}", DateTime.Now));

                        string[] multiemail = Email.Split(",");

                        SmtpClient smtp = new SmtpClient
                        {
                            Host = "smtp.gmail.com",
                            Port = 587,
                            EnableSsl = true,
                            DeliveryMethod = SmtpDeliveryMethod.Network,
                            Credentials = new System.Net.NetworkCredential(senderID, senderPassword),
                            Timeout = 30000
                        };
                        foreach (string userEmail in multiemail)
                        {
                            MailMessage message = new MailMessage(senderID, userEmail, "OTP", htmlBody);//body replaced by msg
                            message.IsBodyHtml = true;
                            smtp.Send(message);

                        }
                        TblUsers tbluser = Uow.Login.GetDeActiveUserBYemail(Email);
                        if (Uow.Login.SaveOTP(tbluser.UserId, Convert.ToString(otp)))
                        { return 1; }
                        else
                        { return 0; }

                    }
                    else
                    {
                        Random rnd = new Random();
                        int otp = rnd.Next(100000, 999999);
                        var webRoot = _env.WebRootPath; //get wwwroot Folder
                        var pathToFile = _env.WebRootPath
                                + Path.DirectorySeparatorChar.ToString()
                                + "EmailTemplate"
                                + Path.DirectorySeparatorChar.ToString()
                                + "GmailOTP.html";

                        var builder = new BodyBuilder();
                        using (StreamReader SourceReader = System.IO.File.OpenText(pathToFile))
                        {
                            builder.HtmlBody = SourceReader.ReadToEnd();
                        }
                        string htmlBody = builder.HtmlBody.Replace("#OTP#", Convert.ToString(otp))
                            //    .Replace("#LoginUserID#", Convert.ToString(LoginUser_Id))
                            //    .Replace("#Name#", Convert.ToString(UserName))
                            .Replace("#Date#", String.Format("{0:dddd, d MMMM yyyy}", DateTime.Now));

                        string[] multiemail = Email.Split(",");

                        SmtpClient smtp = new SmtpClient
                        {
                            Host = "smtp.gmail.com",
                            Port = 587,
                            EnableSsl = true,
                            DeliveryMethod = SmtpDeliveryMethod.Network,
                            Credentials = new System.Net.NetworkCredential(senderID, senderPassword),
                            Timeout = 30000
                        };
                        foreach (string userEmail in multiemail)
                        {
                            MailMessage message = new MailMessage(senderID, userEmail, "OTP", htmlBody);//body replaced by msg
                            message.IsBodyHtml = true;
                            smtp.Send(message);

                        }
                        TblUsers userdata = new TblUsers();
                        userdata.IdOrganization = OrgID;
                        userdata.LoginUserId = Email;
                        userdata.IsFirstTimeLogin = 1;
                        userdata.LoginType = 7;
                        userdata.Email = Email;
                        userdata.Otp = Convert.ToString(otp);
                        userdata.Status = "D";
                        userdata.UpdatedDateTime = DateTime.UtcNow;
                        Uow.Login.Add(userdata);
                        Uow.Complete();
                        TblUsersMappingwithHierarchy userHierdata = new TblUsersMappingwithHierarchy();

                        userHierdata.IdOrgHierarchy = OHId;
                        userHierdata.UserId = userdata.UserId;
                        userHierdata.IsActive = "A";
                        userHierdata.UpdatedDateTime = DateTime.UtcNow;
                        DbContext.TblUsersMappingwithHierarchy.Add(userHierdata);
                        DbContext.SaveChanges();
                        return 1;
                    }


                }
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        [Route("~/api/SignInWithDomain")]
        [HttpPost]
        public IActionResult SignInWithDomain(SignInWithDomainModel user)
        {
            try
            {
                if (user.UserLogin_Id != null && !Uow.Login.ISExist(user.UserLogin_Id))
                {
                    TblUsers userdata = new TblUsers();
                    string[] emailList = user.UserLogin_Id.Split("@");
                    TblOrganization orgdata = Uow.Login.IsEmailDomainExist(emailList[1]);
                    if (orgdata != null)
                    {
                        userdata.IdOrganization = orgdata.IdOrganization;
                        userdata.LoginUserId = user.UserLogin_Id;
                        userdata.IsFirstTimeLogin = 1;
                        userdata.Status = "A";
                        userdata.UpdatedDateTime = DateTime.UtcNow;
                        Uow.Login.Add(userdata);
                        Uow.Complete();
                        return Ok("User Registration Success");
                    }
                    else
                    {
                        return NotFound("Email Domain Not Exist");
                    }

                }
                else
                {
                    return BadRequest("User already exist");
                }
            }
            catch (Exception ex)
            {
                return Conflict("Error in Code");
            }

        }

        [Route("~/api/GetVerifyOrganizationCode")]
        [HttpGet]
        public IActionResult GetVerifyOrganizationCode(string OrganizationCode)
        {
            try
            {
                TblOrganization data = DbContext.TblOrganization.Where(x => x.OrganizationCode == OrganizationCode).FirstOrDefault();
                if (data != null)
                    return Ok(data);
                else
                    return Ok(false);
            }
            catch (Exception ex)
            {

                return Conflict("Error in Code");
            }
        }
        [Route("~/api/UserSignIn")]
        [HttpPost]
        public IActionResult UserSignIn([FromBody] APIRequestModel ApiRequestdata)
        {
            try
            {
                LoginModel user = new LoginModel();
                //string Requestdata = ency.getDecryptedString(ApiRequestdata.Data);
                user = JsonSerializer.Deserialize<LoginModel>(ApiRequestdata.Data);

                if (user.UserLogin_Id != null && !Uow.Login.ISExist(user.UserLogin_Id))
                {
                    TblUsers userdata = new TblUsers();
                    userdata.IdOrganization = user.IdOrganization;
                    userdata.LoginUserId = user.UserLogin_Id;
                    userdata.FirstName = user.Name;
                    userdata.IsFirstTimeLogin = 7;
                    userdata.Password = user.Password;
                    userdata.Status = "A";
                    userdata.UpdatedDateTime = DateTime.UtcNow;
                    Uow.Login.Add(userdata);
                    Uow.Complete();
                    return Ok(userdata);


                }
                else
                {
                    return BadRequest("Email already exist");
                }
            }
            catch (Exception ex)
            {
                return Conflict("Error in Code");
            }

        }
        [Route("~/api/LoginSecurityQuestion")]
        [HttpGet]
        public IActionResult LoginSecurityQuestion()
        {
            try
            {

                List<TblLoginSecurityQuestion> LoginSecurityQuestion = DbContext.TblLoginSecurityQuestion.Where(x => x.IsActive == "A").ToList();

                return Ok(LoginSecurityQuestion);
            }
            catch (Exception ex)
            {

                return Conflict("Error in Code");
            }
        }
        [Route("~/api/PostSecurityQuestion")]
        [HttpPost]
        public IActionResult PostSecurityQuestion([FromBody] APIRequestModel ApiRequestdata)
        {
            try
            {
                List<SecurityQuestionModel> user = new List<SecurityQuestionModel>();
                //string Requestdata = ency.getDecryptedString(ApiRequestdata.Data);
                user = JsonSerializer.Deserialize<List<SecurityQuestionModel>>(ApiRequestdata.Data);
               
                if(user.Count>0)
                {
                    foreach (var item in user)
                    {
                        TblUserSecurityQuestionLog ReqData = new TblUserSecurityQuestionLog();
                        ReqData.UpdatedDateTime = DateTime.UtcNow;
                        ReqData.IdSecurityQuestion = item.IdSecurityQuestion;
                        ReqData.SecurityQuestionAns = Convert.ToString(item.SecurityQuestion);
                        ReqData.IdUser = item.UID;
                        DbContext.TblUserSecurityQuestionLog.Add(ReqData);
                        DbContext.SaveChanges();
                    }
                    return Ok("Save data Successfully");
                }
                else
                {
                    return BadRequest("Model null");
                }

            }
            catch (Exception ex)
            {
                return Conflict("Error in Code");
            }

        }


        [Route("~/api/PostNewPassword")]
        [HttpPost]
        public IActionResult PostNewPassword([FromBody] APIRequestModel ApiRequestdata)
        {
            try
            {
                NewPasswordModel user = new NewPasswordModel();
                //string Requestdata = ency.getDecryptedString(ApiRequestdata.Data);
                user = JsonSerializer.Deserialize<NewPasswordModel>(ApiRequestdata.Data);


                if (user.UID > 0)
                {
                    TblUsers objData = (from c in DbContext.TblUsers
                                        where c.UserId == user.UID
                                        select c).FirstOrDefault();
                    if (objData != null)
                    {
                        objData.Password = Convert.ToString(user.Password);
                        DbContext.SaveChanges();
                        return Ok("Success");
                    }
                    return BadRequest("User Id Not Exist");
                }
                else
                {
                    return BadRequest("Model null");
                }

            }
            catch (Exception ex)
            {
                return Conflict("Error in Code");
            }

        }

        [Route("~/api/PostUserProfileImage")]
        [HttpPost]
        public IActionResult PostUserProfileImage([FromBody] APIRequestModel ApiRequestdata)
        {
            try
            {

                UserProfileImgModel UserModel = new UserProfileImgModel();
                TblOrganization TblOrganizationdata = new TblOrganization();
                UserModel = JsonSerializer.Deserialize<UserProfileImgModel>(ApiRequestdata.Data);

                if (UserModel.UID > 0)
                {
                    TblUsers objData = (from c in DbContext.TblUsers
                                        where c.UserId == UserModel.UID
                                        select c).FirstOrDefault();
                    if (objData != null)
                    {

                        if (UserModel.UserProfileImg != null)
                        {
                            byte[] imageBytes = Convert.FromBase64String(UserModel.UserProfileImg);
                            var mappedPath = Path.Combine(webHostEnvironment.WebRootPath, "UserProfileImg");

                            var fileName = UserModel.UID + "_UserProfileImg.png";
                            using (var fileStream = new FileStream(Path.Combine(mappedPath, fileName), FileMode.Create))
                            {
                                fileStream.Write(imageBytes, 0, imageBytes.Length);
                                objData.ProfilePicture = hrefURL + "UserProfileImg/" + fileName;
                            }
                        }
                        DbContext.SaveChanges();
                        return Ok(objData);
                    }
                    else return BadRequest("User Id Not Exist");
                }
                else
                {
                    return BadRequest("User Id Not Exist");
                }
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        [Route("~/api/CheckUserSecurityQuestion")]
        [HttpPost]
        public IActionResult CheckUserSecurityQuestion([FromBody] APIRequestModel ApiRequestdata)
        {
            try
            {
                ForgetPwdModel user = new ForgetPwdModel();
                //string Requestdata = ency.getDecryptedString(ApiRequestdata.Data);
                user = JsonSerializer.Deserialize<ForgetPwdModel>(ApiRequestdata.Data);

                TblUsers objData = (from x in DbContext.TblUsers
                                    where x.LoginUserId == user.LoginUserId
                                    select x).FirstOrDefault();
                if(objData!=null)
                { 
                return Ok(Uow.Login.IsSecurityExist(objData.UserId,user.IdSecurityQuestion,user.SecurityQuestion));


                }
                else
                {
                    return BadRequest("User Id not exist");
                }
            }
            catch (Exception ex)
            {
                return Conflict("Error in Code");
            }

        }

        [Route("~/api/ChangePassword")]
        [HttpPost]
        public IActionResult ChangePassword([FromBody] APIRequestModel ApiRequestdata)
        {
            try
            {
                ChangePasswordModel user = new ChangePasswordModel();
                //string Requestdata = ency.getDecryptedString(ApiRequestdata.Data);
                user = JsonSerializer.Deserialize<ChangePasswordModel>(ApiRequestdata.Data);

                if (Uow.Login.ISExist(user.LoginUserId))
                {
                    TblUsers objData = (from x in DbContext.TblUsers
                                where x.LoginUserId == user.LoginUserId
                                select x).FirstOrDefault();

                    if (objData != null)
                    {
                        TblUsers data = (from x in DbContext.TblUsers
                                            where x.UserId == objData.UserId
                                            select x).FirstOrDefault();
                        data.Password = user.Password;
                        DbContext.SaveChanges();
                      
                    }
                    return Ok("Success");
                }
                else
                {
                    return BadRequest("User Id not exist");
                }
            }
            catch (Exception ex)
            {
                return Conflict("Error in Code");
            }

        }

    }
}