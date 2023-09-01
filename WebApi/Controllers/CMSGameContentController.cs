using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.Json;
using Acclimate_Models;
using DataAccess.EFCore.Data;
using Domain.Entities;
using Domain.Interfaces;
using m2ostnextservice.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/CMSGameContentController")]

    public class CMSGameContentController : ControllerBase
    {
        private readonly db_cubicall_game_devContext DbContext;
        AESAlgorithm ency = new AESAlgorithm();
        private readonly IUnitOfWork Uow;
        string ExcelConString2 = Startup.GetExcelConString2();
        private IWebHostEnvironment _env;
        string hrefURL = Startup.GethrefURL();
        private readonly IWebHostEnvironment webHostEnvironment;
        public CMSGameContentController(IWebHostEnvironment hostEnvironment, IUnitOfWork unitOfWork, db_cubicall_game_devContext context, IWebHostEnvironment env)
        {
            Uow = unitOfWork;
            DbContext = context;
            _env = env;
            webHostEnvironment = hostEnvironment;
        }
        [HttpPost]
        [Route("~/api/ImportQuestionFromExcel")]
        public IActionResult ImportQuestionFromExcel(IFormFile postedFile,int IdOrgHierarchy)
        {

            bool flag = true;
            string responseMessage = string.Empty;
            var files = HttpContext.Request.Form.Files;
            if (files.Count > 0)
            {
                //var postedFile = files[0];

                if (postedFile != null && postedFile.Length > 0 && (Path.GetExtension(postedFile.FileName).ToLower() == ".xlsx" || Path.GetExtension(postedFile.FileName).ToLower() == ".xls"))
                {
                    try
                    {
                        string filePath = string.Empty;
                        string path = Path.Combine(webHostEnvironment.WebRootPath, "UploadExcelFile");

                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        filePath = path + "\\" + Path.GetFileName(postedFile.FileName);
                        string extension = Path.GetExtension(postedFile.FileName);
                        using (FileStream stream = new FileStream(Path.Combine(path, postedFile.FileName), FileMode.Create))
                        {
                            postedFile.CopyTo(stream);
                        }

                        string conString = string.Empty;
                        switch (extension)
                        {
                            case ".xls": //For Excel 97-03.
                                conString = ExcelConString2;
                                break;
                            case ".xlsx": //For Excel 07 and above.
                                conString = ExcelConString2;
                                break;

                        }

                        DataTable dt = new DataTable();
                        conString = string.Format(conString, filePath);
                        StringBuilder sb = new StringBuilder();
                        int count = 0;
                        using (OleDbConnection connExcel = new OleDbConnection(conString))
                        {
                            using (OleDbCommand cmdExcel = new OleDbCommand())
                            {
                                using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                                {
                                    cmdExcel.Connection = connExcel;
                                    connExcel.Open();
                                    DataTable dtExcelSchema;
                                    dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                                    string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                                    connExcel.Close();
                                    connExcel.Open();
                                    cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                                    odaExcel.SelectCommand = cmdExcel;
                                    odaExcel.Fill(dt);
                                    connExcel.Close();

                                    var QuestionList = new List<TblCubesPertilesQuestionDetails>();
                                    foreach (DataRow objDataRow in dt.Rows)
                                    {


                                        if (objDataRow.ItemArray.All(x => string.IsNullOrEmpty(x?.ToString()))) continue;
                                        if (IsQuestionUnique(Convert.ToInt32(objDataRow["QuestionId"]), Convert.ToInt32(objDataRow["CubesFacesGameId"]), Convert.ToInt32(objDataRow["PerTileId"])))
                                        {
                                            sb.Append("," + Convert.ToInt32(objDataRow["QuestionId"]));
                                        }
                                        else
                                        {
                                            TblCubesPertilesQuestionDetails tbdata = new TblCubesPertilesQuestionDetails();
                                            TblCubesQuestionMappingwithHierarchy questionMappingHierarchy = new TblCubesQuestionMappingwithHierarchy();
                                            //QuestionList.Add(new TblCubesPertilesQuestionDetails()
                                            //{
                                            tbdata.QuestionId = Convert.ToInt32(objDataRow["QuestionId"]);
                                            tbdata.CubesFacesGameId = Convert.ToInt32(objDataRow["CubesFacesGameId"]);
                                            tbdata.PerTileId = Convert.ToInt32(objDataRow["PerTileId"]);
                                            tbdata.Question = Convert.ToString(objDataRow["Question"]);
                                            tbdata.QuestionClue = Convert.ToString(objDataRow["QuestionClue"]);
                                            tbdata.QuestionSet = Convert.ToInt32(objDataRow["QuestionSet"]);
                                            tbdata.IdOrganization = Convert.ToInt32(objDataRow["IdOrganization"]);
                                            tbdata.IsApproved = Convert.ToInt32(objDataRow["IsApproved"]);
                                            tbdata.IsDraft = Convert.ToInt32(objDataRow["IsDraft"]);
                                            tbdata.IsActive = Convert.ToString(objDataRow["IsActive"]);
                                            tbdata.UpdatedDateTime = DateTime.UtcNow;
                                            DbContext.TblCubesPertilesQuestionDetails.Add(tbdata);
                                            DbContext.SaveChanges();

                                            questionMappingHierarchy.IdOrgHierarchy = tbdata.QuestionId;
                                            questionMappingHierarchy.IdOrgHierarchy = IdOrgHierarchy;
                                            questionMappingHierarchy.IsActive = "A";
                                            questionMappingHierarchy.UpdatedDateTime = DateTime.UtcNow;
                                            DbContext.TblCubesQuestionMappingwithHierarchy.Add(questionMappingHierarchy);
                                            DbContext.SaveChanges();

                                            count++;
                                            //});

                                        }


                                    }
                                    
                                }
                            }
                        }

                        flag = true;
                        responseMessage = "Total Successful Upload Question Coun:" + count + " Dublicate Question ID: " + sb;
                        return Ok(responseMessage);
                    }
                    catch (Exception ex)
                    {
                        flag = false;
                        responseMessage = "Upload Failed with error: " + ex.Message;
                        return BadRequest(responseMessage);
                    }
                }
                else
                {
                    flag = false;
                    responseMessage = "Please upload file having extensions .xls and .xlsx  only.";
                    return BadRequest(responseMessage);
                }
            }
            else
            {
                flag = false;
                responseMessage = "File Upload has no file.";
                return BadRequest(responseMessage);
            }

            return Ok( responseMessage );
        }

        [HttpPost]
        [Route("~/api/ImportAnswerFromExcel")]
        public IActionResult ImportAnswerFromExcel(IFormFile postedFile)
        {
            bool flag = true;
            string responseMessage = string.Empty;
            var files = HttpContext.Request.Form.Files;
            if (files.Count > 0)
            {
                //var postedFile = files[0];

                if (postedFile != null && postedFile.Length > 0 && (Path.GetExtension(postedFile.FileName).ToLower() == ".xlsx" || Path.GetExtension(postedFile.FileName).ToLower() == ".xls"))
                {
                    try
                    {
                        string filePath = string.Empty;
                        string path = Path.Combine(webHostEnvironment.WebRootPath, "UploadExcelFile");

                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        filePath = path + "\\" + Path.GetFileName(postedFile.FileName);
                        string extension = Path.GetExtension(postedFile.FileName);
                        using (FileStream stream = new FileStream(Path.Combine(path, postedFile.FileName), FileMode.Create))
                        {
                            postedFile.CopyTo(stream);
                        }

                        string conString = string.Empty;
                        switch (extension)
                        {
                            case ".xls": //For Excel 97-03.
                                conString = ExcelConString2;
                                break;
                            case ".xlsx": //For Excel 07 and above.
                                conString = ExcelConString2;
                                break;

                        }

                        DataTable dt = new DataTable();
                        conString = string.Format(conString, filePath);
                        StringBuilder sb = new StringBuilder();
                        int count = 0;
                        using (OleDbConnection connExcel = new OleDbConnection(conString))
                        {
                            using (OleDbCommand cmdExcel = new OleDbCommand())
                            {
                                using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                                {
                                    cmdExcel.Connection = connExcel;
                                    connExcel.Open();
                                    DataTable dtExcelSchema;
                                    dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                                    string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                                    connExcel.Close();
                                    connExcel.Open();
                                    cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                                    odaExcel.SelectCommand = cmdExcel;
                                    odaExcel.Fill(dt);
                                    connExcel.Close();

                                    var ansList = new List<TblCubesPertilesAnswerDetails>();
                                    foreach (DataRow objDataRow in dt.Rows)
                                    {


                                        //if (objDataRow.ItemArray.All(x => string.IsNullOrEmpty(x?.ToString()))) continue;
                                        //if (IsQuestionUnique(Convert.ToInt32(objDataRow["QuestionId"]), Convert.ToInt32(objDataRow["CubesFacesGameId"]), Convert.ToInt32(objDataRow["PerTileId"])))
                                        //{
                                        //    sb.Append("," + Convert.ToInt32(objDataRow["QuestionId"]));
                                        //}
                                        //else
                                        //{
                                        TblCubesPertilesAnswerDetails tbdata = new TblCubesPertilesAnswerDetails();
                                        //QuestionList.Add(new TblCubesPertilesQuestionDetails()
                                        //{
                                        tbdata.QuestionId = Convert.ToInt32(objDataRow["QuestionId"]);
                                        tbdata.CubesFacesGameId = Convert.ToInt32(objDataRow["CubesFacesGameId"]);
                                        tbdata.PerTileId = Convert.ToInt32(objDataRow["PerTileId"]);
                                        tbdata.Answer = Convert.ToString(objDataRow["Answer"]);
                                        tbdata.IsRightAns = Convert.ToInt32(objDataRow["IsRightAns"]);
                                        tbdata.IdOrganization = Convert.ToInt32(objDataRow["IdOrganization"]);
                                        tbdata.IsActive = Convert.ToString(objDataRow["IsActive"]);
                                        tbdata.UpdatedDateTime = DateTime.UtcNow;
                                        DbContext.TblCubesPertilesAnswerDetails.Add(tbdata);
                                        DbContext.SaveChanges();
                                        count++;
                                        //});

                                        //}

                                    }

                                }
                            }
                        }

                        flag = true;
                        responseMessage = "Total Successful Upload Question Coun:" + count + " Dublicate Question ID: " + sb;
                        return Ok(responseMessage);
                    }
                    catch (Exception ex)
                    {
                        flag = false;
                        responseMessage = "Upload Failed with error: " + ex.Message;
                        return BadRequest(responseMessage);
                    }
                }
                else
                {
                    flag = false;
                    responseMessage = "Please upload file having extensions .xls and .xlsx  only.";
                    return BadRequest(responseMessage);
                }
            }
            else
            {
                flag = false;
                responseMessage = "File Upload has no file.";
                return BadRequest(responseMessage);
            }

        }

        public bool IsQuestionUnique(int QuestionId, int CubesFacesGameId, int PerTileId)
        {
            bool isExist = false;
            bool isQ = false;
            isQ = DbContext.TblCubesPertilesQuestionDetails.Where(x => x.QuestionId == QuestionId).Any();
            if (isQ)
            {
                isExist = true;
            }
            else
            {

                isExist = DbContext.TblCubesPertilesQuestionDetails.Where(x => x.QuestionId == QuestionId && x.CubesFacesGameId == CubesFacesGameId && x.PerTileId == PerTileId).Any();

            }
            return isExist;
        }

        [Route("~/api/GetAllQuestionList")]
        [HttpGet]
        public IActionResult GetAllQuestionList(int OrgId,int CubesFacesGameId)
        {
            try
            {
                List<TblCubesPertilesQuestionDetails> QuestionDetails = DbContext.TblCubesPertilesQuestionDetails
                   .Where(x => x.IdOrganization == OrgId && x.CubesFacesGameId == CubesFacesGameId).ToList();

                return Ok(QuestionDetails);
            }
            catch (Exception)
            {

                return Conflict("Error in Code");
            }
        }
        [Route("~/api/GetQuestionByIdWithAns")]
        [HttpGet]
        public IActionResult GetQuestionByIdWithAns(int QuestionId)
        {
            try
            {
                QuestionAnsListwerModel Data = new QuestionAnsListwerModel();
                TblCubesPertilesQuestionDetails QuestionDetails = DbContext.TblCubesPertilesQuestionDetails
                   .Where(x => x.QuestionId == QuestionId ).FirstOrDefault();
                List<TblCubesPertilesAnswerDetails> AnsDetails = DbContext.TblCubesPertilesAnswerDetails
                   .Where(x => x.QuestionId == QuestionId).ToList();

                Data.QuestionList = QuestionDetails;
                Data.AnsList = AnsDetails;
                return Ok(Data);
            }
            catch (Exception)
            {

                return Conflict("Error in Code");
            }
        }

        [Route("~/api/EditQuestionAns")]
        [HttpPost]
        public IActionResult EditQuestionAns([FromBody] APIRequestModel ApiRequestdata)
        {
            try
            {
                List<TblCubesPertilesAnswerDetails> ansList = new List<TblCubesPertilesAnswerDetails>();
                TblCubesPertilesQuestionDetails Question = new TblCubesPertilesQuestionDetails();
                QuestionAnsListwerModel ReqData = new QuestionAnsListwerModel();
                //string Requestdata = ency.getDecryptedString(ApiRequestdata.Data);
                ReqData = JsonSerializer.Deserialize<QuestionAnsListwerModel>(ApiRequestdata.Data);


                Question = ReqData.QuestionList;
                ansList = ReqData.AnsList;

                if (Question.PerTileQuestionId > 0)
                {
                    TblCubesPertilesQuestionDetails objData = (from c in DbContext.TblCubesPertilesQuestionDetails
                                                               where c.PerTileQuestionId == Question.PerTileQuestionId
                                                               select c).FirstOrDefault();
                    if (objData != null)
                    {
                        objData.CubesFacesGameId = Question.CubesFacesGameId;
                        objData.PerTileId = Question.PerTileId;
                        objData.Question = Question.Question;
                        objData.Complexity = Question.Complexity;
                        objData.QuestionClue = Question.QuestionClue;
                        objData.QuestionSet = Question.QuestionSet;
                        objData.IsApproved = Question.IsApproved;
                        objData.IsDraft = Question.IsDraft;
                        objData.IdOrganization = Question.IdOrganization;
                        objData.IsActive = Question.IsActive;
                        objData.UpdatedDateTime = DateTime.UtcNow;
                        DbContext.SaveChanges();
                    }
                }
                foreach (var item in ansList)
                {

                    TblCubesPertilesAnswerDetails objData = (from c in DbContext.TblCubesPertilesAnswerDetails
                                                             where c.PerTileAnswerId == item.PerTileAnswerId
                                                               select c).FirstOrDefault();
                    if (objData != null)
                    {
                        objData.CubesFacesGameId = item.CubesFacesGameId;
                        objData.QuestionId = item.QuestionId;
                        objData.PerTileId = item.PerTileId;
                        objData.Answer = item.Answer;
                        objData.IsRightAns = item.IsRightAns;
                        objData.IdOrganization = item.IdOrganization;
                        objData.IsActive = item.IsActive;
                        objData.UpdatedDateTime = DateTime.UtcNow;
                        DbContext.SaveChanges();
                     
                    }
                }
                return Ok("Success");
            }
            catch (Exception)
            {

                return Conflict("Error in Code");
            }
        }
        [Route("~/api/GetCMSRoleFunctionList")]
        [HttpGet]
        public IActionResult GetCMSRoleFunctionList(int OrgId)
        {
            try
            {
                List<TblCmsRoleFunction> RoleFunctionList = DbContext.TblCmsRoleFunction
                   .Where(x => x.IdOrganization==OrgId&&x.IsActive == "A").ToList();

                return Ok(RoleFunctionList);
            }
            catch (Exception)
            {

                return Conflict("Error in Code");
            }
        }

        [Route("~/api/GetOrganizationTypeList")]
        [HttpGet]
        public IActionResult GetOrganizationTypeList()
        {
            try
            {
                List<TblOrganizationtype> OrganizationtypeList = DbContext.TblOrganizationtype
                   .Where(x => x.IsActive == "A").ToList();

                return Ok(OrganizationtypeList);
            }
            catch (Exception)
            {

                return Conflict("Error in Code");
            }
        }

    }
}