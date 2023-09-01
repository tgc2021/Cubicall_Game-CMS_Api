using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Acclimate_Models
{
    public class LoginModel
    {
        public string UserLogin_Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int LoginType { get; set; }
        public int IdOrganization { get; set; }
    }
    public class SignUpWithEmailModel
    {
        public string Email { get; set; }
        public string Name { get; set; }
    }
    public class SignInWithDomainModel
    {
        public string UserLogin_Id { get; set; }
    }
    public class SSOLoginModel
    {
        public string UserLogin_Id { get; set; }
        public int IdOrganization { get; set; }
    }
    public class ConfirmOTPModel
    {
        public string Email { get; set; }
        public string Email_OTP { get; set; }
        public int loginorsignup { get; set; }
    }
    public class SignUpModel
    {
        public string Email { get; set; }
    }
    public class UserRegistrationModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public partial class ModelUsers
    {
        public int UserId { get; set; }
        public int? IdOrganization { get; set; }
        public int? TrainerIdUser { get; set; }
        public int? IdBranch { get; set; }
        public int? IdDepartment { get; set; }
        public int? IdRole { get; set; }
        public int? IdReportingManager { get; set; }
        public string LoginUserId { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string BirthDate { get; set; }
        public string PhoneNo { get; set; }
        public string PermanentStreetAddress1 { get; set; }
        public string PermanentStreetAddress2 { get; set; }
        public string PermanentCity { get; set; }
        public string PermanentState { get; set; }
        public string PermanentPincode { get; set; }
        public string CurrentStreetAddress1 { get; set; }
        public string CurrentStreetAddress2 { get; set; }
        public string CurrentCity { get; set; }
        public string CurrentState { get; set; }
        public string CurrentPincode { get; set; }
        public string AadharNumber { get; set; }
        public string AadharCardImage { get; set; }
        public string PanNumber { get; set; }
        public string PanCardImage { get; set; }
        public string ProfilePicture { get; set; }
        public string EmplyeeId { get; set; }
        public string EmployeeDesignation { get; set; }
        public int? IsFirstTimeLogin { get; set; }
        public string Otp { get; set; }
        public int? LoginType { get; set; }
        public string CountryCode { get; set; }
        public string Status { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public string Email { get; set; }
        public int IdOrgHierarchy { get; set; }
    }
    public class SecurityQuestionModel
    {
        public int? IdSecurityQuestion { get; set; }
        public string SecurityQuestion { get; set; }
        public int UID { get; set; }
    }
    public class NewPasswordModel
    {
        public string Password { get; set; }
        public int UID { get; set; }
    }

    public class UserProfileImgModel
    {
        public string UserProfileImg { get; set; }
        public int UID { get; set; }
    }

    public class ForgetPwdModel
    {
        public int? IdSecurityQuestion { get; set; }
        public string SecurityQuestion { get; set; }
        public string LoginUserId { get; set; }
    }
    public class ChangePasswordModel
    {
        public string Password { get; set; }
        public string LoginUserId { get; set; }
    }
}
