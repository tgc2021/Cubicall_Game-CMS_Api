using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Acclimate_Models
{
    public class QuestionAnswerModel
    {
        public int PerTileQuestionId { get; set; }
        public int QuestionId { get; set; }
        public int? CubesFacesGameId { get; set; }
        public int? PerTileId { get; set; }
        public string Question { get; set; }
        
        public string QuestionClue { get; set; }
        public int? Row { get; set; }
        public int? Column { get; set; }
        public string Direction { get; set; }

        public List<TblCubesPertilesAnswerDetails> optionList { get; set; }

    }
    public class QuestionSetModel
    {
        public int QuestionSet { get; set; }
    }
    public class QuestionAnswerGame6Model
    {
      public  List<QuestionAnswerModel> QuestionAnswer { get; set; }
        public List<GridData> Crossword { get; set; }
    }

    public class AllGameStatusModel
    {
        
        public int CubesFacesGameId { get; set; }
        public int PerTileId { get; set; }
        public int IsCompleted { get; set; }
    }
    public class QuestionAnsListwerModel
    {
        public TblCubesPertilesQuestionDetails QuestionList { get; set; }

        public List<TblCubesPertilesAnswerDetails> AnsList { get; set; }

    }
}
