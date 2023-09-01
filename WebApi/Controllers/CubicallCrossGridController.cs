using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using Acclimate_Models;
using CrosswordCreator.Crossword;
using Cubicall_Models;
using DataAccess.EFCore.Data;
using Domain.Entities;
using Domain.Interfaces;
using m2ostnextservice.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using TGC_Game.Web;

namespace WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/CubicallGameSetUPController")]

    public class CubicallCrossGridController : ControllerBase
    {
        private readonly db_cubicall_game_devContext DbContext;
        AESAlgorithm ency = new AESAlgorithm();
        private readonly IUnitOfWork _unitOfWork;
        private IConfiguration Configuration;
        Crossword _board = new Crossword(18, 19);
        Random _rand = new Random();
        public CubicallCrossGridController(IUnitOfWork unitOfWork, db_cubicall_game_devContext context, IConfiguration _configuration)
        {
            _unitOfWork = unitOfWork;
            DbContext = context;
            Configuration = _configuration;
        }

        public class CrosswordGrid
        {
            public char[,] Grid { get; set; }
            public List<AnswerPosition> AnswerPositions { get; set; }
        }

        public class AnswerPosition
        {
            public string Answer { get; set; }
            public int Row { get; set; }
            public int ColumnStart { get; set; }
            public int ColumnEnd { get; set; }
        }


        //[Route("~/api/GenerateCrosswordGrid")]
        //[HttpPost]
        //public IActionResult GenerateCrosswordGrid([FromBody] List<string> answers)
        //{
        //    GenCrossword(answers);

        //    //if (answers == null || !answers.Any())
        //    //    return Conflict("Please provide answers.");

        //    //// Validate the answers and generate the crossword grid
        //    //var crosswordGrid = GenerateGrid(answers);
        //    //if (crosswordGrid.Grid != null && crosswordGrid.Grid.Length > 0)
        //    //{
        //    //    int uBound0 = crosswordGrid.Grid.GetUpperBound(0);
        //    //    int uBound1 = crosswordGrid.Grid.GetUpperBound(1);

        //    //    for (int i = 0; i <= uBound0; i++)
        //    //    {
        //    //        var builder = new StringBuilder();
        //    //        for (int j = 0; j <= uBound1; j++)
        //    //        {
        //    //            char res = crosswordGrid.Grid[i, j];

        //    //            if (j > 0)
        //    //            {
        //    //                if (res == '\0')
        //    //                    builder.Append(Convert.ToString(",*"));
        //    //                else
        //    //                    builder.Append(Convert.ToString("," + res));
        //    //            }
        //    //            else
        //    //            {
        //    //                if (res == '\0')
        //    //                    builder.Append(Convert.ToString("*"));
        //    //                else
        //    //                    builder.Append(Convert.ToString(res));
        //    //            }

        //    //        }
        //    //        TblCubesCrosswordGridDetails data = new TblCubesCrosswordGridDetails();
        //    //        data.IsActive = "A";
        //    //        data.UpdatedDateTime = DateTime.UtcNow;
        //    //        data.IdOrganization = 1;
        //    //        data.Grid = Convert.ToString(builder);
        //    //        DbContext.TblCubesCrosswordGridDetails.Add(data);
        //    //        DbContext.SaveChanges();
        //    //        builder.Clear();
        //    //    }
        //    //}


        //    // int crosswordGridId = InsertCrosswordGrid(crosswordGrid);
        //    //  InsertAnswerPositions(crosswordGridId, crosswordGrid.AnswerPositions);

        //    return Ok("Success");
        //}
        
        private CrosswordGrid GenerateGrid(List<string> answers)
        {
            // Logic to generate the crossword grid based on the answers
            // This is a simplified example, you may need to implement your own algorithm

            // Determine the size of the grid
            //int rows = answers.Count;
            //int cols = answers.Max(a => a.Length);
            int rows = 10;
            int cols = 10;

            // Create a 2D array to represent the grid
            char[,] grid = new char[rows, cols];

            // Populate the grid with the answers
            for (int i = 0; i < rows; i++)
            {
                string answer = answers[i];

                for (int j = 0; j < answer.Length; j++)
                {
                    grid[i, j] = answer[j];
                }
            }

            // Create a list of positions for each answer
            List<AnswerPosition> answerPositions = new List<AnswerPosition>();

            // Populate the answer positions
            for (int i = 0; i < rows; i++)
            {
                string answer = answers[i];
                int colStart = GetColumnStart(grid, i, answer);

                if (colStart >= 0)
                {
                    answerPositions.Add(new AnswerPosition
                    {
                        Answer = answer,
                        Row = i,
                        ColumnStart = colStart,
                        ColumnEnd = colStart + answer.Length - 1
                    });
                }
            }

            // Create a crossword grid object
            CrosswordGrid crosswordGrid = new CrosswordGrid
            {
                Grid = grid,
                AnswerPositions = answerPositions
            };

            return crosswordGrid;
        }

        private int GetColumnStart(char[,] grid, int row, string answer)
        {
            int cols = grid.GetLength(1);

            for (int j = 0; j <= cols - answer.Length; j++)
            {
                bool isValid = true;

                for (int k = 0; k < answer.Length; k++)
                {
                    if (grid[row, j + k] != '\0' && grid[row, j + k] != answer[k])
                    {
                        isValid = false;
                        break;
                    }
                }

                if (isValid)
                    return j;
            }

            return -1;
        }


        private string SerializeGridData(char[,] grid)
        {
            // Logic to serialize the grid data into a string representation
            // You can use any serialization format that suits your requirements (e.g., JSON, XML)

            // Here's a simple example using JSON serialization using Newtonsoft.Json library
            return Newtonsoft.Json.JsonConvert.SerializeObject(grid);
        }
        //static int Comparer(string a, string b)
        //{
        //    var temp = a.Length.CompareTo(b.Length);
        //    return temp == 0 ? a.CompareTo(b) : temp;
        //}
        //void GenCrossword(List<string> _words)
        //{
        //    List<string> _order;
        //    List<string> horizontalWordsListView=new List<string>();
        //    List<string> verticalWordsListView=new List<string>();
        //    _words.Sort(Comparer);
        //    _words.Reverse();
        //    _order = _words;
           
        //    foreach (var word in _order)
        //    {
        //        //var wordToInsert = ((bool)RTLRadioButton.IsChecked) ? word.Reverse().Aggregate("",(x,y) => x + y) : word;

        //        switch (_board.AddWord(word))
        //        {
        //            case 0:
        //                horizontalWordsListView.Add(word);
        //                break;
        //            case 1:
        //                verticalWordsListView.Add(word);
        //                break;

        //        }
        //    }

        //    ActualizeData();
        //}
        //void ActualizeData()
        //{

        //    var count = _board.N * _board.M;

        //    var board = _board.GetBoard;
        //    var p = 0;

        //    for (var i = 0; i < _board.N; i++)
        //    {
        //        var builder = new StringBuilder();
        //        for (var j = 0; j < _board.M; j++)
        //        {
        //            var letter = board[i, j] == '*' ? ' ' : board[i, j];
        //            if (letter != ' ') count--;
        //            char res = board[i, j];

        //            if (j > 0)
        //            {
        //                if (res == ' '|| res =='*')
        //                    builder.Append(Convert.ToString(",*"));
        //                else
        //                    builder.Append(Convert.ToString("," + res));
        //            }
        //            else
        //            {
        //                if (res == ' ' || res == '*')
        //                    builder.Append(Convert.ToString("*"));
        //                else
        //                    builder.Append(Convert.ToString(res));
        //            }
        //            p++;
        //        }
        //        TblCubesCrosswordGridDetails data = new TblCubesCrosswordGridDetails();
        //        data.IsActive = "A";
        //        data.UpdatedDateTime = DateTime.UtcNow;
        //        data.IdOrganization = 1;
        //        data.Grid = Convert.ToString(builder);
        //        DbContext.TblCubesCrosswordGridDetails.Add(data);
        //        DbContext.SaveChanges();
        //        builder.Clear();
        //    }


        //}
    }
}