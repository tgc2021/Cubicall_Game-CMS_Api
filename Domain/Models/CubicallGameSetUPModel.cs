using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Acclimate_Models
{
    public class CubicallGameSetUPModel
    {
        public List<TblGameMaster> GameMasterList { get; set; }
        public List<TblCubesFacesGameDetails> CubesFacesGameDetailsList { get; set; }
        //public List<TblCubesPertilesQuestionDetails> CubesPertilesQuestionDetailsList { get; set; }
        //public List<TblCubesPertilesAnswerDetails> CubesPertilesAnswerDetailsList { get; set; }
        public List<TblCubesFacesAttemptnoDetails> CubesFacesAttemptnoDetailsList { get; set; }
        public List<TblCubesFaceBadgeMaster> CubesFaceBadgeMasterList { get; set; }
        public List<TblCubesFacesColourDetails> CubesFacesColourDetailsList { get; set; }
        public List<TblServiceLevelBandsDetails> ServiceLevelBandsDetailsrList { get; set; }
        public List<TblAvatar> AvatarList { get; set; }
        public List<TblServiceLevelScoringDetails> ServiceLevelScoringDetailsList { get; set; }
        public List<TblServiceLevelStreakScoringDetails> ServiceLevelStreakScoringDetailsList { get; set; }
        //public List<QuestionAnswerModel> QuestionAnswerList { get; set; }
        //public List<TblCubesCrosswordGridDetails> Crossword { get; set; }
    }

}
