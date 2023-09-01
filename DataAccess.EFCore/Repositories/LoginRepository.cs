
using Acclimate_Models;
using DataAccess.EFCore.Data;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.EFCore.Repositories
{
    public class LoginRepository : GenericRepository<TblUsers>, ILoginRepository
    {
        public LoginRepository(db_cubicall_game_devContext context) : base(context)
        {
        }

        public bool IsEmailPassExist(string Login_User_Id, string Password)
        {
            bool isExist = false;
            isExist = _context.TblUsers.Where(x => x.LoginUserId.Trim().ToLower() == Login_User_Id.Trim().ToLower() && x.Password.Trim().ToLower() == Password.Trim().ToLower()).Any();
            return isExist;
        }
        public bool IsCMSEmailPassExist(string Login_User_Id, string Password)
        {
            bool isExist = false;
            isExist = _context.TblCmsUsers.Where(x => x.UserName.Trim().ToLower() == Login_User_Id.Trim().ToLower() && x.Password.Trim().ToLower() == Password.Trim().ToLower()).Any();
            return isExist;
        }
        public TblUsers GetUserBYLogin_Id(string Login_User_Id)
        {
            var user = (from x in _context.TblUsers
                        where x.LoginUserId == Login_User_Id
                        select x).FirstOrDefault();
            return user;
        }
        public TblUsers GetUserBYUserId(int UID)
        {
            var user = (from x in _context.TblUsers
                        where x.UserId == UID
                        select x).FirstOrDefault();
            return user;
        }
        public bool IsUIDExist(int ?UID)
        {
            bool isExist = false;
            isExist = _context.TblUsers.Where(x => x.UserId == UID ).Any();
            return isExist;
        }
        public bool ISExist(string LoginUserId)
        {
            bool isExist = false;
            isExist = _context.TblUsers.Where(x => x.LoginUserId.Trim().ToLower() == LoginUserId.Trim().ToLower() && x.Status=="A").Any();
            return isExist;
        }
        public bool ISExistforOTP(string LoginUserId)
        {
            bool isExist = false;
            isExist = _context.TblUsers.Where(x => x.LoginUserId.Trim().ToLower() == LoginUserId.Trim().ToLower() && x.Status == "D").Any();
            return isExist;
        }
        public TblUsers GetUserBYemail(string Email)
        {
            var user = (from x in _context.TblUsers
                        where x.Status=="A" && x.LoginUserId.Trim().ToLower() == Email.Trim().ToLower()
                        select x).SingleOrDefault();
            return user;
        }
        public TblUsers GetUserBYemailForEmailOTP(string Email)
        {
            var user = (from x in _context.TblUsers
                        where x.Status == "D" && x.LoginUserId.Trim().ToLower() == Email.Trim().ToLower()
                        select x).FirstOrDefault();
            return user;
        }
        public bool SaveOTP(int UID, string otp)
        {
            bool issuccess = false;
            TblUsers objData = (from c in _context.TblUsers
                                 where c.UserId == UID
                                select c).FirstOrDefault();
            if (objData != null)
            {
                objData.Otp = Convert.ToString(otp);
                _context.SaveChanges();
                issuccess = true;
            }
            return issuccess;

        }
        public TblUsers GetDeActiveUserBYemail(string Email)
        {
            var user = (from x in _context.TblUsers
                        where x.Status == "D" && x.LoginUserId.Trim().ToLower() == Email.Trim().ToLower()
                        select x).SingleOrDefault();
            return user;
        }
        public bool SetuserSatus(int UID, string Satus)
        {
            bool issuccess = false;
            TblUsers objData = (from c in _context.TblUsers
                                where c.UserId == UID
                                select c).FirstOrDefault();
            if (objData != null)
            {
                objData.Status = Convert.ToString(Satus);
                _context.SaveChanges();
                issuccess = true;
            }
            return issuccess;

        }
        public bool IsEmailOTPExist(string LoginUserId, string otp)
        {
            bool isExist = false;
            isExist = _context.TblUsers.Where(x => x.LoginUserId.Trim().ToLower() == LoginUserId.Trim().ToLower() && x.Otp.Trim().ToLower() == otp.Trim().ToLower()).Any();
            return isExist;
        }
        public bool UpdatePassword(int UID, string Password)
        {
            bool issuccess = false;
            TblUsers objData = (from c in _context.TblUsers
                                where c.UserId == UID
                                select c).FirstOrDefault();
            if (objData != null)
            {
                objData.Password = Password;
                _context.SaveChanges();
                issuccess = true;
            }
            return issuccess;
        }
        public bool UpdateFirstTimeLogin(int UID)
        {
            bool issuccess = false;
            TblUsers objData = (from c in _context.TblUsers
                                where c.UserId == UID
                                select c).FirstOrDefault();
            if (objData != null)
            {
                objData.IsFirstTimeLogin = 2;
                _context.SaveChanges();
                issuccess = true;
            }
            return issuccess;
        }
        public TblOrganization IsEmailDomainExist(string DomainEmailId)
        {

            var Organizationdata = _context.TblOrganization.Where(x => x.DomainEmailId.Contains(DomainEmailId)).FirstOrDefault();
            return Organizationdata;
        }

        public TblOrganizationHierarchy GetUserHierarchy_Id(int OrgId, string HierarchyName)
        {

            var user = (from x in _context.TblOrganizationHierarchy
                        where x.HierarchyName == HierarchyName && x.IdOrganization==OrgId
                        select x).FirstOrDefault();
            return user;
        }

        public TblUsersMappingwithHierarchy GetbyHierarchy_Id(int UID)
        {

            var user = (from x in _context.TblUsersMappingwithHierarchy
                        where x.UserId == UID && x.IsActive=="A"
                        select x).FirstOrDefault();
            return user;
        }
       public ModelUsers AllUserInfo(int UID)
        {
            ModelUsers userModel = new ModelUsers();
            TblUsers userdata =GetUserBYUserId(UID);
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
            TblUsersMappingwithHierarchy OrgHier = GetbyHierarchy_Id(userdata.UserId);
            if (OrgHier != null)
                userModel.IdOrgHierarchy = OrgHier.IdOrgHierarchy;
            return userModel ;
        }
        public bool IsSecurityExist(int UID, int ?IdSecurityQuestion, string SecurityQuestion)
        {
            bool isExist = false;
            isExist = _context.TblUserSecurityQuestionLog.Where(x => x.IdUser==UID && x.SecurityQuestionAns.Trim().ToLower() == SecurityQuestion.Trim().ToLower()&& x.IdSecurityQuestion== IdSecurityQuestion).Any();
            return isExist;
        }

    }
}
