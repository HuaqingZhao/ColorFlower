using System;
using System.Collections.Generic;

namespace ColorFlower
{
    [Serializable]
    public class CalcResult
    {
        public int IssueNumber { get; set; }
        public string DisplayNumber { get; set; }
        public string Index1 { get; set; }
        public string Index2 { get; set; }
        public string Index3 { get; set; }
        public string Index4 { get; set; }
        public string Index5 { get; set; }
        public string Index6 { get; set; }
        public string Index7 { get; set; }
        public IDictionary<int, bool> ResultMatches { get; set; }
        public string Count { get; set; }
    }
}
