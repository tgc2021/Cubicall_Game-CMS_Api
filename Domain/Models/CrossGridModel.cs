using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Acclimate_Models
{
    public class CrossGridModel
    {

        public char[,] grid { get; set; }
        public List<GridData> CrossGridData { get; set; }

    }
    public class GridData{
        public string grid { get; set; }
        }
}
