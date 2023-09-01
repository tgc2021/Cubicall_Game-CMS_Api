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
    [Route("api/CubicallGameSetUPController")]

    public class CubicallGameSetUPController : ControllerBase
    {
        private readonly db_cubicall_game_devContext DbContext;
        AESAlgorithm ency = new AESAlgorithm();
        private readonly IUnitOfWork Uow;
        Crossword _board = new Crossword(18, 19);
        Random _rand = new Random();
        public CubicallGameSetUPController(IUnitOfWork unitOfWork, db_cubicall_game_devContext context)
        {
            Uow = unitOfWork;
            DbContext = context;
        }

        [Route("~/api/GetQuestionsAnsList")]
        [HttpGet]
        public IActionResult GetQuestionsAnsList(int OrgID, int CubesFacesGameId = 1, int UID = 0)
        {
            try
            {
                int UIDH = Uow.UserHierarchy.GetUserIdOrgHierarchy(UID, OrgID);
                List<int> ContentIds = new List<int>();
                List<int> LastAttemptedQIds = new List<int>();
                List<int> WrongAnsByUserQIds = new List<int>();
                if (UIDH != 0 && UIDH > 0)
                {
                    int maxUserPalyCount = DbContext.TblCubesGameMasterUserLog
                    .Where(x => x.IdUser == UID && x.CubesFacesGameId == CubesFacesGameId)
                    .OrderByDescending(y => y.UserPlayCount).Select(v => v.UserPlayCount).Take(1).FirstOrDefault();

                    LastAttemptedQIds = DbContext.TblCubesGameDetailsUserLog
                 .Where(x => x.IdUser == UID && x.CubesFacesGameId == CubesFacesGameId
                 && x.UserPlayCount == maxUserPalyCount)
                 .Select(v => v.QuestionId).ToList();

                    var Content = (from t1 in DbContext.TblCubesPertilesQuestionDetails
                                   join t2 in DbContext.TblCubesQuestionMappingwithHierarchy
                                   on t1.QuestionId equals t2.QuestionId
                                   where t1.CubesFacesGameId == CubesFacesGameId
                                   && t1.IdOrganization == OrgID
                                   && t1.IsActive == "A"
                                   && t2.IdOrgHierarchy == UIDH
                                   select new { t1.QuestionId }).ToList();
                    if (Content.Count != 0 && Content.Count > 0)
                    {
                        ContentIds = Content.Select(q => q.QuestionId).ToList();

                        int AttemptNo = 0;
                        List<QuestionAnswerModel> QuestionAnswer = new List<QuestionAnswerModel>();
                        QuestionAnswerGame6Model QuestionAnswergame6 = new QuestionAnswerGame6Model();

                        if (CubesFacesGameId == 6)
                        {
                            if (LastAttemptedQIds.Count == 16 || LastAttemptedQIds.Count == 0)
                            {
                                List<TblCubesPertilesQuestionDetails> QuestionList = this.DbContext.TblCubesPertilesQuestionDetails.Where(x => ContentIds.Contains(x.QuestionId) && !LastAttemptedQIds.Contains(x.QuestionId) && x.IdOrganization == OrgID && x.CubesFacesGameId == CubesFacesGameId && x.IsActive == "A").DistinctBy(t => t.PerTileId).ToList();
                                // QuestionList = QuestionList.DistinctBy(t => t.PerTileId).ToList();

                                QuestionList.Shuffle();
                                QuestionList = QuestionList.GroupBy(p => p.PerTileId).Select(x => x.First()).Take(16).ToList();
                                List<string> _words = new List<string>();
                                List<TblCubesPertilesAnswerDetails> anslist = new List<TblCubesPertilesAnswerDetails>();

                                List<string> horizontalWordsListView = new List<string>();
                                List<int> questionIds = new List<int>();

                                char[,] crosswordGrid = new char[18, 19];
                                questionIds = QuestionList.Select(a => a.QuestionId).ToList();
                                if (questionIds != null && questionIds.Count > 0)
                                {
                                    anslist = this.DbContext.TblCubesPertilesAnswerDetails.Where(x => questionIds.Contains(x.QuestionId) && x.CubesFacesGameId == 6 && x.IsActive == "A" && x.IdOrganization == OrgID && x.IsRightAns == 1).ToList();

                                    if (anslist != null && anslist.Count > 0)
                                    {
                                        foreach (var ans in anslist)
                                        {
                                            _words.Add(ReplaceWhitespace(ans.Answer, ""));
                                        }
                                    }
                                }

                                if (QuestionList != null && QuestionList.Count > 0)
                                {
                                    CrossGridModel ModelData = GenCrossword(_words, anslist, ref AttemptNo, OrgID);
                                    if (ModelData != null)
                                    {
                                        crosswordGrid = ModelData.grid;
                                        QuestionAnswergame6.Crossword = ModelData.CrossGridData;
                                    }
                                }
                                List<TblCubesGridrowcolLog> GridrowcolLog = new List<TblCubesGridrowcolLog>();

                                string[] words = _words.ToArray();
                                for (int i = 0; i < words.Length; i++)
                                {
                                    string word = words[i];
                                    for (int row = 0; row < crosswordGrid.GetLength(0); row++)
                                    {
                                        for (int col = 0; col < crosswordGrid.GetLength(1); col++)
                                        {
                                            if (crosswordGrid[row, col] == word[0])
                                            {
                                                bool foundWord = true;
                                                // Check if word fits across
                                                if (col + word.Length <= crosswordGrid.GetLength(1))
                                                {
                                                    for (int j = 1; j < word.Length; j++)
                                                    {
                                                        if (crosswordGrid[row, col + j] != word[j])
                                                        {
                                                            foundWord = false;
                                                            break;
                                                        }
                                                    }

                                                    if (foundWord)
                                                    {
                                                        Console.WriteLine("Across: {0} ", word);
                                                        var dataobj = anslist.Where(x => x.Answer.ToLower().Equals(word.ToLower())).FirstOrDefault();

                                                        if (dataobj != null)
                                                        {

                                                            TblCubesGridrowcolLog tbldata = new TblCubesGridrowcolLog();
                                                            tbldata.UpdatedDateTime = DateTime.UtcNow;
                                                            tbldata.IdOrganization = OrgID;
                                                            tbldata.IdUser = UID;
                                                            tbldata.RowNo = row;
                                                            tbldata.ColumnNo = col;
                                                            tbldata.KeyNo = AttemptNo;
                                                            tbldata.QuestionId = dataobj.QuestionId;
                                                            tbldata.Direction = Convert.ToString("across");
                                                            DbContext.TblCubesGridrowcolLog.Add(tbldata);
                                                            DbContext.SaveChanges();

                                                            GridrowcolLog.Add(new TblCubesGridrowcolLog()
                                                            {
                                                                QuestionId = dataobj.QuestionId,
                                                                RowNo = row,
                                                                ColumnNo = col,
                                                                Direction = Convert.ToString("across"),
                                                                IdOrganization = OrgID,

                                                            });
                                                        }
                                                        break;
                                                    }
                                                }

                                                foundWord = true;
                                                // Check if word fits down
                                                if (row + word.Length <= crosswordGrid.GetLength(0))
                                                {
                                                    for (int j = 1; j < word.Length; j++)
                                                    {
                                                        if (crosswordGrid[row + j, col] != word[j])
                                                        {
                                                            foundWord = false;
                                                            break;
                                                        }
                                                    }

                                                    if (foundWord)
                                                    {
                                                        Console.WriteLine("Down: {0}", word);
                                                        var dataobj = anslist.Where(x => x.Answer.ToLower().Equals(word.ToLower())).FirstOrDefault();

                                                        if (dataobj != null)
                                                        {

                                                            TblCubesGridrowcolLog tbldata = new TblCubesGridrowcolLog();
                                                            tbldata.UpdatedDateTime = DateTime.UtcNow;
                                                            tbldata.IdOrganization = OrgID;
                                                            tbldata.IdUser = UID;
                                                            tbldata.RowNo = row;
                                                            tbldata.ColumnNo = col;
                                                            tbldata.KeyNo = AttemptNo;
                                                            tbldata.QuestionId = dataobj.QuestionId;
                                                            tbldata.Direction = Convert.ToString("down");
                                                            DbContext.TblCubesGridrowcolLog.Add(tbldata);
                                                            DbContext.SaveChanges();
                                                            GridrowcolLog.Add(new TblCubesGridrowcolLog()
                                                            {
                                                                QuestionId = dataobj.QuestionId,
                                                                RowNo = row,
                                                                ColumnNo = col,
                                                                Direction = Convert.ToString("down"),
                                                                IdOrganization = OrgID,

                                                            });
                                                        }
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                foreach (var item in QuestionList)
                                {

                                    var anslistdata = this.DbContext.TblCubesPertilesAnswerDetails.Where(x => x.QuestionId == item.QuestionId && x.CubesFacesGameId == item.CubesFacesGameId && x.PerTileId == item.PerTileId && x.IsActive == "A" && x.IdOrganization == OrgID).ToList();
                                    var rowcoldirection = GridrowcolLog.Where(c => c.QuestionId == item.QuestionId).FirstOrDefault();
                                    QuestionAnswer.Add(new QuestionAnswerModel()
                                    {
                                        PerTileQuestionId = item.PerTileQuestionId,
                                        QuestionId = item.QuestionId,
                                        CubesFacesGameId = item.CubesFacesGameId,
                                        PerTileId = item.PerTileId,
                                        Question = item.Question,
                                        //MixedWords = item.MixedWords,
                                        QuestionClue = item.QuestionClue,
                                        Row = rowcoldirection.RowNo,
                                        Column = rowcoldirection.ColumnNo,
                                        Direction = rowcoldirection.Direction,
                                        optionList = anslistdata

                                    });
                                }
                                QuestionAnswergame6.QuestionAnswer = QuestionAnswer;
                            }
                            else if (LastAttemptedQIds.Count >= 1)
                            {
                                List<TblCubesGridrowcolLog> rowcolumlog = DbContext.TblCubesGridrowcolLog
                    .Where(x => x.IdUser == UID).OrderByDescending(y => y.KeyNo).Take(16).ToList();
                                int keyno = 0;
                                if (rowcolumlog.Count > 0 && rowcolumlog != null)
                                    keyno = rowcolumlog[1].KeyNo;

                                List<GridData> gride = new List<GridData>();
                                List<string> Crossgride = DbContext.TblCubesCrosswordGridDetails
                                    .Where(x => x.KeyNo == keyno).Select(g => g.Grid).ToList();

                                List<int> gridedataQids = DbContext.TblCubesGridrowcolLog.Where(y => y.IdUser == UID).OrderByDescending(y => y.KeyNo).Select(q => q.QuestionId).Take(16).ToList();
                                if (gridedataQids != null && gridedataQids.Count > 0)
                                {
                                    if (Crossgride != null && Crossgride.Count > 0)
                                    {
                                        foreach (var item in Crossgride)
                                        {
                                            gride.Add(new GridData()
                                            {
                                                grid = item
                                            });
                                        }
                                    }

                                    QuestionAnswergame6.Crossword = gride;
                                    List<TblCubesPertilesQuestionDetails> QuestionList = this.DbContext.TblCubesPertilesQuestionDetails.Where(x => gridedataQids.Contains(x.QuestionId) && x.IdOrganization == OrgID && x.CubesFacesGameId == CubesFacesGameId && x.IsActive == "A").DistinctBy(t => t.PerTileId).ToList();
                                    foreach (var item in QuestionList)
                                    {

                                        var anslistdata = this.DbContext.TblCubesPertilesAnswerDetails.Where(x => x.QuestionId == item.QuestionId && x.CubesFacesGameId == item.CubesFacesGameId && x.PerTileId == item.PerTileId && x.IsActive == "A" && x.IdOrganization == OrgID).ToList();
                                        var rowcoldirection = rowcolumlog.Where(c => c.QuestionId == item.QuestionId).FirstOrDefault();
                                        QuestionAnswer.Add(new QuestionAnswerModel()
                                        {
                                            PerTileQuestionId = item.PerTileQuestionId,
                                            QuestionId = item.QuestionId,
                                            CubesFacesGameId = item.CubesFacesGameId,
                                            PerTileId = item.PerTileId,
                                            Question = item.Question,
                                            //MixedWords = item.MixedWords,
                                            QuestionClue = item.QuestionClue,
                                            Row = rowcoldirection.RowNo,
                                            Column = rowcoldirection.ColumnNo,
                                            Direction = rowcoldirection.Direction,
                                            optionList = anslistdata

                                        });
                                    }
                                    QuestionAnswergame6.QuestionAnswer = QuestionAnswer;

                                }
                            }
                            else { return NotFound("No Data Available"); }
                        }
                        else if (CubesFacesGameId == 2)
                        {
                            if (LastAttemptedQIds.Count == 16 || LastAttemptedQIds.Count == 0)
                            {
                                List<int> SequenceForFace2 = new List<int>();
                                SequenceForFace2 = this.DbContext.TblCubesPertilesQuestionDetails.Where(x => ContentIds.Contains(x.QuestionId) && !LastAttemptedQIds.Contains(x.QuestionId) && x.IdOrganization == OrgID && x.CubesFacesGameId == 2 && x.IsActive == "A").Select(c => c.QuestionSet).ToList();
                                SequenceForFace2.Shuffle();
                                int SetNo = Convert.ToInt32(SequenceForFace2[0]);
                                List<TblCubesPertilesQuestionDetails> QuestionList = this.DbContext.TblCubesPertilesQuestionDetails.Where(x => x.IdOrganization == OrgID && x.CubesFacesGameId == 2 && x.IsActive == "A" && x.QuestionSet == SetNo).DistinctBy(t => t.PerTileId).ToList();
                                // QuestionList = QuestionList.DistinctBy(t => t.PerTileId).ToList();

                                QuestionList = QuestionList.GroupBy(p => p.PerTileId).Select(x => x.First()).Take(16).ToList();
                                foreach (var item in QuestionList.Take(16))
                                {
                                    var anslist = this.DbContext.TblCubesPertilesAnswerDetails.Where(x => x.QuestionId == item.QuestionId && x.CubesFacesGameId == item.CubesFacesGameId && x.PerTileId == item.PerTileId && x.IsActive == "A" && x.IdOrganization == OrgID).ToList();

                                    QuestionAnswer.Add(new QuestionAnswerModel()
                                    {
                                        PerTileQuestionId = item.PerTileQuestionId,
                                        QuestionId = item.QuestionId,
                                        CubesFacesGameId = item.CubesFacesGameId,
                                        PerTileId = item.PerTileId,
                                        Question = item.Question,
                                        //MixedWords = item.MixedWords,
                                        QuestionClue = item.QuestionClue,
                                        Row = item.RowNo,
                                        Column = item.ColumnNo,
                                        Direction = item.Direction,
                                        optionList = anslist

                                    });
                                }
                            }
                            else if (LastAttemptedQIds.Count >= 1)
                            {
                                int SetNo = DbContext.TblCubesPertilesQuestionDetails
                                .Where(x => LastAttemptedQIds.Contains(x.QuestionId) && x.CubesFacesGameId == 2)
                                .Select(v => v.QuestionSet).FirstOrDefault();
                                List<TblCubesPertilesQuestionDetails> QuestionList = this.DbContext.TblCubesPertilesQuestionDetails.Where(x => x.IdOrganization == OrgID && x.CubesFacesGameId == 2 && x.IsActive == "A" && x.QuestionSet == SetNo).DistinctBy(t => t.PerTileId).ToList();
                                // QuestionList = QuestionList.DistinctBy(t => t.PerTileId).ToList();

                                QuestionList = QuestionList.GroupBy(p => p.PerTileId).Select(x => x.First()).Take(16).ToList();
                                foreach (var item in QuestionList.Take(16))
                                {
                                    var anslist = this.DbContext.TblCubesPertilesAnswerDetails.Where(x => x.QuestionId == item.QuestionId && x.CubesFacesGameId == item.CubesFacesGameId && x.PerTileId == item.PerTileId && x.IsActive == "A" && x.IdOrganization == OrgID).ToList();

                                    QuestionAnswer.Add(new QuestionAnswerModel()
                                    {
                                        PerTileQuestionId = item.PerTileQuestionId,
                                        QuestionId = item.QuestionId,
                                        CubesFacesGameId = item.CubesFacesGameId,
                                        PerTileId = item.PerTileId,
                                        Question = item.Question,
                                        //MixedWords = item.MixedWords,
                                        QuestionClue = item.QuestionClue,
                                        Row = item.RowNo,
                                        Column = item.ColumnNo,
                                        Direction = item.Direction,
                                        optionList = anslist

                                    });
                                }
                            }
                            else { return NotFound("No Data Available"); }
                        }
                        else
                        {
                            List<TblCubesPertilesQuestionDetails> QuestionList = this.DbContext.TblCubesPertilesQuestionDetails.Where(x => ContentIds.Contains(x.QuestionId) && !LastAttemptedQIds.Contains(x.QuestionId) && x.IdOrganization == OrgID && x.CubesFacesGameId == CubesFacesGameId && x.IsActive == "A").DistinctBy(t => t.PerTileId).ToList();
                            // QuestionList = QuestionList.DistinctBy(t => t.PerTileId).ToList();
                            QuestionList.Shuffle();
                            QuestionList = QuestionList.GroupBy(p => p.PerTileId).Select(x => x.First()).Take(16).ToList();
                            foreach (var item in QuestionList.Take(16))
                            {
                                var anslist = this.DbContext.TblCubesPertilesAnswerDetails.Where(x => x.QuestionId == item.QuestionId && x.CubesFacesGameId == item.CubesFacesGameId && x.PerTileId == item.PerTileId && x.IsActive == "A" && x.IdOrganization == OrgID).ToList();

                                QuestionAnswer.Add(new QuestionAnswerModel()
                                {
                                    PerTileQuestionId = item.PerTileQuestionId,
                                    QuestionId = item.QuestionId,
                                    CubesFacesGameId = item.CubesFacesGameId,
                                    PerTileId = item.PerTileId,
                                    Question = item.Question,
                                    //MixedWords = item.MixedWords,
                                    QuestionClue = item.QuestionClue,
                                    Row = item.RowNo,
                                    Column = item.ColumnNo,
                                    Direction = item.Direction,
                                    optionList = anslist

                                });
                            }
                        }
                        if (CubesFacesGameId == 6)
                        {
                            if (QuestionAnswergame6 != null)
                            {
                                return Ok(QuestionAnswergame6);
                            }
                            else { return NotFound("No Data Available"); }
                        }
                        else
                        {
                            if (QuestionAnswer != null)
                            {
                                if (LastAttemptedQIds.Count == 16)
                                { return Ok(QuestionAnswer); }
                                else
                                {
                                    int count = 0;
                                    foreach (var item in LastAttemptedQIds)
                                    {
                                        QuestionAnswer.RemoveAt(count);
                                        count++;
                                    }


                                    return Ok(QuestionAnswer);
                                }

                            }
                            else { return NotFound("No Data Available"); }
                        }
                    }
                    else
                    {
                        return Conflict("Contet not assign to user");
                    }
                }
                else
                {
                    return Conflict("User Hierarchy not set");
                }
            }
            catch (Exception ex)
            {
                return Conflict("Error in Code");
            }

        }

        [Route("~/api/GetCrossWordGridData")]
        [HttpGet]
        public IActionResult GetCrossWordGridData(int OrgID)
        {
            try
            {
                List<TblCubesCrosswordGridDetails> Crossword = this.DbContext.TblCubesCrosswordGridDetails.Where(x => x.IdOrganization == OrgID).ToList();

                if (Crossword != null)
                {

                    //string Data = JsonSerializer.Serialize(GameDataList);
                    //string Result = ency.getEncryptedString(Data);
                    return Ok(Crossword);
                }
                else { return NotFound("No Data Available"); }

            }
            catch (Exception ex)
            {
                return Conflict("Error in Code");
            }

        }
        [Route("~/api/GetCubicallGameSetUpList")]
        [HttpGet]
        public IActionResult GetCubicallGameSetUpList(int OrgID, int UID)
        {
            try
            {
                int UIDH = Uow.UserHierarchy.GetUserIdOrgHierarchy(UID, OrgID);
                List<int> AttemptMapIds = new List<int>();
                List<int> CubesFacesMapIds = new List<int>();
                if (UIDH != 0 && UIDH > 0)
                {
                    var Content = (from t1 in DbContext.TblFacesAttemptnoMappingwithHierarchy
                                   join t2 in DbContext.TblCubesFacesAttemptnoDetails
                                   on t1.AttemptNoMapId equals t2.AttemptNoMapId
                                   where t1.IdOrganization == OrgID
                                   && t2.IsActive == "A"
                                   && t1.IsActive == "A"
                                   && t1.IdOrgHierarchy == UIDH
                                   select new { t1.AttemptNoMapId }).ToList();


                    if (Content.Count != 0 && Content.Count > 0)
                    {
                        AttemptMapIds = Content.Select(q => q.AttemptNoMapId).ToList();
                    }
                    var CubesFaceContent = (from t1 in DbContext.TblCubesFacesGameMappingwithHierarchy
                                            join t2 in DbContext.TblCubesFacesGameDetails
                                            on t1.CubesFacesMapId equals t2.CubesFacesMapId
                                            where t1.IdOrganization == OrgID
                                            && t2.IsActive == "A"
                                            && t1.IsActive == "A"
                                            && t1.IdOrgHierarchy == UIDH
                                            select new { t1.CubesFacesMapId }).ToList();


                    if (CubesFaceContent.Count != 0 && CubesFaceContent.Count > 0)
                    {
                        CubesFacesMapIds = CubesFaceContent.Select(q => q.CubesFacesMapId).ToList();
                    }
                }
                List<QuestionAnswerModel> QuestionAnswer = new List<QuestionAnswerModel>();
                CubicallGameSetUPModel GameDataList = new CubicallGameSetUPModel();
                GameDataList.GameMasterList = this.DbContext.TblGameMaster.Where(x => x.IdOrganization == OrgID && x.IsActive == "A").ToList();
                GameDataList.CubesFacesGameDetailsList = this.DbContext.TblCubesFacesGameDetails.Where(x => CubesFacesMapIds.Contains(x.CubesFacesMapId) && x.IdOrganization == OrgID && x.IsActive == "A").ToList();
                //var QuestionList = this.DbContext.TblCubesPertilesQuestionDetails.Where(x => x.IdOrganization == OrgID && x.IsActive == "A").ToList();
                // GameDataList.CubesPertilesAnswerDetailsList = this.DbContext.TblCubesPertilesAnswerDetails.Where(x => x.IdOrganization == OrgID && x.IsActive == "A").ToList();
                GameDataList.CubesFaceBadgeMasterList = this.DbContext.TblCubesFaceBadgeMaster.Where(x => x.IdOrganization == OrgID && x.IsActive == "A").ToList();
                GameDataList.AvatarList = this.DbContext.TblAvatar.Where(x => x.IdOrganization == OrgID).ToList();
                // GameDataList.Crossword=this.DbContext.TblCubesCrosswordGridDetails.Where(x => x.IdOrganization == OrgID).ToList();
                GameDataList.CubesFacesColourDetailsList = this.DbContext.TblCubesFacesColourDetails.Where(x => x.IdOrganization == OrgID && x.IsActive == "A").ToList();
                GameDataList.ServiceLevelBandsDetailsrList = this.DbContext.TblServiceLevelBandsDetails.Where(x => x.IdOrganization == OrgID && x.IsActive == "A").ToList();
                GameDataList.ServiceLevelScoringDetailsList = this.DbContext.TblServiceLevelScoringDetails.Where(x => x.IdOrganization == OrgID && x.IsActive == "A").ToList();
                GameDataList.ServiceLevelStreakScoringDetailsList = this.DbContext.TblServiceLevelStreakScoringDetails.Where(x => x.IdOrganization == OrgID && x.IsActive == "A").ToList();
                GameDataList.CubesFacesAttemptnoDetailsList = this.DbContext.TblCubesFacesAttemptnoDetails.Where(x => AttemptMapIds.Contains(x.AttemptNoMapId) && x.IdOrganization == OrgID && x.IsActive == "A").ToList();
                if (GameDataList != null)
                {

                    string Data = JsonSerializer.Serialize(GameDataList);
                    //string Result = ency.getEncryptedString(Data);
                    return Ok(GameDataList);
                }
                else { return NotFound("No Data Available"); }

            }
            catch (Exception ex)
            {
                return Conflict("Error in Code");
            }

        }
        [Route("~/api/GetUserPlayCount")]
        [HttpGet]
        public IActionResult GetUserPlayCount(int UID, int CubesFacesGameId)
        {
            try
            {

                int User_Play_Count = 1;
                var tbldata = (from l in DbContext.TblCubesGameMasterUserLog
                               where l.IdUser == UID && l.CubesFacesGameId == CubesFacesGameId
                               select l).OrderByDescending(x => x.UserPlayCount).FirstOrDefault();
                if (tbldata != null)
                    User_Play_Count = tbldata.UserPlayCount + 1;


                //string Data = JsonSerializer.Serialize(User_Play_Count);
                //string Result = ency.getEncryptedString(Data);
                return Ok(User_Play_Count);
            }
            catch (System.Exception ex)
            {

                return Conflict("Error in Code");
            }
        }

        [Route("~/api/GetUserGameDetailLog")]
        [HttpGet]
        public IActionResult GetUserGameDetailLog(int UID, int CubesFacesGameId)
        {
            try
            {
                var tbldata = (from l in DbContext.TblCubesGameDetailsUserLog
                               where l.IdUser == UID && l.CubesFacesGameId == CubesFacesGameId
                               select l).ToList();
                //string Data = JsonSerializer.Serialize(tbldata);
                //string Result = ency.getEncryptedString(Data);
                return Ok(tbldata);
            }
            catch (System.Exception ex)
            {

                return Conflict("Error in Code");
            }
        }

        [Route("~/api/GetUserGameMasterLog")]
        [HttpGet]
        public IActionResult GetUserGameMasterLog(int UID, int CubesFacesGameId, int User_Play_Count = 1)
        {
            try
            {
                var tbldata = (from l in DbContext.TblCubesGameMasterUserLog
                               where l.IdUser == UID && l.CubesFacesGameId == CubesFacesGameId && l.UserPlayCount == User_Play_Count
                               select l).ToList();
                //string Data = JsonSerializer.Serialize(tbldata);
                //string Result = ency.getEncryptedString(Data);
                return Ok(tbldata);
            }
            catch (System.Exception ex)
            {

                return Conflict("Error in Code");
            }
        }

       //static int Comparer(string a, string b)
        //{
        //    var temp = a.Length.CompareTo(b.Length);
        //    return temp == 0 ? a.CompareTo(b) : temp;
        //}
        CrossGridModel GenCrossword(List<string> _words, List<TblCubesPertilesAnswerDetails> anslist, ref int AttemptNo, int OrgID)
        {
            _board = new Crossword(18, 19);
            List<string> _order;
            List<string> horizontalWordsListView = new List<string>();
            List<string> verticalWordsListView = new List<string>();
            //_words.Sort(Comparer);
            //_words.Reverse();
            _order = _words;

            foreach (var word in _order)
            {

                switch (_board.AddWord(word))
                {
                    case 0:

                        horizontalWordsListView.Add(word);//across
                        break;
                    case 1:

                        verticalWordsListView.Add(word);//down
                        break;

                }

            }
            CrossGridModel GrideDataList = ActualizeData(OrgID, _words, anslist, ref AttemptNo);
            return GrideDataList;
        }
        private static readonly Regex sWhitespace = new Regex(@"\s+");
        public static string ReplaceWhitespace(string input, string replacement)
        {
            return sWhitespace.Replace(input, replacement);
        }
        CrossGridModel ActualizeData(int OrgID, List<string> _words, List<TblCubesPertilesAnswerDetails> anslist, ref int AttemptNo)
        {
            AttemptNo = Get_attempt_no();
            List<GridData> data = new List<GridData>();
            CrossGridModel CrossGridData = new CrossGridModel();
            var count = _board.N * _board.M;
            var board = _board.GetBoard;
            var p = 0;

            for (var i = 0; i < _board.N; i++)
            {
                var builder = new StringBuilder();
                for (var j = 0; j < _board.M; j++)
                {
                    var letter = board[i, j] == '*' ? ' ' : board[i, j];
                    if (letter != ' ') count--;
                    char res = board[i, j];

                    if (j > 0)
                    {
                        if (res == ' ' || res == '*')
                            builder.Append(Convert.ToString(",*"));
                        else
                            builder.Append(Convert.ToString("," + res));
                    }
                    else
                    {
                        if (res == ' ' || res == '*')
                            builder.Append(Convert.ToString("*"));
                        else
                            builder.Append(Convert.ToString(res));
                    }
                    p++;
                }

                TblCubesCrosswordGridDetails data1 = new TblCubesCrosswordGridDetails();
                data1.IsActive = "A";
                data1.UpdatedDateTime = DateTime.UtcNow;
                data1.IdOrganization = OrgID;
                data1.Grid = Convert.ToString(builder);
                data1.KeyNo = AttemptNo;
                DbContext.TblCubesCrosswordGridDetails.Add(data1);
                DbContext.SaveChanges();
                data.Add(new GridData()
                {
                    grid = Convert.ToString(builder)

                });

                builder.Clear();
            }
            int countflag = 0;
            string[] words = _words.ToArray();
            for (int i = 0; i < words.Length; i++)
            {
                string word = words[i];
                for (int row = 0; row < board.GetLength(0); row++)
                {
                    for (int col = 0; col < board.GetLength(1); col++)
                    {
                        if (board[row, col] == word[0])
                        {
                            bool foundWord = true;
                            // Check if word fits across
                            if (col + word.Length <= board.GetLength(1))
                            {
                                for (int j = 1; j < word.Length; j++)
                                {
                                    if (board[row, col + j] != word[j])
                                    {
                                        foundWord = false;
                                        break;
                                    }
                                }

                                if (foundWord)
                                {
                                    Console.WriteLine("Across: {0} ", word);
                                    var dataobj = anslist.Where(x => x.Answer.ToLower().Equals(word.ToLower())).FirstOrDefault();

                                    if (dataobj != null)
                                    {
                                        countflag++;
                                    }
                                    break;
                                }
                            }

                            foundWord = true;
                            // Check if word fits down
                            if (row + word.Length <= board.GetLength(0))
                            {
                                for (int j = 1; j < word.Length; j++)
                                {
                                    if (board[row + j, col] != word[j])
                                    {
                                        foundWord = false;
                                        break;
                                    }
                                }

                                if (foundWord)
                                {
                                    Console.WriteLine("Down: {0}", word);
                                    var dataobj = anslist.Where(x => x.Answer.ToLower().Equals(word.ToLower())).FirstOrDefault();

                                    if (dataobj != null)
                                    {
                                        countflag++;
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            if (countflag != 16)
            {
                GenCrossword(_words, anslist, ref AttemptNo, OrgID);
            }


            CrossGridData.CrossGridData = data;
            CrossGridData.grid = board;
            return CrossGridData;

        }
        public int Get_attempt_no()
        {
            int attempt_no = 0;
            var tbldata = (from l in DbContext.TblCubesGridrowcolLog
                           select l).OrderByDescending(x => x.KeyNo).FirstOrDefault();
            if (tbldata != null)
                attempt_no = tbldata.KeyNo + 1;
            return attempt_no;

        }
    }
}







