
using Acclimate_Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ILoginRepository : IGenericRepository<TblUsers>
    {
        bool IsSecurityExist(int UID, int ?IdSecurityQuestion, string SecurityQuestion);
        bool IsEmailPassExist(string Login_Id, string Password);
        bool IsCMSEmailPassExist(string Login_User_Id, string Password);
        TblUsers GetUserBYLogin_Id(string Login_User_Id);
        TblUsers GetUserBYUserId(int UID);
        bool UpdateFirstTimeLogin(int UID);
        bool ISExist(string email);
        TblUsers GetUserBYemail(string Email);
        bool SaveOTP(int UID,string otp);
        bool SetuserSatus(int UID, string otp);
         bool ISExistforOTP(string LoginUserId);
        TblUsers GetUserBYemailForEmailOTP(string Email);
        bool IsEmailOTPExist(string LoginUserId, string otp);
        bool UpdatePassword(int UID, string Password);
        TblOrganization IsEmailDomainExist(string DomainEmailId);
        bool IsUIDExist(int ? UID);
        TblUsers GetDeActiveUserBYemail(string Email);
        TblOrganizationHierarchy GetUserHierarchy_Id(int OrgId, string HierarchyName);
        TblUsersMappingwithHierarchy GetbyHierarchy_Id(int UID);
        ModelUsers AllUserInfo(int UID);
    }
}
